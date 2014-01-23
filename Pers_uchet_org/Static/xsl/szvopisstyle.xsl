<?xml version="1.0" encoding="Windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:decimal-format name="rusformat" decimal-separator="." grouping-separator=" " NaN="-"/>

<xsl:template match="/">
	<html>
	<HEAD>
	<SCRIPT LANGUAGE="javascript"><xsl:comment><![CDATA[
		]]></xsl:comment></SCRIPT>
	</HEAD>
	<body style="font-family:'Arial';">
	<TABLE width="90%" align="center" border="0">
	<TR><TD>

	<TABLE width="100%" border="0">
		<TR>
			<TD style="font-size:8pt;">����� ���-2</TD>
		</TR>
		<TR>
			<TD align="center" style="font-size:10pt;font-weight:bold;color:gray;">
				������ ��������������� ���� ����������� ����������� ��������������� ���������� ����������
			</TD>
		</TR>
		<TR>
			<TD></TD>
		</TR>
		<TR>
			<TD align="center" style="font-size:12pt;font-weight:bold;">
				����� ����������, ���������� ������������� � ��</TD>
		</TR>
	</TABLE>
	<BR/>

	<TABLE width="100%" border="1" style="border-color:black;">
		<TR style="font-size:8pt;text-align:center;background-color:silver;">
			<TD width="70%" style="border-color:black;">
				��� ����� ��������� &quot;�������������� ��������&quot;<br/>
				&#40;����� ���-1&#41;</TD>
			<TD width="30%" style="border-color:black;">
				���������� ����������<br/>������� ������������ � ������</TD>
		</TR>
		<xsl:for-each select="inddocs/inddoc">
		<TR style="font-size:8pt;text-align:right;font-weight:bold;">
		<xsl:choose>
		<xsl:when test="type_id=21">
			<TD width="70%" style="font-size:10pt;text-align:left;color:navy;font-weight:bold;border-color:black;">
				&#45; �������� �����</TD>
			<TD width="30%" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(count, '##0', 'rusformat')"/></TD>
		</xsl:when>black black black black
		<xsl:when test="type_id=22">
			<TD width="70%" style="font-size:10pt;text-align:left;color:navy;font-weight:bold;border-color:black;">
				&#45; �������������� �����</TD>
			<TD width="30%" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(count, '##0', 'rusformat')"/></TD>
		</xsl:when>
		<xsl:when test="type_id=23">
			<TD width="70%" style="font-size:10pt;text-align:left;color:navy;font-weight:bold;border-color:black;">
				&#45; ���������� �����</TD>
			<TD width="30%" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(count, '##0', 'rusformat')"/></TD>
		</xsl:when>
		<xsl:when test="type_id=24">
			<TD width="70%" style="font-size:10pt;text-align:left;color:navy;font-weight:bold;border-color:black;">
				&#45; ���������� ������</TD>
			<TD width="30%" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(count, '##0', 'rusformat')"/></TD>
		</xsl:when>
		</xsl:choose>
		</TR>
		</xsl:for-each>
	</TABLE>
	<BR/>

	<TABLE width="100%" border="0">
		<TR>
			<TD align="center" style="font-size:12pt;font-weight:bold;">
				�������� � ������ �� �������� ������<br/>
				&#40;����� �� ������&#41;</TD>
		</TR>
	</TABLE>
	<BR/>

	<TABLE width="100%" border="1" style="border-color:black;">
		<TR style="font-size:8pt;text-align:center;background-color:silver;">
			<TD rowspan="2" width="25%" style="border-color:black;">
				��� �����</TD>
			<TD rowspan="2" width="15%" style="border-color:black;">
				����� ������, ��<br/>�������<br/>���������<br/>��������� ������</TD>
			<TD rowspan="2" width="15%" style="border-color:black;">
				����� ������,<br/>���������� ���<br/>���������� ������</TD>
			<TD colspan="2" width="30%" style="border-color:black;">
				����� <br/>��������� �������</TD>
			<TD rowspan="2" width="15%" style="border-color:black;">
				�����<br/>������������,<br/>��������� �������</TD>
		</TR>
		<TR style="font-size:8pt;text-align:center;background-color:silver;">
			<TD width="15%" style="border-color:black;">
				�����������<br/>�������������</TD>
			<TD width="15%" style="border-color:black;">
				����������<br/>�������������</TD>
		</TR>
		<xsl:for-each select="inddocs/inddoc">
		<TR style="font-size:8pt;text-align:right;font-weight:bold;">
		<xsl:choose>
		<xsl:when test="type_id=21">
			<TD width="25%" style="font-size:10pt;text-align:left;color:navy;font-weight:bold;border-color:black;">
				��������</TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_1, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_2, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_3, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_4, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_5, '# ##0.00', 'rusformat')"/></TD>
		</xsl:when>
 		<xsl:when test="type_id=22">
			<TD width="25%" style="font-size:10pt;text-align:left;color:navy;font-weight:bold;border-color:black;">
				��������������</TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_1, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_2, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_3, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_4, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_5, '# ##0.00', 'rusformat')"/></TD>
		</xsl:when>
		<xsl:when test="type_id=23">
			<TD width="25%" style="font-size:10pt;text-align:left;color:navy;font-weight:bold;border-color:black;">
				����������</TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_1, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_2, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_3, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_4, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_5, '# ##0.00', 'rusformat')"/></TD>
		</xsl:when>
		<xsl:when test="type_id=24">
			<TD width="25%" style="font-size:10pt;text-align:left;color:navy;font-weight:bold;border-color:black;">
				���������� ������</TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_1, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_2, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_3, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_4, '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;text-align:center;color:navy;font-weight:bold;border-color:black;">
				<xsl:value-of select="format-number(summary_info/col_5, '# ##0.00', 'rusformat')"/></TD>
		</xsl:when>
		</xsl:choose>
		</TR>
		</xsl:for-each>
	</TABLE>

	</TD></TR>
	</TABLE>
	</body>
	</html>
</xsl:template>
</xsl:stylesheet>
