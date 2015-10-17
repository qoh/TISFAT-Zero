<?xml version="1.0" encoding="UTF-8"?>

<!-- FragmentTemplate - 32.xslt -->
<!-- Version 1.0.0.0 -->
<!-- Latest Batch Version: 1.0.0.25 -->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:wix="http://schemas.microsoft.com/wix/2006/wi" xmlns="http://schemas.microsoft.com/wix/2006/wi" exclude-result-prefixes="wix">
    <xsl:include href="config_Template.xml"/>
    <xsl:output method="xml" encoding="UTF-8" indent="yes" />

    <!-- Mandatory; Starts the 'loop' over every tag in the file -->
	<xsl:template match="wix:Wix">
		<xsl:copy>
			<!-- The following enters the directive for adding the config.wxi include file to the dynamically generated file -->
			<!--xsl:processing-instruction name="include">$(sys.CURRENTDIR)wix\config.wxi</xsl:processing-instruction-->
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()" />
		</xsl:copy>
	</xsl:template>

	<!-- Add Win64 attribute and the shortcut tags to the Component that contains the .exe file definition -->

	<xsl:template match="wix:Component">
		<xsl:copy>
			<xsl:variable name="blah" select="wix:File/@Source"/>

			<xsl:apply-templates select="@*" />


			<xsl:attribute name="Win64">no</xsl:attribute>
			<xsl:choose>
				<xsl:when test="$blah=$ExeFileName">
                    <xsl:text>&#xd;&#x9;&#x9;&#x9;&#x9;</xsl:text>
                    <Shortcut Id="{$DesktopShortcutId}" Advertise="yes" Directory="DesktopFolder" Icon="{$DesktopShortcutIconName}" Name="{$DesktopShortcutName}" WorkingDirectory="INSTALLFOLDER" />
                    
                    <xsl:text>&#xd;&#x9;&#x9;&#x9;&#x9;</xsl:text>
                    <Shortcut Id="{$StartMenuShortcutId}" Advertise="yes" Directory="{$StartMenuFolderDirectoryId}" Icon="{$StartMenuShortcutIconName}" Name="{$StartMenuShortcutName}" WorkingDirectory="INSTALLFOLDER" />
                    <xsl:text>&#xd;&#x9;&#x9;&#x9;&#x9;</xsl:text>
					<RemoveFolder Id="RemoveStartMenuFolder" Directory="{$StartMenuFolderDirectoryId}" On="uninstall" />
                    <xsl:text>&#xd;</xsl:text>
				</xsl:when>
			</xsl:choose>
			

			<xsl:apply-templates select="node()" />
		</xsl:copy>
	</xsl:template>

</xsl:stylesheet>