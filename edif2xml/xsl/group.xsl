<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text"/>
    <xsl:param name="mode" select="'instance'"/>
    <!-- <xsl:param name="mode" select="'page,group,item,fg,library,cell,view,port,instance'"/> -->

    <!--root-->
    <xsl:template match="/">
        <xsl:apply-templates select="edif"/>
    </xsl:template>

    <!--edif-->
    <xsl:template match="edif">
        <xsl:variable name="cell_ref" select="design/cellref/@name"/>
        <xsl:variable name="library_ref" select="design/cellref/libraryref/@name"/>

        <!--page list-->
        <xsl:if test="contains($mode,'page')">
            <xsl:for-each select="(library|external)[@name=$library_ref]">
                <xsl:for-each select="cell[@name=$cell_ref]/view/contents">
                    <xsl:for-each select="page">
                        <xsl:value-of select="@name" disable-output-escaping="yes"/>
                        <xsl:text>&#10;</xsl:text>
                    </xsl:for-each>
                </xsl:for-each>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:if>

        <!--figureGroup list-->
        <xsl:if test="contains($mode,'group')">
            <xsl:for-each select="(library|external)[@name=$library_ref]">
                <xsl:for-each select="technology/figuregroup">
                    <xsl:value-of select="rename/text()|@name" disable-output-escaping="yes"/>
                    <xsl:text>&#10;</xsl:text>
                </xsl:for-each>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:if>

        <!--fogureGroup item list-->
        <xsl:if test="contains($mode,'item')">
            <xsl:for-each select="(library|external)[@name=$library_ref]">
                <xsl:for-each select="technology/figuregroup[1]/*">
                    <xsl:value-of select="name()" disable-output-escaping="yes"/>
                    <xsl:if test="name()='property'">
                        <xsl:text>-</xsl:text>
                        <xsl:value-of select="@name"/>
                    </xsl:if>
                    <xsl:text>&#10;</xsl:text>
                </xsl:for-each>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:if>

        <!--figureGroup list-->
        <xsl:if test="contains($mode,'fg')">
            <xsl:for-each select="(library|external)[@name=$library_ref]">
                <xsl:for-each select="technology/figuregroup">
                    <xsl:value-of select="rename/text()|@name" disable-output-escaping="yes"/>
                    <xsl:text>&#10;</xsl:text>
                    <xsl:for-each select="*">
                        <xsl:text>&#32;&#32;</xsl:text>
                        <xsl:value-of select="name()"/>
                        <xsl:text>&#32;=&#32;</xsl:text>
                        <xsl:value-of select="text()"/>
                        <xsl:copy-of select="./*"/>
                        <xsl:text>&#10;</xsl:text>
                    </xsl:for-each>
                    <xsl:text>&#10;</xsl:text>
                </xsl:for-each>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:if>

        <!--library list-->
        <xsl:if test="contains($mode,'library')">
            <xsl:for-each select="library|external">
                <xsl:variable name="library_name" select="rename/text()|@name"/>
                <xsl:value-of select="$library_name" disable-output-escaping="yes"/>
                <xsl:text>&#10;</xsl:text>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:if>

        <!--cell list-->
        <xsl:if test="contains($mode,'cell')">
            <xsl:for-each select="library|external">
                <xsl:variable name="library_name" select="rename/text()|@name"/>
                <xsl:for-each select="cell">
                    <xsl:variable name="cell_name" select="rename/text()|@name"/>
                    <xsl:variable name="view_name" select="rename/text()|@name"/>
                    <xsl:value-of select="$library_name" disable-output-escaping="yes"/>
                    <xsl:text>,</xsl:text>
                    <xsl:value-of select="$cell_name" disable-output-escaping="yes"/>
                    <xsl:text>&#10;</xsl:text>
                </xsl:for-each>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:if>

        <!--view list-->
        <xsl:if test="contains($mode,'view')">
            <xsl:for-each select="library|external">
                <xsl:variable name="library_name" select="rename/text()|@name"/>
                <xsl:for-each select="cell">
                    <xsl:variable name="cell_name" select="rename/text()|@name"/>
                    <xsl:for-each select="view">
                        <xsl:variable name="view_name" select="rename/text()|@name"/>
                        <xsl:value-of select="$library_name" disable-output-escaping="yes"/>
                        <xsl:text>,</xsl:text>
                        <xsl:value-of select="$cell_name" disable-output-escaping="yes"/>
                        <xsl:text>,</xsl:text>
                        <xsl:value-of select="$view_name" disable-output-escaping="yes"/>
                        <xsl:text>&#10;</xsl:text>
                    </xsl:for-each>
                </xsl:for-each>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:if>

        <!--port list-->
        <xsl:if test="contains($mode,'port2')">
            <xsl:for-each select="library|external">
                <xsl:variable name="library_name" select="rename/text()|@name"/>
                <xsl:for-each select="cell">
                    <xsl:variable name="cell_name" select="rename/text()|@name"/>
                    <xsl:for-each select="view">
                        <xsl:variable name="view_name" select="rename/text()|@name"/>
                        <xsl:for-each select="interface/port">
                            <xsl:variable name="port_name" select="rename/text()|@name"/>
                            <xsl:value-of select="$library_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$cell_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$view_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$port_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="designator" disable-output-escaping="yes"/>
                            <xsl:text>&#10;</xsl:text>
                        </xsl:for-each>
                    </xsl:for-each>
                </xsl:for-each>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:if>

        <!--portImplementation list-->
        <xsl:if test="contains($mode,'port')">
            <xsl:for-each select="library|external">
                <xsl:variable name="library_name" select="rename/text()|@name"/>
                <xsl:for-each select="cell">
                    <xsl:variable name="cell_name" select="rename/text()|@name"/>
                    <xsl:for-each select="view">
                        <xsl:variable name="view_name" select="rename/text()|@name"/>
                        <xsl:for-each select="interface/symbol/portimplementation">
                            <xsl:variable name="port_name" select="rename/text()|@name"/>
                            <xsl:variable name="pin_name" select="../../port[@name=$port_name]/designator"/>
                            <xsl:value-of select="$library_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$cell_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$view_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$port_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$pin_name" disable-output-escaping="yes"/>
                            <xsl:text>&#10;</xsl:text>
                        </xsl:for-each>
                    </xsl:for-each>
                </xsl:for-each>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:if>

        <!--instance list-->
        <!-- <xsl:if test="contains($mode,'instance')">
            <xsl:for-each select="(library|external)[@name=$library_ref]">
                <xsl:for-each select="cell[@name=$cell_ref]/view/contents">
                    <xsl:for-each select="page">
                        <xsl:value-of select="@name" disable-output-escaping="yes"/>
                        <xsl:text>&#10;</xsl:text>
                    </xsl:for-each>
                </xsl:for-each>
            </xsl:for-each>
        </xsl:if>
        <xsl:text>&#10;</xsl:text> -->

        <xsl:if test="contains($mode,'instance')">
            <xsl:for-each select="library|external">
                <xsl:variable name="library_name" select="rename/text()|@name"/>
                <xsl:for-each select="cell">
                    <xsl:variable name="cell_name" select="rename/text()|@name"/>
                    <xsl:for-each select="view">
                        <xsl:variable name="view_name" select="rename/text()|@name"/>
                        <xsl:for-each select=".//page">
                            <xsl:variable name="page_name" select="rename/text()|@name"/>
                            <xsl:for-each select=".//instance">
                                <xsl:variable name="name" select="rename/text()|@name"/>
                                <xsl:value-of select="$library_name" disable-output-escaping="yes"/>
                                <xsl:text>,</xsl:text>
                                <xsl:value-of select="$cell_name" disable-output-escaping="yes"/>
                                <xsl:text>,</xsl:text>
                                <xsl:value-of select="$view_name" disable-output-escaping="yes"/>
                                <xsl:text>,</xsl:text>
                                <xsl:value-of select="$page_name" disable-output-escaping="yes"/>
                                <xsl:text>,</xsl:text>
                                <xsl:value-of select="$name" disable-output-escaping="yes"/>
                                <xsl:text>&#10;</xsl:text>
                            </xsl:for-each>
                        </xsl:for-each>
                    </xsl:for-each>
                </xsl:for-each>
            </xsl:for-each>
            <xsl:text>&#10;</xsl:text>
        </xsl:if>

        <!--portImplementation list-->
        <!-- <xsl:if test="contains($mode,'port')">
            <xsl:for-each select="library|external">
                <xsl:variable name="library_name" select="rename/text()|@name"/>
                <xsl:for-each select="cell">
                    <xsl:variable name="cell_name" select="rename/text()|@name"/>
                    <xsl:for-each select="view">
                        <xsl:variable name="view_name" select="rename/text()|@name"/>
                        <xsl:for-each select="interface/symbol/portImplementation">
                            <xsl:variable name="port_name" select="rename/text()|@name"/>
                            <xsl:variable name="pin_name" select="../../port[@name=$port_name]/designator"/>
                            <xsl:value-of select="$library_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$cell_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$view_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$port_name" disable-output-escaping="yes"/>
                            <xsl:text>,</xsl:text>
                            <xsl:value-of select="$pin_name" disable-output-escaping="yes"/>
                            <xsl:text>&#10;</xsl:text>
                        </xsl:for-each>
                    </xsl:for-each>
                </xsl:for-each>
            </xsl:for-each>
        </xsl:if>
        <xsl:text>&#10;</xsl:text> -->

    </xsl:template>

    <!-- element name -->
    <!-- <xsl:template name="_name">
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
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template> -->

</xsl:stylesheet>
