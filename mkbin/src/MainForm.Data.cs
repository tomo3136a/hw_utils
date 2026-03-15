using System;
using System.Windows.Forms;
using System.Drawing;

namespace mkbin
{
    public partial class MainForm : Form
    {
        private DataSource _data_source;
        private Data _data;

        private void LoadData()
        {
            _data_source = DataSource.Load();
            _data = Data.Load();
        }

        private void SaveData()
        {
            _data_source.Save();
            _data.Save();
        }

        private void SetData()
        {
            //DataType
            SetDataType();

            //file type
            _cbx_src_type.Items.Clear();
            foreach (var ft in _data_source.FileTypes)
                _cbx_src_type.Items.Add(ft.Name);

            //file filter
            _btn_src.Selector.ClearFilter();
            foreach (var k in _data_source.InFilters)
                _btn_src.Selector.AddFilter(k, _data_source.DispName(k), _data_source.Extentions(k));

            _btn_src_map.Selector.ClearFilter();
            foreach (var k in _data_source.MapFilters)
                _btn_src_map.Selector.AddFilter(k, _data_source.DispName(k), _data_source.Extentions(k));

            _btn_out.Selector.ClearFilter();
            foreach (var k in _data_source.OutFilters)
                _btn_out.Selector.AddFilter(k, _data_source.DispName(k), _data_source.Extentions(k));

            //shortcut
            foreach (var ent in _data_source.Shortcuts) AddShortcut(ent);

            //DataType
            UpdateDataType();
        }

        //data type
        private void SetDataType()
        {
            var p = new Point(30, 40);
            foreach (var ent in _data_source.DataTypes)
            {
                if (HasDataType(ent.Name)) continue;
                var rb = new RadioButton();
                rb.Text = ent.Name;
                rb.Location = p;
                rb.AutoSize = true;
                rb.CheckedChanged += OnDataTypeChanged;
                _gbx_datatype.Controls.Add(rb);
                p.Y += rb.Height;
            }
        }

        private bool HasDataType(string s)
        {
            foreach (var c in _gbx_datatype.Controls)
            {
                var rb = c as RadioButton;
                if (rb.Text == s) return true;
            }
            return false;
        }

        private void UpdateDataType()
        {
            foreach (var c in _gbx_datatype.Controls)
            {
                var rb = c as RadioButton;
                if (rb.Text == _data.DataTypeName) rb.Checked = true;
            }
        }

        //shortcut
        private void SwitchConfig(Data cfg)
        {
            _data.CopyFrom(cfg.Name, cfg);
            UpdateDataType();
        }

        private void RegisterShortcut(string name, bool mode = false)
        {
            foreach (var ent in _data_source.Shortcuts)
            {
                if (ent.Name == name || name.Length == 0)
                {
                    _data_source.Shortcuts.Remove(ent);
                    foreach (var c in _pnl_shortcut.Controls)
                    {
                        var btn = c as Button;
                        if (btn.Text == name)
                        {
                            _pnl_shortcut.Controls.Remove(btn);
                            break;
                        }
                    }
                    break;
                }
            }
            if (mode) return;
            if (name.Length == 0) return;

            var shortcut = new Data(name, _data);
            _data_source.Shortcuts.Add(shortcut);

            AddShortcut(shortcut);
        }

        private void AddShortcut(Data ent)
        {
            var btn = new Button();
            btn.Text = ent.Name;
            btn.Padding = new Padding(0, btn.Height, 0, btn.Height);
            btn.AutoSize = true;
            btn.Click += OnShortcut;
            _pnl_shortcut.Controls.Add(btn);
        }

        private void MoveShortcut(string name)
        {
            if (name.Length == 0) return;

            var ind = 0;
            foreach (var ent in _data_source.Shortcuts)
            {
                if (ent.Name == name)
                {
                    if (ind < 1) return;
                    _data_source.Shortcuts.Remove(ent);
                    _data_source.Shortcuts.Insert(ind - 1, ent);
                    break;
                }
                ind++;
            }

            _pnl_shortcut.Controls.Clear();
            foreach (var ent in _data_source.Shortcuts) AddShortcut(ent);
        }
    }
}
