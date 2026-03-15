using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace mkbin
{
    public partial class MainForm : Form
    {
        public void ValidateConfig()
        {
            ValidateVersion();
            ValidatePackage();
            ValidateSource();
            ValidateOutput();

            ValidateAddress();
            ValidateLength();
            ValidateKeyword();
            ValidateTimestamp();
            ValidateReserve1();
            ValidateReserve2();
            ValidateChecksum();
        }

        //version
        public void ValidateVersion()
        {
            Alart(_txt_version, "\\d[.]\\d+");
        }

        //package
        public void ValidatePackage()
        {
            var b = false;
            foreach (var v in _data_source.Packages)
                if (v == _cbx_package.Text) b |= true;
            Alart(_cbx_package, b);
        }

        //source
        public void ValidateSource()
        {
            var fi = new FileInfo(_data.SourceFile);
            var b1 = fi.Exists;
            var b2 = true;
            var b3 = true;
            if (b1)
            {
                var sz = fi.Length;
                var s = _data.Begin;
                var v1 = s == "" ? 0 : Util.HextToLong(s);
                s = _data.End;
                var v2 = s == "" ? sz - 1 : Util.HextToLong(s);
                b2 = 0 <= v1 && v1 < sz && v1 <= v2;
                b3 = 0 <= v2 && v2 < sz && v1 <= v2;
            }
            Alart(_txt_src, b1);
            Alart(_txt_src_bgn, b2);
            Alart(_txt_src_end, b3);
        }

        //output
        public void ValidateOutput()
        {
            var b = !File.Exists(_data.OutputFile);
            var lv = _data.SourceFile == _data.OutputFile ? 0 : 1;
            Alart(_txt_out, b, lv);
        }

        //data
        public void ValidateAddress()
        {
            var b = true;
            if (_data.Address != "")
            {
                b = false;
            }
            Alart(_txt_address, b);
        }
        public void ValidateLength()
        {
            var b = true;
            if (_data.Length != "")
            {
                b = false;
            }
            Alart(_txt_length, b);
        }
        public void ValidateKeyword()
        {
            var b = true;
            if (_data.Keyword != "")
            {
                b = false;
            }
            Alart(_txt_keyword, b);

        }
        public void ValidateTimestamp()
        {
            var b = true;
            if (_data.Timestamp != "")
            {
                b = false;
            }
            Alart(_txt_timestamp, b);

        }
        public void ValidateReserve1()
        {
            var b = true;
            if (_data.Reserved1 != "")
            {
                b = false;
            }
            Alart(_txt_reserved1, b);

        }
        public void ValidateReserve2()
        {
            var b = true;
            if (_data.Reserved2 != "")
            {
                b = false;
            }
            Alart(_txt_reserved2, b);

        }
        public void ValidateChecksum()
        {
            var b = true;
            if (_data.Checksum != "")
            {
                b = false;
            }
            Alart(_txt_checksum, b);
        }

        //level = 0  warning
        //level = 1  attention
        //level = 2  caution
        //level = 3  alert
        //level = 4  safe
        private void Alart(Control control, bool b = false, int lv = 0)
        {
            var color = SystemColors.Window;
            color = lv == 0 ? Color.Yellow : color;
            color = lv == 1 ? Color.LightYellow : color;
            color = lv == 2 ? Color.Orange : color;
            color = lv == 3 ? Color.Red : color;
            color = lv == 4 ? Color.GreenYellow : color;
            color = b ? SystemColors.Window : color;
            if (control.BackColor != color)
                control.BackColor = color;
        }

        private void Alart(Control control, string ptn, int lv = 0)
        {
            var b = Regex.Match(control.Text, ptn).Success;
            Alart(control, b);
        }
    }
}
