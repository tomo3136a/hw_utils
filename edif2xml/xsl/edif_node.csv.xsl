<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text"/>
    <xsl:variable name="lowercase" select="'abcdefghijklmnopqrstuvwxyz'" />
    <xsl:variable name="uppercase" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZ'" />
    <xsl:key name="nodes" match="*" use="translate(name(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
    <xsl:template match="/">
        <xsl:for-each select="//*[generate-id()=generate-id(key('nodes',translate(name(),$lowercase,$uppercase))[1])]">
            <xsl:sort select="name()"/>
            <xsl:value-of select="name()"/>
            <xsl:text>,</xsl:text>
            <xsl:value-of select="count(key('nodes',translate(name(),$lowercase,$uppercase)))"/>
            <xsl:text>&#10;</xsl:text>
        </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>
