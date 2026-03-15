<?xml version="1.0"?>
<!--
    edif2xml.exe test.edif xsl/test_refs.xsl @refs=data.txt
    1. test.edif ファイルを読み込み
    2. test_refs.xsl で変換
    3. 
-->
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text"/>
    <xsl:param name="refs"/>

    <xsl:template match="/">
        <xsl:apply-templates select="$refs/data"/>
    </xsl:template>

    <xsl:template match="data">
        <xsl:apply-templates select="dl"/>
    </xsl:template>

    <xsl:template match="dl">
        <xsl:apply-templates select="dd"/>
    </xsl:template>

    <xsl:template match="dd">
        <xsl:value-of select="text()"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

</xsl:stylesheet>
