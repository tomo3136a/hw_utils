using System;
using System.Windows.Forms;
using System.Drawing;

namespace mkbin
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        TableLayoutPanel _pnl_root = new TableLayoutPanel();
        MenuStrip _menu_bar = new MenuStrip();

        //package information
        ComboBox _cbx_package = new ComboBox();
        GroupBox _gbx_datatype = new GroupBox();
        TextBox _txt_version = new TextBox();

        //source file
        TextBox _txt_src = new TextBox();
        PathSelectButton _btn_src = new PathSelectButton();
        CheckBox _cbx_src_option = new CheckBox();
        FlowLayoutPanel _pnl_src_option = new FlowLayoutPanel();
        ComboBox _cbx_src_type = new ComboBox();
        MaskedTextBox _txt_src_bgn = new MaskedTextBox();
        MaskedTextBox _txt_src_end = new MaskedTextBox();
        Label _lbl_src_cnt = new Label();
        PathSelectButton _btn_src_map = new PathSelectButton();

        //output file
        TextBox _txt_out = new TextBox();
        PathSelectButton _btn_out = new PathSelectButton();
        CheckBox _cbx_out_option = new CheckBox();
        FlowLayoutPanel _pnl_out_option = new FlowLayoutPanel();
        CheckBox _cbx_out_endian = new CheckBox();

        //shortcut
        FlowLayoutPanel _pnl_shortcut = new FlowLayoutPanel();

        //data
        FlowLayoutPanel _pnl_data = new FlowLayoutPanel();
        MaskedTextBox _txt_address = new MaskedTextBox();
        MaskedTextBox _txt_length = new MaskedTextBox();
        MaskedTextBox _txt_keyword = new MaskedTextBox();
        MaskedTextBox _txt_timestamp = new MaskedTextBox();
        TextBox _txt_reserved1 = new TextBox();
        TextBox _txt_reserved2 = new TextBox();
        MaskedTextBox _txt_checksum = new MaskedTextBox();
        TextBox _txt_fill = new TextBox();

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
#if NET6_0_OR_GREATER
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
#else
            this.AutoScaleDimensions = new System.Drawing.Size(96, 96);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
