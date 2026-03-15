# EDIFファイルフォーマット

EDIF ファイルは、回路図の交換フォーマットである。  
EDIF のバージョンは、`2.0.0`や`3.0.0`があるが、各バージョン間の互換性は高くない。  
回路図CADは`2.0.0`が一般的であり、`3.0.0`は使われていないのが現状である。  
このため、このドキュメントは、`2.0.0`を対象とする。

## root of edif

最上位要素は、`edif`である。  
`edif`要素は 、`edifVersion` , `edifLevel` , `keywordMap` の属性要素がある。定型パターンであり、他の値は対象外とする。
`status`属性は、重要ではないが、ツールの管理情報を含む。

`edif`要素は、属性要素の他に `library`, `external`, `design` の下位要素を含むことができる。

```lisp
    (edif
        [nameDef]
        (edifVersion 2 0 0)
        (edifLevel 1)
        (keywordMap (keywordLevel 0))
        (status ...)
        (external [nameDef] ...)...     # 外部ライブラリへの参照
        (library [nameDef]              # 内部ライブラリの定義
            (cell [nameDef]             # cell の定義
                (cellType TIE/RIPPER/GENERIC)
                (status ...)
                (viewMap ...)
                (view [nameDef]         # view の定義
                    (viewType MASKLAYOUT/PCBLAYOUT/NETLIST/
                              SCHEMATIC/SYMBOLIC/BEHAVIOR/
                              LOGICMODEL/DOCUMENT/GRAPHIC/
                              STRANGER)
                    (status ...)
                    (interface ...)     # interface の定義
                    (contents ...)      # contents の定義
                    )...
                    (property...)...
                )...
            )...
        )...
        (design [nameDef] 
            (cellRef [nameRef] (libraryRef [nameRef]))
            (status...)...
            (property...)...
        )
    )
```

`library` と `external` は、複数の `cell` をライブラリとしてまとめたものである。
`external` は外部参照する。  
`cell` は部品に相当し、部品の表示方法として複数の `view` を持つことが出来る。  
`view` には `interface` と `contents` の下位要素がある。  
`interface` はインターフェースを定義し、 `contents` は内容を定義する。  
`design` は、edif ファイルのトップデザイン `cell` を参照を指定する。

** その他に comment/userData 要素を各所に入れられる。ここでは無視することとする。

---

## sutatus

`edif`, `external`, `library`, `cell`, `view`, `design` 要素の属性を定義する。

```lisp
    (status
        (written
            (timeStamp 0 0 0 0 0 0)     # 作成日
            (author "")...              # 作成者
            (program ""                 # 生成ツール名
                (version "")?)...       # ツールバージョン
            (dataOrigin ""              # 元データの作成ツール名
                (version "")?)...       # 元データのバージョン
            (property...)...
        )
    )
```

---

## nameDef / nameRef

定義に使用するID名  
`edif`, `external`, `library`, `cell`, `view`, `design` 要素などにID名を定義する。  
参照呼び出しの呼び出し先に使用し、基本的には一意の名前とする。

名前の定義方法にはいくつかパターンがある。

```lisp
    [ident]                         # ID名を定義
    (name [ident] (display ...))    # ID名を定義, 表示方法指定
    (rename                         # ID名を定義, 別名を定義
        [ident]/(name [ident] ...)  # ID名,別名共に表示方法指定可能
        "str"/(strringDisplay ...)
    )
```

ID名は、使用可能文字列に制限がある。このため、回路の名称とは異なる場合がある。  
このため、別名 "str" を付けることができる。
また、ID名および別名はを表示方法を指定できる。

## 参照

`nameRef` を指定して他の階層にあるデータを参照する。

### library 参照

```lisp
    (libraryRef name)
```

### cell 参照

use : `design`

ライブラリ内の `cell` を参照する。

```lisp
    (cellRef name)
    (cellRef name (libraryRef name))
```

### view 参照

use : `instance`, `site`, `viewList`

