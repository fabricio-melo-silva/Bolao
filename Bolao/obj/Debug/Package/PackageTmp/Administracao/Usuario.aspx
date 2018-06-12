<%@ Page language="c#" Inherits="Bolao.Administracao.Usuario" CodeBehind="Usuario.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Cabecalho" Src="Cabecalho.ascx" %>
<%@ Register TagPrefix="uc" TagName="ListarUsuario" Src="ListarUsuario.ascx" %>
<!DOCTYPE html>
<html>
<head>
	<title>Time</title>
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" href="../Css/Default.css" />
	<script language="javascript" src="../lib/lib_mascara_numero.js"></script>
</head>
<body>
	<form id="Form1" method="post" runat="server">
		<uc:Cabecalho id="ucCabecalho" runat="Server"></uc:Cabecalho>
		<div class="conteudo">
			<uc:ListarUsuario id="ucListarUsuario" runat="Server"></uc:ListarUsuario>
		</div>
		&nbsp;
	</form>
</body>
</html>
