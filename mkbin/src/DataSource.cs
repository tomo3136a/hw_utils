using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace mkbin
{
    [DataContract(Namespace = "")]
    public partial class DataSource
    {
        [DataMember]
        public string[] Packages;
        [DataMember]
        public DataType[] DataTypes;
        [DataMember]
        public List<Data> Shortcuts;

        [DataMember]
        public FileType[] FileTypes;
        [DataMember]
        public string[] InFilters;
        [DataMember]
        public string[] MapFilters;
        [DataMember]
        public string[] OutFilters;

        //file type accessor
        public string DispName(string name)
        {
            foreach (var ft in FileTypes)
                if (ft.Name == name) return ft.Display;
            return "";
        }
        public string Extentions(string name)
        {
            foreach (var ft in FileTypes)
                if (ft.Name == name) return ft.Extentions;
            return "";
        }

        public bool IsSaving { get; set; }
        public const string DafaultFileName = "data.xml";

        public DataSource()
        {
            Packages = new string[] { };
            DataTypes = new DataType[] { };
            Shortcuts = new List<Data>();
            FileTypes = new FileType[] { };
            InFilters = new string[] { };
            MapFilters = new string[] { };
            OutFilters = new string[] { };
            IsSaving = false;
        }

        public static DataSource Load(string path = "")
        {
            if (path == "") path = DataSource.DafaultFileName;
            if (!File.Exists(path))
            {
                var data = new DataSource();
                data.Build();
                return data;
            }

            var ts = new Type[] { typeof(DataSource), typeof(Data) };
            DataContractSerializer ser =
                new DataContractSerializer(typeof(DataSource), ts);
            var bom = new System.Text.UTF8Encoding(false);
            using (var sr = new StreamReader(path, bom))
            using (var xr = XmlReader.Create(sr))
            {
                return (DataSource)ser.ReadObject(xr);
            }
        }

        public bool Save(string path = "")
        {
            if (!IsSaving) return true;
            try
            {
                if (path == "") path = DataSource.DafaultFileName;

                var ts = new Type[] { typeof(DataSource), typeof(Data) };
                DataContractSerializer ser =
                    new DataContractSerializer(typeof(DataSource), ts);
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
