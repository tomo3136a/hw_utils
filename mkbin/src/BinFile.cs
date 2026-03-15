using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Text;
using System.Runtime.CompilerServices;

namespace mkbin
{
    [DataContract(Namespace = "")]
    public class BinFile
    {
        [DataMember]
        public string Name;
        [DataMember]
        public string Mode;
        [DataMember]
        public bool LittleEndian;

        [DataMember]
        public uint Address;
        [DataMember]
        public uint Length;
        [DataMember]
        public string Keyword;
        [DataMember]
        public string Version;
        [DataMember]
        public DateTime Timestamp;
        [DataMember]
        public ushort Reserved1;
        [DataMember]
        public ushort Reserved2;
        [DataMember]
        public uint Checksum;

        [DataMember]
        public List<BinFileSection> Sections { get; set; }

        public int Status { get; private set; }

        public BinFile()
        {
            Name = "";
            Mode = "";
            LittleEndian = false;
            Status = 0;

            Address = 0;
            Length = 0;
            Keyword = "";
            Version = "";
            Timestamp = new DateTime(0);
            Reserved1 = 0xcccc;
            Reserved2 = 0xcccc;
            Checksum = 0;

            Sections = new List<BinFileSection>();
            Sections.Add(new BinFileSection());
        }

        public static BinFile Load(string path)
        {
            if (File.Exists(path))
            {
                var ser = new DataContractSerializer(typeof(BinFile));
                var bom = new System.Text.UTF8Encoding(false);
                try
                {
                    using (var sr = new StreamReader(path, bom))
                    using (var xr = XmlReader.Create(sr))
                    {
                        return (BinFile)ser.ReadObject(xr);
                    }
                }
                catch (Exception) { }
            }
            return new BinFile();
        }

        public bool Save(string path)
        {
            var ser = new DataContractSerializer(typeof(DataSource));
            var xws = new XmlWriterSettings();
            xws.Encoding = new System.Text.UTF8Encoding(false);
            xws.Indent = true;
            try
            {
                using (var sw = XmlWriter.Create(path, xws))
                    ser.WriteObject(sw, this);
            }
            catch (Exception) { return false; }
            return true;
        }

        public byte[] BuildHeader()
        {
            var hdr = new byte[0x20];

            var ba = BitConverter.GetBytes(Address);
            CopyBytes(ba, 0, ref hdr, 0, 4, LittleEndian);

            var len = 0L + Length;
            if (len == 0) len = CalcLength();
            ba = BitConverter.GetBytes(len);
            CopyBytes(ba, 0, ref hdr, 4, 4, LittleEndian);

            var kw = Keyword + "        ";
            for (var i = 0; i < 8; i++) hdr[8 + i] = (byte)kw[i];

            var ver = Version + "0000";
            if (ver[0] == '.') ver = "0" + ver;
            for (var i = 0; i < 4; i++) hdr[16 + i] = (byte)ver[i];

            var ts = Timestamp;
            if (ts.Ticks == 0) ts = CalcTimestamp();
            hdr[20] = (byte)(ts.Year % 100);
            hdr[21] = (byte)(ts.Month);
            hdr[22] = (byte)(ts.Day);
            hdr[23] = (byte)(ts.Hour);
            hdr[24] = (byte)(ts.Minute);
            hdr[25] = (byte)(ts.Second);

            ba = BitConverter.GetBytes(Reserved1);
            CopyBytes(ba, 0, ref hdr, 26, 2, LittleEndian);

            ba = BitConverter.GetBytes(Reserved2);
            CopyBytes(ba, 0, ref hdr, 28, 2, LittleEndian);

            return hdr;
        }

