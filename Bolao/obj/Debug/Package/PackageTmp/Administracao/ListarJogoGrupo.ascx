<%@ Control Language="c#" Inherits="Bolao.Administracao.ListarJogoGrupo" CodeBehind="ListarJogoGrupo.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="EditarJogoGrupo" Src="EditarJogoGrupo.ascx" %>
<asp:Panel ID="pnListar" Runat="server">
	<h1>Jogos do Grupo</h1>
	<p>
		<b>Bolão:</b>
		<asp:Label ID="lbNomeBolao" Runat="server"></asp:Label>
		<br><br>
		<b>Fase:</b>
		<asp:Label ID="lbNomeFase" Runat="server"></asp:Label>
		<br><br>
		<b>Grupo:</b>
		<asp:Label ID="lbNomeGrupo" Runat="server"></asp:Label>
	</p>
	<p>
		<asp:Button ID="btIncluirJogo" Runat="server" Text="Incluir Jogo" onclick="btIncluirJogo_Click"></asp:Button>
		<asp:Button ID="btVoltar" Runat="server" Text="Voltar" onclick="btVoltar_Click"></asp:Button>
	</p>
	<p>
		<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existem jogos para esta fase.</asp:Label>
		<asp:DataGrid ID="dgJogos" Runat="server" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
			<HeaderStyle CssClass="cabecalho"></HeaderStyle>
			<ItemStyle CssClass="item"></ItemStyle>
			<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
			<Columns>
				<asp:BoundColumn DataField="num_jogo" HeaderText="Jogo"></asp:BoundColumn>
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:Image ID="imgTimeA" Runat="server"></asp:Image>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:BoundColumn DataField="nom_time_a"></asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Placar">
					<ItemTemplate>
						<asp:Label ID="lbGolsA" Runat="server">-</asp:Label>
						x
						<asp:Label ID="lbGolsB" Runat="server">-</asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:BoundColumn DataField="nom_time_b"></asp:BoundColumn>
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:Image ID="imgTimeB" Runat="server"></asp:Image>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Dia">
					<ItemTemplate>
						<asp:Label ID="lbData" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Hora">
					<ItemTemplate>
						<asp:Label ID="lbHora" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Limite">
					<ItemTemplate>
						<asp:Label ID="lbLimite" runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:BoundColumn DataField="dsc_local" HeaderText="Local"></asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Alterar">
					<ItemTemplate>
						<asp:Button ID="btAlterar" Runat="server" Text="A" CssClass="botao" OnClick="AlterarJogo"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Excluir">
					<ItemTemplate>
						<asp:Button ID="btExcluir" Runat="server" Text="E" CssClass="botao" OnClick="ExcluirJogo"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</asp:Panel>
<uc:EditarJogoGrupo id="ucEditarJogoGrupo" runat="Server" Visible="False"></uc:EditarJogoGrupo>
