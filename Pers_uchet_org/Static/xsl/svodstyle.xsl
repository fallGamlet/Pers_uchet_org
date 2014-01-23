<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" 
        xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
        xmlns="http://www.w3.org/1999/xhtml">
	<xsl:output method="xml" indent="yes"
	doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" 
	doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"/>

	<xsl:decimal-format name="rusformat" decimal-separator="." grouping-separator=" " NaN="-"/>

	<xsl:template match="/">
		<xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html></xsl:text>
	<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="ru" lang="ru">
		<HEAD>
			<meta http-equiv="Content-Type" content="text/html; charset=windows-1251" />
			<SCRIPT LANGUAGE="javascript"><xsl:comment><![CDATA[]]></xsl:comment></SCRIPT>
		</HEAD>
		<body style="font-family:'Arial';">
			<TABLE width="90%" align="center" border="0">
				<TR><TD>
						<TABLE width="100%" border="0">
							<TR>
								<TD style="font-size:8pt;">Форма СЗВ-3</TD>
							</TR>
							<TR>
								<TD align="center" style="font-size:10pt;font-weight:bold;color:#999;">Единый государственный фонд социального страхования<BR/>Приднестровской Молдавской Республики</TD>
							</TR>
							<TR>
								<TD align="center" style="font-size:12pt;font-weight:bold;">Сводная ведомость<br/>форм документов СЗВ-1, передаваемых страхователем в Фонд</TD>
							</TR>
						</TABLE>
						<BR/>

						<TABLE width="100%" border="0">
							<TR>
								<TD width="290px" style="font-size:10pt;font-weight:bold;" nowrap="true">Количество пакетов с документами СЗВ-1:</TD>
								<TD style="color:navy;font-weight:bold;"><xsl:value-of select="svod/packs_count"/></TD>
							</TR>
							<TR>
								<TD width="290px" style="font-size:10pt;font-weight:bold;" nowrap="true">Количество исходных документов СЗВ-1:</TD>
								<TD style="color:navy;font-weight:bold;"><xsl:value-of select="svod/docs_count"/></TD>
							</TR>
						</TABLE>
						<BR/>

						<TABLE width="100%" border="1" style="border:solid 1px #000; border-collapse:collapse;">
							<TR style="font-size:8pt; text-align:center; background:#ddd;">
								<TD width="5%" rowspan="2" style="border-color:black;">Месяц</TD>
								<TD width="17%" rowspan="2" style="border-color:black;">Сумма дохода,на который начислены страховые взносы</TD>
								<TD width="17%" rowspan="2" style="border-color:black;">Сумма выплат, учитываемых для назначения пенсии</TD>
								<TD width="17%" colspan="2" style="border-color:black;">Сумма страховых взносов</TD>
								<TD width="17%" rowspan="2" style="border-color:black;">Сумма обязательных страховых взносов</TD>
								<TD width="10%" rowspan="2" style="border-color:black;">Средняя численность работников (застрахованных лиц)</TD>
							</TR>
							<TR style="font-size:8pt;text-align:center;background:#ddd;">
								<TD width="17%" style="border-color:black;">начисленных<br/>страхователем</TD>
								<TD width="17%" style="border-color:black;">уплаченных<br/>страхователем</TD>
							</TR>
							<xsl:for-each select="svod/payment/month">
								<TR style="font-size:8pt;text-align:right;font-weight:bold;">
									<TD width="5%" style="text-align:center; border-color:black;"><xsl:number value="position()" format="1"/></TD>

									<xsl:choose>
										<xsl:when test="col_1=0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">-</TD>
										</xsl:when>
										<xsl:when test="col_1>0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_1, '# ##0.00', 'rusformat')"/></TD>
										</xsl:when>
										<xsl:otherwise>
											<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_1, '# ##0.00', 'rusformat')"/></TD>
										</xsl:otherwise>
									</xsl:choose>

									<xsl:choose>
										<xsl:when test="col_2=0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">-</TD>
										</xsl:when>
										<xsl:when test="col_2>0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_2, '# ##0.00', 'rusformat')"/></TD>
										</xsl:when>
										<xsl:otherwise>
											<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_2, '# ##0.00', 'rusformat')"/></TD>
										</xsl:otherwise>
									</xsl:choose>

									<xsl:choose>
										<xsl:when test="col_3=0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">-</TD>
										</xsl:when>
										<xsl:when test="col_3>0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_3, '# ##0.00', 'rusformat')"/></TD>
										</xsl:when>
										<xsl:otherwise>
											<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_3, '# ##0.00', 'rusformat')"/></TD>
										</xsl:otherwise>
									</xsl:choose>

									<xsl:choose>
										<xsl:when test="col_4=0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">-</TD>
										</xsl:when>
										<xsl:when test="col_4>0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_4, '# ##0.00', 'rusformat')"/></TD>
										</xsl:when>
										<xsl:otherwise>
											<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_4, '# ##0.00', 'rusformat')"/></TD>
										</xsl:otherwise>
									</xsl:choose>

									<xsl:choose>
										<xsl:when test="col_5=0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">-</TD>
										</xsl:when>
										<xsl:when test="col_5>0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_5, '# ##0.00', 'rusformat')"/></TD>
										</xsl:when>
										<xsl:otherwise>
											<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_5, '# ##0.00', 'rusformat')"/></TD>
										</xsl:otherwise>
									</xsl:choose>

									<xsl:choose>
										<xsl:when test="col_6=0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">-</TD>
										</xsl:when>
										<xsl:when test="col_6>0">
											<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_6, '# ##0', 'rusformat')"/></TD>
										</xsl:when>
										<xsl:otherwise>
											<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
												<xsl:value-of select="format-number(col_6, '# ##0', 'rusformat')"/></TD>
										</xsl:otherwise>
									</xsl:choose>
								</TR>
							</xsl:for-each>

							<TR style="font-size:8pt;text-align:right;font-weight:bold;color:black;background:#ddd;">
								<TD width="5%" style="border-color:black; text-align:center;">Итого:</TD>
								<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
									<xsl:value-of select="format-number(sum(svod/payment/month/col_1), '# ##0.00', 'rusformat')"/></TD>
								<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
									<xsl:value-of select="format-number(sum(svod/payment/month/col_2), '# ##0.00', 'rusformat')"/></TD>
								<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
									<xsl:value-of select="format-number(sum(svod/payment/month/col_3), '# ##0.00', 'rusformat')"/></TD>
								<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
									<xsl:value-of select="format-number(sum(svod/payment/month/col_4), '# ##0.00', 'rusformat')"/></TD>
								<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
									<xsl:value-of select="format-number(sum(svod/payment/month/col_5), '# ##0.00', 'rusformat')"/></TD>
								<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">-</TD>
							</TR>
						</TABLE>
					</TD></TR>
			</TABLE>
		</body>
	</html>
</xsl:template>
</xsl:stylesheet>
