using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;

namespace hwutils
{
    public class EdifXmlDocument
    {
        readonly string[] nms = {
            "edif", "design", "external", "library", "cell", "view", "page",
            "instance", "portinstance", "port", "portbundle", "portlistalias",
            "portimplementation",
            "viewref", "portref", "instanceref", "cellref", "libraryref",
            "figuregroup", "figuregroupOverride", "figure", "offpageconnector",
            "net", "netbundle", "property", "parameter", "display", "keyworddisplay"
        };
        readonly string[] sns = { "name", "rename", "array" };
        readonly Dictionary<string, string> nm = new Dictionary<string, string>();
        readonly Dictionary<string, string> sn = new Dictionary<string, string>();
        enum PS { S0, S1, S2, S3, S4 };
        enum TK { EXP, STR, STR2 };
        XmlDocument doc;
        XmlNode node;
        PS seq;
        void InitParse(XmlDocument doc = null)
        {
            foreach (string s in nms) { this.nm.Add(s.ToLower(), s); }
            foreach (string s in sns) { this.sn.Add(s.ToLower(), s); }
            this.doc = (doc == null) ? new XmlDocument() : doc;
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
            node = doc;
            seq = PS.S0;
        }
        string Parse(string s)
        {
            if (s.Length <= 0) { return ""; }
            if (s == "(") { seq = PS.S0; return ""; }
            if (s == ")") { node = node.ParentNode; seq = PS.S3; return ""; }
            string sl = s.ToLower();
            switch (seq)
            {
                case PS.S0:
                    node = node.AppendChild(doc.CreateNode("element", sl, null));
                    if (nm.ContainsKey(sl)) { seq = PS.S1; break; }
                    if (sn.ContainsKey(sl)) { seq = PS.S2; break; }
                    seq = PS.S3; break;
                case PS.S1: ((XmlElement)node).SetAttribute("name", s); seq = PS.S4; break;
                case PS.S2: { XmlElement ele = (XmlElement)node.ParentNode;
                    while (sn.ContainsKey(ele.Name)) {ele = (XmlElement)ele.ParentNode; }
                    ele.SetAttribute("name", s); seq = PS.S3; } break;
                case PS.S3: node.AppendChild(doc.CreateTextNode(s)); seq = PS.S4; break;
                case PS.S4:
                    node.AppendChild(doc.CreateWhitespace(" "));
                    node.AppendChild(doc.CreateTextNode(s));
                    break;
                default: break;
            }
            return "";
        }
        void LoadEdif(XmlDocument doc, string src, string enc)
        {
            InitParse(doc);
            using (StreamReader sr = new StreamReader(src, Encoding.GetEncoding(enc)))
            {
                TK seq = TK.EXP;
                string line, s = "";
                while ((line = sr.ReadLine()) != null)
                {
                L1: foreach (char c in line.TrimStart(new char[] { ' ' }))
                        switch (seq)
                        {
                            case TK.EXP:
                                switch (c)
                                {
                                    case '#': case ';': s = Parse(s); goto L1;
                                    case '(': case ')': s = Parse(s); Parse("" + c); break;
                                    case ' ': case '\t': s = Parse(s); break;
                                    case '\"': s = Parse(s) + c; seq = TK.STR; break;
                                    default: s += c; break;
                                }
                                break;
                            case TK.STR:
                                s += c;
                                if (c == '\"') { s = Parse(s); seq = TK.EXP; }
                                else if (c == '\\') { seq = TK.STR2; }
                                break;
                            case TK.STR2:
                                s += c;
                                seq = TK.STR;
                                break;
                            default:
                                break;
                        }
                    //if (seq == TK.STR) { s += '\n'; }
                    if (seq == TK.EXP) { s = Parse(s); }
                }
                Parse(s);
            }
        }
        public static bool ToXmldoc(XmlDocument doc, string src)
        {
            // encoding: utf-8, shift_jis, euc-jp
            string encoding = ConfigurationManager.AppSettings["encoding"];
            EdifXmlDocument edifxml = new EdifXmlDocument();
            edifxml.LoadEdif(doc, src, (encoding == null) ? "shift_jis" : encoding);
            return true;
        }
    }
}
