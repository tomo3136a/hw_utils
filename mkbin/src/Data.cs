using System;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace mkbin
{
    [DataContract(Namespace = "")]
    public partial class Data : DataBase
    {
        public const string DefaultFileName = "config.xml";

        [DataMember]
        public string Name
        {
            get { return _name.Trim(); }
            set { SetProperty(ref _name, value); }
        }
        [DataMember]
        public string PackageName
        {
            get { return _package_name.Trim(); }
            set { SetProperty(ref _package_name, value); }
        }
        [DataMember]
        public string DataTypeName
        {
            get { return _datetype_name.Trim(); }
            set { SetProperty(ref _datetype_name, value); }
        }
        [DataMember]
        public string Version
        {
            get { return _version.Trim(); }
            set { SetProperty(ref _version, value); }
        }
        private string _name;
        private string _package_name;
        private string _datetype_name;
        private string _version;

        [DataMember]
        public string SourceFile
        {
            get { return _src_name.Trim(); }
            set { SetProperty(ref _src_name, value); }
        }
        [DataMember]
        public string SourceType
        {
            get { return _src_type.Trim(); }
            set { SetProperty(ref _src_type, value); }
        }
        [DataMember]
        public bool SourceOption
        {
            get { return _src_option; }
            set { SetProperty(ref _src_option, value); }
        }
        [DataMember]
        public string Begin
        {
            get { return _src_begin.Trim(); }
            set { SetProperty(ref _src_begin, value); }
        }
        [DataMember]
        public string End
        {
            get { return _src_end.Trim(); }
            set { SetProperty(ref _src_end, value); }
        }
        private string _src_name;
        private string _src_type;
        private bool _src_option;
        private string _src_begin;
        private string _src_end;

        [DataMember]
        public string OutputFile
        {
            get { return _out_file.Trim(); }
            set { SetProperty(ref _out_file, value); }
        }
        [DataMember]
        public bool OutputOption
        {
            get { return _out_option; }
            set { SetProperty(ref _out_option, value); }
        }
        [DataMember]
        public bool LittleEndian
        {
            get { return _out_endian; }
            set { SetProperty(ref _out_endian, value); }
        }
        private string _out_file;
        private bool _out_option;
        private bool _out_endian;

        [DataMember]
        public string Address
        {
            get { return _address.Trim(); }
            set { SetProperty(ref _address, value); }
        }
        [DataMember]
        public string Length
        {
            get { return _length.Trim(); }
            set { SetProperty(ref _length, value); }
        }
        [DataMember]
        public string Keyword
        {
            get { return _keyword.Trim(); }
            set { SetProperty(ref _keyword, value); }
        }
        [DataMember]
        public string Timestamp
        {
            get { return _timestamp; }
            set { SetProperty(ref _timestamp, value); }
        }
        [DataMember]
        public string Reserved1
        {
            get { return _reserved1.Trim(); }
            set { SetProperty(ref _reserved1, value); }
        }
        [DataMember]
        public string Reserved2
        {
            get { return _reserved2.Trim(); }
            set { SetProperty(ref _reserved2, value); }
        }
        [DataMember]
        public string Checksum
        {
            get { return _checksum.Trim(); }
            set { SetProperty(ref _checksum, value); }
        }
        private string _address;
        private string _length;
        private string _keyword;
        private string _timestamp;
        private string _reserved1;
        private string _reserved2;
        private string _checksum;

        public Data() { Reset(); }
        public Data(string name, Data cfg) { CopyFrom(name, cfg); }

        public void Reset()
        {
            Name = "";
            PackageName = "";
            DataTypeName = "";
            Version = "";

            SourceFile = "";
            SourceType = "";
            Begin = "";
            End = "";

            OutputFile = "";
            LittleEndian = true;

            Address = "";
            Length = "";
            Keyword = "";
            Timestamp = "";
            Reserved1 = "";
            Reserved2 = "";
            Checksum = "";
        }

        public void CopyFrom(string name, Data cfg)
        {
            Name = name;

            PackageName = cfg.PackageName;
            DataTypeName = cfg.DataTypeName;
            //Version = cfg.Version;

            SourceFile = cfg.SourceFile;
            SourceType = cfg.SourceType;
            SourceOption = cfg.SourceOption;
            Begin = cfg.Begin;
            End = cfg.End;

            OutputFile = cfg.OutputFile;
            OutputOption = cfg.OutputOption;
            LittleEndian = cfg.LittleEndian;

            Address = cfg.Address;
            Length = cfg.Length;
            Keyword = cfg.Keyword;
            Timestamp = cfg.Timestamp;
            Reserved1 = cfg.Reserved1;
            Reserved2 = cfg.Reserved2;
            Checksum = cfg.Checksum;
        }

        public static Data Load(string path = "")
        {
            if (path == "") path = Data.DefaultFileName;
            if (File.Exists(path))
            {
                var ser = new DataContractSerializer(typeof(Data));
                var bom = new System.Text.UTF8Encoding(false);
                try
                {
                    using (var sr = new StreamReader(path, bom))
                    using (var xr = XmlReader.Create(sr))
                    {
                        return (Data)ser.ReadObject(xr);
                    }
                }
                catch (Exception) { }
            }
            return new Data();
        }

        public bool Save(string path = "")
        {
            if (path == "") path = Data.DefaultFileName;
            try
            {
                var ser = new DataContractSerializer(typeof(Data));
                var xws = new XmlWriterSettings();
                xws.Encoding = new System.Text.UTF8Encoding(false);
                xws.Indent = true;
                using (var sw = XmlWriter.Create(path, xws))
                {
                    ser.WriteObject(sw, this);
                }
            }
            catch (Exception) { return false; }
            return true;
        }
    }
}
