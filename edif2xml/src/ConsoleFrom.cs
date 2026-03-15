using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace hwutils
{
    public class ConsoleForm : Form
    {
        private class ControlWriter : TextWriter
        {
            private TextBox textbox;
            public ControlWriter(TextBox textbox)
            {
                this.textbox = textbox;
            }
            public override void Write(char value)
            {
                textbox.AppendText(""+value);
            }
            public override void Write(string value)
            {
                textbox.AppendText(value);
            }
            public override Encoding Encoding
            {
                get { return Encoding.ASCII; }
            }
        }

        private bool running = false;
        private TextBox textbox;
        private Button accept;
        private BackgroundWorker worker;

        public ConsoleForm()
        {
            InitializeComponent();
            Console.SetOut(new ControlWriter(textbox));
            InitializeBackgroundWorker();
        }

        private void InitializeComponent()
        {
            textbox = new TextBox();
            accept = new Button();
            worker = new BackgroundWorker();
            SuspendLayout();
            textbox.Dock = DockStyle.Fill;
            textbox.ReadOnly = true;
            textbox.Multiline = true;
            textbox.ScrollBars = ScrollBars.Vertical;
            accept.Text = "Close";
            accept.Dock = DockStyle.Bottom;
            accept.DialogResult = DialogResult.OK;
            accept.Click += new EventHandler(on_close);
            worker.WorkerSupportsCancellation = true;
            this.Text = "Console";
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.AcceptButton = accept;
            this.Shown += on_shown;
            this.Controls.Add(textbox);
            this.Controls.Add(accept);
            this.ResumeLayout(false);
            this.PerformLayout();
            this.ActiveControl = accept;
        }

        private void InitializeBackgroundWorker()
        {
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                worker_RunWorkerCompleted);
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = Run();
            running = false;
            accept.Text = "Ok";
        }
        private void worker_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            running = false;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                accept.Text = "Canceled";
            }
        }
        private void on_shown(object sender, EventArgs e)
        {
            running = true;
            worker.RunWorkerAsync();
        }
        void on_close(Object sender, EventArgs e)
        {
            if (running){
                var res = MessageBox.Show("Close?", this.Text, 
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (res == DialogResult.Cancel)
                    return;
            }
            running = false;
            this.Close();
        }
        public virtual long Run()
        {
            return 0;
        }
    }
}
