using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using mkbin;

public class PathSelectDialog
{
    public enum Mode { OPEN = 1, SAVE = 2, FOLDER = 4, MULTI = 8 }

    public string Path { get; set; }
    public Mode ShowMode = Mode.OPEN;
    public bool EnvNameEnable = true;
    public string FileType { get; set; }


    public PathSelectDialog() { Path = ""; }
    public PathSelectDialog(Mode mode, string path = "")
    {
        ShowMode = mode;
        Path = path;
        FileType = "";
    }

    //events
    public DialogResult ShowDialog()
    {
        var p = EnvNameEnable ? Util.ConvertFromEnv(Path) : Path;
        var f = "";
        var d = (p == "") ? "" : System.IO.Path.GetFullPath(p);
        if (d != "" && !Directory.Exists(d))
        {
            f = System.IO.Path.GetFileName(d);
            d = System.IO.Path.GetDirectoryName(d);
        }

        if ((ShowMode & Mode.OPEN) != 0)
            p = ShowOpenFile(d, f);
        else if ((ShowMode & Mode.SAVE) != 0)
            p = ShowSaveFile(d, f);
        else if ((ShowMode & Mode.FOLDER) != 0)
            p = ShowSelectFilder(p);
        if (p == "") return DialogResult.Cancel;

        p = Util.RelatedPath(p, Directory.GetCurrentDirectory());
        Path = EnvNameEnable ? Util.ConvertToEnv(p) : p;
        return DialogResult.OK;
    }

    private string ShowOpenFile(string d, string f)
    {
        var dlg = new OpenFileDialog();
        dlg.InitialDirectory = d;
        dlg.FileName = f;
        dlg.Filter = GetFilter();
        dlg.FilterIndex = GetFilterIndex(f);
        if (dlg.ShowDialog() != DialogResult.OK) return "";
        FileType = GetFilterName(dlg.Filter, dlg.FilterIndex);
        return dlg.FileName;
    }

    private IEnumerable<string> ShowOpenMultiFile(string d, string f)
    {
        var dlg = new OpenFileDialog();
        dlg.Multiselect = true;
        dlg.InitialDirectory = d;
        dlg.FileName = f;
        dlg.Filter = GetFilter();
        dlg.FilterIndex = GetFilterIndex(f);
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            FileType = GetFilterName(dlg.Filter, dlg.FilterIndex);
            var s = "";
            foreach (var p in dlg.FileNames)
            {
                var n = EnvNameEnable ? Util.ConvertToEnv(p) : p;
                yield return n;
                s += (s.Length > 0 ? "" : ";") + n;
            }
            Path = s;
        }
    }

    private string ShowSaveFile(string d, string f)
    {
        var dlg = new SaveFileDialog();
        dlg.InitialDirectory = d;
        dlg.FileName = f;
        dlg.Filter = GetFilter();
        var idx = GetFilterIndex(f);
        dlg.FilterIndex = idx;
        if (dlg.ShowDialog() != DialogResult.OK) return "";
        FileType = GetFilterName(dlg.Filter, dlg.FilterIndex);
        return dlg.FileName;
    }

    private string ShowSelectFilder(string s)
    {
        var dlg = new FolderBrowserDialog();
        //dlg.InitialDirectory = s;
        dlg.SelectedPath = s;
        if (dlg.ShowDialog() != DialogResult.OK) return "";
        return dlg.SelectedPath;
    }

    //filter
    public bool FilterAllUse = true;
    private string _flt = "";
    public Dictionary<string, string> _infos = new Dictionary<string, string>();

    public void ClearFilter()
    {
        _flt = "";
    }


    public void AddFilter(string id, string info, params string[] exts)
    {
        if (!_infos.ContainsKey(info)) _infos.Add(info, id);

        if (exts.Length == 0) return;
        if (info == "")
        {
            info = exts[0].ToUpper().Replace("*", "").Replace(".", "");
            info = info.Replace(";", ",") + "ファイル";
        }
        foreach (var ext in exts)
        {
            var f = false;
            var s = "";
            var ss = _flt.Split('|');
            for (var i = 0; i < ss.Length / 2; i++)
            {
                var s1 = ss[2 * i].Split(new char[] { '(' }, 2)[0].Trim();
                var s2 = ss[2 * i + 1];
                if (s1 == info) { s2 += ";" + ext; f = true; }
                if (s != "") s += "|";
                s += s1 + " (" + s2 + ")|" + s2;
            }
            if (f) { _flt = s; continue; }
            if (s != "") s += "|";
            _flt = s + info + " (" + ext + ")|" + ext;
        }
    }

    public void AddFilter(string ext)
    {
        AddFilter("", ext);
    }

    public string GetFilter()
    {
        if (!FilterAllUse || _flt.Length == 0) return _flt;
        return _flt + "|すべてのファイル (*.*)|*.*";
    }

    private int GetFilterIndex(string s)
    {
        var ind = 0;
        foreach (var s1 in _flt.Split('|'))
        {
            if (ind % 2 == 1)
            {
                foreach (var s2 in s1.Split(';'))
                {
                    var k = s2.Replace("*", "").ToLower();
                    if (k.Length < 2) continue;
                    var i = s.ToLower().IndexOf(k);
                    if (i >= 0) return 1 + ind / 2;
                }
            }
            ind++;
        }
        return GetFilterIndex2();
    }

    private int GetFilterIndex2()
    {
        var name = FileType;
        foreach (var k in _infos.Keys)
        {
            if (_infos[k] == name)
            {
                var ind = 0;
                foreach (var s1 in _flt.Split('|'))
                {
                    if (ind % 2 == 0)
                    {
                        var ss = s1.Split('(');
                        var s2 = ss[0].Trim();
                        if (s2 == k) return ind / 2 + 1;
                    }
                    ind++;
                }
            }
        }
        var cnt = _flt.Split('|').Length / 2;
        if (cnt > 0) return cnt + 1;
        return 1;
    }

    public string GetFilterName(string flt, int ind)
    {
        var ss = flt.Split('|');
        ind--;
        if (ind < 0 || ind * 2 >= ss.Length) return "";
        var s = ss[2 * ind];
        ss = s.Split('(');
        s = ss[0].Trim();
        return _infos.ContainsKey(s) ? _infos[s] : "";
    }

    //result
    public string FullPath()
    {
        var s = Path;
        return EnvNameEnable ? Util.ConvertFromEnv(s) : s;
    }
}

