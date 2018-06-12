<%@ Control Language="c#" Inherits="Bolao.Administracao.ListarGrupo" CodeBehind="ListarGrupo.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="EditarGrupo" Src="EditarGrupo.ascx" %>
<%@ Register TagPrefix="uc" TagName="ListarTimeGrupo" Src="ListarTimeGrupo.ascx" %>
<%@ Register TagPrefix="uc" TagName="ListarJogoGrupo" Src="ListarJogoGrupo.ascx" %>
<asp:Panel ID="pnListar" Runat="server">
	<h1>Cadastro de Grupo</h1>
	<p>
		<b>Bolão:</b>
		<asp:Label ID="lbNomeBolao" Runat="server"></asp:Label>
		<br><br>
		<b>Fase:</b>
		<asp:Label ID="lbNomeFase" Runat="server"></asp:Label>
	</p>
	<p>
		<asp:Button ID="btIncluirGrupo" Runat="server" Text="Incluir Grupo" onclick="btIncluirGrupo_Click"></asp:Button>
		<asp:Button ID="btVoltar" Runat="server" Text="Voltar" onclick="btVoltar_Click"></asp:Button>
 	</p>
	<p>
		<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existe nenhum Grupo cadastrada.</asp:Label>
		<asp:DataGrid ID="dgGrupo" Runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
			<HeaderStyle CssClass="cabecalho"></HeaderStyle>
			<ItemStyle CssClass="item"></ItemStyle>
			<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
			<Columns>
				<asp:BoundColumn DataField="nom_grupo" HeaderText="Grupo"></asp:BoundColumn>
				<asp:BoundColumn DataField="sgl_grupo" HeaderText="Sigla"></asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Times">
					<ItemTemplate>
						<asp:Button ID="btTimes" Runat="server" Text="T" CssClass="botao" OnClick="ExibirTimes"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Jogos">
					<ItemTemplate>
						<asp:Button ID="btJogos" Runat="server" Text="J" CssClass="botao" OnClick="ExibirJogos"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Alterar">
					<ItemTemplate>
						<asp:Button ID="btAlterar" Runat="server" Text="A" CssClass="botao" OnClick="AlterarGrupo"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Excluir">
					<ItemTemplate>
						<asp:Button ID="btExcluir" Runat="server" Text="E" CssClass="botao" OnClick="ExcluirGrupo"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</asp:Panel>
<uc:EditarGrupo id="ucEditarGrupo" runat="Server" Visible="False"></uc:EditarGrupo>
<uc:ListarTimeGrupo id="ucListarTimeGrupo" runat="Server" Visible="False"></uc:ListarTimeGrupo>
<uc:ListarJogoGrupo id="ucListarJogoGrupo" runat="Server" Visible="False"></uc:ListarJogoGrupo>