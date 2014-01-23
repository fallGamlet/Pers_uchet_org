<?xml version="1.0" encoding="Windows-1251"?>
<xsl:stylesheet version="1.0" 
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes"
	doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" 
	doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"/>

	<xsl:decimal-format name="rusformat" decimal-separator="." grouping-separator=" " NaN="-"/>

	<xsl:template match="/">
		<html>
			<HEAD>
				<SCRIPT LANGUAGE="javascript"><xsl:comment><![CDATA[]]></xsl:comment></SCRIPT>
			</HEAD>
			<body style="font-family:'Arial';">
				<TABLE width="90%" align="center" border="0">
					<TR>
						<TD>

							<TABLE width="100%" border="0">
								<TR>
									<TD style="font-size:8pt;">����� ���-1</TD>
								</TR>
								<TR>
									<TD align="center" style="font-size:10pt;font-weight:bold;color:gray;">������ ��������������� ���� ����������� �����������<BR/>��������������� ���������� ����������</TD>
								</TR>
								<TR>
									<TD/>
								</TR>
								<TR>
									<TD align="center" style="font-size:12pt;font-weight:bold;">
				�������������� �������� � ������ � �����<BR/>��������������� ����</TD>
								</TR>
							</TABLE>
							<BR/>

							<TABLE width="100%" border="0">
								<TR>
									<TD width="290px" style="font-size:10pt;font-weight:bold;" nowrap="true">������� </TD>
									<TD style="color:navy;font-weight:bold;">
										<xsl:value-of select="doc_info/person/lname"/>
									</TD>
									<TD rowspan="5" width="140" align="right" style="vertical-align:top;">
										<TABLE width="130" style="border:solid 1px #000; border-collapse:collapse;">
											<TR align="center">
												<TD style="border:solid 1px black; padding:0px;">
													<P style="font-size:10pt;background-color:#ddd;">��� �����</P>
												</TD>
											</TR>
											<TR>
												<TD>
													<TABLE border="0" style="font-size:8pt;">
														<TR>
															<xsl:choose>
																<xsl:when test="doc_info/formtype='21'">
																	<TD width="8" height="6" style="font-weight:bold;">x</TD>
																	<TD style="font-weight:bold;">��������</TD>
																</xsl:when>
																<xsl:otherwise>
																	<TD width="8" height="6"/>
																	<TD>��������</TD>
																</xsl:otherwise>
															</xsl:choose>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<TABLE border="0" style="font-size:8pt;">
														<TR>
															<xsl:choose>
																<xsl:when test="doc_info/formtype='22'">
																	<TD width="8" height="6" style="font-weight:bold;">x</TD>
																	<TD style="font-weight:bold;">��������������</TD>
																</xsl:when>
																<xsl:otherwise>
																	<TD width="8" height="6"/>
																	<TD>��������������</TD>
																</xsl:otherwise>
															</xsl:choose>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<TABLE border="0" style="font-size:8pt;">
														<TR>
															<xsl:choose>
																<xsl:when test="doc_info/formtype='23'">
																	<TD width="8" height="6" style="font-weight:bold;">x</TD>
																	<TD style="font-weight:bold;">����������</TD>
																</xsl:when>
																<xsl:otherwise>
																	<TD width="8" height="6"/>
																	<TD>����������</TD>
																</xsl:otherwise>
															</xsl:choose>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<TABLE border="0" style="font-size:8pt;">
														<TR>
															<xsl:choose>
																<xsl:when test="doc_info/formtype='24'">
																	<TD width="8" height="6" style="font-weight:bold;">x</TD>
																	<TD style="font-weight:bold;">���������� ������</TD>
																</xsl:when>
																<xsl:otherwise>
																	<TD width="8" height="6"/>
																	<TD>���������� ������</TD>
																</xsl:otherwise>
															</xsl:choose>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD width="290px" style="font-size:10pt;font-weight:bold;" nowrap="true">��� </TD>
									<TD style="color:navy;font-weight:bold;">
										<xsl:value-of select="doc_info/person/fname"/>
									</TD>
								</TR>
								<TR>
									<TD width="290px" style="font-size:10pt;font-weight:bold;" nowrap="true">�������� </TD>
									<TD style="color:navy;font-weight:bold;">
										<xsl:value-of select="doc_info/person/mname"/>
									</TD>
								</TR>
								<TR>
									<TD width="290px" style="font-size:10pt;font-weight:bold;" nowrap="true">����� �������� ����� </TD>
									<TD style="color:navy;font-weight:bold;">
										<xsl:value-of select="doc_info/person/regnum"/>
									</TD>
								</TR>
								<TR>
									<TD width="290px" style="font-size:10pt;font-weight:bold;" nowrap="true">�����������</TD>
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
									<TD width="290px" style="font-size:10pt;font-weight:bold;" nowrap="true">��� ��������� ��������������� ����</TD>
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
										<TABLE width="130" style="border:solid 1px #000; border-collapse:collapse;">
											<TR align="center">
												<TD style="border:solid 1px black; padding:0px;">
													<P style="font-size:10pt;background-color:#ddd;">����� ������</P>
												</TD>
											</TR>
											<TR>
												<TD>
													<TABLE border="0" style="font-size:8pt;">
														<TR>
															<xsl:choose>
																<xsl:when test="doc_info/work_place='1'">
																	<TD width="8" height="6" style="font-weight:bold;">x</TD>
																	<TD style="font-weight:bold;">��������</TD>
																</xsl:when>
																<xsl:otherwise>
																	<TD width="8" height="6"/>
																	<TD>��������</TD>
																</xsl:otherwise>
															</xsl:choose>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<TABLE border="0" style="font-size:8pt;">
														<TR>
															<xsl:choose>
																<xsl:when test="doc_info/work_place='2'">
																	<TD width="8" height="6" style="font-weight:bold;">x</TD>
																	<TD style="font-weight:bold;">����������</TD>
																</xsl:when>
																<xsl:otherwise>
																	<TD width="8" height="6"/>
																	<TD>����������</TD>
																</xsl:otherwise>
															</xsl:choose>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD colspan="2" style="font-size:10pt;font-weight:bold;">�������� � ������������</TD>
								</TR>
								<TR>
									<TD width="240px" style="font-size:10pt; padding-left:50px;">��������������� ����� � �����</TD>
									<TD style="color:navy;font-weight:bold;">
										<xsl:value-of select="doc_info/firm/regnum"/>
									</TD>
								</TR>
								<TR>
									<TD width="240px" style="font-size:10pt; padding-left:50px;">������������</TD>
									<TD style="color:navy;font-weight:bold;">
										<xsl:value-of select="doc_info/firm/name"/>
									</TD>
								</TR>
							</TABLE>

							<TABLE width="100%" border="0">
								<TR>
									<TD width="65%" style="font-size:10pt;">
				����� ��������� ������� �����������, ������������ �������������</TD>
									<TD align="right" style="font-size:10pt;color:navy;font-weight:bold;">
										<xsl:value-of select="doc_info/firm_add"/>
									</TD>
									<TD rowspan="2" width="140" align="right" style="vertical-align:top;">
										<TABLE width="130" style="border:solid 1px #000; border-collapse:collapse;">
											<TR align="center">
												<TD style="border:solid 1px black; padding:0px;">
													<P style="font-size:10pt;background-color:#ddd;">�������� ������</P>
												</TD>
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
				����� ������������ ��������� �������
									</TD>
									<TD align="right" style="font-size:10pt;color:navy;font-weight:bold;">
										<xsl:value-of select="doc_info/firm_pay"/>
									</TD>
								</TR>
							</TABLE>
							<BR/>

							<!-- ������ �� ����� ����� � �������� ���� -->
							<TABLE width="100%" border="1" style="border:solid 1px #000; border-collapse:collapse">
								<TR style="font-size:8pt;text-align:center;background-color:#ddd;">
									<TD width="5%" rowspan="2" style="border-color:black;">
				�����</TD>
									<TD width="15%" rowspan="2" style="border-color:black;">
				����� ������, �� �������<br/>��������� ���������<br/>������</TD>
									<TD width="15%" rowspan="2" style="border-color:black;">
				����� ������,<br/>����������� ���<br/>���������� ������</TD>
									<TD width="15%" colspan="2" style="border-color:black;">
				����� ��������� �������</TD>
									<TD width="15%" rowspan="2" style="border-color:black;">
				����� ������������<br/>��������� �������,<br/>������������ ��<br/>���������</TD>
									<TD width="5%" rowspan="2" style="border-color:black;">
				�����<br/>������<br/>���� ���<br/>������<br/>�����</TD>
								</TR>
								<TR style="font-size:8pt;text-align:center;background-color:#ddd;">
									<TD style="border-color:black;">�����������<br/>�������������</TD>
									<TD style="border-color:black;">����������<br/>�������������</TD>
								</TR>
								<xsl:for-each select="doc_info/payment/month">
									<TR style="font-size:8pt;text-align:right;font-weight:bold;">
										<TD width="5%" style="border-color:black; text-align:center;">
											<xsl:number value="position()" format="1 "/>
										</TD>

										<xsl:choose>
											<xsl:when test="col_1=0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
											</xsl:when>
											<xsl:when test="col_1>0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_1, '# ##0.00', 'rusformat')"/>
												</TD>
											</xsl:when>
											<xsl:otherwise>
												<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_1, '# ##0.00', 'rusformat')"/>
												</TD>
											</xsl:otherwise>
										</xsl:choose>

										<xsl:choose>
											<xsl:when test="col_2=0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
											</xsl:when>
											<xsl:when test="col_2>0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_2, '# ##0.00', 'rusformat')"/>
												</TD>
											</xsl:when>
											<xsl:otherwise>
												<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_2, '# ##0.00', 'rusformat')"/>
												</TD>
											</xsl:otherwise>
										</xsl:choose>

										<xsl:choose>
											<xsl:when test="col_3=0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
											</xsl:when>
											<xsl:when test="col_3>0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_3, '# ##0.00', 'rusformat')"/>
												</TD>
											</xsl:when>
											<xsl:otherwise>
												<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_3, '# ##0.00', 'rusformat')"/>
												</TD>
											</xsl:otherwise>
										</xsl:choose>

										<xsl:choose>
											<xsl:when test="col_4=0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
											</xsl:when>
											<xsl:when test="col_4>0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_4, '# ##0.00', 'rusformat')"/>
												</TD>
											</xsl:when>
											<xsl:otherwise>
												<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_4, '# ##0.00', 'rusformat')"/>
												</TD>
											</xsl:otherwise>
										</xsl:choose>

										<xsl:choose>
											<xsl:when test="col_5=0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
											</xsl:when>
											<xsl:when test="col_5>0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_5, '# ##0.00', 'rusformat')"/>
												</TD>
											</xsl:when>
											<xsl:otherwise>
												<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_5, '# ##0.00', 'rusformat')"/>
												</TD>
											</xsl:otherwise>
										</xsl:choose>

										<xsl:choose>
											<xsl:when test="col_6=0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
											</xsl:when>
											<xsl:when test="col_6>0">
												<TD nowrap="yes" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_6, '# ##0', 'rusformat')"/>
												</TD>
											</xsl:when>
											<xsl:otherwise>
												<TD nowrap="yes" style="font-size:10pt;color:red;font-weight:bold;border-color:black;">
													<xsl:value-of select="format-number(col_6, '# ##0', 'rusformat')"/>
												</TD>
											</xsl:otherwise>
										</xsl:choose>
									</TR>
								</xsl:for-each>

								<TR style="font-size:8pt;text-align:right;font-weight:bold;color:black;background-color:#ddd;">
									<TD width="5%" style="border-color:black; text-align:center;">�����</TD>
									<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
										<xsl:value-of select="format-number(sum(doc_info/payment/month/col_1), '# ##0.00', 'rusformat')"/>
									</TD>
									<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
										<xsl:value-of select="format-number(sum(doc_info/payment/month/col_2), '# ##0.00', 'rusformat')"/>
									</TD>
									<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
										<xsl:value-of select="format-number(sum(doc_info/payment/month/col_3), '# ##0.00', 'rusformat')"/>
									</TD>
									<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
										<xsl:value-of select="format-number(sum(doc_info/payment/month/col_4), '# ##0.00', 'rusformat')"/>
									</TD>
									<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
										<xsl:value-of select="format-number(sum(doc_info/payment/month/col_5), '# ##0.00', 'rusformat')"/>
									</TD>
									<TD nowrap="yes" style="font-size:10pt;color:black;font-weight:bold;border-color:black;">
										<xsl:value-of select="format-number(sum(doc_info/payment/month/col_6), '# ##0', 'rusformat')"/>
									</TD>
								</TR>
							</TABLE>
							<BR/>

							<!-- ������� ������ � �������� ���� -->
							<TABLE width="100%" border="0" style="font-size:8pt;">
								<TR>
									<TD colspan="3" style="font-weight:bold;">������ � ����� � �������� ����</TD>
								</TR>
								<TR>
									<TD nowrap="true" width="7%">� �/�</TD>
									<TD nowrap="true" width="10%">������</TD>
									<TD nowrap="true">�����</TD>
								</TR>
								<xsl:for-each select="doc_info/gen_period/period">
									<xsl:sort select="gen_start" data-type="number" order="ascending"/>
									<xsl:sort select="gen_end" data-type="number" order="ascending"/>
									<TR>
										<TD style="font-size:10pt;color:navy;font-weight:bold;">
											<xsl:number value="position()" format="1. "/>
										</TD>
										<TD style="font-size:10pt;color:navy;font-weight:bold;">
											<xsl:value-of select="gen_start"/>
										</TD>
										<TD style="font-size:10pt;color:navy;font-weight:bold;">
											<xsl:value-of select="gen_end"/>
										</TD>
									</TR>
								</xsl:for-each>
							</TABLE>

							<!-- ����������� ���� ������ � �������� ���� -->
							<TABLE width="100%" border="0">
								<TR>
									<TD colspan="8" style="font-size:8pt;font-weight:bold;">
				����������� ���� ������ �� �������� ������
									</TD>
								</TR>
							</TABLE>
							<TABLE width="100%" border="1" style="border:solid 1px #000; border-collapse:collapse; text-align:center;">
								<TR style="font-size:8pt;background-color:#ddd;">
									<TD width="5%" rowspan="2" style="border-color:black;">�<br/>�/�</TD>
									<TD width="15%" rowspan="2" style="border-color:black;">������ �������<br/>(��.��.����)</TD>
									<TD width="15%" rowspan="2" style="border-color:black;">����� �������<br/>(��.��.����)</TD>
									<TD width="15%" rowspan="2" style="border-color:black;">������<br/>������� �����<br/>(���)</TD>
									<TD width="15%" colspan="2" style="border-color:black;">����������� ��������� ����</TD>
									<TD width="15%" colspan="2" style="border-color:black;">������� ���</TD>
								</TR>
								<TR style="font-size:8pt;background-color:#ddd;">
									<TD style="border-color:black;">���������<br/>(���)</TD>
									<TD style="border-color:black;">��������������<br/>��������</TD>
									<TD style="border-color:black;">���������<br/>(���)</TD>
									<TD style="border-color:black;">��������������<br/>��������</TD>
								</TR>
								<xsl:for-each select="doc_info/spec_staj/spec">
									<xsl:sort select="start_date" data-type="number" order="ascending"/>
									<xsl:sort select="end_date" data-type="number" order="ascending"/>
									<TR style="font-size:8pt;font-weight:bold;">
										<TD style="border-color:black;">
											<xsl:number value="position()" format="1 "/>
										</TD>

										<xsl:choose>
											<xsl:when test="start_date=''">
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
											</xsl:when>
											<xsl:otherwise>
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="start_date"/>
												</TD>
											</xsl:otherwise>
										</xsl:choose>


										<xsl:choose>
											<xsl:when test="end_date=''">
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
											</xsl:when>
											<xsl:otherwise>
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="end_date"/>
												</TD>
											</xsl:otherwise>
										</xsl:choose>


										<!-- ������ ������� ����� -->
										<xsl:choose>
											<xsl:when test="part_condition_id>0">
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="part_condition_name"/>
												</TD>
												<xsl:choose>
													<xsl:when test="staj_base_id>0">
														<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
															<xsl:value-of select="staj_base_name"/>
														</TD>
														<TD width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
															<xsl:choose>
																<xsl:when test="smonths>0">
																	<xsl:value-of select="smonths"/>
																	<xsl:text> �.</xsl:text>
																</xsl:when>
															</xsl:choose>
															<xsl:choose>
																<xsl:when test="sdays>0">
																	<xsl:value-of select="sdays"/>
																	<xsl:text> ��.</xsl:text>
																</xsl:when>
															</xsl:choose>
															<xsl:choose>
																<xsl:when test="shours>0">
																	<xsl:value-of select="shours"/>
																	<xsl:text> �.</xsl:text>
																</xsl:when>
															</xsl:choose>
															<xsl:choose>
																<xsl:when test="sminutes>0">
																	<xsl:value-of select="sminutes"/>
																	<xsl:text> ���.</xsl:text>
																</xsl:when>
															</xsl:choose>
														</TD>
													</xsl:when>
													<xsl:otherwise>
														<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
															<xsl:text>-</xsl:text>
														</TD>
														<TD width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
															<xsl:choose>
																<xsl:when test="smonths>0">
																	<xsl:value-of select="smonths"/>
																	<xsl:text> �.</xsl:text>
																</xsl:when>
															</xsl:choose>
															<xsl:choose>
																<xsl:when test="sdays>0">
																	<xsl:value-of select="sdays"/>
																	<xsl:text> ��.</xsl:text>
																</xsl:when>
															</xsl:choose>
															<xsl:choose>
																<xsl:when test="shours>0">
																	<xsl:value-of select="shours"/>
																	<xsl:text> �.</xsl:text>
																</xsl:when>
															</xsl:choose>
															<xsl:choose>
																<xsl:when test="sminutes>0">
																	<xsl:value-of select="sminutes"/>
																	<xsl:text> ���.</xsl:text>
																</xsl:when>
															</xsl:choose>
														</TD>
													</xsl:otherwise>
												</xsl:choose>
											</xsl:when>
											<xsl:otherwise>
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
												<xsl:choose>
													<xsl:when test="staj_base_id>0">
														<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
															<xsl:value-of select="staj_base_name"/>
														</TD>
														<TD width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
															<xsl:choose>
																<xsl:when test="smonths>0">
																	<xsl:value-of select="smonths"/>
																	<xsl:text> �.</xsl:text>
																</xsl:when>
															</xsl:choose>
															<xsl:choose>
																<xsl:when test="sdays>0">
																	<xsl:value-of select="sdays"/>
																	<xsl:text> ��.</xsl:text>
																</xsl:when>
															</xsl:choose>
															<xsl:choose>
																<xsl:when test="shours>0">
																	<xsl:value-of select="shours"/>
																	<xsl:text> �.</xsl:text>
																</xsl:when>
															</xsl:choose>
															<xsl:choose>
																<xsl:when test="sminutes>0">
																	<xsl:value-of select="sminutes"/>
																	<xsl:text> ���.</xsl:text>
																</xsl:when>
															</xsl:choose>
														</TD>
													</xsl:when>
													<xsl:otherwise>
														<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
															<xsl:text>-</xsl:text>
														</TD>
														<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
															<xsl:text>-</xsl:text>
														</TD>
													</xsl:otherwise>
												</xsl:choose>
											</xsl:otherwise>
										</xsl:choose>

										<!-- ������� ��� -->
										<xsl:choose>
											<xsl:when test="servyear_base_id>0">
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="servyear_base_name"/>
												</TD>
												<TD width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:choose>
														<xsl:when test="smonths>0">
															<xsl:value-of select="smonths"/>
															<xsl:text> �.</xsl:text>
														</xsl:when>
													</xsl:choose>
													<xsl:choose>
														<xsl:when test="sdays>0">
															<xsl:value-of select="sdays"/>
															<xsl:text> ��.</xsl:text>
														</xsl:when>
													</xsl:choose>
													<xsl:choose>
														<xsl:when test="shours>0">
															<xsl:value-of select="shours"/>
															<xsl:text> �.</xsl:text>
														</xsl:when>
													</xsl:choose>
													<xsl:choose>
														<xsl:when test="sminutes>0">
															<xsl:value-of select="sminutes"/>
															<xsl:text> ���.</xsl:text>
														</xsl:when>
													</xsl:choose>
												</TD>
											</xsl:when>
											<xsl:otherwise>
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
											</xsl:otherwise>
										</xsl:choose>
									</TR>

									<!-- ��������� -->
									<xsl:choose>
										<xsl:when test="profession!=''">
											<TR style="font-size:8pt;font-weight:bold;">
												<TD colspan="8" align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="profession"/>
												</TD>
											</TR>
										</xsl:when>
									</xsl:choose>

								</xsl:for-each>
							</TABLE>
							<BR/>

							<!-- �������� �� ���� ������ � �������� ���� -->
							<TABLE width="100%" border="0" style="font-size:8pt;">
								<TR>
									<TD colspan="4" style="font-size:8pt;font-weight:bold;">�������� �� ���� ������, ������������� � ��������� ����</TD>
								</TR>
								<TR>
									<TD nowrap="true" width="7%">� �/�</TD>
									<TD nowrap="true" width="10%">���</TD>
									<TD nowrap="true" width="10%">������</TD>
									<TD nowrap="true">�����</TD>
								</TR>
								<xsl:for-each select="doc_info/dop_staj/record">
									<xsl:sort select="start_date" order="ascending"/>
									<TR>
										<TD style="font-size:10pt;color:navy;font-weight:bold;">
											<xsl:number value="position()" format="1. "/>
										</TD>
										<xsl:choose>
											<xsl:when test="dop_code_id='0'">
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
												<TD align="center" width="15%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:text>-</xsl:text>
												</TD>
											</xsl:when>
											<xsl:otherwise>
												<TD width="7%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="dop_code_name"/>
												</TD>
												<TD width="10%" style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="dop_start"/>
												</TD>
												<TD style="font-size:10pt;color:navy;font-weight:bold;border-color:black;">
													<xsl:value-of select="dop_end"/>
												</TD>
											</xsl:otherwise>
										</xsl:choose>
									</TR>
								</xsl:for-each>
							</TABLE>

						</TD>
					</TR>
				</TABLE>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>