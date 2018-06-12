<%@ Control Language="c#" Inherits="Bolao.Administracao.EditarFase" CodeBehind="EditarFase.ascx.cs" %>
<h1>Cadastro de Fase</h1>
<div>
	<fieldset>
		<legend><asp:Label ID="lbOperacao" Runat="server">Incluir Fase</asp:Label></legend>
		<div class="conteudo-fieldset">
			<asp:ValidationSummary ID="vsFase" Runat="server" CssClass="erro"></asp:ValidationSummary>
			<asp:Label ID="lbMensagem" Runat="server" CssClass="erro" Visible="False"></asp:Label>
			<p>
				Descrição:
				<asp:RequiredFieldValidator ID="rfvDescricao" Runat="server" ControlToValidate="tbDescricao" Display="Dynamic" ErrorMessage="Informe a descrição da Fase">*</asp:RequiredFieldValidator><br>
				<asp:TextBox ID="tbDescricao" Runat="server" Width="400px" MaxLength="100"></asp:TextBox>
			</p>
			<p>
				Tipo:<br>
				<asp:RadioButtonList ID="rblTipo" Runat="server" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Flow">
					<asp:ListItem Value="G">Grupos</asp:ListItem>
					<asp:ListItem Value="E">Eliminatória</asp:ListItem>
				</asp:RadioButtonList>
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
				<asp:Button ID="btSalvar" Runat="server" Text="Salvar" onclick="btSalvar_Click"></asp:Button>
				<asp:Button ID="btCancelar" Runat="server" Text="Cancelar" CausesValidation="False" onclick="btCancelar_Click"></asp:Button>
			</p>
		</div>
	</fieldset>
</div>