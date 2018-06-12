<%@ Control Language="c#" Inherits="Bolao.Administracao.ListarFase" CodeBehind="ListarFase.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="EditarFase" Src="EditarFase.ascx" %>
<%@ Register TagPrefix="uc" TagName="ListarGrupo" Src="ListarGrupo.ascx" %>
<asp:Panel ID="pnListar" Runat="server">
	<h1>Cadastro de Fase</h1>
	<p>
		<b>Bolão:</b>
		<asp:Label ID="lbNomeBolao" Runat="server"></asp:Label>
	</p>
	<p>
		<asp:Button ID="btIncluirFase" Runat="server" Text="Incluir Fase" onclick="btIncluirFase_Click"></asp:Button>
		<asp:Button ID="btVoltar" Runat="server" Text="Voltar" onclick="btVoltar_Click"></asp:Button>
 	</p>
	<p>
		<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existe nenhuma Fase cadastrada.</asp:Label>
		<asp:DataGrid ID="dgFase" Runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
			<HeaderStyle CssClass="cabecalho"></HeaderStyle>
			<ItemStyle CssClass="item"></ItemStyle>
			<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
			<Columns>
				<asp:BoundColumn DataField="dsc_fase" HeaderText="Fase"></asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Tipo">
					<ItemTemplate>
						<asp:Label ID="lbTipo" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Status">
					<ItemTemplate>
						<asp:Label ID="lbStatus" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Grupos">
					<ItemTemplate>
						<asp:Button ID="btGrupos" Runat="server" Text="G" CssClass="botao" OnClick="ExibirGrupo"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Alterar">
					<ItemTemplate>
						<asp:Button ID="btAlterar" Runat="server" Text="A" CssClass="botao" OnClick="AlterarFase"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Excluir">
					<ItemTemplate>
						<asp:Button ID="btExcluir" Runat="server" Text="E" CssClass="botao" OnClick="ExcluirFase"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</asp:Panel>
<uc:EditarFase id="ucEditarFase" runat="Server" Visible="False"></uc:EditarFase>
<uc:ListarGrupo id="ucListarGrupo" runat="Server" Visible="False"></uc:ListarGrupo>
