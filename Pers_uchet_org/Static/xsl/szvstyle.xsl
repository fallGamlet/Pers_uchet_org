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
			<TD style="font-size:8pt;">Форма СЗВ-1-1</TD>
		</TR>
		<TR>
			<TD align="center" style="font-size:10pt;font-weight:bold;color:gray;">
				Государственный Пенсионный Фонд Приднестровской Молдавской Республики</TD>
		</TR>
		<TR>
			<TD></TD>
		</TR>
		<TR>
			<TD align="center" style="font-size:12pt;font-weight:bold;">
				Индивидуальные сведения о заработке(доходе) и стаже<BR/>застрахованного лица</TD>
		</TR>
	</TABLE>
	<BR/>

	<TABLE width="100%" border="0">
		<TR>
			<TD width="40%" style="font-size:10pt;font-weight:bold;" nowrap="true">Фамилия </TD>
			<TD style="color:navy;font-weight:bold;"><xsl:value-of select="doc_info/person/lname"/></TD>
			<TD rowspan="5" width="140" align="right" style="vertical-align:top;">
			<TABLE width="130" border="1" style="border-color:black black black black;">
				<TR align="center">
				<TD style="border-color:black black black black;">
					<P style="font-size:10pt;background-color:silver;">Тип формы</P></TD>
				</TR>
				<TR>
				<TD>
					<TABLE border="0" style="font-size:8pt;">
					<TR>
						<xsl:choose>
						<xsl:when test="doc_info/formtype='21'">
							<TD width="8" height="6" style="font-weight:bold;">x</TD>
							<TD style="font-weight:bold;">исходная</TD>
						</xsl:when>
						<xsl:otherwise>
							<TD width="8" height="6"></TD>
							<TD>исходная</TD>
						</xsl:otherwise>
						</xsl:choose>
					</TR></TABLE>
				</TD>
				</TR>
				<TR>
				<TD>
					<TABLE border="0" style="font-size:8pt;"><TR>
						<xsl:choose>
						<xsl:when test="doc_info/formtype='22'">
							<TD width="8" height="6" style="font-weight:bold;">x</TD>
							<TD style="font-weight:bold;">корректирующая</TD>
						</xsl:when>
						<xsl:otherwise>
							<TD width="8" height="6"></TD>
							<TD>корректирующая</TD>
						</xsl:otherwise>
						</xsl:choose>
					</TR></TABLE>
				</TD>
				</TR>
				<TR>
				<TD>
					<TABLE border="0" style="font-size:8pt;"><TR>
						<xsl:choose>
						<xsl:when test="doc_info/formtype='23'">
							<TD width="6" height="6" style="font-weight:bold;">x</TD>
							<TD style="font-weight:bold;">отменяющая</TD>
						</xsl:when>
						<xsl:otherwise>
							<TD width="6" height="6"></TD>
							<TD>отменяющая</TD>
						</xsl:otherwise>
						</xsl:choose>
					</TR></TABLE>
				</TD>
				</TR>
				<TR>
				<TD>
					<TABLE border="0" style="font-size:8pt;"><TR>
						<xsl:choose>
						<xsl:when test="doc_info/formtype='24'">
							<TD width="6" height="6" style="font-weight:bold;">x</TD>
							<TD style="font-weight:bold;">назначение пенсии</TD>
						</xsl:when>
						<xsl:otherwise>
							<TD width="6" height="6"></TD>
							<TD>назначение пенсии</TD>
						</xsl:otherwise>
						</xsl:choose>
					</TR></TABLE>
				</TD>
				</TR>
			</TABLE>
			</TD>
		</TR>
		<TR>
			<TD width="40%" style="font-size:10pt;font-weight:bold;" nowrap="true">Имя </TD>
			<TD style="color:navy;font-weight:bold;"><xsl:value-of select="doc_info/person/fname"/></TD>
		</TR>
		<TR>
			<TD width="40%" style="font-size:10pt;font-weight:bold;" nowrap="true">Отчество </TD>
			<TD style="color:navy;font-weight:bold;"><xsl:value-of select="doc_info/person/mname"/></TD>
		</TR>
		<TR>
			<TD width="40%" style="font-size:10pt;font-weight:bold;" nowrap="true">Номер лицевого счёта </TD>
			<TD style="color:navy;font-weight:bold;"><xsl:value-of select="doc_info/person/regnum"/></TD>
		</TR>
		<TR>
			<TD width="40%" style="font-size:10pt;font-weight:bold;" nowrap="true">Гражданство</TD>
			<TD>
			<TABLE border="0" width="500">
				<TR>
				<TD width="5%" style="font-size:10pt;font-weight:bold;">1.</TD>
				<TD width="45%" style="color:navy;font-weight:bold;">
					<xsl:value-of select="doc_info/person/citizen/first/name"/>
				</TD>
				<TD width="5%" style="font-size:10pt;font-weight:bold;">2.</TD>
				<TD width="45%" style="color:navy;font-weight:bold;">
					<xsl:value-of select="doc_info/person/citizen/second/name"/>
				</TD>
				</TR>
			</TABLE>
			</TD>
		</TR>
		</TABLE>

	<TABLE width="100%" border="0">
		<TR>
			<TD width="40%" style="font-size:10pt;font-weight:bold;" nowrap="true">Код категории застрахованного лица</TD>
			<TD style="color:navy;font-weight:bold;">
				<xsl:value-of select="doc_info/person/category_name"/>
				<xsl:choose>
				<xsl:when test="doc_info/person/privelege_id>0">
					<xsl:text> / </xsl:text>
					<xsl:value-of select="doc_info/person/privelege_name"/>
				</xsl:when>
				</xsl:choose>
			</TD>
			<TD rowspan="4" width="140" align="right" style="vertical-align:top;">
			<TABLE width="130" border="1" style="border-color:black black black black;">
				<TR align="center">
				<TD style="border-color:black black black black;">
					<P style="font-size:10pt;background-color:silver;">Место работы</P></TD>
				</TR>
				<TR>
				<TD>
					<TABLE border="0" style="font-size:8pt;"><TR>
						<xsl:choose>
						<xsl:when test="doc_info/work_place='1'">
							<TD width="6" height="6" style="font-weight:bold;">x</TD>
							<TD style="font-weight:bold;">основное</TD>
						</xsl:when>
						<xsl:otherwise>
							<TD width="6" height="6"></TD>
							<TD>Основное</TD>
						</xsl:otherwise>
						</xsl:choose>
					</TR></TABLE>
				</TD>
				</TR>
				<TR>
				<TD>
					<TABLE border="0" style="font-size:8pt;"><TR>
						<xsl:choose>
						<xsl:when test="doc_info/work_place='2'">
							<TD width="6" height="6" style="font-weight:bold;">x</TD>
							<TD style="font-weight:bold;">неосновное</TD>
						</xsl:when>
						<xsl:otherwise>
							<TD width="6" height="6"></TD>
							<TD>Не основное</TD>
						</xsl:otherwise>
						</xsl:choose>
					</TR></TABLE>
				</TD>
				</TR>
			</TABLE>
			</TD>
		</TR>
		<TR>
			<TD colspan="2" style="font-size:10pt;font-weight:bold;">Сведения о работодателе</TD>
		</TR>
		<TR>
			<TD style="font-size:10pt;">.....Регистрационный номер в ПФ</TD>
			<TD style="color:navy;font-weight:bold;">
				<xsl:value-of select="doc_info/firm/regnum"/>
			</TD>
		</TR>
		<TR>
			<TD style="font-size:10pt;">.....Наименование</TD>
			<TD style="color:navy;font-weight:bold;">
				<xsl:value-of select="doc_info/firm/name"/>
			</TD>
		</TR>
	</TABLE>

	<TABLE width="100%" border="0">
		<TR>
			<TD width="65%" style="font-size:10pt;">
				Сумма начисленных страховых взносов, уплачиваемых работодателем:</TD>
			<TD align="right" style="font-size:10pt;color:navy;font-weight:bold;">
				<xsl:value-of select="doc_info/firm_add"/>
			</TD>
			<TD rowspan="2" width="140" align="right" style="vertical-align:top;">
			<TABLE width="130" border="1" style="border-color:black black black black;">
				<TR align="center">
				<TD style="border-color:black black black black;">
					<P style="font-size:10pt;background-color:silver;">Отчётный период</P></TD>
				</TR>
				<TR>
				<TD align="center" style="font-size:8pt;font-weight:bold;">
					<xsl:value-of select="doc_info/rep_year"/>
				</TD>
				</TR>
			</TABLE>
			</TD>
		</TR>
		<TR>
			<TD width="65%" style="font-size:10pt;">
				уплачиваемых из заработка застрахованного лица (обязательные страховые взносы с граждан):
				</TD>
			<TD align="right" style="font-size:10pt;color:navy;font-weight:bold;">
				<xsl:value-of select="doc_info/firm_pay"/>
			</TD>
		</TR>
	</TABLE>
	<BR/>
	
	<!-- Данные об общем стаже в отчетном году -->
	<TABLE width="100%" border="1" style="border-color:black black black black;">
		<TR style="font-size:8pt;text-align:center;background-color:silver;">
			<TD width="5%" rowspan="2" style="border-color:black black black black;">
				Месяц</TD>
			<TD width="15%" rowspan="2" style="border-color:black black black black;">
				Сумма заработка<br/>(дохода),на который<br/>начислены страховые<br/>взносы</TD>
			<TD width="15%" rowspan="2" style="border-color:black black black black;">
				Сумма выплат,<br/>учитываемых для<br/>назначения пенсии</TD>
			<TD width="15%" colspan="2" style="border-color:black black black black;">
				Сумма страховых взносов</TD>
			<TD width="15%" rowspan="2" style="border-color:black black black black;">
				Сумма обязательных<br/>страховых взносов,<br/>уплачиваемых из<br/>заработка</TD>
			<TD width="5%" rowspan="2" style="border-color:black black black black;">
				Всего<br/>полных<br/>дней для<br/>общего<br/>стажа</TD>
		</TR>
		<TR style="font-size:8pt;text-align:center;background-color:silver;">
			<TD style="border-color:black black black black;">начисленных<br/>работодателем</TD>
			<TD style="border-color:black black black black;">уплаченных<br/>работодателем</TD>
		</TR>
		<xsl:for-each select="doc_info/payment/month">
		<TR style="font-size:8pt;text-align:right;font-weight:bold;">
			<TD width="5%" style="border-color:black black black black;">
				<xsl:number value="position()" format="1. "/>
			</TD>

			<xsl:choose>
			<xsl:when test="col_1=0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
			</xsl:when>
			<xsl:when test="col_1>0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_1, '# ##0.00', 'rusformat')"/></TD>
			</xsl:when>
			<xsl:otherwise>
				<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_1, '# ##0.00', 'rusformat')"/></TD>
			</xsl:otherwise>
			</xsl:choose>

			<xsl:choose>
			<xsl:when test="col_2=0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
			</xsl:when>
			<xsl:when test="col_2>0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_2, '# ##0.00', 'rusformat')"/></TD>
			</xsl:when>
			<xsl:otherwise>
				<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_2, '# ##0.00', 'rusformat')"/></TD>
			</xsl:otherwise>
			</xsl:choose>

			<xsl:choose>
			<xsl:when test="col_3=0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
			</xsl:when>
			<xsl:when test="col_3>0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_3, '# ##0.00', 'rusformat')"/></TD>
			</xsl:when>
			<xsl:otherwise>
				<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_3, '# ##0.00', 'rusformat')"/></TD>
			</xsl:otherwise>
			</xsl:choose>

			<xsl:choose>
			<xsl:when test="col_4=0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
			</xsl:when>
			<xsl:when test="col_4>0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_4, '# ##0.00', 'rusformat')"/></TD>
			</xsl:when>
			<xsl:otherwise>
				<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_4, '# ##0.00', 'rusformat')"/></TD>
			</xsl:otherwise>
			</xsl:choose>

			<xsl:choose>
			<xsl:when test="col_5=0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
			</xsl:when>
			<xsl:when test="col_5>0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_5, '# ##0.00', 'rusformat')"/></TD>
			</xsl:when>
			<xsl:otherwise>
				<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_5, '# ##0.00', 'rusformat')"/></TD>
			</xsl:otherwise>
			</xsl:choose>

			<xsl:choose>
			<xsl:when test="col_6=0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
			</xsl:when>
			<xsl:when test="col_6>0">
				<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_6, '# ##0', 'rusformat')"/></TD>
			</xsl:when>
			<xsl:otherwise>
				<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(col_6, '# ##0', 'rusformat')"/></TD>
			</xsl:otherwise>
			</xsl:choose>
		</TR>
		</xsl:for-each>

		<TR style="font-size:8pt;text-align:right;font-weight:bold;color:black;background-color:silver;">
			<TD width="5%" style="border-color:black black black black;">Итого:</TD>
			<TD nowrap="yes" style="font-size:10pt;color:black;background-color:silver;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(sum(doc_info/payment/month/col_1), '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;color:black;background-color:silver;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(sum(doc_info/payment/month/col_2), '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;color:black;background-color:silver;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(sum(doc_info/payment/month/col_3), '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;color:black;background-color:silver;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(sum(doc_info/payment/month/col_4), '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;color:black;background-color:silver;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(sum(doc_info/payment/month/col_5), '# ##0.00', 'rusformat')"/></TD>
			<TD nowrap="yes" style="font-size:10pt;color:black;background-color:silver;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="format-number(sum(doc_info/payment/month/col_6), '# ##0', 'rusformat')"/></TD>
		</TR>
	</TABLE>
	<BR/>

	<!-- Периоды работы в отчётном году -->
	<TABLE width="100%" border="0" style="font-size:8pt;">
		<TR>
			<TD colspan="3">Данные о стаже в отчётном году:</TD>
		</TR>
		<TR>
			<TD nowrap="true" width="7%">№ п/п</TD>
			<TD nowrap="true" width="10%">Начало</TD>
			<TD nowrap="true">Конец</TD>
		</TR>
		<xsl:for-each select="doc_info/gen_period/period">
		<xsl:sort select="gen_start" data-type="number" order="ascending"/>
		<xsl:sort select="gen_end" data-type="number" order="ascending"/>
		<TR>
			<TD style="font-size:10pt;color:navy;font-weight:bold;">
				<xsl:number value="position()" format="1. "/></TD>
			<TD style="font-size:10pt;color:navy;font-weight:bold;">
				<xsl:value-of select="gen_start"/></TD>
			<TD style="font-size:10pt;color:navy;font-weight:bold;">
				<xsl:value-of select="gen_end"/></TD>
		</TR>
		</xsl:for-each>
	</TABLE>

	<!-- Специальный стаж работы в отчётном году -->
	<TABLE width="100%" border="0">
		<TR>
			<TD colspan="8" style="font-size:8pt;font-weight:bold;">
				Специальный стаж работы за отчётный период
			</TD>
		</TR>
	</TABLE>
	<TABLE width="100%" border="1" style="border-color:black black black black;">
		<TR style="font-size:8pt;text-align:center;background-color:silver;">
			<TD width="5%" rowspan="2" style="border-color:black black black black;">№<br/>п/п</TD>
			<TD width="15%" rowspan="2" style="border-color:black black black black;">Начало периода<br/>(дд.мм.гггг)</TD>
			<TD width="15%" rowspan="2" style="border-color:black black black black;">Конец периода<br/>(дд.мм.гггг)</TD>
			<TD width="15%" rowspan="2" style="border-color:black black black black;">Особые<br/>условия труда<br/>(код)</TD>
			<TD width="15%" colspan="2" style="border-color:black black black black;">Исчисляемый трудовой стаж</TD>
			<TD width="15%" colspan="2" style="border-color:black black black black;">Выслуга лет</TD>
		</TR>
		<TR style="font-size:8pt;text-align:center;background-color:silver;">
			<TD style="border-color:black black black black;">основание<br/>(код)</TD>
			<TD style="border-color:black black black black;">дополнительные<br/>сведения</TD>
			<TD style="border-color:black black black black;">основание<br/>(код)</TD>
			<TD style="border-color:black black black black;">дополнительные<br/>сведения</TD>
		</TR>
		<xsl:for-each select="doc_info/spec_staj/spec">
		<xsl:sort select="start_date" data-type="number" order="ascending"/>
		<xsl:sort select="end_date" data-type="number" order="ascending"/>
		<TR style="font-size:8pt;text-align:right;font-weight:bold;">
			<TD style="border-color:black black black black;"><xsl:number value="position()" format="1. "/></TD>

			<xsl:choose>
			<xsl:when test="start_date=''">
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
			</xsl:when>
			<xsl:otherwise>
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="start_date"/></TD>
			</xsl:otherwise>
			</xsl:choose>


			<xsl:choose>
			<xsl:when test="end_date=''">
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
			</xsl:when>
			<xsl:otherwise>
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:value-of select="end_date"/>
				</TD>
			</xsl:otherwise>
			</xsl:choose>


			<!-- Особые условия труда + Исчисляемый трудовой стаж -->
			<xsl:choose>
			<xsl:when test="part_condition_id>0">
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:value-of select="part_condition_name"/>
				</TD>
				<xsl:choose>
				<xsl:when test="staj_base_id>0">
					<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
						<xsl:value-of select="staj_base_name"/>
					</TD>
					<TD width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:choose>
					<xsl:when test="smonths>0">
						<xsl:value-of select="smonths"/><xsl:text> м.</xsl:text>
					</xsl:when>
					</xsl:choose>
					<xsl:choose>
					<xsl:when test="sdays>0">
						<xsl:value-of select="sdays"/><xsl:text> дн.</xsl:text>
					</xsl:when>
					</xsl:choose>
					<xsl:choose>
					<xsl:when test="shours>0">
						<xsl:value-of select="shours"/><xsl:text> ч.</xsl:text>
					</xsl:when>
					</xsl:choose>
					<xsl:choose>
					<xsl:when test="sminutes>0">
					<xsl:value-of select="sminutes"/><xsl:text> мин.</xsl:text>
					</xsl:when>
					</xsl:choose>
					</TD>
				</xsl:when>
				<xsl:otherwise>
					<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
						<xsl:text>-</xsl:text>
					</TD>
					<TD width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:choose>
					<xsl:when test="smonths>0">
						<xsl:value-of select="smonths"/><xsl:text> м.</xsl:text>
					</xsl:when>
					</xsl:choose>
					<xsl:choose>
					<xsl:when test="sdays>0">
						<xsl:value-of select="sdays"/><xsl:text> дн.</xsl:text>
					</xsl:when>
					</xsl:choose>
					<xsl:choose>
					<xsl:when test="shours>0">
						<xsl:value-of select="shours"/><xsl:text> ч.</xsl:text>
					</xsl:when>
					</xsl:choose>
					<xsl:choose>
					<xsl:when test="sminutes>0">
					<xsl:value-of select="sminutes"/><xsl:text> мин.</xsl:text>
					</xsl:when>
					</xsl:choose>
					</TD>
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
				<xsl:choose>
				<xsl:when test="staj_base_id>0">
					<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
						<xsl:value-of select="staj_base_name"/>
					</TD>
					<TD width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:choose>
					<xsl:when test="smonths>0">
						<xsl:value-of select="smonths"/><xsl:text> м.</xsl:text>
					</xsl:when>
					</xsl:choose>
					<xsl:choose>
					<xsl:when test="sdays>0">
						<xsl:value-of select="sdays"/><xsl:text> дн.</xsl:text>
					</xsl:when>
					</xsl:choose>
					<xsl:choose>
					<xsl:when test="shours>0">
						<xsl:value-of select="shours"/><xsl:text> ч.</xsl:text>
					</xsl:when>
					</xsl:choose>
					<xsl:choose>
					<xsl:when test="sminutes>0">
					<xsl:value-of select="sminutes"/><xsl:text> мин.</xsl:text>
					</xsl:when>
					</xsl:choose>
					</TD>
				</xsl:when>
				<xsl:otherwise>
					<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
						<xsl:text>-</xsl:text>
					</TD>
					<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
						<xsl:text>-</xsl:text>
					</TD>
				</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
			</xsl:choose>

			<!-- Выслуга лет -->
			<xsl:choose>
			<xsl:when test="servyear_base_id>0">
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:value-of select="servyear_base_name"/>
				</TD>
				<TD width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:choose>
				<xsl:when test="smonths>0">
					<xsl:value-of select="smonths"/><xsl:text> м.</xsl:text>
				</xsl:when>
				</xsl:choose>
				<xsl:choose>
				<xsl:when test="sdays>0">
					<xsl:value-of select="sdays"/><xsl:text> дн.</xsl:text>
				</xsl:when>
				</xsl:choose>
				<xsl:choose>
				<xsl:when test="shours>0">
					<xsl:value-of select="shours"/><xsl:text> ч.</xsl:text>
				</xsl:when>
				</xsl:choose>
				<xsl:choose>
				<xsl:when test="sminutes>0">
					<xsl:value-of select="sminutes"/><xsl:text> мин.</xsl:text>
				</xsl:when>
				</xsl:choose>
				</TD>
			</xsl:when>
			<xsl:otherwise>
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
			</xsl:otherwise>
			</xsl:choose>
		</TR>

		<!-- Профессия -->
		<xsl:choose>
		<xsl:when test="profession!=''">
			<TR style="font-size:8pt;text-align:right;font-weight:bold;">
				<TD colspan="8" align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:value-of select="profession"/>
				</TD>
			</TR>
		</xsl:when>
		</xsl:choose>

		</xsl:for-each>
	</TABLE>
	<BR/>

	<!-- Сведения за иной период в отчётном году -->
	<TABLE width="100%" border="0" style="font-size:8pt;">
		<TR>
			<TD colspan="4" style="font-size:8pt;font-weight:bold;">Сведения за иной период, засчитываемые в страховой стаж:</TD>
		</TR>
		<TR>
			<TD nowrap="true" width="7%">№ п/п</TD>
			<TD nowrap="true" width="10%">Код</TD>
			<TD nowrap="true" width="10%">Начало</TD>
			<TD nowrap="true">Конец</TD>
		</TR>
		<xsl:for-each select="doc_info/dop_staj/record">
		<xsl:sort select="start_date" order="ascending"/>
		<TR>
			<TD style="font-size:10pt;color:navy;font-weight:bold;">
				<xsl:number value="position()" format="1. "/></TD>
			<xsl:choose>
			<xsl:when test="dop_code_id='0'">
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
				<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
					<xsl:text>-</xsl:text>
				</TD>
			</xsl:when>
			<xsl:otherwise>
				<TD width="7%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="dop_code_name"/></TD>
				<TD width="10%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="dop_start"/></TD>
				<TD style="font-size:10pt;color:navy;font-weight:bold;border-color:black black black black;">
				<xsl:value-of select="dop_end"/></TD>
			</xsl:otherwise>
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