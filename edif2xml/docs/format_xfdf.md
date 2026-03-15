# XFDFファイルフォーマット

## 概要

XFDF ファイルは、 FDF ファイルの XML ファイル版である。
フォームデータと注釈を保存する。

## XFDFの仕様

XFDFの形式は、「ISO 19444-1」に準拠する。

※ 使用するPDFビューア、バージョン等により[制約](format_xfdf_limit.md)がある。

## 拡張子

`.xfdf`

## example

```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <xfdf xmlns="http://ns.adobe.com/xfdf/" xml:space="preserve">
        <annots>
            <square width="9.000000" color="#E52237" opacity="0.494995"
                creationdate="D:20211219185715+09'00'" flags="print"
                interior-color="#FFAABF" 
                date="D:20211219191624+09'00'"
                name="f4873cca-4d0f-47fb-8be9-ded59e5dabaa"
                page="0" fringe="4.500000,4.500000,4.500000,4.500000" 
                rect="0.500000,1.500000,611.500000,791.500000" 
                subject="長方形" title="tomo3">
                <popup flags="print,nozoom,norotate" open="no" page="0" 
                    rect="612.000000,673.000000,816.000000,787.000000"/>
            </square>
        </annots>
        <f href="xxx.pdf"/>
        <ids original="D44B3069C43280202DC5423980241179" 
            modified="B45F09C7D1006DEBF9E6EC5B88B0E834"/>
    </xfdf>
```

---

## ルート

```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <xfdf xmlns="http://ns.adobe.com/xfdf/" xml:space="preserve">
        ...
        <f href="xxx.pdf"/>
        <ids original="D44B3069C43280202DC5423980241179" 
            modified="B45F09C7D1006DEBF9E6EC5B88B0E834"/>
    </xfdf>
```

* ルートは、`<xfdf xmlns="http://ns.adobe.com/xfdf/" xml:space="preserve">`とする
* `<f href="...">` はpdfファイルへのリンク。無くてもよい。
* `<ids original="..." modified="...">` はpdfファイルへのリンク情報。無くてもよい。

---

## 子要素

ルート`<xfdf ...>`の子要素としてフォームデータやコメントを配置する。

* `<fields>` [フォームデータ](format_xfdf_fields.md)
* `<annots>` [コメント](format_xfdf_annots.md)
  * [テキストの校正](format_xfdf_proof.md)
  * [テキスト](format_xfdf_text.md)
  * [図形](format_xfdf_figure.md)
  * [スタンプ](format_xfdf_stamp.md)
  * [埋め込みオブジェクト](format_xfdf_embd.md)

---
