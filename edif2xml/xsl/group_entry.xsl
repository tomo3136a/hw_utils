<?xml version="1.0" encoding="shift_jis"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text"/>
    <xsl:param name="page"/>

    <!--root-->
    <xsl:template match="/">
        <xsl:apply-templates select="edif"/>
    </xsl:template>

    <xsl:key name="fgl" match="//figureGroup" use="concat(../../@name,'-',@name)"/>
    <xsl:key name="fgi" match="//figureGroup/*" use="concat(../../../@name,'-',../@name,'-',name(),'-',@name)"/>

    <!--active page-->
    <xsl:template match="edif">
        <xsl:variable name="cell_ref" select="design/cellRef/@name"/>
        <xsl:variable name="library_ref" select="design/cellRef/libraryRef/@name"/>

        <!--page list-->
        <xsl:for-each select="(library|external)[@name=$library_ref]">
            <xsl:for-each select="cell[@name=$cell_ref]/view/contents">
                <xsl:text>page-list:&#10;</xsl:text>
                <xsl:apply-templates select="page[@name=$page]"/>
            </xsl:for-each>
        </xsl:for-each>

        <!--figureGroup list-->
        <xsl:for-each select="(library|external)[@name=$library_ref]/technology">
            <xsl:text>figureGroup-list:&#10;</xsl:text>
            <xsl:for-each select="figureGroup">
                <xsl:call-template name="_name"/>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:for-each>

        <!--fogureGroup item list-->
        <xsl:for-each select="(library|external)[@name=$library_ref]/technology/figureGroup[1]">
            <xsl:text>figureGroup-item-list:&#10;</xsl:text>
            <xsl:call-template name="_figureGroupItem"/>
        </xsl:for-each>

        <!--figureGroup contents-->
        <xsl:for-each select="(library|external)[@name=$library_ref]/technology">
            <!-- <xsl:message>
                <xsl:text>figureGroup-list:&#10;</xsl:text>
                <xsl:for-each select="figureGroup">
                    <xsl:value-of select="@name" disable-output-escaping="yes"/>
                    <xsl:text>&#10;</xsl:text>
                    <xsl:copy-of select="./node()"/>
                    <xsl:text>&#10;</xsl:text>
                </xsl:for-each>
            </xsl:message> -->
        </xsl:for-each>

    </xsl:template>

    <!--print page-->
    <xsl:template match="page">
        <xsl:value-of select="@name" disable-output-escaping="yes"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <!--print figureGroup-->
    <xsl:template name="_name">
        <xsl:choose>
            <xsl:when test="not (count(rename)=0)">
                <xsl:value-of select="rename/@name" disable-output-escaping="yes"/>
                <xsl:text>&#32;&#32;</xsl:text>
                <xsl:value-of select="rename/text()" disable-output-escaping="yes"/>
            </xsl:when>
            <xsl:when test="not (count(name)=0)">
                <xsl:value-of select="rename/text" disable-output-escaping="yes"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="@name" disable-output-escaping="yes"/>
                <xsl:text>&#10;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!--print figureGroupItem-->
    <xsl:template name="_figureGroupItem">
        <xsl:for-each select="./*">
            <xsl:value-of select="name()" disable-output-escaping="yes"/>
            <xsl:if test="name()='property'">
                <xsl:text>-</xsl:text>
                <xsl:value-of select="@name"/>
            </xsl:if>
            <xsl:text>&#10;</xsl:text>
        </xsl:for-each>
    </xsl:template>

</xsl:stylesheet>
