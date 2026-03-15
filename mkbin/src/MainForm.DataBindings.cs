using System;
using System.Windows.Forms;

namespace mkbin
{
    public partial class MainForm : Form
    {
        private MainFormSettings _uiseting = new MainFormSettings();

        private void Binding()
        {
            var db = new DataSource();
            FormBinding();
            ConfigBinding();
        }

        private void FormBinding()
        {
            //form
            this.DataBindings.Add("Location", _uiseting, "FormLocation", true, DataSourceUpdateMode.OnPropertyChanged);
            this.DataBindings.Add("Size", _uiseting, "FormSize", true, DataSourceUpdateMode.OnPropertyChanged);
            this.DataBindings.Add("Font", _uiseting, "FormFont", true, DataSourceUpdateMode.OnPropertyChanged);

            //layout
            _menu_bar.DataBindings.Add("Visible", _uiseting, "MenuVisible", true, DataSourceUpdateMode.OnPropertyChanged);
            _pnl_data.DataBindings.Add("Visible", _uiseting, "DataVisible", true, DataSourceUpdateMode.OnPropertyChanged);
            _pnl_root.DataBindings.Add("CellBorderStyle", _uiseting, "CellBorderStyle", true, DataSourceUpdateMode.Never);
        }

        private void ConfigBinding()
        {
            //configration data binding
            _txt_version.DataBindings.Add("Text", _data, "Version", true, DataSourceUpdateMode.OnPropertyChanged);
            _cbx_package.DataBindings.Add("Text", _data, "PackageName", true, DataSourceUpdateMode.OnPropertyChanged);

            _txt_src.DataBindings.Add("Text", _data, "SourceFile", true, DataSourceUpdateMode.OnPropertyChanged);
            _cbx_src_option.DataBindings.Add("CheckState", _data, "SourceOption", true, DataSourceUpdateMode.OnPropertyChanged);
            _cbx_src_type.DataBindings.Add("Text", _data, "SourceType", true, DataSourceUpdateMode.OnPropertyChanged);
            _txt_src_bgn.DataBindings.Add("Text", _data, "Begin", true, DataSourceUpdateMode.OnPropertyChanged);
            _txt_src_end.DataBindings.Add("Text", _data, "End", true, DataSourceUpdateMode.OnPropertyChanged);

            _txt_out.DataBindings.Add("Text", _data, "OutputFile", true, DataSourceUpdateMode.OnPropertyChanged);
            _cbx_out_option.DataBindings.Add("CheckState", _data, "OutputOption", true, DataSourceUpdateMode.OnPropertyChanged);
            _cbx_out_endian.DataBindings.Add("CheckState", _data, "LittleEndian", true, DataSourceUpdateMode.OnPropertyChanged);

            _txt_address.DataBindings.Add("Text", _data, "Address", true, DataSourceUpdateMode.OnPropertyChanged);
            _txt_length.DataBindings.Add("Text", _data, "Length", true, DataSourceUpdateMode.OnPropertyChanged);
            _txt_keyword.DataBindings.Add("Text", _data, "Keyword", true, DataSourceUpdateMode.OnPropertyChanged);
            _txt_timestamp.DataBindings.Add("Text", _data, "Timestamp", true, DataSourceUpdateMode.OnPropertyChanged);
            _txt_reserved1.DataBindings.Add("Text", _data, "Reserved1", true, DataSourceUpdateMode.OnPropertyChanged);
            _txt_reserved2.DataBindings.Add("Text", _data, "Reserved2", true, DataSourceUpdateMode.OnPropertyChanged);
            _txt_checksum.DataBindings.Add("Text", _data, "Checksum", true, DataSourceUpdateMode.OnPropertyChanged);

            _txt_fill.DataBindings.Add("Text", _data, "Fill", true, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
