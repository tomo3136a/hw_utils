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
        <xsl:text>page,@page,instance,net</xsl:text>
        <xsl:text>,pagesize-xs,pagesize-ys,pagesize-xe,pagesize-ye</xsl:text>
        <xsl:text>.boundbox-xs,boundbox-ys,boundbox-xe,boundbox-ye</xsl:text>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="page"/>
    </xsl:template>
    <xsl:template match="page">
        <xsl:value-of select="@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="@name" disable-output-escaping="yes"/>
        <xsl:text>,</xsl:text>
        <xsl:if test="instance">true</xsl:if>
        <xsl:text>,</xsl:text>
        <xsl:if test="net">true</xsl:if>
        <xsl:text>,</xsl:text>
        <xsl:call-template name="_pt">
            <xsl:with-param name="pt" select="pagesize/rectangle/pt[1]"/>
        </xsl:call-template>
        <xsl:text>,</xsl:text>
        <xsl:call-template name="_pt">
            <xsl:with-param name="pt" select="pagesize/rectangle/pt[2]"/>
        </xsl:call-template>
        <xsl:text>,</xsl:text>
        <xsl:call-template name="_pt">
            <xsl:with-param name="pt" select="boundingbox/rectangle/pt[1]"/>
        </xsl:call-template>
        <xsl:text>,</xsl:text>
        <xsl:call-template name="_pt">
            <xsl:with-param name="pt" select="boundingbox/rectangle/pt[2]"/>
        </xsl:call-template>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>
    <xsl:template name="_pt">
        <xsl:param name="pt" select="true()"/>
        <xsl:value-of select="substring-before($pt,' ')"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="substring-after($pt,' ')"/>
    </xsl:template>
    
</xsl:stylesheet>
