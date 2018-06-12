<%@ Control Language="c#" Inherits="Bolao.Administracao.EditarGrupo" CodeBehind="EditarGrupo.ascx.cs" %>
<h1>Cadastro de Grupo</h1>
<div>
	<fieldset>
		<legend><asp:Label ID="lbOperacao" Runat="server">Incluir Grupo</asp:Label></legend>
		<div class="conteudo-fieldset">
			<asp:ValidationSummary ID="vsGrupo" Runat="server" CssClass="erro"></asp:ValidationSummary>
			<asp:Label ID="lbMensagem" Runat="server" CssClass="erro" Visible="False"></asp:Label>
			<p>
				Nome:
				<asp:RequiredFieldValidator ID="rfvNome" Runat="server" ControlToValidate="tbNome" Display="Dynamic" ErrorMessage="Informe a descrição do Grupo">*</asp:RequiredFieldValidator><br>
				<asp:TextBox ID="tbNome" Runat="server" Width="400px" MaxLength="100"></asp:TextBox>
			</p>
			<p>
				Sigla:
				<asp:RequiredFieldValidator ID="rfvSigla" Runat="server" ControlToValidate="tbSigla" Display="Dynamic" ErrorMessage="Informe a sigla do Grupo">*</asp:RequiredFieldValidator><br>
				<asp:TextBox ID="tbSigla" Runat="server" Width="100px" MaxLength="5"></asp:TextBox>
			</p>
			<p>
				<asp:Button ID="btSalvar" Runat="server" Text="Salvar" onclick="btSalvar_Click"></asp:Button>
				<asp:Button ID="btCancelar" Runat="server" Text="Cancelar" CausesValidation="False" onclick="btCancelar_Click"></asp:Button>
			</p>
		</div>
	</fieldset>
</div>