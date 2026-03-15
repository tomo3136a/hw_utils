# XFDFファイルフォーマット(fields)

`<fields>` にフォームデータを配置する。

```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <xfdf xmlns="http://ns.adobe.com/xfdf/" xml:space="preserve">
        <fields>
            ...
        </fields>
        <f href="xxx.pdf"/>
        <ids original="D44B3069C43280202DC5423980241179" 
            modified="B45F09C7D1006DEBF9E6EC5B88B0E834"/>
    </xfdf>
```

フォームデータには、単純フォームデータと階層構造フォームデータの二種類がある。

* 単純フォームデータ        形式： {名前}={値}
* 階層構造フォームデータ    形式： {グループ名}.{名前}={値}

---

## 単純フォームデータ

フォームデータは、`<field ...>`要素の`name`属性をキーとして`<value>`要素に値を指定する。  
フォームデータは複数のフィールドを持つことが出来る。

  例：  
    Name=Adobe Systems, Inc.  
    Street=345 Park Ave.

```xml
    <fields>
        <field name=”Name”>
            <value>Adobe Systems, Inc.</value>
        </field>
        <field name=”Street”>
            <value>345 Park Ave.</value>
        </field>
        ...
    </fields>
```

---

## 階層構造

`<field ...>`要素は入れ子にすることで階層構造化し、グルーピングできる。  

  例：  
    Address.Name=Adobe Systems, Inc.  
    Address.Street=345 Park Ave.

```xml
    <fields>
        <field name=”Address”>
            <field name=”Name”>
                <value>Adobe Systems, Inc.</value>
            </field>
            <field name=”Street”>
                <value>345 Park Ave.</value>
            </field>
            ...
        </field>
    </fields>
```

---

## フィールドの値

* `<value>` 要素
* `<value-ritchtext>` 要素

フィールドの値として `<value>` 要素を使用する場合は、単純なテキストデータとして扱う。  
`<value>` 要素の代わりに `<value-ritchtext>` 要素を使用した場合、リッチテキストを使用できる。  
リッチテキストは xhtml のサブセットで指定し、フォントの指定などテキストの表現を指定できる。  
ただし、リッチテキストとして扱うか単純なテキストデータとして扱うかは、値を扱う環境による。

  例：  xhtml 形式の文章( 
            <span style="font-size:10.0pt"><i>リッチ</i>テキスト</span>
        )の場合

```xml
    <value-richtext>
        <body xmlns="http://www.w3.org/1999/xhtml"
            xmlns:xfa="http://www.xfa.org/schema/xfa-data/1.0/" 
            xfa:APIVersion="Acrobat:21.7.0" 
            xfa:spec="2.0.2">
            <p>
                <span style="font-size:10.0pt"><i>リッチ</i>テキスト</span>
            </p>
        </body>
    </value-richtext>
```

xml 名前空間は、`http://www.w3.org/1999/xhtml` を指定し、xhtml のサブセットを使用することが出来る。  
xml 名前空間は、`http://www.xfa.org/schema/xfa-data/1.0/` を指定し、 `xfa:APIVersion` と `xfa:spec` を使用することが出来る。  
xml 名前空間、xml 名前空間、および`xfa:APIVersion` と `xfa:spec` の値との組み合わせは定型である。この定義以外の場合は、意図した表現が出来ない可能性がある。
