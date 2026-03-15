<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:import href="common.xsl"/>
    <xsl:output method="text"/>
    <xsl:template match="/">
        <xsl:text>library,name</xsl:text>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="edif/library"/>
    </xsl:template>
    <xsl:template match="library">
        <xsl:value-of select="@name"/>
        <xsl:text>,</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>
</xsl:stylesheet>
