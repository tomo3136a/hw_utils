# SVGファイルフォーマット

## 概要

SVG ファイルは、 ベクタ画像の XML ファイルである。

仕様は次を参照：  
https://triple-underscore.github.io/SVG11/index.html

## 拡張子

`.svg`

## example

```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <svg style="background: #444" viewBox="120,30,1290,860" 
        version="1.1" 
        xmlns="http://www.w3.org/2000/svg"
        xmlns:xlink="http://www.w3.org/1999/xlink">
        <g>
            <rect fill="#fff" x="130" y="40" width="1270" height="840" />
            <g stroke="black" stroke-width="1" fill="none">
                <text fill="rgb(0,0,0)" stroke-width="0" 
                    font-weight="bold" font-size="7" font-family="Arial" 
                    dominant-baseline="central" text-anchor="start" 
                    transform="translate(1368,789)">0</text>
                <circle r="1" fill="#0" stroke="green" stroke-width="1"
                    cx="1368" cy="789" />
            </g>
            <path fill="none" d="M 50,-110 50,120" />
        </g>
    </svg>
```

## ルート

```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <svg version="1.1"
        xmlns="http://www.w3.org/2000/svg"
        xmlns:xlink="http://www.w3.org/1999/xlink">
        ...
    </svg>
```

* `<?xml version="1.0" encoding="UTF-8"?>` はあってもなくてもいい。
* rootは、`<svg xmlns="http://www.w3.org/2000/svg" version="1.1"/>`とする
* `xlink`も使うので`xmlns:xlink="http://www.w3.org/1999/xlink"`も宣言する。

## 座標

座標は、左上を原点として第二象限配置する。

単位はポイント(pt) 96dpi, 1inch=25.4mm, 1inch=96pt  
⇒ pt(x)=x*96/25.4

96dpi = 72dpi * 4/3

## 図形

図形はいろいろある

* 直線 `<line x1= y1= x2= y2= .../>`
  * rx=,ry= 角丸
* 折れ線 `<polyline points= .../>`
* 四角 `<rect x= y= width= height= .../>`
* 多角形 `<polygon points= .../>`
* 円 `<circle cx= cy= r= .../>`
* 楕円 `<ellipse cx= cy= rx= ry= .../>`
* パス `<path d= .../>`
* テキスト `<text x= y= ...>...</text>`
  * 字体 `font-family=`
  * ウェイト `font-weight=`
  * サイズ `font-size=`
  * スタイル `font-style=`
  * 装飾 `text-decoration=`
  * 寄せ位置 `text-anchor=`
  * 文字間隔 `letter-spacing=`
  * 単語間隔 `word-spacing=`
* イメージ `<image x= y= width= height= xlink:href= .../>`
  * アスペクト比 `preserveAspectRatio`

## 線の属性

* 線色 `stroke=`
  * `none` 描画なし
* 線幅 `stroke-width=`
* 線端形状 `stroke-linecap=`
  * デフォルト `butt`
  * 丸 `round`
  * 四角 `square`
* 線接続部形状 `stroke-linejoin=`
  * 尖がり `miter`
  * 丸 `round`
  * 面取り `bevel`
* 線接続部形状閾値 `stroke-miterlimit=`
* 破線 `stroke-dasharray=`
* 透明度 `stroke-opacity=`

## マーカー

`<marker id=>...</marker>`

* 始点 `<line marker-start=>...</line>`
* 終点 `<line marker-end=>...</line>`
* 中間の頂点 `<line marker-mid=>...</line>`

## 面の属性

* 塗り色 `fill=`
  * 塗りなし `none`
* 塗り透明度 `opacity=`

## パターン・グラディェーション

## テキスト

空白文字の扱い

そのままあつかう `xml:space="preserve"`

## テキストの軌跡

```xml
  <text>
    <textPath xlink:href="#...">
      ...
    </textPath>
  </text>
```

## テキストのリンク無効

`<text pointer-events="none">...</text>`

## グループ化

`<g>`でグループ化して属性をまとめて定義する  
`<use>`で図形を使用する

```xml
  <g id="aaa">...</g>

  <use x=100 y=200 xlink:href="#aaa" />
```

## シンボル

```xml
  <symbol id="aaa" ...>
    ...
  </symbol>
```

* 表示範囲 `viewbox=`
* アスペクト比 `preserveAspectRatio`

## 定義

非表示の図形をまとめて定義する

`<defs>...</defs>`

## 図形変形

* 図形変形 `transform=`
  * 並行移動 `translate(tx,ty)`
  * 拡大縮小 `scale(sx,sy)`
  * 回転 `rotate(angle,cx,cy)`
  * 横へせん断 `skewX(angle)`
  * 縦へせん断 `skewY(angle)`
  * アファイン変換 `matrix(a,b,c,d,e,f)`
