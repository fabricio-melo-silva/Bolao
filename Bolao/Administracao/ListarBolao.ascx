<%@ Control Language="c#" Inherits="Bolao.Administracao.ListarBolao" CodeBehind="ListarBolao.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="EditarBolao" Src="EditarBolao.ascx" %>
<%@ Register TagPrefix="uc" TagName="ListarFase" Src="ListarFase.ascx" %>
<%@ Register TagPrefix="uc" TagName="ListarParticipante" Src="ListarParticipante.ascx" %>
<asp:Panel ID="pnListar" Runat="server">
	<h1>Cadastro de Bolão</h1>
	<p>
		<asp:Button ID="btIncluirBolao" Runat="server" Text="Incluir Bolão" onclick="btIncluirBolao_Click"></asp:Button>
		<asp:Button ID="btAtualizarRanking" Runat="server" Text="Atualizar Rankings" onclick="btAtualizarRanking_Click"></asp:Button>
	</p>
	<p>
		<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existe nenhum Bolão cadastrado.</asp:Label>
		<asp:DataGrid ID="dgBolao" Runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
			<HeaderStyle CssClass="cabecalho"></HeaderStyle>
			<ItemStyle CssClass="item"></ItemStyle>
			<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
			<Columns>
				<asp:BoundColumn DataField="dsc_bolao" HeaderText="Bolão" SortExpression="dsc_bolao"></asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Status" SortExpression="ind_status">
					<ItemTemplate>
						<asp:Label ID="lbStatus" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Valor" SortExpression="vlr_bolao">
					<ItemTemplate>
						<asp:Label ID="lbValor" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Ranking" SortExpression="dat_ranking">
					<ItemTemplate>
						<asp:Label ID="lbDataRanking" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Fases">
					<ItemTemplate>
						<asp:Button ID="btFases" Runat="server" Text="F" OnClick="ExibirFases" CssClass="botao"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Participantes">
					<ItemTemplate>
						<asp:Button ID="btParticipantes" Runat="server" Text="P" OnClick="ExibirParticipantes" CssClass="botao"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Alterar">
					<ItemTemplate>
						<asp:Button ID="btAlterar" Runat="server" Text="A" OnClick="AlterarBolao" CssClass="botao"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Excluir">
					<ItemTemplate>
						<asp:Button ID="btExcluir" Runat="server" Text="E" OnClick="ExcluirBolao" CssClass="botao"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</asp:Panel>
<uc:EditarBolao id="ucEditarBolao" runat="Server" Visible="False"></uc:EditarBolao>
<uc:ListarFase id="ucListarFase" runat="Server" Visible="False"></uc:ListarFase>
<uc:ListarParticipante id="ucListarParticipante" runat="Server" Visible="False"></uc:ListarParticipante>
