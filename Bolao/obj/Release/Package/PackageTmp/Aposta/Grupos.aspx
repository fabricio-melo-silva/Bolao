<%@ Page language="c#" Inherits="Bolao.Aposta.Grupos" CodeBehind="Grupos.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Cabecalho" Src="Cabecalho.ascx" %>
<%@ Register TagPrefix="uc" TagName="ListarJogo" Src="ListarJogo.ascx" %>
<!DOCTYPE html>

<html>
<head>
	<title>Grupos</title>
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" href="../Css/Default.css" />
</head>
<body>
	<form id="Form1" method="post" runat="server">
		<uc:Cabecalho id="ucCabecalho" runat="Server"></uc:Cabecalho>
		<div class="conteudo">
			<h1>Grupos</h1>

			<asp:ValidationSummary Runat="server" ID="vsGrupo"></asp:ValidationSummary>
			<p>
				Fase:
				<asp:DropDownList Runat="server" ID="ddlFase" AutoPostBack="True" onselectedindexchanged="ddlFase_SelectedIndexChanged">
				</asp:DropDownList>
				<asp:RequiredFieldValidator Runat="server" id="rfvFase" ErrorMessage="Por favor, selecione uma fase" ControlToValidate="ddlFase">*</asp:RequiredFieldValidator>
				
				&nbsp;&nbsp;&nbsp;
				
				Grupo:
				<asp:DropDownList Runat="server" ID="ddlGrupo" AutoPostBack="True" onselectedindexchanged="ddlGrupo_SelectedIndexChanged">
					<asp:ListItem Value="0">Selecione...</asp:ListItem>
				</asp:DropDownList>
			</p>
			<p>
				<asp:DataGrid ID="dgTimes" Runat="server" BorderWidth="0" CellPadding="4" CellSpacing="0" CssClass="resultado" AutoGenerateColumns="False">
					<HeaderStyle CssClass="cabecalho"></HeaderStyle>
					<ItemStyle CssClass="item"></ItemStyle>
					<Columns>
						<asp:BoundColumn DataField="nom_time" HeaderText="Time" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>						
						<asp:BoundColumn DataField="qtd_ponto" HeaderText="PG" ItemStyle-Width="30px"></asp:BoundColumn>
						<asp:BoundColumn DataField="qtd_vitoria" HeaderText="V" ItemStyle-Width="30px"></asp:BoundColumn>
						<asp:BoundColumn DataField="qtd_empate" HeaderText="E" ItemStyle-Width="30px"></asp:BoundColumn>
						<asp:BoundColumn DataField="qtd_derrota" HeaderText="D" ItemStyle-Width="30px"></asp:BoundColumn>
						<asp:BoundColumn DataField="qtd_gol_feito" HeaderText="GF" ItemStyle-Width="30px"></asp:BoundColumn>
						<asp:BoundColumn DataField="qtd_gol_sofrido" HeaderText="GC" ItemStyle-Width="30px"></asp:BoundColumn>
						<asp:BoundColumn DataField="qtd_saldo_gol" HeaderText="SG" ItemStyle-Width="30px"></asp:BoundColumn>
					</Columns>
				</asp:DataGrid>
				<uc:ListarJogo id="ucListarJogo" runat="Server"></uc:ListarJogo>
			</p>
		</div>
	</form>
</body>
</html>
