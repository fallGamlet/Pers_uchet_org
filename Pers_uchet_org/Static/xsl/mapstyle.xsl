<?xml version="1.0" encoding="Windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
	<HTML>
	<HEAD>
	<STYLE>
		BODY { font-family:arial; font-size:12px;}
		H1 { font-size:120%; font-style:italic; color:navy; }
		UL { margin-left:5px; margin-bottom:5px; }
		LI UL { display:none; margin-left:16px; }
		LI { font-weight:bold; list-style-type:square; cursor:default; }
		LI.clsHasKids {font-size:12px; list-style-type:none; cursor:hand; color:maroon; }
		A:link, A:visited, A:active { margin:auto 5px; font-weight:normal; color:blue; text-decoration:none;}
		A:hover {text-decoration:underline;}
		BUTTON {font-family:arial; font-size:12px; margin:5px;}
		TD {font-family:arial;}
	</STYLE>
	<SCRIPT LANGUAGE="javascript"><xsl:comment><![CDATA[
	function GetChildElem(eSrc,sTagName)
	{
		var cKids = eSrc.children;
		for (var i=0;i<cKids.length;i++)
		{
			if (sTagName == cKids[i].tagName) return cKids[i];
		}
		return false;
	}
	document.onmousedown = function()
	{
		var eSrc = window.event.srcElement;
		if ("clsHasKids" == eSrc.className && (eChild = GetChildElem(eSrc,"UL")))
		{
			eChild.style.display = ("block" == eChild.style.display ? "none" : "block");
		}
	}
	function ShowAll(sTagName)
	{
		var cElems = document.all.tags(sTagName);
		var iNumElems = cElems.length;
		for (var i=1;i<iNumElems;i++) cElems[i].style.display = "block";
	}
	function HideAll(sTagName)
	{
		var cElems = document.all.tags(sTagName);
		var iNumElems = cElems.length;
		for (var i=1;i<iNumElems;i++) cElems[i].style.display = "none";
	}
	function ShowFirst()
	{
		var cElems = document.all.tags("UL");
		cElems[1].style.display = "block";
	}
	]]></xsl:comment>
	</SCRIPT>
	</HEAD>
	<BODY onLoad="ShowFirst()">
		
	<DIV class="insert_here" />
	
	<BUTTON ONCLICK="ShowAll('UL')" style="visibility:visible;">Показать все</BUTTON>
	<BUTTON ONCLICK="HideAll('UL')" style="visibility:visible;">Свернуть все</BUTTON>
	<UL style="margin: auto auto auto 20px;">
		<xsl:apply-templates select="TOPICS" />
	</UL>
	<DIV STYLE="BORDER: buttonhighlight 2px outset; FONT-SIZE: 8pt; Z-INDEX: 
		4; FONT-FAMILY: Tahoma; POSITION: absolute; BACKGROUND-COLOR: buttonface; 
		DISPLAY: none; WIDTH: 350px; CURSOR: default" ID="divProgressDialog" 
		onselectstart="window.event.returnValue=false;">
		<DIV ID="divCaption" STYLE="PADDING: 3px; FONT-WEIGHT: bolder; COLOR: captiontext; 
			BORDER-BOTTOM: white 2px groove; BACKGROUND-COLOR: activecaption"></DIV>
		<DIV ID="divTitle" STYLE="PADDING: 3px"></DIV>
		<DIV ID="divProgressTitle" STYLE="TEXT-ALIGN: center; PADDING: 2px"></DIV>

		<DIV STYLE="PADDING: 5px;">
			<DIV ID="divProgressOuter" STYLE="BORDER: 1px solid threedshadow; WIDTH: 336px; HEIGHT: 15px">
				<DIV ID="divProgressInner" STYLE="COLOR: white; TEXT-ALIGN: center; 
					BACKGROUND-COLOR: infobackground; MARGIN: 0px; WIDTH: 0px; HEIGHT: 13px;">
				</DIV>
			</DIV>
		</DIV>
	</DIV>

	<DIV ID="divModal" STYLE="BACKGROUND-COLOR: white; FILTER: alpha(opacity=75); LEFT: 0px; 
		POSITION: absolute; TOP: 0px; Z-INDEX: 3"
		onclick="window.event.cancelBubble=true; window.event.returnValue=false;">
	</DIV>
	</BODY>
	</HTML>
</xsl:template>

<xsl:template match="TOPICS">
<LI CLASS="clsHasKids">
	<xsl:value-of select="@TYPE" />
	
	<xsl:for-each select="SVOD">
		<A TARGET="_self">
		<xsl:attribute name="HREF">
			svd:<xsl:value-of select="PATH" />\<xsl:value-of select="FILENAME" />
		</xsl:attribute>
		<xsl:value-of select="TITLE" /></A>
	</xsl:for-each>
	
	<xsl:for-each select="OPIS">
		<A TARGET="_self">
		<xsl:attribute name="HREF">
			ops:<xsl:value-of select="PATH" /><xsl:value-of select="FILENAME" />
		</xsl:attribute>
		<xsl:value-of select="TITLE" /></A>
	</xsl:for-each>
	
	<UL>
		<xsl:for-each select="TOPIC">
		<xsl:sort order="ascending" select="TITLE"/>
		<xsl:sort order="ascending" select="DOCTYPE"/>
		<LI>
			<SPAN style="color:green; display:inline-block; width:100px;"><xsl:value-of select="REGNUM" /></SPAN>
			<A TARGET="_self" style="display:inline-block; width:210px;">
			<xsl:attribute name="HREF">
				ind:<xsl:value-of select="PATH" /><xsl:value-of select="FILENAME" />
			</xsl:attribute>
			<xsl:value-of select="TITLE" /></A>
			<FONT style="color:black;"> (
			<xsl:choose>
				<xsl:when test="DOCTYPE[.='1']">Анкета</xsl:when>
				<xsl:when test="DOCTYPE[.='2']">Листок исправлений</xsl:when>
				<xsl:when test="DOCTYPE[.='3']">Заявление об изменении</xsl:when>
				<xsl:when test="DOCTYPE[.='4']">Заявление о восстановлении</xsl:when>
				<xsl:when test="DOCTYPE[.='21']">Исходная форма</xsl:when>
				<xsl:when test="DOCTYPE[.='22']">Корректирующая форма</xsl:when>
				<xsl:when test="DOCTYPE[.='23']">Отменяющая форма</xsl:when>
				<xsl:when test="DOCTYPE[.='24']">Назначение пенсии</xsl:when>
			</xsl:choose> )
			</FONT>
		</LI>
		</xsl:for-each>
		<xsl:if test="TOPICS"><xsl:apply-templates select="TOPICS" /></xsl:if>
	</UL>
</LI>
</xsl:template>


</xsl:stylesheet>

