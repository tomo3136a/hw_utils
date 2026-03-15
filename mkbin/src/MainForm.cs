using System;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;

namespace mkbin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            LoadData();
            InitializeComponent();
            InitializeEvents();

            this.Load += new EventHandler(MainForm_Load);
        }

        //form load
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            //ui setting
            _uiseting.SettingChanging += new SettingChangingEventHandler(MainForm_SettingChanging);
            _uiseting.SettingsSaving += new SettingsSavingEventHandler(MainForm_SettingsSaving);

            //binding
            Binding();

            //data
            SetData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        //ui settings serialization
        private void MainForm_SettingChanging(object sender, SettingChangingEventArgs e)
        {
            SettingSaveEnable = true;
        }

        private void MainForm_SettingsSaving(object sender, CancelEventArgs e)
        {
            if (SettingSaveEnable)
            {
                var res = MessageBox.Show(
                    "UI 設定を保存しますか？", "Save Settings", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == res)
                {
                    _uiseting.Save();
                    return;
                }
            }
            e.Cancel = true;
        }

        private bool SettingSaveEnable = false;

        //base events
        private void OnClose(Object sender, EventArgs args)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TextBox_Hex_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)Keys.Menu == e.KeyValue) return; //ALT
            if ('A' <= e.KeyValue && e.KeyValue <= 'F') return;
            if ('0' <= e.KeyValue && e.KeyValue <= '9') return;
            if ((int)Keys.NumPad0 <= e.KeyValue && e.KeyValue <= (int)Keys.NumPad9) return;
            if ((int)Keys.Back == e.KeyValue || (int)Keys.Delete == e.KeyValue) return;
            if ((int)Keys.Left == e.KeyValue || (int)Keys.Right == e.KeyValue) return;
            if ((int)Keys.F1 <= e.KeyValue && e.KeyValue <= (int)Keys.F24) return;
            e.SuppressKeyPress = true;
        }

        //override key press
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //valiidation
            if (keyData == Keys.F1) { ValidateConfig(); return true; }

            //data panel on/off
            if (keyData == Keys.F12) { _uiseting.DataVisible = !_uiseting.DataVisible; return true; }

            //width
            if (keyData == Keys.F9) { SetTextWidth(-10); return true; }
            if (keyData == Keys.F10) { SetTextWidth(10); return true; }

            //development function, layout separate line on/off
            if (keyData == Keys.F8)
            {
                if (_uiseting.CellBorderStyle == TableLayoutPanelCellBorderStyle.None)
                    _uiseting.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                else
                    _uiseting.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
