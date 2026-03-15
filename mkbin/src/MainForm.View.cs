using System;
using System.IO;
using System.Windows.Forms;

namespace mkbin
{
    public partial class MainForm : Form
    {
        //Text width
        private int TextWidth(int value)
        {
            var w = (1 + value) * _uiseting.TextEm * 8.5;
            return w > 20 ? (int)w : 20;
        }

        private void SetTextWidth(int dw = 1)
        {
            var v1 = _uiseting.TextEm;
            if (v1 < 0.1) v1 = 0.1f;
            var v2 = v1 + dw / 100.0;
            if (v2 < 0.1) v2 = 0.1f;
            _uiseting.TextEm = (float)v2;
            var a = v2 / v1;

            _cbx_package.Width = (int)(_cbx_package.Width * a);
            _txt_version.Width = (int)(_txt_version.Width * a);

            _txt_src_bgn.Width = (int)(_txt_src_bgn.Width * a);
            _txt_src_end.Width = (int)(_txt_src_end.Width * a);

            _txt_address.Width = (int)(_txt_address.Width * a);
            _txt_length.Width = (int)(_txt_length.Width * a);
            _txt_keyword.Width = (int)(_txt_keyword.Width * a);
            _txt_timestamp.Width = (int)(_txt_timestamp.Width * a);
            _txt_reserved1.Width = (int)(_txt_reserved1.Width * a);
            _txt_reserved2.Width = (int)(_txt_reserved2.Width * a);
            _txt_checksum.Width = (int)(_txt_checksum.Width * a);
            _txt_fill.Width += (int)(_txt_fill.Width * a);
        }

        //title
        private string GetTitle()
        {
            var s = Application.ExecutablePath;
            s = Path.GetFileNameWithoutExtension(s);
            s += " " + Application.ProductVersion;
            var opt = _data.PackageName + " " + _data.DataTypeName;
            opt = opt.Trim();
            if (opt != "") s += " - " + opt;
            return s;
        }

        //ui settings
        private void UISettingsClear()
        {
            var p = MainFormSettings.ConfigurationPath();
            if (File.Exists(p)) File.Delete(p);
            SettingSaveEnable = false;
        }

        //actions
        private bool GenerateFile()
        {
            if (_data.GenerateFile())
            {
                var msg = "ファイルを作成しました。\r\n";
                MessageBox.Show(msg + _data.Message);
                return true;
            }

            if (_data.Status == -2)
            {
                var msg = "入力ファイルが見つかりません。\r\n";
                MessageBox.Show(msg + _data.Message);
                return false;
            }
            if (_data.Status == -3)
            {
                var msg = "タイムスタンプが正しくありません。\r\n";
                MessageBox.Show(msg + _data.Message);
            }
            MessageBox.Show("未定義");
            return false;
        }

        private bool RestoreFile()
        {
            var dst = Util.FullPath(_data.OutputFile);
            if (dst == "")
            {
                var src = Util.FullPath(_data.SourceFile);
                dst = Path.ChangeExtension(src, ".bin");
                if (src == dst) dst = src + ".bin";
            }

            var bin = new BinFile();
            return bin.Restore(dst);
        }

        private void ReportView()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var binfile = new BinFile();
                binfile.Restore(dlg.FileName);
                MessageBox.Show(binfile.Report());
            }
        }

        //read map file
        public void ReadMapFile(string path)
        {
            var mf = new MapFile();
            if (mf.Read(path))
            {
                _data.Begin = mf.Begin.ToString("X8");
                _data.End = mf.End.ToString("X8");
            }
        }
    }
}
