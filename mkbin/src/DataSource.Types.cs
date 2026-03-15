using System;

namespace mkbin
{
    public class DataType
    {
        public string Name { get; set; }
        public string PackageName { get; set; }
        public string Keyword { get; set; }
        public string ExtentionName { get; set; }

        public DataType()
        {
            Name = "";
            PackageName = "";
            Keyword = "";
            ExtentionName = "";
        }
    }

    public class FileType
    {
        public string Name { get; set; }
        public string Display { get; set; }
        public string Extentions { get; set; }

        public FileType()
        {
            Name = "";
            Display = "";
            Extentions = "";
        }
    }
}
