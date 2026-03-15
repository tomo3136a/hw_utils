using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;

namespace mkbin
{
    public partial class Data : DataBase
    {
        public bool ValidateVesrion()
        {
            return Regex.Match(Version, "\\d[.]\\d+").Success;
        }
    }
}
