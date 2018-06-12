<%@ Control Language="c#" Inherits="Bolao.Administracao.EditarParticipante" CodeBehind="EditarParticipante.ascx.cs" %>
<h1>Cadastro de Participante</h1>
<div>
	<fieldset>
		<legend><asp:Label ID="lbOperacao" Runat="server">Incluir Participante</asp:Label></legend>
		<div class="conteudo-fieldset">
			<asp:ValidationSummary ID="vsParticipante" Runat="server" CssClass="erro"></asp:ValidationSummary>
			<asp:Label ID="lbMensagem" Runat="server" CssClass="erro" Visible="False"></asp:Label>
			<p>
				Usuário:
				<asp:CompareValidator ID="cvUsuario" runat="server" ControlToValidate="ddlUsuario" ValueToCompare="0" Operator="NotEqual" Display="Dynamic" ErrorMessage="Selecione o usuário">*</asp:CompareValidator><br>
				<asp:DropDownList ID="ddlUsuario" runat="server" OnSelectedIndexChanged="ddlUsuario_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
				<asp:Label ID="lbNome" Runat="server"></asp:Label>
			</p>
			<p>
				Email:<br>
				<asp:HyperLink ID="hlEmail" Runat="server" CssClass="email"></asp:HyperLink>
			</p>
			<p>
				Efetuou o pagamento do Bolão?<br>
				<asp:RadioButtonList ID="rblBolaoPago" Runat="server" CssClass="sem-borda" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
					<asp:ListItem Value="S">Sim</asp:ListItem>
					<asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
				</asp:RadioButtonList>
			</p>
			<p>
				Administrador?<br>
				<asp:RadioButtonList ID="rblAdministrador" Runat="server" CssClass="sem-borda" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
					<asp:ListItem Value="S">Sim</asp:ListItem>
					<asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
				</asp:RadioButtonList>
			</p>
			<p>
				<asp:Button ID="btSalvar" Runat="server" Text="Salvar" onclick="btSalvar_Click"></asp:Button>
				<asp:Button ID="btCancelar" Runat="server" Text="Cancelar" CausesValidation="False" onclick="btCancelar_Click"></asp:Button>
			</p>
		</div>
	</fieldset>
</div>