<%@ Control Language="c#" Inherits="Bolao.Administracao.EditarBolao" CodeBehind="EditarBolao.ascx.cs" %>
<h1>Cadastro de Bolão</h1>
<div>
	<fieldset>
		<legend><asp:Label ID="lbOperacao" Runat="server">Incluir Bolão</asp:Label></legend>
		<div class="conteudo-fieldset">
			<asp:ValidationSummary ID="vsBolao" Runat="server" CssClass="erro"></asp:ValidationSummary>
			<asp:Label ID="lbMensagem" Runat="server" CssClass="erro" Visible="False"></asp:Label>
			<p>
				Descrição:
				<asp:RequiredFieldValidator ID="rfvDescricao" Runat="server" ControlToValidate="tbDescricao" Display="Dynamic" ErrorMessage="Informe a descrição do Bolão">*</asp:RequiredFieldValidator><br>
				<asp:TextBox ID="tbDescricao" Runat="server" Width="400px" MaxLength="100"></asp:TextBox>
			</p>
			<p>
				Status:<br>
				<asp:DropDownList ID="ddlStatus" Runat="server">
					<asp:ListItem Value="N">Não-iniciado</asp:ListItem>
					<asp:ListItem Value="A">Em andamento</asp:ListItem>
					<asp:ListItem Value="F">Finalizado</asp:ListItem>
				</asp:DropDownList>
			</p>
			<p>
				Valor (R$):
				<asp:RequiredFieldValidator ID="rfvValor" Runat="server" ControlToValidate="tbValor" Display="Dynamic" ErrorMessage="Informe o valor a ser pago pela participação no Bolão">*</asp:RequiredFieldValidator><asp:CustomValidator ID="cvValor" Runat="server" ControlToValidate="tbValor" Display="Dynamic" ErrorMessage="Informe o valor em um formato correto" OnServerValidate="ValidarValor">*</asp:CustomValidator><br>
				<asp:TextBox ID="tbValor" Runat="server" Width="100px" MaxLength="12"></asp:TextBox>
			</p>
			<p>
				<asp:Button ID="btSalvar" Runat="server" Text="Salvar" onclick="btSalvar_Click"></asp:Button>
				<asp:Button ID="btCancelar" Runat="server" Text="Cancelar" CausesValidation="False" onclick="btCancelar_Click"></asp:Button>
			</p>
		</div>
	</fieldset>
</div>