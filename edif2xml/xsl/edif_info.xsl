<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:import href="common.xsl"/>
    <xsl:output method="text"/>

    <!--root/edif/library/external/cell/view-->
    <xsl:template match="/">
        <xsl:apply-templates select="edif"/>
    </xsl:template>

    <xsl:template match="edif">
        <xsl:apply-templates select="design"/>
        <xsl:apply-templates select="library"/>
        <xsl:apply-templates select="external"/>
    </xsl:template>

    <xsl:template match="design">
        <xsl:text>トップデザイン：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:text>&#32;&#32;</xsl:text>
        <xsl:value-of select="cellref/libraryref/@name"/>
        <xsl:text>-</xsl:text>
        <xsl:value-of select="cellref/@name"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <xsl:template match="library|external">
        <xsl:text>ライブラリ：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="technology"/>
        <xsl:apply-templates select="cell"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <xsl:template match="technology">
        <xsl:apply-templates select="figuregroup"/>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <xsl:template match="figuregroup">
        <xsl:text>&#32;&#32;グループ：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <xsl:template match="cell">
        <xsl:text>&#32;&#32;セル：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="view"/>
    </xsl:template>

    <xsl:template match="view">
        <xsl:text>&#32;&#32;&#32;&#32;ビュー：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:text>&#32;</xsl:text>
        <xsl:variable name="id" select="concat(../../@name,'-',../@name,'-',@name)"/>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="interface[count(*)!=0]"/>
        <xsl:apply-templates select="contents[count(*)!=0]"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <xsl:template match="interface">
        <xsl:text>&#32;&#32;&#32;&#32;&#32;&#32;(I/F)</xsl:text>
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="name"/>
        <xsl:apply-templates select="port"/>
        <xsl:apply-templates select="symbol"/>
    </xsl:template>

    <xsl:template match="contents">
        <xsl:text>&#32;&#32;&#32;&#32;&#32;&#32;(本体)</xsl:text>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="page"/>
    </xsl:template>

    <xsl:template match="page">
        <xsl:text>&#32;(ページ&#32;</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:text>)</xsl:text>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="instance"/>
        <xsl:apply-templates select="net"/>
    </xsl:template>

    <!--interface-->
    <xsl:template match="port">
        <xsl:text>&#32;ポート：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <xsl:template match="symbol">
        <xsl:text>&#32;シンボル：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="portimplementation"/>
    </xsl:template>

    <xsl:template match="portimplementation">
        <xsl:text>&#32;&#32;ポート実装：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <!--contents/page-->
    <xsl:template match="instance">
        <xsl:text>&#32;&#32;部品：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="portinstance"/>
    </xsl:template>

    <xsl:template match="portinstance">
        <xsl:text>&#32;&#32;&#32;&#32;部品ポート：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <xsl:template match="net">
        <xsl:text>&#32;&#32;&#32;&#32;ネット：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="array"/>
    </xsl:template>

    <xsl:template match="array">
        <xsl:text>&#32;&#32;&#32;&#32;バス：</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:apply-templates select="designator"/>
        <xsl:apply-templates select="property"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <!--common-->
    <xsl:template match="name">
        <xsl:text>&#32;name[</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:text>]</xsl:text>
    </xsl:template>

    <xsl:template match="designator">
        <xsl:if test="string-length(normalize-space(text()))!=0">
            <xsl:text>&#32;designator[</xsl:text>
            <xsl:value-of select="text()"/>
            <xsl:text>]</xsl:text>
        </xsl:if>
    </xsl:template>

    <xsl:template match="property">
        <xsl:text>&#32;</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:text>=</xsl:text>
        <xsl:call-template name="_value"/>
    </xsl:template>

    <!--common function-->
    <!-- <xsl:template name="_name">
        <xsl:call-template name="_pprint">
            <xsl:with-param name="s">
                <xsl:choose>
                    <xsl:when test="count(rename)!=0">
                        <xsl:value-of select="rename/text()"/>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:value-of select="@name"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>

    <xsl:template name="_value">
        <xsl:call-template name="_pprint">
            <xsl:with-param name="s">
                <xsl:value-of select="string/text()"/>
                <xsl:value-of select="integer/text()"/>
                <xsl:value-of select="number/text()"/>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>

    <xsl:template name="_pprint">
        <xsl:param name="s"/>
        <xsl:variable name="s2">
            <xsl:if test="not (starts-with($s,'&amp;'))">
                <xsl:value-of select="substring($s,1,1)"/>
            </xsl:if>
            <xsl:value-of select="substring($s,2)"/>
        </xsl:variable>
        <xsl:variable name="s3" select="translate($s2,'&quot;','')"/>
        <xsl:value-of select="$s3" disable-output-escaping="yes"/>
    </xsl:template> -->

</xsl:stylesheet>