        public bool Generate(string path)
        {
            var hdr = BuildHeader();

            var sum = CalcSum(hdr);
            if (Checksum != 0) sum = ~Checksum;
            var ba = BitConverter.GetBytes(~sum);
            CopyBytes(ba, 0, ref hdr, 30, 2, LittleEndian);

            try
            {
                if (File.Exists(path))
                    File.Delete(path);

                using (var ofs = File.OpenWrite(path))
                using (var bw = new BinaryWriter(ofs))
                {
                    //write header
                    bw.Write(hdr);

                    //write contents
                    foreach (var sect in Sections)
                        foreach (var ba2 in sect.Bytes())
                            bw.Write(ba2);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Restore(string path)
        {
            var fi = new FileInfo(path);
            if (!fi.Exists) return false;
            var sz = fi.Length;
            if (sz < 0x20) return false;
            if (sz >= 65536L * 65536L + 0x20) return false;

            using (var ifs = fi.OpenRead())
            using (var br = new BinaryReader(ifs))
            {
                var hdr = br.ReadBytes(0x20);
                var ba = new byte[8];

                Status = 0;
                CopyBytes(hdr, 4, ref ba, 0, 4, true);
                var sz2 = BitConverter.ToUInt32(ba, 0) - sz;
                if (sz2 < 0) sz2 = -sz2;
                CopyBytes(hdr, 4, ref ba, 0, 4, false);
                var sz3 = BitConverter.ToUInt32(ba, 0) - sz;
                if (sz3 < 0) sz3 = -sz3;
                LittleEndian = sz2 < sz3;

                CopyBytes(hdr, 0, ref ba, 0, 4, LittleEndian);
                Address = BitConverter.ToUInt32(ba, 0);

                CopyBytes(hdr, 4, ref ba, 0, 4, LittleEndian);
                Length = BitConverter.ToUInt32(ba, 0);

                Keyword = Encoding.UTF8.GetString(hdr, 8, 8);

                Version = Encoding.UTF8.GetString(hdr, 16, 4);

                Timestamp = new DateTime(
                    2000 + hdr[20], hdr[21], hdr[22], hdr[23], hdr[24], hdr[25]);

                CopyBytes(hdr, 26, ref ba, 0, 2, LittleEndian);
                Reserved1 = BitConverter.ToUInt16(ba, 0);

                CopyBytes(hdr, 28, ref ba, 0, 2, LittleEndian);
                Reserved2 = BitConverter.ToUInt16(ba, 0);

                CopyBytes(hdr, 30, ref ba, 0, 2, LittleEndian);
                Checksum = BitConverter.ToUInt16(ba, 0);
            }

            var sect = new BinFileSection("(bin)", path, 0x20);
            Sections.Clear();
            Sections.Add(sect);

            return true;
        }

        public static void CopyBytes(
            byte[] src, int spos, ref byte[] dst, int dpos, int cnt, bool le)
        {
            var epos = dpos + cnt - 1;
            if (le)
                for (var i = 0; i < cnt; i++) dst[dpos++] = src[spos++];
            else
                for (var i = 0; i < cnt; i++) dst[epos--] = src[spos++];
        }

        public long CalcLength()
        {
            var len = 0L;
            foreach (var sect in Sections)
                len += sect.GetCount();
            return len;
        }

        public DateTime CalcTimestamp()
        {
            var ts = new DateTime(0);
            foreach (var sect in Sections)
            {
                var src = sect.Source;
                if (File.Exists(src))
                {
                    var fi = new FileInfo(src);
                    var fts = fi.LastWriteTime;
                    if (ts < fts) ts = fts;
                }
            }
            return ts;
        }

        public long CalcSum(byte[] hdr)
        {
            var sum = 0L;
            foreach (var b in hdr) sum += (uint)(0x0FFL & b);
            foreach (var sect in Sections) sum += sect.Sum();
            return sum;
        }

        public string Report()
        {
            //var len = Length > 0 ? Length : CalcLength();
            //var ts = Timestamp.Ticks > 0 ? Timestamp : CalcTimestamp();
            var s = "";
            if (Name != "") s += "Name\t\t: " + Name + "\r\n";
            if (Mode != "") s += "Mode\t\t: " + Mode + "\r\n";
            s += "LittleEndian\t: " + LittleEndian + "\r\n";
            s += "Address\t\t: " + Util.HexDisp32(Address) + "\r\n";
            s += "Length\t\t: " + Util.HexDisp32(Length) + "\r\n";
            s += "Keyword\t\t: " + Keyword + "\r\n";
            s += "Version\t\t: " + Version + "\r\n";
            s += "Timestamp\t: " + Timestamp + "\r\n";
            s += "Reserved1\t: " + Util.HexDisp16(Reserved1) + "\r\n";
            s += "Reserved2\t: " + Util.HexDisp16(Reserved2) + "\r\n";
            s += "Checksum\t: " + Util.HexDisp16(Checksum) + "\r\n";

            var hdr = BuildHeader();
            var sum = 0L;
            foreach (var b in hdr) sum += b;
            s += "Headersum\t: " + Util.HexDisp16(sum) + "\r\n";
            s += "\r\n";

            foreach (var sect in Sections)
            {
                s += sect.ToReport() + "\r\n";
                sum += sect.Sum();
            }
            s += "Totalsum\t\t: " + Util.HexDisp16(sum) + "\r\n";
            s += "Checksum(1)\t: " + Util.HexDisp16(~sum) + "\r\n";
            return s;
        }
    }
}
