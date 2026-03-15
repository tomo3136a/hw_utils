<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text"/>
    <xsl:template match="/">
        <xsl:apply-templates select="edif"/>
    </xsl:template>
    <xsl:template match="edif">
        <xsl:variable name="lib" select="design/cellref/libraryref/@name"/>
        <xsl:variable name="cell" select="design/cellref/@name"/>
        <xsl:apply-templates
            select="library[@name=$lib]/cell[@name=$cell]/view/contents"/>
    </xsl:template>
    <xsl:template match="contents">
        <xsl:text>offconn,@offconn</xsl:text>
        <xsl:text>&#10;</xsl:text>
        <xsl:for-each select="offpageconnector[not(@name=preceding::offpageconnector/@name)]">
            <xsl:sort select="@name"/>
            <xsl:apply-templates select="."/>
        </xsl:for-each>
    </xsl:template>
    <xsl:template match="offpageconnector">
        <xsl:value-of select="@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="@name" disable-output-escaping="yes"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>
</xsl:stylesheet>
