<%@ Page language="c#" Inherits="Bolao.Aposta._Default_old" CodeBehind="Default_old.aspx.cs" %>
<%@ Reference Page="~/Default.aspx" %>
<%@ Register TagPrefix="uc" TagName="Cabecalho" Src="Cabecalho.ascx" %>
<%@ Register TagPrefix="uc" TagName="ListarJogo" Src="ListarJogo.ascx" %>
<!DOCTYPE html>

<html>
<head>
	<title>Aposta</title>
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" href="../Css/Default.css" />
</head>
<body>
	<form id="Form1" method="post" runat="server">
		<uc:Cabecalho id="ucCabecalho" runat="Server"></uc:Cabecalho>
		<div class="conteudo">
			<h1>Jogos</h1>
			
			<div>
				Filtrar Fase:
				<asp:DropDownList ID="ddlFase" Runat="server" AutoPostBack="True" onselectedindexchanged="ddlFase_SelectedIndexChanged"></asp:DropDownList>
				&nbsp;&nbsp;&nbsp;
				Filtrar Jogos:
				<asp:DropDownList ID="ddlJogos" Runat="server" AutoPostBack="True" onselectedindexchanged="ddlJogos_SelectedIndexChanged">
					<asp:ListItem Value="T">Todos</asp:ListItem>
					<asp:ListItem Value="N">Somente jogos não realizados</asp:ListItem>
					<asp:ListItem Value="R">Somente jogos já realizados</asp:ListItem>
				</asp:DropDownList>
			</div>

			<uc:ListarJogo id="ucListarJogo" runat="Server"></uc:ListarJogo>
		</div>
		&nbsp;
	</form>
</body>
</html>
