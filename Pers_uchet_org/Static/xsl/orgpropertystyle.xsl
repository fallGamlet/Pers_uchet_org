<?xml version="1.0" encoding="Windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
	<DIV style="width:100%; background:#EEE; padding:3px 15px; margin:0;">
		<xsl:for-each select="Properties">
			<DIV style="font-size:14px; font-weight:bold; text-align:center; margin:0px auto 5px; color:blue;">
				<SPAN style="margin:auto 10px;">
					<xsl:value-of select="Organization_Name" />
				</SPAN>
				<SPAN style="margin:auto 10px;">
					<xsl:value-of select="Organization_Regid" />
				</SPAN>
			</DIV>
			<TABLE class="org_properties" style="color:#000; font-family:arial; font-size:12px; margin:auto 0; padding:15px; border-collapse:collapse;">
				<COLGROUP>
					<COL style="width:280px; vertical-align:top;"/>
					<COL style="vertical-align:top; font-weight:bold;"/>
				</COLGROUP>
				<TR>
					<TD>Отчетный год</TD>
					<TD><xsl:value-of select="Report_Year" /></TD>
				</TR>
				<TR>
					<TD><xsl:value-of select="Director_Type" /></TD>
					<TD><xsl:value-of select="Director_FIO" /></TD>
				</TR>
				<TR>
					<TD>Бухгалтер</TD>
					<TD><xsl:value-of select="Bookkeeper_FIO" /></TD>
				</TR>
				<TR>
					<TD>Подготовил</TD>
					<TD><xsl:value-of select="Operator_Name" /> <SPAN style="padding-left:10px;"><xsl:value-of select="Date_of_Construction" /></SPAN></TD>
				</TR>
				<TR>
					<TD>Версия контейнера</TD>
					<TD><xsl:value-of select="Version" /></TD>
				</TR>
				<TR>
					<TD>Наименование программы</TD>
					<TD><xsl:value-of select="Program_Name" /> <SPAN style="padding-left:10px;"><xsl:value-of select="Program_Version" /></SPAN></TD>
				</TR>
			</TABLE>
		</xsl:for-each>
	</DIV>
</xsl:template>
</xsl:stylesheet>
