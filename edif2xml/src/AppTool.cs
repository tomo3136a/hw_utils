using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace hwutils
{
    public partial class App
    {
        public string GetDestName(string src, string s)
        {
            string dst = Path.GetFileNameWithoutExtension(s);
            return (Path.HasExtension(dst)) ? dst : (dst + ".txt");
        }

        public bool Csv2Xmldoc(XmlDocument doc, string src)
        {
            XmlElement root = doc.CreateElement("data");
            foreach (string line in File.ReadAllLines(src)) {
                XmlElement dl = doc.CreateElement("dl");
                foreach (string v in line.Split(',')) {
                    XmlElement dd = doc.CreateElement("dd");
                    dd.InnerText = v;
                    dl.AppendChild(dd);
                }
                root.AppendChild(dl);
            }
            doc.AppendChild(root);
            return true;
        }

        private string GetConvertType(string path)
        {
            string s = path.ToLower();
            foreach (string ext in edf_lst) 
                if (s.EndsWith(ext)) return ".xedf";
            foreach (string ext in csv_lst) 
                if (s.EndsWith(ext)) return ".data";
            return null;
        }

        private bool ConvertToXmldoc(XmlDocument doc, string src, string ext)
        {
            return (ext == ".xedf") ? EdifXmlDocument.ToXmldoc(doc, src) : 
                   (ext == ".data") ? Csv2Xmldoc(doc, src) : false;
        }

        private string GetSource(string path)
        {
            string ext = GetConvertType(path);
            if (ext == null) return path;
            string src = path;
            string dst = Path.ChangeExtension(src, ext);
            if (!File.Exists(src)) { return ""; }
            if (File.Exists(dst))
            {
                DateTime src_dt = File.GetLastWriteTime(src);
                DateTime dst_dt = File.GetLastWriteTime(dst);
                if (dst_dt > src_dt) { return dst; }
            }
            string name = (ext == ".xedf") ? "edif2xml" :
                          (ext == ".data") ? "csv2xml" : null;
            string s = Path.GetFileNameWithoutExtension(src);
            Console.Write(":"+name+": "+s);
            XmlDocument doc = new XmlDocument();
            var tm = new System.Diagnostics.Stopwatch();
            tm.Start();
            if (ConvertToXmldoc(doc, src, ext)) {
                doc.Save(dst);
                src = dst;
            }
            tm.Stop();
            Console.WriteLine(" : "+tm.ElapsedMilliseconds+"ms");
            return src;
        }

        // xslt transformation
        public void Xslt(string src, string xsl, string dst, Dictionary<string, string> col)
        {
            Console.WriteLine("src: "+src);
            Console.WriteLine("dst: "+dst);
            Console.WriteLine("xsl: "+xsl);
            var tm = new System.Diagnostics.Stopwatch();
            tm.Start();
            XslCompiledTransform trans = new XslCompiledTransform();
            XmlWriterSettings ws = new XmlWriterSettings();
            XsltArgumentList al = new XsltArgumentList();
            trans.Load(xsl);
            foreach (string k in col.Keys) {
                string v = col[k];
                if (k[0] == '@') {
                    if (!File.Exists(v)){
                        Console.WriteLine("not find: "+v);
                        continue;
                    }
                    XmlDocument doc = new XmlDocument();
                    string ext = GetConvertType(v);
                    if (! ConvertToXmldoc(doc, v, ext)) doc.Load(v);
                    al.AddParam(k.Substring(1), "", doc);
                    continue;
                }
                al.AddParam(k, "", v);
            }
            ws.ConformanceLevel = ConformanceLevel.Fragment;
            ws.Indent = true;
            ws.IndentChars = "  ";
            XmlWriter wrt = XmlWriter.Create(dst, ws);
            trans.Transform(src, al, wrt);
            wrt.Close();
            tm.Stop();
            Console.Write(":xsl: "+Path.GetFileNameWithoutExtension(dst));
            Console.WriteLine(" : "+tm.ElapsedMilliseconds+"ms");
        }

        public void Format(string src, string dst)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(src);
            doc.Save(dst);
        }

        public void Execute(string src, Dictionary<string, string> col)
        {
            src = Path.GetFullPath(src);
            string src_dir = Path.GetDirectoryName(src);
            string name = Path.GetFileNameWithoutExtension(src);
            string out_dir = Path.Combine(src_dir, name);
            if (!Directory.Exists(out_dir))
                Directory.CreateDirectory(out_dir);

            foreach (string k in Directory.EnumerateFiles(xsl_dir, "data_*.xsl"))
            {
                string xsl = Path.Combine(xsl_dir, k);
                string dst = GetDestName(src, xsl);
                Xslt(src, xsl, Path.Combine(out_dir, dst), col);
                string k1 = "@"+Path.GetFileNameWithoutExtension(dst);
                string v1 = Path.Combine(out_dir, dst);
                if (col.ContainsKey(k1)) col.Remove(k1);
                col.Add(k1, v1);
            }

            foreach (string k in Directory.EnumerateFiles(xsl_dir, "edif_*.xsl"))
            {
                string xsl = Path.Combine(xsl_dir, k);
                string dst = GetDestName(src, xsl);
                Xslt(src, xsl, Path.Combine(out_dir, dst), col);
            }

            string[] sub = new string[]{"page"};
            foreach (string grp in sub){
                string lst = Path.Combine(out_dir, "edif_"+grp+".lst");
                if (!File.Exists(lst)) return;
                foreach (string id in File.ReadAllLines(lst)) {
                    if (col.ContainsKey(grp)) col.Remove(grp);
                    col.Add(grp, id);
                    foreach (string k in Directory.EnumerateFiles(xsl_dir, grp+"_*.xsl"))
                    {
                        string xsl = Path.Combine(xsl_dir, k);
                        string dst = GetDestName(src, xsl).Replace(grp + "_", id + "_");
                        Xslt(src, xsl, Path.Combine(out_dir, dst), col);
                    }
                }
            }
        }
    }
}
