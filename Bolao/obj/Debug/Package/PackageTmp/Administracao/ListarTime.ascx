<%@ Control Language="c#" Inherits="Bolao.Administracao.ListarTime" CodeBehind="ListarTime.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="EditarTime" Src="EditarTime.ascx" %>
<asp:Panel ID="pnListar" Runat="server">
	<h1>Cadastro de Time</h1>
	<p>
		<asp:Button ID="btIncluirTime" Runat="server" Text="Incluir Time" CssClass="botao" onclick="btIncluirTime_Click"></asp:Button>
	</p>
	<p>
		<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existe nenhum time cadastrado.</asp:Label>
		<asp:DataGrid ID="dgTime" Runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
			<HeaderStyle CssClass="cabecalho"></HeaderStyle>
			<ItemStyle CssClass="item"></ItemStyle>
			<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
			<Columns>
				<asp:BoundColumn DataField="nom_time" HeaderText="Time" SortExpression="nom_time"></asp:BoundColumn>
				<asp:BoundColumn DataField="sgl_time" HeaderText="Sigla" SortExpression="sgl_time"></asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Ícone">
					<ItemTemplate>
						<asp:Image ID="imgIcone" Runat="server" CssClass="bandeira"></asp:Image>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Alterar">
					<ItemTemplate>
						<asp:Button ID="btAlterar" Runat="server" Text="A" CssClass="botao" OnClick="AlterarTime"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Excluir">
					<ItemTemplate>
						<asp:Button ID="btExcluir" Runat="server" Text="E" CssClass="botao" OnClick="ExcluirTime"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</asp:Panel>
<uc:EditarTime id="ucEditarTime" runat="Server" Visible="False"></uc:EditarTime>
