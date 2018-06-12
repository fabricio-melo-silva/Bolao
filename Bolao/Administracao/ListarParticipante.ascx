<%@ Control Language="c#" Inherits="Bolao.Administracao.ListarParticipante" CodeBehind="ListarParticipante.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="EditarParticipante" Src="EditarParticipante.ascx" %>
<asp:Panel ID="pnListar" Runat="server">
	<h1>Cadastro de Fase</h1>
	<p>
		<b>Bolão:</b>
		<asp:Label ID="lbNomeBolao" Runat="server"></asp:Label>
	</p>
	<p>
		<asp:Button ID="btIncluir" runat="server" Text="Incluir" OnClick="btIncluir_Click" />
		<asp:Button ID="btVoltar" Runat="server" Text="Voltar" onclick="btVoltar_Click"></asp:Button>
 	</p>
	<p>
		<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existe nenhum participante cadastrado.</asp:Label>
		<asp:DataGrid ID="dgParticipante" Runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
			<HeaderStyle CssClass="cabecalho"></HeaderStyle>
			<ItemStyle CssClass="item"></ItemStyle>
			<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
			<Columns>
				<asp:BoundColumn DataField="NomeUsuario" HeaderText="Nome"></asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Email">
					<ItemTemplate>
						<asp:HyperLink ID="hlEmail" Runat="server" CssClass="email"></asp:HyperLink>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Administrador">
					<ItemTemplate>
						<asp:Label ID="lbAdministrador" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Bolão Pago">
					<ItemTemplate>
						<asp:Label ID="lbBolaoPago" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Alterar">
					<ItemTemplate>
						<asp:Button ID="btAlterar" Runat="server" Text="A" CssClass="botao" OnClick="AlterarParticipante"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Excluir">
					<ItemTemplate>
						<asp:Button ID="btExcluir" Runat="server" Text="E" CssClass="botao" OnClick="ExcluirParticipante"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</asp:Panel>
<uc:EditarParticipante id="ucEditarParticipante" runat="Server" Visible="False"></uc:EditarParticipante>
