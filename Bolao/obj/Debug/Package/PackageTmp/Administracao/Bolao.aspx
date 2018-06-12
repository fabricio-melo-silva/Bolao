<%@ Page language="c#" Inherits="Bolao.Administracao.Bolao" CodeBehind="Bolao.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Cabecalho" Src="Cabecalho.ascx" %>
<%@ Register TagPrefix="uc" TagName="ListarBolao" Src="ListarBolao.ascx" %>
<!DOCTYPE html>

<html>
<head>
	<title>Bolão</title>
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" href="../Css/Default.css" />
	<script language="javascript" src="../lib/lib_mascara_numero.js"></script>
</head>
<body>
	<form id="Form1" method="post" runat="server">
		<uc:Cabecalho id="ucCabecalho" runat="Server"></uc:Cabecalho>
		<div class="conteudo">
			<uc:ListarBolao id="ucListarBolao" runat="Server"></uc:ListarBolao>
		</div>
		&nbsp;
	</form>
</body>
</html>
