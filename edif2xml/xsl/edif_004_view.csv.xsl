<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:import href="common.xsl"/>
    <xsl:output method="text"/>
    <xsl:template match="/">
        <xsl:text>library,cell,view,name,type,interface,contents</xsl:text>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="edif/library/cell/view"/>
    </xsl:template>
    <xsl:template match="view">
        <xsl:value-of select="ancestor::library/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="ancestor::cell/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="@name"/>
        <xsl:text>,</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="viewtype"/>
        <xsl:text>,</xsl:text>
        <xsl:if test="interface">true</xsl:if>
        <xsl:text>,</xsl:text>
        <xsl:if test="contents">true</xsl:if>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

</xsl:stylesheet>
