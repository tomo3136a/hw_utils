using System;
using System.Text;
using System.Windows.Forms;

namespace mkbin
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
#if NET6_0_OR_GREATER
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ApplicationConfiguration.Initialize();
#else
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.SetCompatibleTextRenderingDefault(true);
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
            //Application.Current.Resources["ApplicationFontFamily"] = new FontFamily("Meiryo UI");
            Application.Run(new MainForm());
        }
    }
}