ライブラリ内の `cell` 内の `view` を参照する。

```lisp
    (viewRef name)
    (viewRef name (cellRef name))
    (viewRef name (cellRef name (libraryRef name)))
```

### figureGroup 参照

use : `includeFigureGroup`, `intersection`, `inverse`,
      `overSize`, `union`, `difference`, `figureGroupObject`

ライブラリ内の `figureGroup` を参照する。

```lisp
    (figureGroupRef name)
    (figureGroupRef name (libraryRef name))
```

### globalPort 参照

use : `joined`

ポートへの参照をする。

```lisp
    (globalPortRef name)
```

### port 参照

use : `portGroup`, `portInstance`, `portList`, `portMap`,
      `steady`, `tableDefault`, `weekJoined`, `change`,
      `entry`, `event`, `follow`, `logicAssign`,
      `loginInput`, `logicOutput`, `maintain`, `match`,
      `mustJoin`, `nonPermut`, `Permutable`, `portBackAnotate`

`view` の `port` への参照をする。  
`view` の `instance` の `port` への参照をする。

```lisp
    (portRef name)
    (portRef name (portRef name (...)))
    (portRef name (viewRef name))
    (portRef name (viewRef name (cellRef name)))
    (portRef name (viewRef name (cellRef name (libraryRef name))))
    (portRef name (instanceRef name))
    (portRef name (instanceRef name (viewRef name)))
    (portRef name (instanceRef name (viewRef name (cellRef name))))
    (portRef name (instanceRef name (viewRef name (cellRef name (libraryRef name)))))
```

### net 参照

use : `netGroup`, `netMap`, `event`, `netBackAnnotate`

`view` の `net` への参照をする。  
`view` の `instance` の `net` への参照をする。

```lisp
    (netRef name)
    (netRef name (netRef name (...)))
    (netRef name (viewRef name))
    (netRef name (viewRef name (cellRef name)))
    (netRef name (viewRef name (cellRef name (libraryRef name))))
    (netRef name (instanceRef name))
    (netRef name (instanceRef name (viewRef name)))
    (netRef name (instanceRef name (viewRef name (cellRef name))))
    (netRef name (instanceRef name (viewRef name (cellRef name (libraryRef name)))))
```

### logic 参照

シミュレーション用参照。

```lisp
    (logicRef name)
    (logicRef name (libraryRef name))
```

---

## design

トップデザインの `cell` への参照を指定する。

```lisp
    (design [nameDef] 
        (cellRef [nameRef]
            (libraryRef [nameRef])))
```

---

## library / external

内部ライブラリ(`library`)や外部ライブラリ(`external`)を定義する。  
`edif` は、複数のライブラリを持つことができ、ライブラリは複数の`cell`を持つことができる。  
`cell` は、複数の `view` を持つことができる。
`technology` でライブラリの共通の基礎情報を定義する。  

```lisp
    (library [nameDef]                  # 内部ライブラリの定義
        (technology ...)                # 設計情報
        (cell [nameDef] ...)...         # cell の定義
    )...
```

## technology

設計情報定義

異なるツールによって生成された複数のライブラリを利用する場合に基準を合わせに使用する。  
通常は同じ設定にするため変換処理は省いて良い。

```lisp
    (technology
        (numberDefinition               # 設値
            (scale                      # スケールの定義
                0/(e 0 0)
                0/(e 0 0)
                (unit ...))...
            (gridMap                    # グリッドの定義
                0/(e 0 0) 
                0/(e 0 0))...
        )
        (figureGroup ...)...            # 図形属性設定
        (fabricate ...)...              # レイヤに対応した図形属性の対応設定
        (simulationInfo ...)...         # シミュレーション情報
        (physicalDesignRule ...)...     # 物理設計ルール情報
    )
```

## scale

```lisp
    (scale 1 (e 254 -6) (unit DISTANCE))
```

例) (e 254 -6) => 254*10^(6-6)=254 cm

