# XFDFファイルフォーマット(埋め込みオブジェクト)

`<annots>` の子要素としてオブジェクト要素を配置する。

```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <xfdf xmlns="http://ns.adobe.com/xfdf/" xml:space="preserve">
        <annots>
          <fileattachment .../> <!-- 添付ファイル -->
          <sound .../>          <!-- サウンドデータ -->
            ...
        </annots>
        ...
    </xfdf>
```

オブジェクト要素には次の種類がある。

* 添付ファイル `<fileattachment>`
* サウンドデータ `<sound>`

---

## 添付ファイル

添付ファイルは、ファイルアイコンを配置し、アイコンを選択すると添付ファイルを取得できる。  
添付ファイルは、 `<fileattachment ...>` 要素を使用する。

```xml
    <fileattachment 
        color="#4055FF" 
        checksum="B7D08EB508BF8F1361DC8E72345AD4A6" 
        modification="D:20221002085649+09'00'" 
        size="605" 
        mimetype="text/plain" 
        file="list.txt" 
        name="45f67f6d-ab43-4ed9-804d-fdf3182fdbaf" 
        icon="Paperclip" 
        page="0" 
        rect="115.277695,679.661560,122.277695,696.661560" >
        <contents-richtext>
            <body xmlns="http://www.w3.org/1999/xhtml" 
                xmlns:xfa="http://www.xfa.org/schema/xfa-data/1.0/" 
                xfa:APIVersion="Acrobat:22.2.0" xfa:spec="2.0.2">
                <p>list.txt</p>
            </body>
        </contents-richtext>
        <data MODE="raw" encoding="hex" length="226" filter="FlateDecode">
            488974913D0EC2300C85E756EA1D72800A25717F60AED811
            034BD5898BD06660AA5409A9576163421C8289A3D0E716D2...
            02BFBEB4EF5CCAA12AF747256528BAA8C22E6864244C6D0E...
            ...
        </data>
    </fileattachment>
```

* `icon` アイコンを指定する
  * 省略時 添付
  * `icon="Paperclip"` クリップ
  * `icon="Graph"` グラフ
  * `icon=""` 添付
  * `icon="Tag"` タグ
* `rect` アイコンの表示領域を指定する
* `checksum` データのチェックサム
* `size` データのサイズ
* `mimetype` MIME-Type を指定
* `file` ファイル名
* データは `<data>` 要素に指定する。
* 注釈テキストは `<contents-richtext>` 要素、または `<contents>` 要素に指定する

---

## サウンドデータ

サウンドデータ、アイコンを配置し、アイコンを選択するとサウンドを再生できる。  
サウンドデータは、 `<sound ...>` 要素を使用する。

```xml
    <sound 
        color="#4055FF" 
        name="61e9cfdf-b7c6-4bc9-abe5-fa4befd40286" 
        page="0" 
        rect="138.502136,682.083862,158.502136,697.083862" 
        bits="16" 
        encoding="signed" 
        rate="8192" >
        <contents-richtext>
            <body xmlns="http://www.w3.org/1999/xhtml" 
                xmlns:xfa="http://www.xfa.org/schema/xfa-data/1.0/" 
                xfa:APIVersion="Acrobat:22.2.0" xfa:spec="2.0.2">
                <p>サウンドクリップ (62 KB)</p>
            </body>
        </contents-richtext>
        <data MODE="raw" encoding="hex" length="64436" filter="FlateDecode">
            48890C942F70E20A02C60384BF0934B440534AFBD26DFA96
            9DE5BDE5EE987B15BD196E068140202210880804A2028140...
            CC3C04338F79CBBDE576DB2D6DB3348514420934402801AE...
            E0B7F5797EA8EA095C9A8FDE38EFE867102C1ACB4AA7C8CE...
            ...
        </data>
    </sound>
```

* `icon` アイコンを指定する
  * 省略時 サウンド
  * `icon=""` サウンド
  * `icon="Mic"` マイク
  * `icon="Ear"` 耳
* `rect` アイコンの表示領域を指定する
* `bits` サンプリングデータのビット数
* `encoding` エンコード方法
* `rate` サンプリングレート
* データは `<data>` 要素に指定する。
* 注釈テキストは `<contents-richtext>` 要素、または `<contents>` 要素に指定する
