using System;
using System.IO;

namespace mkbin
{
    public class MapFile
    {
        public long Begin;
        public long End;

        public MapFile()
        {
            Begin = 0;
            End = 0;
        }

        public bool Read(string path)
        {
            if (!File.Exists(path)) return false;

            foreach (var line in File.ReadAllLines(path))
            {
                var ss = line.Split(new char[] { ' ', '\t' });
                if (ss.Length < 5) continue;
                if (ss[0].Length < 4) continue;
                if (ss[0].Substring(0, 4) != "Page") continue;
                Begin = Util.HextToLong(ss[2].Replace("0x", ""));
                End = Util.HextToLong(ss[4].Replace("0x", ""));
            }
            return true;
        }
    }
}
