using System;
using System.Windows.Forms;

namespace mkbin
{
    public partial class MainForm : Form
    {
        private void InitializeEvents()
        {
            _cbx_package.TextChanged += OnPackageChanged;

            _btn_src.PathChanged += OnSourcePathChanged;
            _cbx_src_option.CheckedChanged += OnSourceOptionCheckedChanged;
            //_cbx_src_type.LostFocus += (sender, args) => { _btn_src.Selector.FileType = _data.SourceType; };
            _txt_src_bgn.KeyDown += TextBox_Hex_KeyDown;
            _txt_src_bgn.TextChanged += OnSourceRangeChanged;
            _txt_src_end.KeyDown += TextBox_Hex_KeyDown;
            _txt_src_end.TextChanged += OnSourceRangeChanged;
            _btn_src_map.PathChanged += OnReadMapFile;

            _cbx_out_option.CheckedChanged += OnOutputOptionCheckedChanged;

            _txt_address.KeyDown += TextBox_Hex_KeyDown;
            _txt_length.KeyDown += TextBox_Hex_KeyDown;
            _txt_reserved1.KeyDown += TextBox_Hex_KeyDown;
            _txt_reserved2.KeyDown += TextBox_Hex_KeyDown;
            _txt_checksum.KeyDown += TextBox_Hex_KeyDown;
        }

        //user events: shortcut
        private void OnShortcut(Object sender, EventArgs args)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                _data.Name = btn.Text;
                _mi_cbx_name.Text = btn.Text;
                foreach (var ent in _data_source.Shortcuts)
                {
                    if (ent.Name != btn.Text) continue;
                    SwitchConfig(ent);
                    break;
                }
            }
        }

        //user events : package
        private void OnPackageChanged(Object sender, EventArgs args)
        {
            this.Text = GetTitle();
        }

        private void OnDataTypeChanged(Object sender, EventArgs args)
        {
            var rb = sender as RadioButton;
            if (!rb.Checked) return;

            _data.DataTypeName = rb.Text;
            foreach (var ent in _data_source.DataTypes)
            {
                if (ent.Name != _data.DataTypeName) continue;
                _data.Keyword = ent.Keyword;
                _data.SourceType = ent.ExtentionName;
                _btn_src.Selector.FileType = _data.SourceType;
            }
            this.Text = GetTitle();
        }

        //user events : source
        void OnSourcePathChanged(Object sender, EventArgs args)
        {
            var c = sender as PathSelectButton;
            if (c == null) return;
            //if (_data.SourceType != c.Selector.FileType)
            //    _data.SourceType = c.Selector.FileType;
        }

        void OnSourceOptionCheckedChanged(Object sender, EventArgs args)
        {
            var cb = sender as CheckBox;
            _pnl_src_option.Enabled = cb.Checked;
            _btn_src_map.Enabled = cb.Checked;
        }

        void OnSourceRangeChanged(Object sender, EventArgs args)
        {
            var v1 = Util.HextToLong(_data.Begin);
            var v2 = -1L;
            var s = _data.End.Trim();
            if (s.Length > 0) v2 = Util.HextToLong(s);
            var cnt = v2 >= v1 ? (v2 - v1 + 1) : 0;
            s = "=" + Util.LongToHex(cnt) + "(" + cnt + ")";
            _lbl_src_cnt.Text = s;
        }

        public void OnReadMapFile(object sender, EventArgs args)
        {
            var c = sender as PathSelectButton;
            if (c == null) return;
            ReadMapFile(c.Path);
        }


        //user events : output
        void OnOutputOptionCheckedChanged(Object sender, EventArgs args)
        {
            var cb = sender as CheckBox;
            _pnl_out_option.Enabled = cb.Checked;
        }
    }
}