#endif
            this.SuspendLayout();

            this.Text = GetTitle();
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.MinimumSize = this.ClientSize;

            //root panel
            _pnl_root.ColumnCount = 4;
            _pnl_root.RowCount = 9;
            for (var i = 0; i < _pnl_root.ColumnCount; i++)
                _pnl_root.ColumnStyles.Add(i == 2
                    ? new ColumnStyle(SizeType.Percent, 100F)
                    : new ColumnStyle(SizeType.AutoSize));
            for (var i = 0; i < _pnl_root.RowCount; i++)
                _pnl_root.RowStyles.Add(i == 7
                    ? new RowStyle(SizeType.Percent, 100F)
                    : new RowStyle(SizeType.AutoSize));
            _pnl_root.Dock = DockStyle.Fill;
            _pnl_root.AutoSize = true;
            this.Controls.Add(_pnl_root);

            //menu
            InitializeMenu(_menu_bar);
            this.MainMenuStrip = _menu_bar;
            this.Controls.Add(_menu_bar);

            //package panel
            var pnl_package = new FlowLayoutPanel();
            pnl_package.FlowDirection = FlowDirection.TopDown;
            pnl_package.WrapContents = false;
            pnl_package.AutoSize = true;
            pnl_package.Dock = DockStyle.Fill;
            _pnl_root.Controls.Add(pnl_package);
            _pnl_root.SetRowSpan(pnl_package, 8);

            var lbl = new Label();
            lbl.Text = "バージョン：";
            lbl.Margin = new Padding(10, 10, 0, 0);
            lbl.AutoSize = true;
            pnl_package.Controls.Add(lbl);

            _txt_version.MaxLength = 4;
            _txt_version.ImeMode = ImeMode.Disable;
            _txt_version.Margin = new Padding(20, 10, 10, 10);
            _txt_version.AutoSize = true;
            _txt_version.Width = TextWidth(5);
            _txt_version.TextAlign = HorizontalAlignment.Center;
            pnl_package.Controls.Add(_txt_version);

            lbl = new Label();
            lbl.Text = "パッケージ：";
            lbl.Margin = new Padding(10, 10, 0, 0);
            lbl.AutoSize = true;
            pnl_package.Controls.Add(lbl);

            foreach (var ent in _data_source.Packages)
                if (!_cbx_package.Items.Contains(ent))
                    _cbx_package.Items.Add(ent);
            _cbx_package.Margin = new Padding(20, 10, 10, 10);
            _cbx_package.Dock = DockStyle.Fill;
            _cbx_package.AutoSize = true;
            _cbx_package.Width = TextWidth(10);
            pnl_package.Controls.Add(_cbx_package);

            _gbx_datatype.Text = "タイプ";
            _gbx_datatype.Dock = DockStyle.Fill;
            _gbx_datatype.AutoSize = true;
            pnl_package.Controls.Add(_gbx_datatype);

            //source
            lbl = new Label();
            lbl.Text = "入力：";
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Dock = DockStyle.Left;
            lbl.AutoSize = true;
            _pnl_root.Controls.Add(lbl, 1, 0);

            _txt_src.WordWrap = false;
            _txt_src.Dock = DockStyle.Fill;
            _txt_src.AutoSize = true;
            _pnl_root.Controls.Add(_txt_src, 2, 0);

            _btn_src.Add(_txt_src);
            _btn_src.ShowMode = PathSelectButton.Mode.OPEN;
            _btn_src.AutoSize = true;
            _pnl_root.Controls.Add(_btn_src, 3, 0);

            _cbx_src_option = new CheckBox();
            _cbx_src_option.Dock = DockStyle.Right;
            _cbx_src_option.AutoSize = true;
            _pnl_root.Controls.Add(_cbx_src_option, 1, 1);

            _pnl_src_option.FlowDirection = FlowDirection.LeftToRight;
            _pnl_src_option.WrapContents = false;
            _pnl_src_option.Dock = DockStyle.Fill;
            _pnl_src_option.AutoSize = true;
            _pnl_src_option.Enabled = false;
            _pnl_root.Controls.Add(_pnl_src_option, 2, 1);

            _txt_src_bgn.Mask = ">AAAA AAAA";
            _txt_src_bgn.ImeMode = ImeMode.Disable;
            _txt_src_bgn.AutoSize = true;
            _txt_src_bgn.Width = TextWidth(9);
            _pnl_src_option.Controls.Add(_txt_src_bgn);

            lbl = new Label();
            lbl.Text = "-";
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Dock = DockStyle.Left;
            lbl.AutoSize = true;
            _pnl_src_option.Controls.Add(lbl);

            _txt_src_end.Mask = ">AAAA AAAA";
            _txt_src_end.ImeMode = ImeMode.Disable;
            _txt_src_end.AutoSize = true;
            _txt_src_end.Width = TextWidth(9);
            _pnl_src_option.Controls.Add(_txt_src_end);

            _lbl_src_cnt = new Label();
            _lbl_src_cnt.TextAlign = ContentAlignment.MiddleLeft;
            _lbl_src_cnt.Dock = DockStyle.Left;
            _lbl_src_cnt.AutoSize = true;
            _pnl_src_option.Controls.Add(_lbl_src_cnt);

            _btn_src_map.ShowMode = PathSelectButton.Mode.OPEN;
            _btn_src_map.AutoSize = true;
            _btn_src_map.Enabled = false;
            _pnl_root.Controls.Add(_btn_src_map, 3, 1);

            //output
            lbl = new Label();
            lbl.Text = "出力：";
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Dock = DockStyle.Left;
            lbl.AutoSize = true;
            _pnl_root.Controls.Add(lbl, 1, 2);

            _txt_out.WordWrap = false;
            _txt_out.Dock = DockStyle.Fill;
            _txt_out.Margin = new Padding(0, 5, 0, 0);
            _txt_out.AutoSize = true;
            _pnl_root.Controls.Add(_txt_out, 2, 2);

            _btn_out.Add(_txt_out);
            _btn_out.ShowMode = PathSelectButton.Mode.SAVE;
            _btn_out.AutoSize = true;
            _pnl_root.Controls.Add(_btn_out, 3, 2);

            _cbx_out_option = new CheckBox();
            _cbx_out_option.Dock = DockStyle.Right;
            _cbx_out_option.AutoSize = true;
            _pnl_root.Controls.Add(_cbx_out_option, 1, 3);

            _pnl_out_option.FlowDirection = FlowDirection.LeftToRight;
            _pnl_out_option.WrapContents = false;
            _pnl_out_option.Dock = DockStyle.Fill;
            _pnl_out_option.AutoSize = true;
            _pnl_out_option.Enabled = false;
            _pnl_root.Controls.Add(_pnl_out_option, 2, 3);

            _cbx_out_endian = new CheckBox();
            _cbx_out_endian.Text = "リトルエンディアン";
            _cbx_out_endian.Dock = DockStyle.Right;
            _cbx_out_endian.AutoSize = true;
            _pnl_out_option.Controls.Add(_cbx_out_endian);

            //shortcut button
            _pnl_shortcut.FlowDirection = FlowDirection.LeftToRight;
            _pnl_shortcut.WrapContents = true;
            _pnl_shortcut.Dock = DockStyle.Fill;
            _pnl_shortcut.MinimumSize = new Size(400, 10);
            _pnl_shortcut.AutoSize = true;
            _pnl_shortcut.AllowDrop = true;
            _pnl_root.Controls.Add(_pnl_shortcut, 1, 4);
            _pnl_root.SetColumnSpan(_pnl_shortcut, 3);

            //generate
            var btn_gen = new Button();
            btn_gen.Text = "変換";
            btn_gen.Padding = new Padding(0, btn_gen.Height, 0, btn_gen.Height);
            btn_gen.Dock = DockStyle.Fill;
            btn_gen.AutoSize = true;
            btn_gen.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_gen.Click += (sender, args) => { GenerateFile(); };
            _pnl_root.Controls.Add(btn_gen, 1, 5);
            _pnl_root.SetColumnSpan(btn_gen, 3);

            //data panel
            _pnl_data.FlowDirection = FlowDirection.LeftToRight;
            _pnl_data.Dock = DockStyle.Fill;
            _pnl_data.MinimumSize = new Size(10, 10);
            _pnl_data.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _pnl_data.AutoSize = true;
            _pnl_root.Controls.Add(_pnl_data, 1, 6);
            _pnl_root.SetColumnSpan(_pnl_data, 3);

            lbl = new Label();
            lbl.Text = "アドレス：";
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Dock = DockStyle.Left;
            lbl.AutoSize = true;
            _pnl_data.Controls.Add(lbl);

            _txt_address.Mask = ">AAAA AAAA";
            _txt_address.ImeMode = ImeMode.Disable;
            _txt_address.AutoSize = true;
            _txt_address.Width = TextWidth(9);
            _pnl_data.Controls.Add(_txt_address);

            lbl = new Label();
            lbl.Text = "サイズ：";
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Dock = DockStyle.Left;
            lbl.AutoSize = true;
            _pnl_data.Controls.Add(lbl);

            _txt_length.Mask = ">AAAA AAAA";
            _txt_length.ImeMode = ImeMode.Disable;
            _txt_length.AutoSize = true;
            _txt_length.Width = TextWidth(9);
            _pnl_data.Controls.Add(_txt_length);

            lbl = new Label();
            lbl.Text = "キーワード：";
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Dock = DockStyle.Left;
            lbl.AutoSize = true;
            _pnl_data.Controls.Add(lbl);

            _txt_keyword.Mask = ">AAAAAAAA";
            _txt_keyword.ImeMode = ImeMode.Disable;
            _txt_keyword.AutoSize = true;
            _txt_keyword.Width = TextWidth(8);
            _pnl_data.Controls.Add(_txt_keyword);
            _pnl_data.SetFlowBreak(_txt_keyword, true);

            lbl = new Label();
            lbl.Text = "タイムスタンプ：";
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Dock = DockStyle.Left;
            lbl.AutoSize = true;
            _pnl_data.Controls.Add(lbl);

            _txt_timestamp.ResetOnSpace = true;
            _txt_timestamp.Mask = "0000/00/00 00:00:00";
            _txt_timestamp.ImeMode = ImeMode.Disable;
            _txt_timestamp.AutoSize = true;
            _txt_timestamp.Width = TextWidth(19);
            _pnl_data.Controls.Add(_txt_timestamp);

            lbl = new Label();
            lbl.Text = "予備：";
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Dock = DockStyle.Left;
            lbl.AutoSize = true;
            _pnl_data.Controls.Add(lbl);

            _txt_reserved1.MaxLength = 4;
            _txt_reserved1.ImeMode = ImeMode.Disable;
            _txt_reserved1.AutoSize = true;
            _txt_reserved1.Width = TextWidth(4);
            _pnl_data.Controls.Add(_txt_reserved1);

            _txt_reserved2.MaxLength = 4;
            _txt_reserved2.ImeMode = ImeMode.Disable;
            _txt_reserved2.AutoSize = true;
            _txt_reserved2.Width = TextWidth(4);
            _pnl_data.Controls.Add(_txt_reserved2);

            lbl = new Label();
            lbl.Text = "チェックサム：";
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Dock = DockStyle.Left;
            lbl.AutoSize = true;
            _pnl_data.Controls.Add(lbl);

            _txt_checksum.Mask = ">AAAA AAAA";
            _txt_checksum.ImeMode = ImeMode.Disable;
            _txt_checksum.PromptChar = ' ';
            _txt_checksum.AutoSize = true;
            _txt_checksum.Width = TextWidth(9);
            _pnl_data.Controls.Add(_txt_checksum);
            _pnl_data.SetFlowBreak(_txt_checksum, true);

            //control
            var pnl_control = new FlowLayoutPanel();
            pnl_control.FlowDirection = FlowDirection.LeftToRight;
            pnl_control.WrapContents = true;
            pnl_control.Dock = DockStyle.Bottom;
            pnl_control.AutoSize = true;
            _pnl_root.Controls.Add(pnl_control, 0, 7);
            _pnl_root.SetColumnSpan(pnl_control, 4);

            var btn_close = new Button();
            btn_close.Text = "閉じる";
            btn_close.CausesValidation = false;
            btn_close.AutoSize = true;
            btn_close.DialogResult = DialogResult.Cancel;
            btn_close.Click += OnClose;
            pnl_control.Controls.Add(btn_close);

            _cbx_src_type.AutoSize = true;
            _cbx_src_type.Width = TextWidth(5);
            pnl_control.Controls.Add(_cbx_src_type);

            this.CancelButton = btn_close;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
