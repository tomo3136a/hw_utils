<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:import href="common.xsl"/>
    <xsl:output method="text"/>
    <xsl:param name="page" select="true()"/>
    <xsl:param name="refs" select="true()"/>

    <xsl:key name="vw" match="library/cell/view" 
        use="concat(ancestor::library/@name,'-',ancestor::cell/@name,'-',@name)"/>

    <xsl:template match="/">
        <xsl:apply-templates select="edif"/>
    </xsl:template>

    <xsl:template match="edif">
        <xsl:variable name="lname" select="design/cellref/libraryref/@name"/>
        <xsl:variable name="cname" select="design/cellref/@name"/>
        <xsl:apply-templates
            select="library[@name=$lname]/cell[@name=$cname]/view/contents"/>
    </xsl:template>

    <xsl:template match="contents">
        <xsl:variable name="header"
            select="page[@name=$page]/instance/property[not(@name=preceding::property/@name)]"/>
        <!-- <xsl:variable name="header"> -->
            <xsl:for-each select="page[@name=$page]/instance/property[not(@name=preceding::property/@name)]">
                <xsl:sort select="@name"/>
                <xsl:copy-of select="."/>
            </xsl:for-each>
        <!-- </xsl:variable> -->
        <!--header-->
        <xsl:text>id,name,designator</xsl:text>
        <xsl:for-each select="$header">
            <xsl:sort select="@name"/>
            <xsl:text>,</xsl:text>
            <xsl:call-template name="_pprint">
                <xsl:with-param name="s" select="@name"/>
            </xsl:call-template>
        </xsl:for-each>
        <xsl:text>&#10;</xsl:text>
        <!--data-->
        <xsl:for-each select="page[@name=$page]/instance">
            <xsl:variable name="line" select="."/>
            <xsl:variable name="vwn" select="concat(viewref/cellref/libraryref/@name,
                '-',viewref/cellref/@name,'-',viewref/@name)"/>
            <xsl:call-template name="_pprint">
                <xsl:with-param name="s" select="@name"/>
            </xsl:call-template>
            <xsl:text>,</xsl:text>
            <xsl:apply-templates select="rename"/>
            <xsl:text>,</xsl:text>
            <xsl:apply-templates select="designator"/>
            <xsl:text>,</xsl:text>
            <xsl:value-of select="$vwn"/>
            <xsl:text>,</xsl:text>
            <xsl:apply-templates select="key('vw',$vwn)"/>
            <xsl:text>,</xsl:text>
            <xsl:for-each select="$header">
                <xsl:sort select="@name"/>
                <xsl:variable name="kw" select="@name"/>
                <xsl:text>,</xsl:text>
                <xsl:for-each select="$line">
                    <xsl:variable name="prop" select="property[@name=$kw]"/>
                    <xsl:text>&quot;</xsl:text>
                    <xsl:call-template name="_pprint">
                        <xsl:with-param name="s" select="normalize-space($prop/number/text())"/>
                    </xsl:call-template>
                    <xsl:call-template name="_pprint">
                        <xsl:with-param name="s" select="normalize-space($prop/integer/text())"/>
                    </xsl:call-template>
                    <xsl:call-template name="_pprint">
                        <xsl:with-param name="s" select="normalize-space($prop/string/text())"/>
                    </xsl:call-template>
                    <xsl:call-template name="_pprint">
                        <xsl:with-param name="s" select="normalize-space($prop/text())"/>
                    </xsl:call-template>
                    <xsl:text>&quot;</xsl:text>
                </xsl:for-each>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:for-each>
    </xsl:template>

    <xsl:template match="view">
        <xsl:value-of select="@name"/>
        <xsl:call-template name="_pprint">
            <xsl:with-param name="s" select="text()"/>
        </xsl:call-template>
    </xsl:template>

    <!-- <xsl:for-each select=".//viewref[generate-id()=generate-id(key('refs',translate(concat(cellref/libraryref/@name,'-',cellref/@name,'-',@name),$lowercase,$uppercase))[1])]"> -->

    <!--common-->
    <xsl:template match="rename">
        <xsl:call-template name="_pprint">
            <xsl:with-param name="s" select="text()"/>
        </xsl:call-template>
    </xsl:template>

    <xsl:template match="designator">
        <xsl:if test="string-length(normalize-space(text()))!=0">
            <xsl:call-template name="_pprint">
                <xsl:with-param name="s" select="text()"/>
            </xsl:call-template>
        </xsl:if>
        <xsl:apply-templates select="stringdisplay"/>
    </xsl:template>

    <xsl:template match="stringdisplay">
        <xsl:call-template name="_pprint">
            <xsl:with-param name="s" select="text()"/>
        </xsl:call-template>
    </xsl:template>

    <xsl:template match="property">
        <xsl:text>&#32;</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>
        <!-- <xsl:call-template name="_name"/> -->
        <xsl:text>=</xsl:text>
        <xsl:call-template name="_value"/>
    </xsl:template>

</xsl:stylesheet>
