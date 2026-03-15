<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:import href="common.xsl"/>
    <xsl:output method="text"/>
    <xsl:template match="/">
        <xsl:text>library,cell,view,port#,figure#,portimplementation#,figure/pt,_xs,_ys,_xe,_ye</xsl:text>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="edif/library/cell/view/interface/symbol"/>
    </xsl:template>
    <xsl:template match="symbol">
        <xsl:value-of select="ancestor::library/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="ancestor::cell/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="ancestor::view/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="count(../port)"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="count(figure)"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="count(portimplementation)"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="count(figure//pt)"/>
        <xsl:text>,</xsl:text>
        <xsl:call-template name="_xs"/>
        <xsl:text>,</xsl:text>
        <xsl:call-template name="_ys"/>
        <xsl:text>,</xsl:text>
        <xsl:call-template name="_xe"/>
        <xsl:text>,</xsl:text>
        <xsl:call-template name="_ye"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <xsl:template name="_xs">
        <xsl:for-each select=".//pt">
            <xsl:sort select="substring-before(.,' ')" data-type="number"/>
            <xsl:if test="position()=1">
                <xsl:value-of select="0+substring-before(.,' ')"/>
            </xsl:if>
        </xsl:for-each>
    </xsl:template>
    <xsl:template name="_xe">
        <xsl:for-each select=".//pt">
            <xsl:sort select="substring-before(.,' ')" data-type="number" order="descending"/>
            <xsl:if test="position()=1">
                <xsl:value-of select="0+substring-before(.,' ')"/>
            </xsl:if>
        </xsl:for-each>
    </xsl:template>
    <xsl:template name="_ys">
        <xsl:for-each select=".//pt">
            <xsl:sort select="-substring-after(.,' ')" data-type="number"/>
            <xsl:if test="position()=1">
                <xsl:value-of select="0-substring-after(.,' ')"/>
            </xsl:if>
        </xsl:for-each>
    </xsl:template>
    <xsl:template name="_ye">
        <xsl:for-each select=".//pt">
            <xsl:sort select="-substring-after(.,' ')" data-type="number" order="descending"/>
            <xsl:if test="position()=1">
                <xsl:value-of select="0-substring-after(.,' ')"/>
            </xsl:if>
        </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>
