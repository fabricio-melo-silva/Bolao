<%@ Control Language="c#" Inherits="Bolao.Administracao.EditarUsuario" CodeBehind="EditarUsuario.ascx.cs" %>
<h1>Cadastro de Usuário</h1>
<div>
	<fieldset>
		<legend><asp:Label ID="lbOperacao" Runat="server">Incluir Usuário</asp:Label></legend>
		<div class="conteudo-fieldset">
			<asp:ValidationSummary ID="vsGrupo" Runat="server" CssClass="erro"></asp:ValidationSummary>
			<asp:Label ID="lbMensagem" Runat="server" CssClass="erro" Visible="False"></asp:Label>
			<p>
				Nome:
				<asp:RequiredFieldValidator ID="rfvNome" Runat="server" ControlToValidate="tbNome" Display="Dynamic" ErrorMessage="Informe o nome do Time">*</asp:RequiredFieldValidator><br>
				<asp:TextBox ID="tbNome" Runat="server" Width="400px" MaxLength="100"></asp:TextBox>
			</p>
			<p>
				E-mail:
				<asp:RequiredFieldValidator ID="rfvSigla" Runat="server" ControlToValidate="tbEmail" Display="Dynamic" ErrorMessage="Informe a sigla do Time">*</asp:RequiredFieldValidator><br>
				<asp:TextBox ID="tbEmail" Runat="server" Width="400px" MaxLength="100"></asp:TextBox>
			</p>
			<p>
				Ativo:
				<asp:RadioButtonList ID="rblAtivo" Runat="server">
					<asp:ListItem Selected="True" Value="S" Text="Sim"></asp:ListItem>
					<asp:ListItem Value="N" Text="Nâo"></asp:ListItem>
				</asp:RadioButtonList>
			</p>
			<p>
				<asp:Button ID="btSalvar" Runat="server" Text="Salvar" onclick="btSalvar_Click"></asp:Button>
				<asp:Button ID="btCancelar" Runat="server" Text="Cancelar" CausesValidation="False" onclick="btCancelar_Click"></asp:Button>
			</p>
		</div>
	</fieldset>
</div>