<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:import href="common.xsl"/>
    <xsl:output method="text"/>
    <xsl:key name="vwl" match="//view" use="concat(../../@name,'-',../@name,'-',@name)"/>

    <xsl:template match="/">
        <xsl:text>library,cell,view,page,instance,name,@name,lref,cref,vref,desinator,pt,rot</xsl:text>
        <xsl:text>,_xs,_ys,_xe,_ye</xsl:text>
        <xsl:text>&#10;</xsl:text>
        <xsl:apply-templates select="edif/library/cell/view/contents//instance"/>
    </xsl:template>
    <xsl:template match="instance">
        <xsl:variable name="lref" select="viewref/cellref/libraryref/@name"/>
        <xsl:variable name="cref" select="viewref/cellref/@name"/>
        <xsl:variable name="vref" select="viewref/@name"/>
        <xsl:variable name="vw" select="key('vwl',concat($lref,'-',$cref,'-',$vref))"/>
        <xsl:variable name="pos" select="transform/origin/pt"/>
        <xsl:variable name="rot" select="transform/orientation"/>

        <xsl:value-of select="ancestor::library/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="ancestor::cell/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="ancestor::view/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="ancestor::page/@name"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="@name"/>

        <xsl:text>,</xsl:text>
        <xsl:apply-templates select="." mode="_name"/>

        <xsl:text>,</xsl:text>
        <xsl:value-of select="$lref"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="$cref"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="$vref"/>

        <xsl:text>,</xsl:text>
        <xsl:apply-templates select="designator" mode="_designator"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="transform/origin/pt"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="transform/orientation"/>
        <xsl:text>|</xsl:text>

        <xsl:for-each select="$vw">
            <xsl:call-template name="_i_xs">
                <xsl:with-param name="x" select="substring-before($pos,' ')"/>
                <xsl:with-param name="rot" select="$rot"/>
            </xsl:call-template>
        </xsl:for-each>
        <xsl:text>,</xsl:text>
        <xsl:for-each select="$vw">
            <xsl:call-template name="_i_ys">
                <xsl:with-param name="y" select="substring-after($pos,' ')"/>
                <xsl:with-param name="rot" select="$rot"/>
            </xsl:call-template>
        </xsl:for-each>
        <xsl:text>,</xsl:text>
        <xsl:for-each select="$vw">
            <xsl:call-template name="_i_xe">
                <xsl:with-param name="x" select="substring-before($pos,' ')"/>
                <xsl:with-param name="rot" select="$rot"/>
            </xsl:call-template>
        </xsl:for-each>
        <xsl:text>,</xsl:text>
        <xsl:for-each select="$vw">
            <xsl:call-template name="_i_ye">
                <xsl:with-param name="y" select="substring-after($pos,' ')"/>
                <xsl:with-param name="rot" select="$rot"/>
            </xsl:call-template>
        </xsl:for-each>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>

    <!--     ixs  iys  ixe  iye
        0     xs   ys   xe   ye
        90    ys  -xe   ye  -xs
        180  -xe  -ye  -xs  -ys
        270  -ye   xs  -ys   xe

        mx    xs  -ye   xe  -ys
        my90  ys   xs   ye   xe
        my   -xe   ys  -xs   ye
        mx90 -ye  -xe  -ys  -xs
     -->
    <xsl:template name="_i_xs">
        <xsl:param name="x" select="0"/>
        <xsl:param name="y" select="0"/>
        <xsl:param name="scx" select="1"/>
        <xsl:param name="scy" select="1"/>
        <xsl:param name="rot" select="'R0'"/>
        <xsl:variable name="v">
            <xsl:choose>
                <xsl:when test="$rot='R90' or $rot='MYR90'">
                    <xsl:call-template name="_ys"/>
                </xsl:when>
                <xsl:when test="$rot='R180' or $rot='MY'">
                    <xsl:call-template name="_xe"/>
                </xsl:when>
                <xsl:when test="$rot='R270' or $rot='MXR90'">
                    <xsl:call-template name="_ye"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:call-template name="_xs"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:choose>
            <xsl:when test="$rot='R180' or $rot='R270' or $rot='MY' or $rot='MXR90'">
                <xsl:value-of select="$x -$scx*$v"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$x +$scx*$v"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template name="_i_ys">
        <xsl:param name="x" select="0"/>
        <xsl:param name="y" select="0"/>
        <xsl:param name="scx" select="1"/>
        <xsl:param name="scy" select="1"/>
        <xsl:param name="rot" select="'R0'"/>
        <xsl:variable name="v">
            <xsl:choose>
                <xsl:when test="$rot='R90' or $rot='MXR90'">
                    <xsl:call-template name="_xe"/>
                </xsl:when>
                <xsl:when test="$rot='R180' or $rot='MX'">
                    <xsl:call-template name="_ye"/>
                </xsl:when>
                <xsl:when test="$rot='R270' or $rot='MYR90'">
                    <xsl:call-template name="_xs"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:call-template name="_ys"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:choose>
            <xsl:when test="$rot='R90' or $rot='R180' or $rot='MX' or $rot='MXR90'">
                <xsl:value-of select="$y -$scy*$v"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$y +$scy*$v"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template name="_i_xe">
        <xsl:param name="x" select="0"/>
        <xsl:param name="y" select="0"/>
        <xsl:param name="scx" select="1"/>
        <xsl:param name="scy" select="1"/>
        <xsl:param name="rot" select="'R0'"/>
        <xsl:variable name="v">
            <xsl:choose>
                <xsl:when test="$rot='R90' or $rot='MYR90'">
                    <xsl:call-template name="_ye"/>
                </xsl:when>
                <xsl:when test="$rot='R180' or $rot='MY'">
                    <xsl:call-template name="_xs"/>
                </xsl:when>
                <xsl:when test="$rot='R270' or $rot='MXR90'">
                    <xsl:call-template name="_ys"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:call-template name="_xe"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:choose>
            <xsl:when test="$rot='R180' or $rot='R270' or $rot='MY' or $rot='MXR90'">
                <xsl:value-of select="$x -$scx*$v"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$x +$scx*$v"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template name="_i_ye">
        <xsl:param name="x" select="0"/>
        <xsl:param name="y" select="0"/>
        <xsl:param name="scx" select="1"/>
        <xsl:param name="scy" select="1"/>
        <xsl:param name="rot" select="'R0'"/>
        <xsl:variable name="v">
            <xsl:choose>
                <xsl:when test="$rot='R90' or $rot='MXR90'">
                    <xsl:call-template name="_xs"/>
                </xsl:when>
                <xsl:when test="$rot='R180' or $rot='MX'">
                    <xsl:call-template name="_ys"/>
                </xsl:when>
                <xsl:when test="$rot='R270' or $rot='MYR90'">
                    <xsl:call-template name="_xe"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:call-template name="_ye"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:choose>
            <xsl:when test="$rot='R90' or $rot='R180' or $rot='MX' or $rot='MXR90'">
                <xsl:value-of select="$y -$scy*$v"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$y +$scy*$v"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- area: xs,ys,xe,ye -->
    <xsl:template name="_xs">
        <xsl:for-each select=".//pt">
            <xsl:sort select="substring-before(.,' ')" data-type="number"/>
            <xsl:if test="position()=1">
                <xsl:value-of select="substring-before(.,' ')"/>
            </xsl:if>
        </xsl:for-each>
    </xsl:template>
    <xsl:template name="_xe">
        <xsl:for-each select=".//pt">
            <xsl:sort select="substring-before(.,' ')" data-type="number" order="descending"/>
            <xsl:if test="position()=1">
                <xsl:value-of select="substring-before(.,' ')"/>
            </xsl:if>
        </xsl:for-each>
    </xsl:template>
    <xsl:template name="_ys">
        <xsl:for-each select=".//pt">
            <xsl:sort select="substring-after(.,' ')" data-type="number"/>
            <xsl:if test="position()=1">
                <xsl:value-of select="substring-after(.,' ')"/>
            </xsl:if>
        </xsl:for-each>
    </xsl:template>
    <xsl:template name="_ye">
        <xsl:for-each select=".//pt">
            <xsl:sort select="substring-after(.,' ')" data-type="number" order="descending"/>
            <xsl:if test="position()=1">
                <xsl:value-of select="substring-after(.,' ')"/>
            </xsl:if>
        </xsl:for-each>
    </xsl:template>

</xsl:stylesheet>
