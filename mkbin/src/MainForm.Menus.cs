using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace mkbin
{
    public partial class MainForm : Form
    {
        private bool _menu_hold = false;
        private ToolStripComboBox _mi_cbx_name = new ToolStripComboBox();

        public void InitializeMenu(MenuStrip menu)
        {
            //menubar
            menu.MenuActivate += (sender, args) => { this.MainMenuStrip.Visible = true; };
            menu.MenuDeactivate += (sender, args) => { if (_menu_hold) this.MainMenuStrip.Visible = false; };

            //file menu
            var submenu = new ToolStripMenuItem("ファイル(&F)");
            menu.Items.Add(submenu);
            {
                var item = new ToolStripMenuItem("設定リセット(&R)");
                item.Click += (sender, args) => { _data.Reset(); };
                submenu.DropDownItems.Add(item);

                submenu.DropDownItems.Add(new ToolStripSeparator());

                item = new ToolStripMenuItem("終了(&X)");
                item.Click += OnClose;
                submenu.DropDownItems.Add(item);
            }

            //test menu
            submenu = new ToolStripMenuItem("テスト(&T)");
            submenu.Click += (sender, args) =>
            {
                var dlg = new InputBox();
                dlg.SetText("test");
                if (dlg.ShowDialog() == DialogResult.OK)
                    MessageBox.Show(dlg.GetText());
            };
            menu.Items.Add(submenu);

            //tool menu
            submenu = new ToolStripMenuItem("ツール(&T)");
            menu.Items.Add(submenu);
            {
                var item = new ToolStripMenuItem("復元");
                item.Click += (sender, args) => { RestoreFile(); };
                submenu.DropDownItems.Add(item);

                item = new ToolStripMenuItem("検証");
                item.Click += (sender, args) => { ValidateConfig(); };
                submenu.DropDownItems.Add(item);

                item = new ToolStripMenuItem("レポート");
                item.Click += (sender, args) => { ReportView(); };
                submenu.DropDownItems.Add(item);
            }

            //data menu
            submenu = new ToolStripMenuItem("データ(&D)");
            menu.Items.Add(submenu);
            {
                //sortcut menu
                var subsubmenu = new ToolStripMenuItem("ショートカット");
                submenu.DropDownItems.Add(subsubmenu);
                {
                    var subitem = new ToolStripMenuItem("全削除");
                    subitem.Click += (sender, args) => { RegisterShortcut("", true); };
                    subsubmenu.DropDownItems.Add(subitem);

                    subsubmenu.DropDownItems.Add(new ToolStripSeparator());

                    _mi_cbx_name.DropDown += (sender, args) =>
                    {
                        _mi_cbx_name.Items.Clear();
                        foreach (var ent in _data_source.Shortcuts)
                            _mi_cbx_name.Items.Add(ent.Name);
                    };
                    subsubmenu.DropDownItems.Add(_mi_cbx_name);

                    subitem = new ToolStripMenuItem("追加");
                    subitem.Click += (sender, args) => { RegisterShortcut(_mi_cbx_name.Text); };
                    subsubmenu.DropDownItems.Add(subitem);

                    subitem = new ToolStripMenuItem("削除");
                    subitem.Click += (sender, args) => { RegisterShortcut(_mi_cbx_name.Text, true); };
                    subsubmenu.DropDownItems.Add(subitem);

                    subitem = new ToolStripMenuItem("<<");
                    subitem.Click += (sender, args) => { MoveShortcut(_mi_cbx_name.Text); };
                    subsubmenu.DropDownItems.Add(subitem);
                }

                submenu.DropDownItems.Add(new ToolStripSeparator());

                var item = new ToolStripMenuItem("データソース保存");
                item.CheckOnClick = true;
                item.CheckedChanged += (sender, args) => { _data_source.IsSaving = item.Checked; };
                submenu.DropDownItems.Add(item);
            }

            //view menu
            submenu = new ToolStripMenuItem("表示(&V)");
            menu.Items.Add(submenu);
            {
                var item = new ToolStripMenuItem("フォント(&F)");
                item.Click += (sender, args) =>
                {
                    var dlg = new FontDialog();
                    dlg.Font = this.Font;
                    if (dlg.ShowDialog() == DialogResult.OK)
                        this.Font = dlg.Font;
                };
                submenu.DropDownItems.Add(item);

                submenu.DropDownItems.Add(new ToolStripSeparator());

                item = new ToolStripMenuItem("表示設定ファイル削除");
                item.Click += (sender, args) =>
                {
                    UISettingsClear();
                };
                submenu.DropDownItems.Add(item);
            }

            //menu off
            var menu_off = new ToolStripMenuItem("メニュー保持(&0)");
            menu_off.Alignment = ToolStripItemAlignment.Right;
            menu_off.CheckOnClick = true;
            menu_off.Checked = true;
            menu_off.Click += (sender, args) =>
            {
                _menu_hold = !_menu_hold;
                if (_menu_hold) this.MainMenuStrip.Visible = false;
            };
            menu.Items.Add(menu_off);
        }
    }
}
