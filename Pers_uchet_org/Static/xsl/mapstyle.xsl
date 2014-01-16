<?xml version="1.0" encoding="Windows-1251"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">

<xsl:template match="/">
	<HTML>
	<HEAD>
	<STYLE>
		BODY { font-family:tahoma; font-size:70%; }
		H1 { font-size:120%; font-style:italic; color:navy; }
		UL { margin-left:0px; margin-bottom:5px; }
		LI UL { display:none; margin-left:16px; }
		LI { font-weight:bold; list-style-type:square; cursor:default; }
		LI.clsHasKids { list-style-type:none; cursor:hand; color:maroon; }
		A:link, A:visited, A:active { font-weight:normal; color:blue; text-decoration:none; font-size:110%; }
		A:hover { text-decoration:underline; font-weight:bold; }
		BUTTON { font-family:tahoma; font-size:100%; }
		TD { font-family:tahome; font-size:110%; }
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
	function document.onmousedown()
	{
		var mBtn = window.event.button;
		if (mBtn == 1)
		{
			var eSrc = window.event.srcElement;
			if ("clsHasKids" == eSrc.className && (eChild = GetChildElem(eSrc,"UL")))
			{
				eChild.style.display = ("block" == eChild.style.display ? "none" : "block");
			}
		}
		else
		{
			alert('Use left button, please');
			window.event.cancelBubble = true;
			return;
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
	<UL><xsl:apply-templates select="TOPICS" /></UL>
	<BUTTON ONCLICK="ShowAll('UL')" style="visibility:'hidden';">�������� ���</BUTTON>
	<BUTTON ONCLICK="HideAll('UL')" style="visibility:'hidden';">�������� ���</BUTTON>
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
			ops:<xsl:value-of select="PATH" />\<xsl:value-of select="FILENAME" />
		</xsl:attribute>
		<xsl:value-of select="TITLE" /></A>
	</xsl:for-each>
	<UL>
		<xsl:for-each select="TOPIC" order-by="DOCTYPE;TITLE">
		<LI>
			<FONT style="color:'green';"><xsl:value-of select="REGNUM" /></FONT>
			<A TARGET="_self">
			<xsl:attribute name="HREF">
				ind:<xsl:value-of select="PATH" />\<xsl:value-of select="FILENAME" />
			</xsl:attribute>
			<xsl:value-of select="TITLE" /></A>
			<FONT style="color:'black';"> (
			<xsl:choose>
				<xsl:when test="DOCTYPE[.='1']">������</xsl:when>
				<xsl:when test="DOCTYPE[.='2']">������ �����������</xsl:when>
				<xsl:when test="DOCTYPE[.='3']">��������� �� ���������</xsl:when>
				<xsl:when test="DOCTYPE[.='4']">��������� � ��������������</xsl:when>
				<xsl:when test="DOCTYPE[.='21']">�������� �����</xsl:when>
				<xsl:when test="DOCTYPE[.='22']">�������������� �����</xsl:when>
				<xsl:when test="DOCTYPE[.='23']">���������� �����</xsl:when>
				<xsl:when test="DOCTYPE[.='24']">���������� ������</xsl:when>
			</xsl:choose> )
			</FONT>
		</LI>
		</xsl:for-each>
		<xsl:if test="TOPICS"><xsl:apply-templates /></xsl:if>
	</UL>
</LI>
</xsl:template>


</xsl:stylesheet>