単位の種別

```lisp
    (unit DISTANCE/CAPACITANCE/CURRENT/RESISTANCE/TEMPERATURE/
          TIME/VOLTAGE/MASS/FREQUENCY/INDUCTANCE/ENERGY/POWER/
          CHARGE/CONDUCTANCE/FLUX/ANGLE)
```

## gridMap

```lisp
        (gridmap 1 1)
```

## figureGroup

`figureGroup` は、図形の機能種別。  
色(`color`)、コーナー形状(`cornerType`)、端点形状(`endType`)、文字列の高さ(`textHeight`)、
パスの幅(`pathWidth`)、フィルパターン(`fillPattern`)、プロパティ(`property`)の
共通する基準要素属性を纏める。  
`property` には、`ETCFONTNAME` , `TEXTWIDTH` などあるが、ツールに依存する。

図の設定を名前を付けて定義する。

```lisp
    (figureGroup [nameDef]
        (cornerType EXTEND/ROUND/TRUNCATE)      # 角の形状指定 (伸ばす、丸める、切り詰める)
        (endType EXTEND/ROUND/TRUNCATE)         # 始点・終点の形状指定
        (pathWidth 0)                           # 線幅
        (borderWidth 0)                         # 囲み枠の幅
        (color 0 0 0)                           # 色 (値は整数値かスケール値、各項の範囲は0-100?)
        (fillPattern 0 0 (boolean (true)))      # 塗りスタイル
        (borderPattern 0 0 (boolean (true)))    # 囲みの塗りスタイル
        (textHeight 0)                          # 文字列の高さ
        (visible (boolean (true)))              # 表示有無
        (property ...)
        (includeFigureGroup ...)
        ...
    )
```

figureGroup の名前は、ツール依存であるが、主に以下がある。

* PINNUMBER
* PINNAME
: PARTREFERENCE
* PARTVALUE
* DISPLAYPROPERTY
* WIRE
* PARTBODY
* OFFPAGECONNECTORTEXT
* TITLEBLOCK
* PAGEBORDER
* COMMENTTEXT

表示側の要素で `figureGroupOverride` することで、一時的に `figureGroup` の設定を置き換えて使用する。  

---

## cell

`cell` は、複数の `view` を持つことができる。  
`cell` は、 `cellType` でセルの種別を定義する。  

```lisp
    (cell [nameDef]                 # cell の定義
        (cellType TIE/RIPPER/GENERIC)
        (status ...)
        (viewMap ...)
        (view [nameDef] ...)...     # view の定義
        (property...)...
    )...
```

## cellType

* `TIE`     -- ?
* `RIPPER`  -- バスの結線点
* `GENERIC` -- 図形

## viewMap

`view` のマッピング。バックアノテーションなどをサポートする。  
`port` のマッピングとして `portRef`, `portGroup` を使用する。

```lisp
    (viewMap
        (portMap (portRef)... (portGroup [nameRef]/(portRef)...)...)...
        (portBackAnotate (portRef) ...)... 
        (instanceMap (instanceRef)... (instanceGroup)...)... 
        (instanceBackAnotate (instanceRef) ...)... 
        (netMap (netRef)... (netGroup)...)... 
        (netBackAnotate ...)...
    )...
```

## view

`view` は、`interface` や `contents` を持つ。  
`view` は、 `viewType` でセルの種別を定義する。  

```lisp
    (view [nameDef]                             # view の定義
        (viewType SCHEMATIC/GRAPHIC/...)
        (status ...)
        (interface ...)                         # interface の定義
        (contents ...)                          # contents の定義
        (property ...)...
    )...
```

## viewType

基本

* `SCHEMATIC` -- 回路部品図形
* `GRAPHIC` -- 回路図形

そのほか

* `MASKLAYOUT`
* `PCBLAYOUT`
* `NETLIST`
* `SYMBOLIC`
* `BEHAVIOR`
* `LOGICMODEL`
* `DOCUMENT`
* `STRANGER`

