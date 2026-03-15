<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns="http://www.w3.org/2000/svg">
    <xsl:import href="common.xsl"/>
    <xsl:output method="xml" media-type="text/xsl" indent="yes"/>
    <xsl:param name="page"/>
    <xsl:param name="style" select="'background: #444'"/>

    <xsl:key name="fgl" match="//figuregroup" use="concat(../../@name,'-',@name)"/>
    <xsl:key name="vwl" match="//view" use="concat(../../@name,'-',../@name,'-',@name)"/>

    <!--root/edif-->
    <xsl:template match="/">
        <xsl:apply-templates select="edif"/>
    </xsl:template>

    <xsl:template match="edif">
        <xsl:variable name="cref" select="design/cellref/@name"/>
        <xsl:variable name="lref" select="design/cellref/libraryref/@name"/>
        <xsl:variable name="cell" select="library[@name=$lref]/cell[@name=$cref]"/>
        <xsl:apply-templates select="$cell/view/contents/page[@name=$page]"/>
    </xsl:template>

    <xsl:template match="page">
        <svg xmlns="http://www.w3.org/2000/svg"
            xmlns:xlink="http://www.w3.org/1999/xlink">
            <xsl:attribute name="style"><xsl:value-of select="$style"/></xsl:attribute>
            <xsl:call-template name="_attr_viewbox"/>
            <g class="sheet" stroke="black" stroke-width="10" fill="none">
                <xsl:call-template name="_sheet"/>
                <xsl:apply-templates select="pagesize"/>
                <xsl:apply-templates select="boundingbox"/>
            </g>
            <g stroke="black" stroke-width="10" fill="none">
                <xsl:attribute name="id"><xsl:value-of select="@name"/></xsl:attribute>
                <xsl:apply-templates select="commentgraphics"/>
                <xsl:apply-templates select="figure"/>
                <xsl:apply-templates select="instance"/>
                <xsl:apply-templates select="portimplementation"/>
                <xsl:apply-templates select="net"/>
            </g>
        </svg>
    </xsl:template>

    <!--view-->
    <xsl:template match="view">
        <xsl:apply-templates select="interface"/>
        <xsl:apply-templates select="contents"/>
        <xsl:apply-templates select="property"/>
    </xsl:template>

    <!--interface-->
    <xsl:template match="interface">
        <xsl:apply-templates select="protectionframe"/>
        <xsl:apply-templates select="symbol"/>
        <xsl:apply-templates select="port"/>
        <xsl:apply-templates select="portbundle"/>

        <xsl:apply-templates select="arrayrelatedinfo"/>
        <xsl:apply-templates select="joined"/>
        <xsl:apply-templates select="weekjoined"/>
        <xsl:apply-templates select="mustjoin"/>
        <!-- <xsl:apply-templates select="permutable"/> -->
        <xsl:apply-templates select="name"/>
        <xsl:apply-templates select="rename"/>
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="parameter"/>
        <xsl:apply-templates select="property"/>
    </xsl:template>

    <xsl:template match="protectionframe">
        <xsl:apply-templates select="boundingbox"/>
        <xsl:apply-templates select="commentgraphics"/>
        <xsl:apply-templates select="figure"/>
        <xsl:apply-templates select="instance"/>
        <xsl:apply-templates select="portimplementation"/>
        <xsl:apply-templates select="parameterdisplay"/>
        <xsl:apply-templates select="propertydisplay"/>
        <xsl:apply-templates select="keyworddisplay"/>
        <xsl:apply-templates select="property"/>
    </xsl:template>

    <!--symbol-->
    <xsl:template match="symbol">
        <xsl:apply-templates select="boundingbox"/>
        <xsl:apply-templates select="commentgraphics"/>
        <xsl:apply-templates select="figure"/>
        <xsl:apply-templates select="instance"/>
        <xsl:apply-templates select="portimplementation"/>
        <xsl:apply-templates select="net"/>
        <xsl:apply-templates select="netbundle"/>
        <xsl:apply-templates select="annotate"/>
        <xsl:apply-templates select="parameterdisplay"/>
        <xsl:apply-templates select="propertydisplay"/>
        <xsl:apply-templates select="keyworddisplay"/>
        <xsl:apply-templates select="property"/>
    </xsl:template>

    <!--port-->
    <xsl:template match="port">
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
    </xsl:template>

    <xsl:template match="portbundle">
        <xsl:apply-templates select="listofports"/>
        <xsl:apply-templates select="property"/>
    </xsl:template>

    <xsl:template match="listofports">
        <xsl:apply-templates select="port"/>
        <xsl:apply-templates select="portbundle"/>
        <xsl:apply-templates select="property"/>
    </xsl:template>

    <!--portimplementation-->
    <xsl:template match="portimplementation">
        <xsl:apply-templates select="figure"/>
        <xsl:apply-templates select="instance"/>
        <xsl:apply-templates select="connectlocation"/>
        <xsl:apply-templates select="name"/>
        <xsl:apply-templates select="rename"/>
        <xsl:apply-templates select="keyworddisplay"/>
    </xsl:template>

    <xsl:template match="connectlocation">
        <xsl:apply-templates select="figure"/>
    </xsl:template>

    <!--contents-->
    <xsl:template match="contents">
        <xsl:apply-templates select="boundingbox"/>
        <xsl:apply-templates select="commentgraphics"/>
        <xsl:apply-templates select="figure"/>
        <xsl:apply-templates select="instance"/>
        <xsl:apply-templates select="portimplementation"/>
        <xsl:apply-templates select="net"/>
        <xsl:apply-templates select="netbundle"/>
    </xsl:template>

    <!--symbol/contents common-->
    <xsl:template match="boundingbox">
        <rect stroke="#ff0000" stroke-dasharray="20 20">
            <xsl:call-template name="_box">
                <xsl:with-param name="d" select="'100'"/>
            </xsl:call-template>
        </rect>
    </xsl:template>

    <xsl:template match="pagesize">
        <g stroke="#ff0000" stroke-dasharray="20 20">
            <xsl:apply-templates select="rectangle"/>
        </g>
    </xsl:template>

    <xsl:template match="commentgraphics">
        <xsl:if test="count(node())">
            <g class="commentGraphics">
                <xsl:apply-templates select="boundingbox"/>
                <xsl:apply-templates select="figure"/>
                <xsl:apply-templates select="instance"/>
                <xsl:apply-templates select="annotate"/>
                <xsl:apply-templates select="property"/>
            </g>
        </xsl:if>
    </xsl:template>

    <xsl:template match="designator">
        <xsl:if test="count(ancestor::contents)!=0">
            <xsl:apply-templates select="stringdisplay"/>
        </xsl:if>
    </xsl:template>

    <xsl:template match="annotate">
        <xsl:apply-templates select="stringdisplay"/>
    </xsl:template>

    <xsl:template match="property">
        <xsl:apply-templates select="name"/>
        <xsl:apply-templates select="string|integer|number"/>
    </xsl:template>

    <xsl:template match="string">
        <xsl:apply-templates select="stringdisplay"/>
    </xsl:template>

    <xsl:template match="integer">
        <xsl:apply-templates select="integerdisplay"/>
    </xsl:template>

    <xsl:template match="number">
        <xsl:apply-templates select="numberdisplay"/>
    </xsl:template>

    <!--instance-->
    <xsl:template match="instance">
        <g>
            <xsl:for-each select="transform">
                <xsl:call-template name="_transform_figure"/>
            </xsl:for-each>
            <xsl:call-template name="_instance_viewref"/>
        </g>
        <xsl:apply-templates select="portinstance"/>
        <xsl:apply-templates select="parametersssign"/>
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
    </xsl:template>

    <xsl:template match="parameterassign">
    </xsl:template>

    <xsl:template name="_instance_viewref">
        <xsl:if test="count(viewref)!=0">
            <xsl:variable name="kw">
                <xsl:call-template name="_viewref_name"/>
            </xsl:variable>
            <xsl:apply-templates select="key('vwl',$kw)">
            </xsl:apply-templates>
        </xsl:if>
        <xsl:for-each select="viewlist">
            <xsl:call-template name="_instance_viewref"/>
        </xsl:for-each>
    </xsl:template>

    <xsl:template name="_viewref_name">
        <xsl:variable name="viewref" select="viewref/@name"/>
        <xsl:variable name="cellref" select="viewref/cellref/@name"/>
        <xsl:variable name="libraryref" select="viewref/cellref/libraryref/@name"/>
        <xsl:value-of select="$libraryref"/>
        <xsl:if test="libraryref=''">
            <xsl:value-of select="ancestor::node()/library/@name"/>
        </xsl:if>
        <xsl:text>-</xsl:text>
        <xsl:value-of select="$cellref"/>
        <xsl:if test="cellref=''">
            <xsl:value-of select="ancestor::node()/cell/@name"/>
        </xsl:if>
        <xsl:text>-</xsl:text>
        <xsl:value-of select="$viewref"/>
        <xsl:if test="viewref=''">
            <xsl:value-of select="ancestor::node()/view/@name"/>
        </xsl:if>
    </xsl:template>

    <!--view_name-->
    <xsl:template name="_view_name">
        <xsl:variable name="view_ref" select="viewref/@name"/>
        <xsl:variable name="cell_ref" select="cellref/@name"/>
        <xsl:variable name="library_ref" select="cellref/libraryref/@name"/>
        <xsl:value-of select="concat($library_ref,'-',$cell_ref,'-',$view_ref)"/>
    </xsl:template>

    <!--portinstance-->
    <xsl:template match="portinstance">
        <!-- <g>
            <xsl:for-each select="transform">
                <xsl:call-template name="_transform_figure"/>
            </xsl:for-each>
            <xsl:call-template name="_instance_viewref"/>
        </g> -->
        <!-- <xsl:apply-templates select="designator"/> -->
        <!-- <xsl:apply-templates select="property"/> -->



        <!-- <xsl:variable name="name" select="@name"/>
        <xsl:variable name="ref">
            <xsl:for-each select="../viewref[1]">
                <xsl:call-template name="_view_name"/>
            </xsl:for-each>
        </xsl:variable>
        <xsl:for-each select="key('vwl',$ref)">
            <xsl:for-each select=".//portimplementation[@name=$name]">
                <xsl:for-each select="./keyworddisplay[@name='designator']">
                    <xsl:apply-templates select="display">
                        <xsl:with-param name="s" select="@name"/>
                    </xsl:apply-templates>
                </xsl:for-each>
            </xsl:for-each>
        </xsl:for-each>
 -->
        <xsl:apply-templates select="name"/>
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
    </xsl:template>

    <!-- <xsl:template match="nameDef">
        <xsl:choose>
            <xsl:when test="not (count(name)=0)">
            </xsl:when>
            <xsl:when test="not (count(rename)=0)">
            </xsl:when>
            <xsl:otherwise>
            </xsl:otherwise>
        </xsl:choose>
        <xsl:apply-templates select="designator"/>
    </xsl:template> -->

    <!--net-->
    <xsl:template match="net">
        <g stroke="black" stroke-width="10" id="{@name}">
            <!--joined-->
            <!--criticary-->
            <!--netDelay-->
            <xsl:apply-templates select="commonGraphics"/>
            <xsl:apply-templates select="figure"/>
            <xsl:apply-templates select="instance"/>
            <xsl:apply-templates select="array"/>
            <xsl:apply-templates select="net"/>
            <xsl:apply-templates select="property"/>
            <xsl:apply-templates select="name"/>
            <xsl:apply-templates select="rename"/>
        </g>
    </xsl:template>

    <xsl:template match="array">
        <xsl:apply-templates select="name"/>
        <xsl:apply-templates select="rename"/>
    </xsl:template>

    <!--viewbox-->
    <xsl:template name="_attr_viewbox">
        <xsl:call-template name="_box">
            <xsl:with-param name="mode" select="'viewbox'"/>
            <xsl:with-param name="d" select="'60'"/>
        </xsl:call-template>
    </xsl:template>

    <xsl:template name="_sheet">
        <rect fill="#fff">
            <xsl:call-template name="_box">
                <xsl:with-param name="d" select="'50'"/>
            </xsl:call-template>
        </rect>
    </xsl:template>

    <xsl:template name="_border">
        <rect stroke-width="0" fill="#000" fill-opacity="0.2">
            <xsl:call-template name="_box"/>
        </rect>
    </xsl:template>

    <xsl:template name="_box">
        <xsl:param name="mode" select="'xywh'"/>
        <xsl:param name="d" select="'0'"/>
        <xsl:variable name="xs">
            <xsl:for-each select=".//figure//pt|.//rectangle/pt">
                <xsl:sort select="substring-before(.,' ')" data-type="number"/>
                <xsl:if test="position()=1">
                    <xsl:value-of select="0+substring-before(.,' ')"/>
                </xsl:if>
            </xsl:for-each>
        </xsl:variable>
        <xsl:variable name="xe">
            <xsl:for-each select=".//figure//pt|.//rectangle/pt">
                <xsl:sort select="substring-before(.,' ')" data-type="number" order="descending"/>
                <xsl:if test="position()=1">
                    <xsl:value-of select="0+substring-before(.,' ')"/>
                </xsl:if>
            </xsl:for-each>
        </xsl:variable>
        <xsl:variable name="ys">
            <xsl:for-each select=".//figure//pt|.//rectangle/pt">
                <xsl:sort select="-substring-after(.,' ')" data-type="number"/>
                <xsl:if test="position()=1">
                    <xsl:value-of select="0-substring-after(.,' ')"/>
                </xsl:if>
            </xsl:for-each>
        </xsl:variable>
        <xsl:variable name="ye">
            <xsl:for-each select=".//figure//pt|.//rectangle/pt">
                <xsl:sort select="-substring-after(.,' ')" data-type="number" order="descending"/>
                <xsl:if test="position()=1">
                    <xsl:value-of select="0-substring-after(.,' ')"/>
                </xsl:if>
            </xsl:for-each>
        </xsl:variable>
        <xsl:message><xsl:value-of select="concat($xs,',',$ys,',',$xe,',',$ye)"/></xsl:message>
        <xsl:if test="contains($mode,'viewbox')">
            <xsl:attribute name="viewBox">
                <xsl:value-of select="$xs - $d"/>
                <xsl:text>,</xsl:text>
                <xsl:value-of select="$ys - $d"/>
                <xsl:text>,</xsl:text>
                <xsl:value-of select="$xe - $xs + 2*$d"/>
                <xsl:text>,</xsl:text>
                <xsl:value-of select="$ye - $ys + 2*$d"/>
            </xsl:attribute>
        </xsl:if>
        <xsl:if test="contains($mode,'xy')">
            <xsl:attribute name="x">
                <xsl:value-of select="$xs - $d"/>
            </xsl:attribute>
            <xsl:attribute name="y">
                <xsl:value-of select="$ys - $d"/>
            </xsl:attribute>
        </xsl:if>
        <xsl:if test="contains($mode,'wh')">
            <xsl:attribute name="width">
                <xsl:value-of select="$xe - $xs + 2*$d"/>
            </xsl:attribute>
            <xsl:attribute name="height">
                <xsl:value-of select="$ye - $ys + 2*$d"/>
            </xsl:attribute>
        </xsl:if>
    </xsl:template>

    <!--figure-->
    <xsl:template match="figure">
        <xsl:variable name="fg" select="key('fgl',concat(ancestor::library/@name,'-',@name))"/>
        <xsl:variable name="visible">
            <xsl:call-template name="_fg_visible">
                <xsl:with-param name="fg" select="$fg"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:if test="concat('',$visible)">
            <g stroke="#0" fill="#0">
                <xsl:for-each select="$fg">
                    <xsl:call-template name="_attr_fgo"/>
                </xsl:for-each>
                <xsl:for-each select="figuregroupoverride">
                    <xsl:call-template name="_attr_fgo"/>
                </xsl:for-each>
                <xsl:apply-templates select="circle|dot|openshape|path|polygon|rectangle|shape"/>
            </g>
        </xsl:if>
    </xsl:template>

    <xsl:template name="_fg_visible">
        <xsl:param name="s"/>
        <xsl:param name="fg" select="key('fgl',$s)"/>
        <xsl:choose>
            <xsl:when test="count(.//figuregroupoverride/visible//false)>0">
            </xsl:when>
            <xsl:when test="count(.//figuregroupoverride/visible//true)>0">
                <xsl:value-of select="true()"/>
            </xsl:when>
            <xsl:when test="count($fg/visible//false)>0">
            </xsl:when>
            <xsl:when test="count($fg/visible//true)>0">
                <xsl:value-of select="true()"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="true()"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="_attr_fgo">
        <xsl:call-template name="_fg_line_style"/>
        <xsl:call-template name="_fg_pen_type"/>
    </xsl:template>

    <xsl:template name="_fg_color">
        <xsl:attribute name="stroke">
            <xsl:call-template name="_color"/>
        </xsl:attribute>
    </xsl:template>

    <xsl:template name="_color">
        <xsl:param name="rgb" select="color"/>
        <xsl:param name="r" select="round(2.55*substring-before($rgb,' '))"/>
        <xsl:param name="g" select="round(2.55*substring-before(substring-after($rgb,' '),' '))"/>
        <xsl:param name="b" select="round(2.55*substring-after(substring-after($rgb,' '),' '))"/>
        <xsl:value-of select="concat('rgb(',$r,',',$g,',',$b,')')"/>
    </xsl:template>

    <xsl:template name="_fg_line_style">
        <xsl:if test="not (count(pathWidth)=0)">
            <xsl:attribute name="stroke-width">
                <xsl:value-of select="1+(pathWidth)"/>
            </xsl:attribute>
        </xsl:if>
        <xsl:if test="not (count(borderWidth)=0)">
            <xsl:attribute name="stroke-width">
                <xsl:value-of select="1+(borderWidth)"/>
            </xsl:attribute>
        </xsl:if>
        <xsl:if test="not (count(cornertype)=0)">
            <xsl:variable name="s" select="cornertype/text()"/>
            <xsl:attribute name="stroke-linejoin">
                <xsl:choose>
                    <xsl:when test="$s='EXTEND'">
                        <xsl:text>bevel</xsl:text>
                    </xsl:when>
                    <xsl:when test="$s='ROUND'">
                        <xsl:text>round</xsl:text>
                    </xsl:when>
                    <xsl:when test="$s='TRUNCATE'">
                        <xsl:text>miter</xsl:text>
                    </xsl:when>
                </xsl:choose>
            </xsl:attribute>
        </xsl:if>
        <xsl:if test="not (count(endtype)=0)">
            <xsl:variable name="s" select="endtype/text()"/>
            <xsl:attribute name="stroke-linecap">
                <xsl:choose>
                    <xsl:when test="$s='EXTEND'">
                        <xsl:text>square</xsl:text>
                    </xsl:when>
                    <xsl:when test="$s='ROUND'">
                        <xsl:text>round</xsl:text>
                    </xsl:when>
                    <xsl:when test="$s='TRUNCATE'">
                        <xsl:text>butt</xsl:text>
                    </xsl:when>
                </xsl:choose>
            </xsl:attribute>
        </xsl:if>
    </xsl:template>

    <xsl:template name="_fg_pen_type">
        <xsl:if test="not (count(fillPattern)=0)">
            <xsl:attribute name="fill">
                <xsl:if test="count(.//false)=0">
                    <xsl:call-template name="_color"/>
                </xsl:if>
                <xsl:if test="not (count(.//false)=0)">
                    <xsl:text>none</xsl:text>
                </xsl:if>
            </xsl:attribute>
        </xsl:if>

        <xsl:if test="not (count(borderPattern)=0)">
            <xsl:attribute name="fill">
                <xsl:if test="count(.//false)=0">
                    <xsl:call-template name="_color"/>
                </xsl:if>
                <xsl:if test="not (count(.//false)=0)">
                    <xsl:text>none</xsl:text>
                </xsl:if>
            </xsl:attribute>
        </xsl:if>
    </xsl:template>

    <xsl:template name="_transform_figure">
        <xsl:variable name="t">
            <xsl:call-template name="_transform_figure_position"/>
            <xsl:call-template name="_transform_figure_angle"/>
        </xsl:variable>
        <xsl:if test="string-length($t)!=0">
            <xsl:attribute name="transform">
                <xsl:value-of select="$t"/>
            </xsl:attribute>
        </xsl:if>
    </xsl:template>

    <xsl:template name="_transform_figure_position">
        <xsl:param name="x" select="0+substring-before(origin/pt,' ')"/>
        <xsl:param name="y" select="0-substring-after(origin/pt,' ')"/>
        <xsl:if test="not (count(origin)=0)">
            <xsl:text>translate(</xsl:text>
            <xsl:value-of select="$x"/>
            <xsl:text>,</xsl:text>
            <xsl:value-of select="$y"/>
            <xsl:text>)</xsl:text>
        </xsl:if>
    </xsl:template>

    <xsl:template name="_transform_figure_angle">
        <xsl:param name="a" select="orientation/text()"/>
        <xsl:if test="not (count(orientation)=0)">
            <xsl:if test="contains($a,'R0')">
                <xsl:text>rotate(0)</xsl:text>
            </xsl:if>
            <xsl:if test="contains($a,'R90')">
                <xsl:text>rotate(270)</xsl:text>
            </xsl:if>
            <xsl:if test="contains($a,'R180')">
                <xsl:text>rotate(180)</xsl:text>
            </xsl:if>
            <xsl:if test="contains($a,'R270')">
                <xsl:text>rotate(90)</xsl:text>
            </xsl:if>
            <xsl:if test="contains($a,'MY')">
                <xsl:text>scale(-1,1)</xsl:text>
            </xsl:if>
            <xsl:if test="contains($a,'MX')">
                <xsl:text>scale(1,-1)</xsl:text>
            </xsl:if>
        </xsl:if>
    </xsl:template>

    <xsl:template match="circle">
        <xsl:param name="x1" select="0+substring-before(pt[1],' ')"/>
        <xsl:param name="y1" select="0-substring-after(pt[1],' ')"/>
        <xsl:param name="x2" select="0+substring-before(pt[2],' ')"/>
        <xsl:param name="y2" select="0-substring-after(pt[2],' ')"/>
        <xsl:param name="dx" select="(2*($x1&lt;$x2)-1)*($x2 - $x1)"/>
        <xsl:param name="dy" select="(2*($y1&lt;$y2)-1)*($y2 - $y1)"/>
        <circle>
            <xsl:attribute name="cx">
                <xsl:value-of select="0.5*($x2+$x1)"/>
            </xsl:attribute>
            <xsl:attribute name="cy">
                <xsl:value-of select="0.5*($y2+$y1)"/>
            </xsl:attribute>
            <xsl:attribute name="r">
                <xsl:value-of select="0.5*($dx+$dy)"/>
            </xsl:attribute>
        </circle>
    </xsl:template>

    <xsl:template match="dot">
        <xsl:param name="x" select="0+substring-before(pt[1],' ')"/>
        <xsl:param name="y" select="0-substring-after(pt[1],' ')"/>
        <circle r="1">
            <xsl:attribute name="cx">
                <xsl:value-of select="$x"/>
            </xsl:attribute>
            <xsl:attribute name="cy">
                <xsl:value-of select="$y"/>
            </xsl:attribute>
        </circle>
    </xsl:template>

    <xsl:template match="path">
        <path fill="none">
            <xsl:attribute name="d">
                <xsl:text>M</xsl:text>
                <xsl:apply-templates select="pointlist/pt"/>
            </xsl:attribute>
        </path>
    </xsl:template>

    <xsl:template match="polygon">
        <path>
            <xsl:attribute name="d">
                <xsl:text>M</xsl:text>
                <xsl:apply-templates select="pointlist/pt"/>
                <xsl:text>Z</xsl:text>
            </xsl:attribute>
        </path>
    </xsl:template>

    <xsl:template match="pointlist/pt">
        <xsl:param name="x" select='0+substring-before(text()," ")'/>
        <xsl:param name="y" select='0-substring-after(text()," ")'/>
        <xsl:text>&#32;</xsl:text>
        <xsl:value-of select="$x"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="$y"/>
    </xsl:template>

    <xsl:template match="rectangle">
        <xsl:param name="x1" select="0+substring-before(pt[1],' ')"/>
        <xsl:param name="y1" select="0-substring-after(pt[1],' ')"/>
        <xsl:param name="x2" select="0+substring-before(pt[2],' ')"/>
        <xsl:param name="y2" select="0-substring-after(pt[2],' ')"/>
        <rect fill="none">
            <xsl:attribute name="x">
                <xsl:value-of select="$x1"/>
            </xsl:attribute>
            <xsl:attribute name="y">
                <xsl:value-of select="$y1"/>
            </xsl:attribute>
            <xsl:attribute name="width">
                <xsl:value-of select="number($x2)-number($x1)"/>
            </xsl:attribute>
            <xsl:attribute name="height">
                <xsl:value-of select="number($y2)-number($y1)"/>
            </xsl:attribute>
        </rect>
    </xsl:template>

    <xsl:template match="openshape">
        <path fill="none">
            <xsl:attribute name="d">
                <xsl:apply-templates select="curve/arc"/>
            </xsl:attribute>
        </path>
    </xsl:template>

    <xsl:template match="sharpe">
        <path fill="none">
            <xsl:attribute name="d">
                <xsl:apply-templates select="curve/arc"/>
                <xsl:text>Z</xsl:text>
            </xsl:attribute>
        </path>
    </xsl:template>

    <xsl:template match="arc">
        <xsl:param name="x1" select="0+substring-before(pt[1],' ')"/>
        <xsl:param name="y1" select="0-substring-after(pt[1],' ')"/>
        <xsl:param name="x2" select="0+substring-before(pt[2],' ')"/>
        <xsl:param name="y2" select="0-substring-after(pt[2],' ')"/>
        <xsl:param name="x3" select="0+substring-before(pt[3],' ')"/>
        <xsl:param name="y3" select="0-substring-after(pt[3],' ')"/>
        <xsl:text>M</xsl:text>
        <xsl:value-of select="$x1"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="$y1"/>
        <xsl:text>Q</xsl:text>
        <xsl:value-of select="-0.5*($x3+$x1)+2*$x2"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="-0.5*($y3+$y1)+2*$y2"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="$x3"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="$y3"/>
    </xsl:template>

    <!--display-->
    <xsl:template match="name">
        <xsl:variable name="s" select="../@name"/>
        <xsl:apply-templates select="display">
            <xsl:with-param name="s" select="$s"/>
        </xsl:apply-templates>
        <xsl:apply-templates select="stringdisplay|integerdisplay|numberdisplay"/>
    </xsl:template>

    <xsl:template match="rename">
        <xsl:apply-templates select="display">
            <xsl:with-param name="s" select="text()"/>
        </xsl:apply-templates>
        <xsl:apply-templates select="stringdisplay|integerdisplay|numberdisplay"/>
    </xsl:template>

    <xsl:template match="stringdisplay|integerdisplay|numberdisplay">
        <xsl:apply-templates select="display">
            <xsl:with-param name="s" select="text()"/>
        </xsl:apply-templates>
    </xsl:template>

    <xsl:template match="keyworddisplay">
        <xsl:param name="name" select="../@name"/>
        <xsl:param name="kw" select="@name"/>
        <xsl:variable name="s">
            <xsl:for-each select="../../../port[@name=$name]/*[name()=$kw]">
                <xsl:if test="position()=1">
                    <xsl:value-of select="translate(text(),'&quot;','')"/>
                </xsl:if>
            </xsl:for-each>
        </xsl:variable>
        <xsl:apply-templates select="display">
            <xsl:with-param name="s">
                <xsl:value-of select="$s"/>
            </xsl:with-param>
        </xsl:apply-templates>
    </xsl:template>

    <!--display-->
    <xsl:template match="display">
        <xsl:param name="s" select="../text()"/>
        <xsl:variable name="fg_name" select="concat(ancestor::library/@name,'-',@name,figuregroupoverride/@name)"/>
        <xsl:variable name="fg" select="key('fgl',$fg_name)"/>
        <xsl:variable name="vw">
            <xsl:for-each select="ancestor::instance">
                <xsl:call-template name="_viewref_name"/>
            </xsl:for-each>
        </xsl:variable>
        <xsl:variable name="visible">
            <xsl:call-template name="_fg_visible">
                <xsl:with-param name="fg" select="$fg"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:if test="concat('',$visible)">
            <xsl:if test="string-length(normalize-space(translate($s,'&quot;','')))!=0">
                <!-- <xsl:message>
                    <xsl:value-of select="concat('-',$fg_name,'-',$visible,'-',normalize-space(translate($s,'&quot;','')))"/>
                </xsl:message> -->
                <text fill="blue" stroke-width="0" font-size="19">
                    <xsl:for-each select="$fg">
                        <xsl:call-template name="_fg_font"/>
                    </xsl:for-each>
                    <xsl:for-each select="figuregroupoverride">
                        <xsl:call-template name="_fg_font"/>
                    </xsl:for-each>
                    <xsl:call-template name="_text_anchor"/>
                    <xsl:if test="not (count(origin)=0)">
                        <xsl:call-template name="_transform_text"/>
                    </xsl:if>
                    <xsl:call-template name="_text_out">
                        <xsl:with-param name="s" select="$s"/>
                    </xsl:call-template>
                </text>
                <!-- <xsl:call-template name="_transform_debug"/> -->
            </xsl:if>
        </xsl:if>
    </xsl:template>

    <xsl:template name="_fg_font">
        <xsl:attribute name="stroke-width">
            <xsl:text>0</xsl:text>
        </xsl:attribute>
        <xsl:attribute name="font-weight">
            <xsl:text>bold</xsl:text>
        </xsl:attribute>
        <xsl:if test="not (count(color)=0)">
            <xsl:attribute name="fill">
                <xsl:call-template name="_color"/>
            </xsl:attribute>
        </xsl:if>
        <xsl:if test="not (count(textheight)=0)">
            <xsl:attribute name="font-size">
                <xsl:value-of select="textheight"/>
            </xsl:attribute>
        </xsl:if>
        <xsl:for-each select="property[contains(@name,'FONTNAME')]">
            <xsl:attribute name="font-family">
                <xsl:value-of select="translate(string/text(),'&quot;','')"/>
            </xsl:attribute>
        </xsl:for-each>
    </xsl:template>

    <xsl:template name="_text_anchor">
        <xsl:param name="j1" select="substring-before(justify/text(),'ER')"/>
        <xsl:param name="j2" select="substring-after(justify/text(),'ER')"/>
        <xsl:if test="not (count(justify)=0)">
            <xsl:attribute name="dominant-baseline">
                <xsl:choose>
                    <xsl:when test="$j1='CENT'">
                        <xsl:text>central</xsl:text>
                    </xsl:when>
                    <xsl:when test="$j1='LOW'">
                        <xsl:text>auto</xsl:text>
                    </xsl:when>
                    <xsl:when test="$j1='UPP'">
                        <xsl:text>hanging</xsl:text>
                    </xsl:when>
                </xsl:choose>
            </xsl:attribute>
            <xsl:attribute name="text-anchor">
                <xsl:choose>
                    <xsl:when test="$j2='CENTER'">
                        <xsl:text>middle</xsl:text>
                    </xsl:when>
                    <xsl:when test="$j2='LEFT'">
                        <xsl:text>start</xsl:text>
                        <!-- <xsl:text>end</xsl:text> -->
                    </xsl:when>
                    <xsl:when test="$j2='RIGHT'">
                        <!-- <xsl:text>start</xsl:text> -->
                        <xsl:text>end</xsl:text>
                    </xsl:when>
                </xsl:choose>
            </xsl:attribute>
        </xsl:if>
    </xsl:template>

    <xsl:template name="_transform_text">
        <xsl:variable name="s">
            <xsl:call-template name="_transform_text_position"/>
            <xsl:call-template name="_transform_text_angle"/>
            <!-- <xsl:for-each select="../ancestor::*/transform">
                <xsl:message><xsl:value-of select="../@name"/></xsl:message>
                <xsl:call-template name="_transform_text_angle"/>
            </xsl:for-each> -->
        </xsl:variable>
        <xsl:if test="not (string-length($s)=0)">
            <xsl:attribute name="transform">
                <xsl:value-of select="$s"/>
            </xsl:attribute>
        </xsl:if>
    </xsl:template>

    <xsl:template name="_transform_text_position">
        <xsl:param name="x" select="0+substring-before(origin/pt,' ')"/>
        <xsl:param name="y" select="0-substring-after(origin/pt,' ')"/>
        <xsl:if test="not (count(origin)=0)">
            <xsl:if test="not (count(origin)=0)">
                <xsl:text>translate(</xsl:text>
                <xsl:value-of select="$x"/>
                <xsl:text>,</xsl:text>
                <xsl:value-of select="$y"/>
                <xsl:text>)</xsl:text>
            </xsl:if>
        </xsl:if>
    </xsl:template>

    <xsl:template name="_transform_text_angle">
        <xsl:param name="a" select="orientation/text()"/>
        <xsl:if test="not (count(orientation)=0)">
            <xsl:if test="$a='R0'">
                <xsl:text>rotate(0)</xsl:text>
            </xsl:if>
            <xsl:if test="$a='R90'">
                <xsl:text>rotate(270)</xsl:text>
            </xsl:if>
            <xsl:if test="$a='R180'">
                <xsl:text>rotate(180)</xsl:text>
            </xsl:if>
            <xsl:if test="$a='R270'">
                <xsl:text>rotate(90)</xsl:text>
            </xsl:if>
            <xsl:if test="$a='MY'">
                <xsl:text>scale(-1,1)</xsl:text>
            </xsl:if>
            <xsl:if test="$a='MX'">
                <xsl:text>scale(1,-1)</xsl:text>
            </xsl:if>
            <xsl:if test="$a='MYR90'">
                <xsl:text>rotate(90)scale(-1,1)</xsl:text>
            </xsl:if>
            <xsl:if test="$a='MXR90'">
                <xsl:text>rotate(90)scale(1,-1)</xsl:text>
            </xsl:if>
        </xsl:if>
    </xsl:template>

    <!--text_out-->
    <xsl:template name="_text_out">
        <xsl:param name="s"/>
        <xsl:param name="s2" select="translate($s,'&quot;&#13;','')"/>
        <xsl:choose>
            <xsl:when test="contains($s2,'&#10;')">
                <xsl:call-template name="_text_area">
                    <xsl:with-param name="s" select="$s2"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$s2"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="_text_area">
        <xsl:param name="s" select="."/>
        <xsl:param name="y" select="0"/>
        <xsl:param name="h" select="10"/>
        <xsl:if test="string-length($s)">
            <tspan x="0">
                <xsl:attribute name="y">
                    <xsl:value-of select="$y"/>
                </xsl:attribute>
                <xsl:value-of select="substring-before(concat($s,'&#10;'),'&#10;')"/>
            </tspan>
            <xsl:call-template name="_text_area">
                <xsl:with-param name="s" select="substring-after($s, '&#10;')"/>
                <xsl:with-param name="y" select="number($y)+$h"/>
            </xsl:call-template>
        </xsl:if>
    </xsl:template>

    <!--text()-->
    <xsl:template match="text()">
        <xsl:message>
            <xsl:value-of select="name(..)"/>
            <xsl:text>no&#32;support:&#32;</xsl:text>
            <xsl:value-of select="."/>
        </xsl:message>
    </xsl:template>

    <!--debug-->
    <!-- <xsl:template name="_transform_figure_debug">
        <xsl:variable name="x" select="0+substring-before(transform/origin/pt,' ')"/>
        <xsl:variable name="y" select="0-substring-after(transform/origin/pt,' ')"/>
        <xsl:variable name="a" select="transform/orientation/text()"/>
        <text x="0" y="0" font-size="6" fill="red" stroke-width="0">
            <xsl:value-of select="$a"/>
        </text>
        <circle r="1" fill="#0" stroke="red" stroke-width="1" cx="0" cy="0"/>
    </xsl:template> -->

    <!-- <xsl:template name="_transform_debug">
        <xsl:param name="x" select="0+substring-before(origin/pt,' ')"/>
        <xsl:param name="y" select="0-substring-after(origin/pt,' ')"/>
        <xsl:param name="a" select="orientation/text()"/>
        <text x="{3+$x}" y="{-3+$y}" font-size="6" fill="green" stroke-width="0">
            <xsl:value-of select="$a"/>
        </text>
        <circle r="1" fill="#0" stroke="green" stroke-width="1" cx="{$x}" cy="{$y}"/>
    </xsl:template> -->

</xsl:stylesheet>
