<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="xml" media-type="text/xsl" indent="yes"/>

    <xsl:template match="/">
        <xsl:apply-templates />
    </xsl:template>

    <xsl:template match="text()">
        <xsl:if test="not (string-length(normalize-space(.))=0)">
        <xsl:value-of select="." />
        </xsl:if>
    </xsl:template>

    <xsl:template match="*">
        <xsl:element name="{name()}">
            <xsl:for-each select="@*">
                <xsl:sort select="name()"/>
                <xsl:attribute name="{name()}">
                    <xsl:value-of select="."/>
                </xsl:attribute>
            </xsl:for-each>
            <xsl:apply-templates select="text()"/>
            <xsl:variable name="a" select="name()"/>
            <xsl:choose>
                <xsl:when test="($a='edif')">
                    <xsl:apply-templates select="*"/>
                </xsl:when>
                <xsl:when test="($a='status')">
                    <xsl:copy-of select="*"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:for-each select="*">
                        <xsl:sort select="name()"/>
                        <xsl:apply-templates select="."/>
                    </xsl:for-each>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:element>
    </xsl:template>

</xsl:stylesheet>
