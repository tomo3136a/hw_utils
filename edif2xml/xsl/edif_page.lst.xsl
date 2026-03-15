<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text"/>
    <xsl:template match="/">
        <xsl:apply-templates select="edif"/>
    </xsl:template>
    <xsl:template match="edif">
        <xsl:variable name="cell_ref" select="design/cellref/@name"/>
        <xsl:variable name="library_ref" select="design/cellref/libraryref/@name"/>
        <xsl:for-each select="(library|external)[@name=$library_ref]">
            <xsl:for-each select="cell[@name=$cell_ref]/view">
                <xsl:apply-templates select="contents/page"/>
            </xsl:for-each>
        </xsl:for-each>
    </xsl:template>
    <xsl:template match="page">
        <xsl:value-of select="@name" disable-output-escaping="yes"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>
</xsl:stylesheet>
