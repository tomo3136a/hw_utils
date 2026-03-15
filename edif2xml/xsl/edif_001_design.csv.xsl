<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text"/>
    <xsl:template match="/">
        <xsl:text>design,library,cell</xsl:text>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="/edif/design"/>
    </xsl:template>
    <xsl:template match="design">
        <xsl:value-of select="@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="cellref/libraryref/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="cellref/@name"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>
</xsl:stylesheet>
