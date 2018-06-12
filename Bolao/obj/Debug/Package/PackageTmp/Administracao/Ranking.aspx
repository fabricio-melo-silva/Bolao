<%@ Page language="c#" Inherits="Bolao.Administracao.Ranking" CodeBehind="Ranking.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Cabecalho" Src="Cabecalho.ascx" %>
<!DOCTYPE html>

<html>
<head>
	<title>Ranking</title>
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" href="../Css/Default.css" />
</head>
<body>
	<form id="Form1" method="post" runat="server">
		<uc:Cabecalho id="ucCabecalho" runat="Server"></uc:Cabecalho>
		<div class="conteudo">
			<h1>Ranking</h1>
			
			<div>
				<p>
					Bolão:
					<asp:DropDownList ID="ddlBolao" Runat="server" AutoPostBack="True" onselectedindexchanged="ddlBolao_SelectedIndexChanged"></asp:DropDownList>
				</p>
				<p>
					<asp:Label ID="lbMensagem" Runat="server" Visible="False"></asp:Label>
					<asp:DataGrid ID="dgUsuarios" Runat="server" AutoGenerateColumns="False" AllowSorting="True" CssClass="resultado" CellPadding="4" BorderWidth="0px">
						<HeaderStyle CssClass="cabecalho"></HeaderStyle>
						<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
						<Columns>
							<asp:BoundColumn DataField="num_ranking" SortExpression="num_ranking" HeaderText="Posição"></asp:BoundColumn>
							<asp:BoundColumn DataField="nom_usuario" SortExpression="nom_usuario" HeaderText="Participante" ItemStyle-CssClass="nome-participante"></asp:BoundColumn>
							<asp:BoundColumn DataField="vlr_pontuacao" SortExpression="vlr_pontuacao" HeaderText="Pontuação"></asp:BoundColumn>
						</Columns>
					</asp:DataGrid>
				</p>
			</div>
		</div>
	</form>
</body>
</html>
