using System;
using System.Collections.Generic;
using System.IO;

namespace mkbin
{
    public class BinFileSection
    {
        public enum SectionType { NONE, AUTO, BINARY }

        public string Name { get; set; }
        public SectionType Type { get; set; }
        public string Source { get; set; }

        public long Index { get; set; }
        public long Count { get; set; }
        public long Offset { get; set; }
        public long Blocks { get; set; }

        public byte Fill { get; set; }

        private byte[] _raw_data;

        public byte[] RawData
        {
            get
            {
                if (_raw_data == null) _raw_data = new byte[0];
                if (_raw_data.Length == 0) Load();
                return _raw_data;
            }
            private set { _raw_data = value; }
        }

        public BinFileSection()
        {
            Name = "";
            Type = SectionType.NONE;
            Source = "";
            Index = 0L;
            Count = 0L;
            Offset = 0L;
            Blocks = 0L;
            Fill = 0xFF;
            _raw_data = new byte[0];
        }

        public BinFileSection(
            string name, string path, long ind = 0L, long cnt = 0L,
            long off = 0L, long blk = 0L, byte fill = 0xFF)
        {
            Name = name;
            Type = SectionType.NONE;
            Source = path;
            Index = ind;
            Count = cnt;
            Offset = off;
            Blocks = blk;
            Fill = fill;
            _raw_data = new byte[0];
        }

        public void Reset()
        {
            _raw_data = new byte[0];
        }

        public bool Load(string src = "")
        {
            if (src == "" && Source != null) src = Source;
            if (!File.Exists(src)) return false;
            _raw_data = File.ReadAllBytes(src);
            Source = src;
            return true;
        }

        public IEnumerable<byte[]> Bytes()
        {
            var pos = 0L;

            //pre fill
            if (Offset > 0)
            {
                var ba = new byte[Offset];
                for (var i = 0; i < ba.Length; i++) ba[i] = Fill;
                yield return ba;
                pos += Offset;
            }

            //contents
            var raw = RawData;
            var ind = Index < 0 ? 0 : Index;
            var cnt = Count < 0 ? 0 : Count;
            var cnt2 = raw.Length - ind;
            cnt = cnt > 0 ? cnt : cnt2;
            cnt = cnt < cnt2 ? cnt : cnt2;
            if (cnt > 0)
            {
                var ba = new byte[cnt];
                Array.Copy(raw, ind, ba, 0, cnt);
                yield return ba;
                pos += cnt;
            }

            //post fill
            if (Blocks > 0)
            {
                cnt = pos / Blocks;
                cnt *= Blocks;
                if (cnt < pos) cnt += Blocks;
                cnt -= pos;
                var ba = new byte[cnt];
                for (var i = 0; i < ba.Length; i++) ba[i] = Fill;
                yield return ba;
                pos += cnt;
            }
        }

        public long GetIndex()
        {
            return Index > 0 ? Index : 0;
        }

        public long GetCount()
        {
            var cnt = 0L;
            foreach (var ba in Bytes())
                cnt += ba.Length;
            return cnt;
        }

        public long Sum()
        {
            var sum = 0L;
            foreach (var ba in Bytes())
                foreach (var b in ba)
                    sum += 0x0FF & b;
            return sum;
        }

        public string ToReport()
        {
            var s = "";
            s += "Name\t\t: " + Name + "\r\n";
            s += "  Type\t\t: " + Type + "\r\n";
            s += "  Source\t\t: " + Source + "\r\n";
            s += "  SourceSize\t: " + Util.HexDisp32(RawData.Length) + "\r\n";
            s += "  Index\t\t: " + Util.HexDisp32(GetIndex()) + "\r\n";
            s += "  Count\t\t: " + Util.HexDisp32(GetCount()) + "\r\n";
            if (Offset > 0) s += "  Offset\t\t: " + Util.HexDisp32(Offset) + "\r\n";
            if (Blocks > 0) s += "  BlockSize\t: " + Util.HexDisp32(Blocks) + "\r\n";
            s += "  Sum\t\t: " + Util.HexDisp16(Sum()) + "\r\n";
            return s;
        }
    }
}
