using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace hwutils
{
    /////////////////////////////////////////////////////////////////////
    // dialog

    public class SettingDialog : Form
    {
        Button accept = new Button();
        Button cancel = new Button();
        Label src1Name = new Label();
        TextBox src1Box = new TextBox();
        Button src1Btn = new Button();
        Label src2Name = new Label();
        TextBox src2Box = new TextBox();
        Button src2Btn = new Button();
        Label xslName = new Label();
        TextBox xslBox = new TextBox();
        Button xslBtn = new Button();
        Label[] dtName = new Label[4];
        TextBox[] kyBox = new TextBox[4];
        TextBox[] dtBox = new TextBox[4];
        Button[] dtBtn = new Button[4];
        TextBox textBox = new TextBox();

        public SettingDialog(string caption)
        {
            int width = 500;
            int height = 360;
            int g = 10;

            this.Width = width;
            this.Height = height;
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.Text = caption;
            this.MinimumSize = new Size(width, height);
            this.StartPosition = FormStartPosition.CenterScreen;
            int w = this.ClientRectangle.Width;
            int h = this.ClientRectangle.Height;

            this.SuspendLayout();

            cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancel.Text = "Cancel";
            cancel.Width = 100;
            cancel.Left = w - g - cancel.Width;
            cancel.Top = h - g - cancel.Height;
            cancel.DialogResult = DialogResult.Cancel;
            cancel.Click += new EventHandler(on_close);

            accept.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            accept.Text = "Ok";
            accept.Width = cancel.Width;
            accept.Left = cancel.Left - g - accept.Width;
            accept.Top = cancel.Top;
            accept.DialogResult = DialogResult.OK;
            accept.Click += new EventHandler(on_close);

            this.Controls.Add(accept);
            this.Controls.Add(cancel);

            int top = g;

            // source 1
            src1Name.Text = "Source";
            src1Btn.Click += new EventHandler(on_src1);
            AddPathBox(top, src1Name, null, src1Box, src1Btn);
            top += src1Name.Height + g;

            // source 2
            src2Name.Text = "Source";
            src2Btn.Click += new EventHandler(on_src2);
            AddPathBox(top, src2Name, null, src2Box, src2Btn);
            top += src2Name.Height + g;

            // xsl
            xslName.Text = "Xsl";
            xslBtn.Click += new EventHandler(on_xsl);
            AddPathBox(top, xslName, null, xslBox, xslBtn);
            top += xslName.Height + g;

            // data
            dtName[0] = new Label();
            kyBox[0] = new TextBox();
            dtBox[0] = new TextBox();
            dtBtn[0] = new Button();

            dtName[0].Text = "data";
            dtBtn[0].Click += new EventHandler(on_dt1);
            AddPathBox(top, dtName[0], kyBox[0], dtBox[0], dtBtn[0]);
            top += dtName[0].Height + g;

            // data
            dtName[1] = new Label();
            kyBox[1] = new TextBox();
            dtBox[1] = new TextBox();
            dtBtn[1] = new Button();

            dtName[1].Text = "data";
            dtBtn[1].Click += new EventHandler(on_dt2);
            AddPathBox(top, dtName[1], kyBox[1], dtBox[1], dtBtn[1]);
            top += dtName[1].Height + g;

            // data
            dtName[2] = new Label();
            kyBox[2] = new TextBox();
            dtBox[2] = new TextBox();
            dtBtn[2] = new Button();

            dtName[2].Text = "data";
            dtBtn[2].Click += new EventHandler(on_dt3);
            AddPathBox(top, dtName[2], kyBox[2], dtBox[2], dtBtn[2]);
            top += dtName[2].Height + g;

            // data
            dtName[3] = new Label();
            kyBox[3] = new TextBox();
            dtBox[3] = new TextBox();
            dtBtn[3] = new Button();

            dtName[3].Text = "data";
            dtBtn[3].Click += new EventHandler(on_dt4);
            AddPathBox(top, dtName[3], kyBox[3], dtBox[3], dtBtn[3]);
            top += dtName[3].Height + g;

            this.textBox.AcceptsReturn = true;
            this.textBox.AcceptsTab = true;
            //this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Multiline = true;
            this.textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Width = w - 2 * g;
            this.textBox.Left = g;
            this.textBox.Height = accept.Top - top - 2 * g;
            this.textBox.Top = top + g;
            this.Controls.Add(this.textBox);

            this.AcceptButton = accept;
            this.CancelButton = cancel;

            this.ActiveControl = src1Box;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        void AddPathBox(int top, Label name, TextBox key, TextBox val, Button btn)
        {
            int w = this.ClientRectangle.Width;
            int h = this.ClientRectangle.Height;
            int g = 10;
            int w2 = 44;
            int h2 = 5;
            int kw = 50;

            name.Left = g;
            name.Top = top;
            name.AutoSize = true;

            btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn.Text = "Select";
            btn.AutoSize = true;
            btn.Left = w - g - btn.Width;
            btn.Top = name.Top - h2;

            if (key != null) {
                key.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                key.AutoSize = true;
                key.Width = kw;
                key.Left = g + w2;
                key.Top = btn.Top + (btn.Height - btn.Height);
            }
            val.Anchor = AnchorStyles.Top | AnchorStyles.Bottom 
                | AnchorStyles.Left | AnchorStyles.Right;
            val.AutoSize = true;
            val.Width = w - 3 * g - btn.Width - w2;
            val.Left = g + w2;
            if (key != null) {
                val.Width -= g + kw;
                val.Left += g + kw;
            }
            val.Top = btn.Top + (btn.Height - btn.Height);

            this.Controls.Add(name);
            if (key != null) this.Controls.Add(key);
            this.Controls.Add(val);
            this.Controls.Add(btn);
        }

        void on_close(Object sender, EventArgs e)
        {
            this.Close();
        }

        List<string> src_lst = null;
        List<string> xsl_lst = null;
        Dictionary<string, string> col = null;

        public bool SetData(List<string> src_lst, List<string> xsl_lst, Dictionary<string, string> col)
        {
            this.src_lst = src_lst;
            this.xsl_lst = xsl_lst;
            this.col = col;
            if (src_lst.Count > 0) src1Box.Text = src_lst[0];
            if (src_lst.Count > 1) src2Box.Text = src_lst[1];
            if (xsl_lst.Count > 0) xslBox.Text = xsl_lst[0];
            foreach (var k in col.Keys) {
                kyBox[0].Text = k;
                dtBox[0].Text = col[k];
            }
            return true;
        }
        public bool Restore()
        {
            src_lst.Clear();
            xsl_lst.Clear();
            string f = (src1Box.Text == null) ? "" : src1Box.Text;
            if (f.Length > 0) src_lst.Add(f);
            f = (src2Box.Text == null) ? "" : src2Box.Text;
            if (f.Length > 0) src_lst.Add(f);
            f = (xslBox.Text == null) ? "" : xslBox.Text;
            if (f.Length > 0) xsl_lst.Add(f);
            string k = (kyBox[0].Text == null) ? "" : kyBox[0].Text;
            string d = (kyBox[0].Text == null) ? "" : kyBox[0].Text;
            if (k.Length > 0) {
                if (col.ContainsKey(k)){
                    col.Remove(k);
                }
                col.Add(k,d);
            }
            return true;
        }

        string src_flt = "source files (*.edf;*.edif;*.xedf)|*.edf;*.edif;*.xedf" + 
            "|edif files (*.edf;*.edif)|*.edf;*.edif" + "|xml edif files (*.xedf)|*.xedf"+ 
            "|xml files (*.xml)|*.xml" + "|All files (*.*)|*.*";
        string xsl_flt = "xsl files (*.xsl)|*.xsl";
        string txt_flt = "text files (*.txt)|*.txt" + "|All files (*.*)|*.*";

        // OpenFileDialog openFileDialog1 = new OpenFileDialog();

        public void testDlg()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
        }


        bool select_source(TextBox tb, string flt)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string path = tb.Text;
            path = (path.Length > 0) ? path : ".\\";
            dlg.InitialDirectory = Path.GetDirectoryName(path);
            dlg.FileName = Path.GetFileName(path);
            dlg.Filter = flt;
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tb.Text = dlg.FileName;
                return true;
            }
            return false;
        }

        void on_src1(Object sender, EventArgs e)
        {
            select_source(this.src1Box, src_flt);
        }

        void on_src2(Object sender, EventArgs e)
        {
            select_source(this.src2Box, src_flt);
        }

        void on_xsl(Object sender, EventArgs e)
        {
            select_source(this.xslBox, xsl_flt);
        }

        void on_dt1(Object sender, EventArgs e)
        {
            select_source(this.dtBox[0], txt_flt);
        }

        void on_dt2(Object sender, EventArgs e)
        {
            select_source(this.dtBox[1], txt_flt);
        }

        void on_dt3(Object sender, EventArgs e)
        {
            select_source(this.dtBox[2], txt_flt);
        }

        void on_dt4(Object sender, EventArgs e)
        {
            select_source(this.dtBox[3], txt_flt);
        }
    }
}