---

## interface

`cell` のインターフェースを定義する。

```lisp
    (interface                                  # interface の定義
        (port ...)...
        (portBundle ...)...
        (symbol ...)...
        (protectionFrame ...)...
        (designator ""/(stringDisplay))... 
        (parameter [nameDef] [typedValue] (Uint)?)... 
        (property ...)...

        # others
        (arrayrelatedinfo (baseArray)/(arraySite)(arrayMacro))...
        (joined (portRef)... (portList)... (globalPortRef)...)... 
        (weakJoined (portRef)... (portList)... (joined)...)... 
        (mustJoin (portRef)... (portList)... (weakJoined)... (joined)...)... 
        (permutable ...)... 
        (timing ...)... (simurate ...)... 
    )
```

## contents

`contents` は、`cellType`/`viewType` の組み合わせにより使用する要素が異なる。

* 回路図の場合 ( viewType = `SCHEMATIC` )

```lisp
    (contents                                   # contents の定義
        (offPageConnector ...)...
        (page "" 
            (pageSize (rectangle (pt) (pt)))
            (instance ...)
            (net ...)
            (netBundle ...)
            (commentgraphics ...)
            (portimplementation ...)
            (boundingBox (rectangle (pt) (pt)))...
        )...
    )...
```

* 図形の場合 ( viewType=`GRAPHIC` )

```lisp
    (contents                                   # contents の定義
        (figure ...)...                          # 図形
        (instance ...)...
        (net ...)...
        (netBundle ...)...
        (commentGraphics ...)...
        (portimplementation)...
        (boundingBox (rectangle (pt) (pt)))...
    )...
```

* そのほか

```lisp
    (contents                                   # contents の定義
        (figure...)...                          # 図形
        (section "" (section...)/""/(instance...)...)...
        (net...)...
        (netBundle...)...
        (commentGraphics 
            (annotate...) 
            (figure...) 
            (instance...) 
            (boundingbox...) 
            (property...)
        )...
        (portimplementation)...
        (boundingBox (rectangle (pt) (pt)))...
        (timing ...)... (simulate ...)... (when ...)...
        (follow ...)... (logicPort ...)...
    )...
```

---

## value

値は、boolean値, 整数値, 小数値, 座標, 文字列, 状態がある。
状態はシミュレーション用。

```lisp
    BOOL  = (true)/(false)                                          # Boolean値(true/false)
    INT   = 0                                                       # 整数値
    NUM   = 0/(e 0 0)                                               # 小数値(a*10^(6+b))
    POINT = (pt 0 0)                                                # 座標(x y)
    STR   = ""                                                      # 文字列
    MNM   = 0/(e 0 0)/(mnm 0/(e 0 0)/(undefined)/(unconstrained)    # 状態
                           0/(e 0 0)/(undefined)/(unconstrained)
                           0/(e 0 0)/(undefined)/(unconstrained))
```

## typedValue

型付き値は、boolean, integer, number, point, string, minomax がある。
それぞれの値に型を適用する。minomax はシミュレーション用。

```lisp
    (boolean [BOOL]/(booleanDisplay [BOOL] (display...))/(boolean...) ...)          # Bool値
    (integer [INT]/(intDisplay      [INT] (display...))/(integer...) ...)           # 整数値
    (number  [NUM]/(numberDisplay   [NUM] (display...))/(number...) ...)            # 数値
    (point [POINT]/(pointDisp     [POINT] (display...))/(point...) ...)             # 座標
    (string  [STR]/(stringDisplay   [STR] (display...))/(string...) ...)            # 文字列
    (minoMax [MNM]/(minoMaxDisplay  [MNM] (display...))/(minoMax...) ...)           # 状態
```

---

## 参照表示

```lisp
    (keywordDisplay [ident] (display...))                           # キーワードを参照して表示
    (propertyDisplay [nameRef] (display...))                        # プロパティを参照して表示
    (parameterDisplay [nameRef] (display...))                       # パラメータを参照して表示
```

