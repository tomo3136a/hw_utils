using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace mkbin
{
    /// <summary>
    /// ユーティティクラス
    /// </summary>
    public class Util
    {
        /// <summary>
        /// HEX文字列から数値に変換
        /// </summary>
        /// <param name="s">HEX文字列</param>
        /// <returns>変換後の数値</returns>
        public static long HextToLong(string s)
        {
            var v = 0L;
            foreach (var c in s)
            {
                var a = 0;
                if ('0' <= c && c <= '9') a = c - '0';
                else if ('A' <= c && c <= 'F') a = c - 'A' + 10;
                else if ('a' <= c && c <= 'f') a = c - 'a' + 10;
                else continue;
                v = (v << 4) + a;
            }
            return v;
        }

        /// <summary>
        /// 数値からHEX文字列に変換
        /// </summary>
        /// <param name="v">数値</param>
        /// <param name="sz">HEX文字列の文字数</param>
        /// <returns>HEX文字列</returns>
        public static string LongToHex(long v, int sz = 8)
        {
            var cm = "0123456789ABCDEF";
            var s = "";
            for (var i = 0; i < sz; i++)
            {
                var idx = v % 16;
                var a = cm[(int)idx];
                v >>= 4;
                s = a + s;
            }
            return s;
        }

        /// <summary>
        /// 数値の下位8bitを16進数および10進数表示
        /// </summary>
        /// <param name="v">数値</param>
        /// <returns>表示文字列</returns>
        public static string HexDisp8(long v)
        {
            v &= 0x0FF;
            return string.Format("{0:X2} ({1:N0})", v, v);
        }

        /// <summary>
        /// 数値の下位16bitを16進数および10進数表示
        /// </summary>
        /// <param name="v">数値</param>
        /// <returns>表示文字列</returns>
        public static string HexDisp16(long v)
        {
            v &= 0x0FFFF;
            return string.Format("{0:X4} ({1:N0})", v, v);
        }

        /// <summary>
        /// 数値の下位8bitを32進数および10進数表示
        /// </summary>
        /// <param name="v">数値</param>
        /// <returns>表示文字列</returns>
        public static string HexDisp32(long v)
        {
            v &= 0x0FFFFFFFFL;
            return string.Format("{0:X8} ({1:N0})", v, v);
        }

        /// <summary>
        /// 絶対パスを相対パスに変換
        /// </summary>
        /// <param name="s">パス</param>
        /// <param name="d">基準ディレクトリ</param>
        /// <param name="n">許容する親ディレクトリ階層数</param>
        /// <returns></returns>
        public static string RelatedPath(string s, string d, int n = 0)
        {
            var ss1 = s.Split('\\');
            var ss2 = d.Split('\\');
            var cnt1 = ss1.Length;
            var cnt2 = ss2.Length;
            var cnt = (cnt1 < cnt2) ? cnt1 : cnt2;

            var i = 0;
            for (i = 0; i < cnt; i++)
                if (ss1[i] != ss2[i]) break;
            if (i <= 0) return s;
            if (i + n < cnt2) return s;

            s = "";
            for (var j = i; j < cnt2; j++)
                s += "..\\";
            for (var j = i; j < cnt1 - 1; j++)
                s += ss1[j] + "\\";
            s += ss1[cnt1 - 1];
            return s;
        }

        /// <summary>
        /// 環境変数を含むパスに環境変数の値を適用
        /// </summary>
        /// <param name="p">パス</param>
        /// <returns>環境変数適用後のパス</returns>
        public static string ConvertFromEnv(string p)
        {
            var col = Environment.GetEnvironmentVariables();
            foreach (var nm in col.Keys)
            {
                var k = "%" + nm + "%";
                var v = col[nm].ToString();
                if (v == "") continue;
                p = p.Replace(k, v);
            }
            return p;
        }

        /// <summary>
        /// 絶対パスを環境変数を含むパスに変換
        /// </summary>
        /// <param name="p">絶対パス</param>
        /// <returns>環境変数を含むパス</returns>
        public static string ConvertToEnv(string p)
        {
            var col = Environment.GetEnvironmentVariables();
            var s = p;
            foreach (var nm in col.Keys)
            {
                var k = "%" + nm + "%";
                var v = col[nm].ToString();
                if (v == "") continue;
                if (v.IndexOf('\\') < 0) continue;
                var s1 = p.Replace(v, k);
                if (s1.Length < s.Length) s = s1;
            }
            return s;
        }

        public static string FullPath(string p)
        {
            return ConvertToEnv(p);
        }
    }
}
