<%@ Page language="c#" Inherits="Bolao.Administracao.Jogo" CodeBehind="Jogo.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Cabecalho" Src="Cabecalho.ascx" %>
<!DOCTYPE html>

<html>
<head>
	<title>Jogo</title>
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" href="../Css/Default.css" />
</head>
<body>
	<form id="Form1" method="post" runat="server">
		<uc:Cabecalho id="ucCabecalho" runat="Server"></uc:Cabecalho>
		<div class="conteudo">
			<h1>Jogos</h1>
			
			<div>
				Bolão:
				<asp:DropDownList ID="ddlBolao" Runat="server" AutoPostBack="True" onselectedindexchanged="ddlBolao_SelectedIndexChanged"></asp:DropDownList>
				&nbsp;&nbsp;&nbsp;
				Fase:
				<asp:DropDownList ID="ddlFase" Runat="server" AutoPostBack="True" onselectedindexchanged="ddlFase_SelectedIndexChanged"></asp:DropDownList>
				&nbsp;&nbsp;&nbsp;
				Jogos:
				<asp:DropDownList ID="ddlJogos" Runat="server" AutoPostBack="True" onselectedindexchanged="ddlJogos_SelectedIndexChanged">
					<asp:ListItem Value="T">Todos</asp:ListItem>
					<asp:ListItem Value="N">Somente jogos não realizados</asp:ListItem>
					<asp:ListItem Value="R">Somente jogos já realizados</asp:ListItem>
				</asp:DropDownList>
			</div>
			<div>
				<p>
					<asp:Button ID="btSalvarResultado1" Runat="server" Text="Salvar Resultados" OnClick="SalvarResultados"></asp:Button>
					<asp:Button ID="btAtualizarRanking1" Runat="server" Text="Atualizar Ranking" OnClick="AtualizarRanking"></asp:Button>
				</p>
				<p>
					<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existem jogos para esta fase.</asp:Label>
					<asp:DataGrid ID="dgJogos" Runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
						<HeaderStyle CssClass="cabecalho"></HeaderStyle>
						<ItemStyle CssClass="item"></ItemStyle>
						<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
						<Columns>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:CheckBox ID="cbRealizado" Runat="server" CssClass="sem-borda"></asp:CheckBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Jogo" SortExpression="num_jogo">
								<ItemTemplate>
									<input type="hidden" id="hdCodigoJogo" runat="server">
									<asp:Label ID="lbNumeroJogo" Runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:Image ID="imgTimeA" Runat="server"></asp:Image>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:Label ID="lbTimeA" Runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:TextBox ID="tbGolsA" Runat="server" MaxLength="2"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									x
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:TextBox ID="tbGolsB" Runat="server" MaxLength="2"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:Label ID="lbTimeB" Runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:Image ID="imgTimeB" Runat="server"></asp:Image>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Dia" SortExpression="dat_jogo">
								<ItemTemplate>
									<asp:Label ID="lbData" Runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Hora">
								<ItemTemplate>
									<asp:Label ID="lbHora" Runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Local" SortExpression="dsc_local">
								<ItemTemplate>
									<asp:Label ID="lbLocal" Runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Grupo" SortExpression="nom_grupo">
								<ItemTemplate>
									<asp:Label ID="lbGrupo" Runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
				</p>
				<p>
					<asp:Button ID="btSalvarResultado2" Runat="server" Text="Salvar Resultados" OnClick="SalvarResultados"></asp:Button>
					<asp:Button ID="btAtualizarRanking2" Runat="server" Text="Atualizar Ranking" OnClick="AtualizarRanking"></asp:Button>
				</p>
				&nbsp;
			</div>
		</div>
	</form>
</body>
</html>
