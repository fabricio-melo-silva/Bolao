<%@ Control Language="c#" Inherits="Bolao.Administracao.ListarUsuario" CodeBehind="ListarUsuario.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="EditarUsuario" Src="EditarUsuario.ascx" %>
<asp:Panel ID="pnListar" Runat="server">
	<h1>Cadastro de Usuário</h1>
	<p>
		<asp:Button ID="btIncluirUsuario" Runat="server" Text="Incluir Usuário" CssClass="botao" onclick="btIncluirUsuario_Click"></asp:Button>
	</p>
	<p>
		<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existe nenhum usuário cadastrado.</asp:Label>
		<asp:DataGrid ID="dgUsuario" Runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
			<HeaderStyle CssClass="cabecalho"></HeaderStyle>
			<ItemStyle CssClass="item"></ItemStyle>
			<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
			<Columns>
				<asp:BoundColumn DataField="NomeUsuario" HeaderText="Nome" SortExpression="NomeUsuario"></asp:BoundColumn>
				<asp:BoundColumn DataField="Email" HeaderText="E-mail" SortExpression="Email"></asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Ativo">
					<ItemTemplate>
						<asp:Label ID="lbAtivo" runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Data de Cadastro">
					<ItemTemplate>
						<asp:Label ID="lbDataCadastro" runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Alterar">
					<ItemTemplate>
						<asp:Button ID="btAlterar" Runat="server" Text="A" CssClass="botao" OnClick="AlterarUsuario"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Ativar / Inativar">
					<ItemTemplate>
						<asp:Button ID="btAtivarInativar" Runat="server" Text="-" CssClass="botao" OnClick="AtivarInativar"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</asp:Panel>
<uc:EditarUsuario id="ucEditarUsuario" runat="Server" Visible="False"></uc:EditarUsuario>
