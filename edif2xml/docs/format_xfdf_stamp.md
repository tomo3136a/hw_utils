# XFDFファイルフォーマット(スタンプ)

`<annots>` 内にスタンプ要素を配置する。

```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <xfdf xmlns="http://ns.adobe.com/xfdf/" xml:space="preserve">
        <annots>
            <stamp .../>
            ...
        </annots>
        ...
    </xfdf>
```

スタンプは、 `<stamp ...>` 要素を使用する。スタンプの図は、アイコンの指定により変更する。

```xml
    <stamp
        color=”#FF0000”
        icon=”SBApproved” 
        page=”0” 
        rect=”54.987381,671.039063,216.486893,718.539551” >
    </stamp>
```

* `icon` にアイコンの名前
  * `SBApproved` 承認ボタン
  * ... いろいろ
* `color` スタンプの色を指定
* `page` , `rect` スタンプの配置領域の座標を指定

---
