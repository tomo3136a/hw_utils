# XFDFファイルフォーマット(テキストの校正)

校正は、基本的には 「取り消し」 「挿入」 「置換」 を明示する。

* 「取り消し」 は取り消し線を使用する。
* 「挿入」 はキャレットにテキストを入れる。
* 「置換」 は、 「取り消し」 と 「挿入」 を並べたものになる。

`<annots>` の子要素としてテキスト校正の要素を配置する。

```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <xfdf xmlns="http://ns.adobe.com/xfdf/" xml:space="preserve">
        <annots>
          <highlight .../>    <!-- ハイライト -->
          <underline .../>    <!-- 下線 -->
          <squiggly .../>     <!-- 波下線 -->
          <strikeout .../>    <!-- 取り消し線 -->
          <caret .../>        <!-- 挿入テキスト -->
          ...
        </annots>
        ...
    </xfdf>
```

テキスト校正の要素は、座標で囲んだ範囲内のテキストを対象に、各種修飾を行う。

テキスト校正の要素には次の種類がある。

* テキストに修飾
  * ハイライト `highlight`
  * 下線 `underline`
  * 波下線 `squiggly`
  * 取り消し線 `strikeout`
* 文字間に修飾
  * 挿入テキスト `caret`

---

## テキストに修飾

テキスト文字列に対して文字修飾( ハイライト, 下線, 取り消し線 )する。

```xml
    <highlight 
      color="#FFD100" 
      opacity="0.399994" 
      page="0" 
      coords="80.000000,589.023804,173.543976,589.023804,
              80.000000,558.774170,173.543976,558.774170" 
      rect="71.924774,557.828857,181.619202,589.969116" >
    </highlight>
```

* ハイライトは、`<highlight ...>` 要素で指定する。
* 下線は、`<underline ...>` 要素で指定する。
* 波下線は、`<squiggly ...>` 要素で指定する。
* 取り消し線は、`<strikeout ...>` 要素で指定する。

`coords` 属性で指定する範囲にテキストがあるとものとして修飾する。  
  値は "x1,y1,x2,y2,x3,y3,x4,y4" と座標をカンマ区切りで表す。  
  (x1,y1)-(x2,y2)の直線と(x3,y3)-(x4,y4)の直線の間を1つのテキスト範囲とする。  
  続けて複数指定することにより複数行を対象に出来る

`rect` 属性で全体の領域を指定する。

`IT` 属性で種別表示を変更する。
種別表示のアイコンが変わるだけで、何かを判断しているわけではない。

* `IT="HighlightNote"` ハイライトノート
* `IT="StrikeOutTextEdit"` 置換テキストの取り消し線

---

## 文字間に修飾

テキストの文字間に注釈を付ける。

```xml
    <caret 
      color="#0000FF" 
      page="0" 
      fringe="0.810425,0.810425,0.810425,0.810425" 
      rect="144.145630,486.358154,156.081207,496.083344" >
      <contents-richtext>
        <body xmlns="http://www.w3.org/1999/xhtml"
              xmlns:xfa="http://www.xfa.org/schema/xfa-data/1.0/"
              xfa:APIVersion="Acrobat:21.7.0" xfa:spec="2.0.2">
          <p dir="ltr">
            <span dir="ltr" style="font-size:10.5pt;text-align:left;
            color:#000000;font-weight:normal;
            font-style:normal">テキスト挿入</span>
          </p>
        </body>
      </contents-richtext>
    </caret>
```

挿入テキストは、`<caret ...>` 要素で指定する。

`IT` 属性で種別表示を変更する。
種別表示のアイコンが変わるだけで、何かを判断しているわけではない。

* `IT="Replace"` 置換テキスト

`fringe` 属性により、修飾の範囲を指定する

`content-richtext` 要素に挿入文字列を追加する。

---

## テキストの置換

テキスト置換は、対象のテキストを取り消し `<strikout>` 、テキストを挿入 `<caret>` することで対応する。

```xml
    <strikeout 
      ...
      inreplyto="f68646b2-c171-4022-a033-f5731ed590a8" 
      IT="StrikeOutTextEdit" 
      replyType="group" 
      ... >
    </strikeout>
    <caret 
      ...
      IT="Replace" 
      ... >
      <contents-richtext>
        ...
      </contents-richtext>
    </caret>
```
