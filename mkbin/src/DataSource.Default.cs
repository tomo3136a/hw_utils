using System;
using System.Collections.Generic;

namespace mkbin
{
    public partial class DataSource
    {
        public void Build(string mode = "")
        {
            Packages = new string[] { "Package A", "Package B" };

            DataTypes = new DataType[] {
                new DataType { Name = "AAA", Keyword = "AAA_TYPE", ExtentionName="rbf" },
                new DataType { Name = "BBB", Keyword = "BBB_TYPE", ExtentionName="rpd" },
                new DataType { Name = "CCC", Keyword = "CCC_TYPE" },
                new DataType { Name = "DDD", Keyword = "DDD_TYPE" }
            };

            Shortcuts.AddRange(new Data[] {
                new Data {
                    Name = "A base",
                    PackageName ="Package A", DataTypeName = "AAA",
                    SourceFile = "test.bin"
                },
                new Data {
                    Name = "B base",
                    PackageName ="Package B", DataTypeName = "BBB",
                    SourceFile = "test2.rpd",
                    OutputFile = "test2.bin",
                    Address = ""
                },
                new Data {
                    //未登録パケージの場合
                    Name = "C base",
                    PackageName = "Package C", DataTypeName = "CCC",
                    SourceFile = "test_test.rpd",
                    OutputFile = "test2.bin",
                    Address = ""
                }
            });

            FileTypes = new FileType[] {
                new FileType {Name="bin", Display = "バイナリファイル", Extentions="*.bin"},
                new FileType {Name="rbf", Display = "ローバイナリファイル", Extentions="*.rbf"},
                new FileType {Name="rpd", Display = "ロープログラムデータファイル", Extentions="*.rpd"},
                new FileType {Name="mot", Display = "Sフォーマットファイル", Extentions="*.mot;*.srec"},
                new FileType {Name="hex", Display = "HEXファイル", Extentions="*.ihex;*.hexout;*.hex"},
                new FileType {Name="map", Display = "マップファイル", Extentions="*.map"},
                new FileType {Name="txt", Display = "テキストファイル", Extentions="*.txt"},
            };

            InFilters = new string[] { "bin", "rbf", "rpd", "mot", "hex" };
            MapFilters = new string[] { "map", "txt" };
            OutFilters = new string[] { "bin" };
        }
    }
}