### keywordDisplay

keyword で定義された項目名が ident の値を表示

* portImplementation の name が port の項目名 ident の値
* protectionFrame (不明)
* symbol (不明)

### propertyDisplay

property で定義された名前が nameRef の値を表示

* portImplementation の property の名前が nameRef の値
* protectionFrame の property の名前が nameRef の値
* symbol の property の名前が nameRef の値

### parameterDisplay

cell/interface/parameter で定義された名前が nameRef の値を表示

* protectionFrame (不明)
* symbol (不明)

---

## display

表示属性を設定

```lisp
    (display
        [nameRef]/(figureGroupoverride ...)
        (justify CENTERCENTER/CENTERLEFT/CENTERRIGHT/               # アンカー位置指定
                 LOWERCENTER/LOWERLEFT/LOWERRIGHT/
                 UPPERCENTER/UPPERLEFT/UPPERRIGHT)
        (orientation R0/R90/R270/MX/MY/MYR90/MXR90)                 # 回転
        (origin (pt 0 0))                                           # 座標
    )
```

---

## figure

図形を定義する。\
figure の nameRef で指定した figureGroup の設定を引き継ぐ。さらに部分的に設定を変更する場合は、 figureGroupOverride で上書きする。

```lisp
    (figure [nameRef]/(FigureGroupoverride ...)
        (circle (pt 0 0) (pt 0 0) (property ...)...)            # 二点間を直径とした円
        (dot (pt 0 0) (property ...)...)                        # 点
        (path (pointList (pt 0 0)...) (property ...)...)        # pointList の各点を順番に直線で接続
        (polygon (pointList (pt 0 0)...) (property ...)...)     # pointList の各点を順番に直線で接続し最後と最初の点も接続
        (rectangle (pt 0 0) (pt 0 0) (property)...)             # 2点を対角とした四角形
        (shape/openShape 
            (curve
                (arc (pt 0 0) (pt 0 0) (pt 0 0))...             # 円弧 (開始点、中間点、終了点)
                (pt 0 0)...                                     # 各点を順番に接続した曲線
            )
            (property ...)...
        )
    )
```

** shape と openShape の差は不明(開始点の終了点を接続するかしないかの違い？)

`figure` は、`figureGroup` の名前で図形を描画する。  
`figureArea` , `figureRepimeter` , `figureWidth`

## figureGroupOverride

figureGroup の属性の一部を上書きした属性を適用対象にする。

```lisp
    (figureGroupOverride
        (figureGroupRef [nameRef] (LibraryRef [nameRef])?)
        (...)           # figureGroup の各項目。ただし includeFigureGroup は除く
        ...
    )
```

---

## commentGraphics

コメント図形を定義する。

```lisp
    (commentGraphics
        (figure ...)...
        (instance ...)...
        (annotate ....)...
        (boundingBox ...)
        (property ...)...
    )
```

## symbol

シンボルの図

```lisp
    (symbol
        (portimplementation)
        (figure)
        (instance)
        (commentGraphics)
        (annotate)
        (pageSize)
        (boundingBox)
        (propertyDisplay)
        (KeywordDisplay)
        (ParameterDisplay)
        (Property)
    )
```

nameRef:

```lisp
    [Ident]
    (Name [Ident]
        (Display
            [FigGrpNameRef]/(FigureGroupride
                (FigureGroupRef [FigGrpNameRef] (LibraryRef [name])?)
                (CornerType)
                (EndType)
                (PathWidth)
                (BorderWidth)
                (Color)
                (FillPattern)
                (BorderPat)
                (TextHeight)
                (Visible)
                (Property)
            )
            (justify 
                CENTERCENTER/CENTERLEFT/CENTERRIGHT/
                LOWERCENTER/LOWERLEFT/LOWERRIGHT/
                UPPERCENTER/UPPERLEFT/UPPERRIGHT
            )
            (orientation R0/R90/R270/MX/MY/MYR90/MXR90)
            (Origin (pt 0 0))
        )
    )

    (viewRef [nameRef] (CellRef [nameRef] (LibraryRef [nameRef])?)?)
```

