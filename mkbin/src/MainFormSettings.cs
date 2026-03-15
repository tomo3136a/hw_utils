using System;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;

namespace mkbin
{
    [SettingsGroupName("mainform.settings")]
    public class MainFormSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("0, 0")]
        public Point FormLocation
        {
            get { return (Point)(this["FormLocation"]); }
            set { this["FormLocation"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("640, 480")]
        public Size FormSize
        {
            get { return (Size)this["FormSize"]; }
            set { this["FormSize"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("1.0")]
        public float TextEm
        {
            get { return (float)this["TextEm"]; }
            set { this["TextEm"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Meiryo UI, 8.25pt")]
        public Font FormFont
        {
            get { return (Font)this["FormFont"]; }
            set { this["FormFont"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("False")]
        public bool MenuVisible
        {
            get { return (bool)this["MenuVisible"]; }
            set { this["MenuVisible"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("False")]
        public bool DataVisible
        {
            get { return (bool)this["DataVisible"]; }
            set { this["DataVisible"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("None")]
        public TableLayoutPanelCellBorderStyle CellBorderStyle
        {
            get { return (TableLayoutPanelCellBorderStyle)this["CellBorderStyle"]; }
            set { this["CellBorderStyle"] = value; }
        }

        public static string ConfigurationPath()
        {
            var lv = ConfigurationUserLevel.PerUserRoamingAndLocal;
            var cf = ConfigurationManager.OpenExeConfiguration(lv);
            return cf.FilePath;
        }
    }
}
