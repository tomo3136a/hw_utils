<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:import href="common.xsl"/>
    <xsl:output method="text"/>
    <xsl:template match="/">
        <xsl:text>library,cell,name,type</xsl:text>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="edif/library/cell"/>
    </xsl:template>
    <xsl:template match="cell">
        <xsl:value-of select="ancestor::library/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="@name"/>
        <xsl:text>,</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="celltype"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>
</xsl:stylesheet>
