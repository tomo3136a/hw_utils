# XFDFファイルフォーマット(テキスト)

`<annots>` 内にテキスト要素を配置する。

```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <xfdf xmlns="http://ns.adobe.com/xfdf/" xml:space="preserve">
        <annots>
          <text .../>           <!-- ノート注釈 -->
          <freetext .../>       <!-- テキストボックス -->
            ...
        </annots>
        ...
    </xfdf>
```

テキスト要素には次の種類がある。

* ノート注釈 `text`
* テキスト注釈・テキストボックス・引き出し線付きテキストボックス `freetext`

---

## ノート注釈

ノート注釈は、アイコンを配置し、アイコンを選択することによりテキストを表示する。  
ノート注釈は、 `<text ...>` 要素を使用する。

```xml
    <text 
        color="#FFD100" 
        icon="Comment" 
        page="0" 
        rect="100.000000,76.000000,124.000000,100.000000" >
        <contents-richtext>
            <body xmlns="http://www.w3.org/1999/xhtml"
                xmlns:xfa="http://www.xfa.org/schema/xfa-data/1.0/" 
                xfa:APIVersion="Acrobat:21.7.0" xfa:spec="2.0.2">
                <p>ノート</p>
            </body>
        </contents-richtext>
    </text>
```

* `icon` アイコンを指定する
  * 省略時 ノート
  * `"Check"` チェックマーク
  * `icon="Checkmark"` テキストを挿入
  * `icon="Circle"` 円形
  * `icon="Comment"` コメント
  * `icon="Cross"` 十字型
  * `icon="CrossHairs"` 十字アイコン
  * `icon="Help"` ヘルプ
  * `icon="Insert"` テキストを挿入
  * `icon="Key"` キー
  * `icon="NewParagraph"` 新規段落
  * `icon="Paragraph"` 段落
  * `icon="RightArrow"` 右向き矢印
  * `icon="RightPointer"` 右向きポインター
  * `icon="Star"` 星形
  * `icon="UpArrow"` 上向き矢印
  * `icon="UpLeftArrow"` 左上向き矢印
* `rect` アイコンの表示領域を指定する
* 選択し表示するテキスト文字列は `<contents-richtext>` または `<contents>` に指定する

---

## テキスト注釈・テキストボックス

自由に配置できるテキストには、 `<freetext ...>` 要素を使用する。  
通常はテキストボックス(枠で囲んだテキスト)であるが、 `width="0.0"` を指定することにより枠のないテキスト注釈となる。

```xml
    <freetext 
        width="0.000000" 
        IT="FreeTextTypewriter" 
        page="0" 
        rect="128.010803,646.210144,176.020813,662.470154" >
        <contents-richtext>
            <body xmlns="http://www.w3.org/1999/xhtml"
                xmlns:xfa="http://www.xfa.org/schema/xfa-data/1.0/" 
                xfa:APIVersion="Acrobat:21.7.0" xfa:spec="2.0.2"
                style="font-size:12.0pt;text-align:left;color:#000000;
                    font-weight:normal;font-style:normal;
                    font-family:KozMinPr6N-Regular;font-stretch:normal">
                <p>テキスト</p>
            </body>
        </contents-richtext>
        <defaultappearance>16.25 TL /MSGothic 12 Tf</defaultappearance>
        <defaultstyle>
            font: KozMinPr6N-Regular 12.0pt;font-stretch:Normal;
            text-align:left; color:#000000 
        </defaultstyle>
    </freetext>
```

* `width` 枠の幅(単位はポイント) ※省略時は1ポイント幅
* `IT` 種別表示を変更する
  * `IT="FreeTextTypewriter"` タイプライタ
* `rect` テキストの表示領域を指定する
* テキスト文字列は `<contents-richtext>` または `<contents>` に指定する
* `defaultappearance` 基準のフォントを指定する
* `defaultstyle` 基準のスタイルを指定する

(フォントの指定が複数あり冗長だが、必要なのかは不明)

---

## テキスト文字列

テキスト文字列は、`<contents ...>` または `<contents-richtext ...>` で指定する。

`<contents>`要素を使用する場合は、単純なテキストデータとして扱う。

`<contents-ritchtext>`要素を使用した場合、リッチテキストを使用できる。  
リッチテキストは xhtml のサブセットで指定し、フォントの指定などテキストの表現を指定できる。  
ただし、リッチテキストとして扱うか単純なテキストデータとして扱うかは、値を扱う環境による。

```xml
    <contents-richtext>
        <body xmlns="http://www.w3.org/1999/xhtml"
            xmlns:xfa="http://www.xfa.org/schema/xfa-data/1.0/" 
            xfa:APIVersion="Acrobat:21.7.0" 
            xfa:spec="2.0.2" 
            style="font-size:12.0pt;text-align:left;color:#000000;
            font-weight:normal;font-style:normal;
            font-family:KozMinPr6N-Regular;
            font-stretch:normal">
            <p dir="ltr">
                <span style="line-height:16.2pt;
                    font-family:'Kozuka Mincho Pr6N R'">テキスト</span>
            </p>
        </body>
    </contents-richtext>
```

xml 名前空間は、`http://www.w3.org/1999/xhtml` を指定し、xhtml のサブセットを使用することが出来る。  
xml 名前空間は、`http://www.xfa.org/schema/xfa-data/1.0/` を指定し、 `xfa:APIVersion` と `xfa:spec` を使用することが出来る。  
xml 名前空間、xml 名前空間、および`xfa:APIVersion` と `xfa:spec` の値との組み合わせは定型である。この定義以外の場合は、意図した表現が出来ない可能性がある。
