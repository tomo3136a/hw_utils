<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text"/>
    <xsl:template match="/">
        <xsl:text>page,net</xsl:text>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="edif/library/cell/view/contents/page/net"/>
    </xsl:template>
    <xsl:template match="net">
        <xsl:value-of select="ancestor::page/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="@name"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>
</xsl:stylesheet>