---

## instance

```lisp
    (instance [nameDef]
        (viewRef [nameRef] (cellRef [nameRef] (libraryRef [nameRef])))
        (viewList (viewRef..)... (viewList...)...)
        (Transform)...
        (parameterAssign)...
        (PortInstance)...
        (timing)...
        (designator)...
        (property)...
    )
```

```lisp
    (portInstance [nameRef]/(portRef)
        (unused...)
        (portDelay...)
        (designator...)
        (dcFanInLoad...)
        (dcFanOutLoad...)
        (dcMaxFanIn...)
        (dcMaxFanOut...)
        (acLoad...)
        (property...)
    )
```

---

## net / netBundle

use: `page`, `contents`

ネットを定義する。

```lisp
    (net [nameDef]              #ネット名
        (joined)
        (criticality)...
        (netDelay)...
        (net)...
        (instance)...
        (figure)...
        (commonGraphics)...
        (property)...
    )

    (netBundle [nameDef]        #ネットバンドル名
        (listOfNets (net)...)
        (figure)...
        (commentGraphics)...
        (property)...
    )
```

## joined

use: `interface`, `net`

ポート間のつながりを指定する。

```lisp
    (joined
        (portRef [NameDef] (instanceRef [NameDef]))...
        (portList)...
        (globalPortRef)...
    )... 

    (weakJoined (portRef)... (portList)... (joined)...)
    (mustJoin (portRef)... (portList)... (weakJoined)... (joined)...)
```

## port

ポートの定義

```lisp
    (port ""
        (direction INOUT/INPUT/OUTPUT)
        (unused ...)
        (portDelay ...)
        (designator ""/(strDisplay))
        (DcFanInLoad) (DcFanOutLoad) (DcMaxFanIn) (DcMaxFanOut) (AcLoad)
        (Property ...)...
    )
```

```lisp
    (portBundle ""
        (listOfPorts (port ...)... (portBundle ...)...)
        (Property ...)...
    )
```

ポートの実装

```lisp
    (portImplementation
        (Name)/[Ident]
        (figure)
        (connectLocation)
        (instance)
        (commentGraphics)
        (propertyDisplay)
        (KeywordDisplay)
        (Property)
    )
```

---

```lisp
    (protectionFrame
        (portImplementation)...
        (figure)
        (instance)
        (commentGraphics)
        (boundingBox)
        (PropDisp) 
        (KeywordDisplay) 
        (parameterDisplay [nameRef] (display)) 
        (Property)
    )
```

```lisp
    (instanceRef "" (InstanceRef)? (viewRef)?)
    (netRef "" (netRef)? (instanceRef)? (viewRef)?)
    (portRef "" (portRef)? (instanceRef)? (viewRef)?)
    (site (viewRef) (trasform)?)
    (viewList (viewRef)... (viewList)...)

    (figureGroupRef "" (LibraryRef "")?)

    (globalportRef)
```

---

## property

カスタムの属性を定義する。

```lisp
    (Property [nameDef]
        (TypedValue ...)
        (Owner "")?
        (Unit 
            DISTANCE/CAPACITANCE/CURRENT/RESISTANCE/TEMPERATURE/
            TIME/VOLTAGE/MASS/FREQUENCY/INDUCTANCE/ENERGY/POWER/
            CHARGE/CONDUCTANCE/FLUX/ANGLE
        )?
        (Property ...)?
    )
```

## comment

いろんなところで定義できる。使わないので常に無視。

```lisp
    (comment "")
```

## userData

いろんなところで定義できる。使わないので常に無視。

```lisp
    (userData [ident]
        [Int]/[Str]/[Ident]/(Keyword [Int]/[Str]/[Ident]/(Keyword ...))
    )
```
