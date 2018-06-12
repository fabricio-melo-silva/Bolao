<%@ Control Language="c#" Inherits="Bolao.Administracao.EditarTimeGrupo" CodeBehind="EditarTimeGrupo.ascx.cs" %>
<h1>Cadastro de Time</h1>
<div>
	<fieldset>
		<legend><asp:Label ID="lbOperacao" Runat="server">Incluir Time</asp:Label></legend>
		<div class="conteudo-fieldset">
			<asp:ValidationSummary ID="vsGrupo" Runat="server" CssClass="erro"></asp:ValidationSummary>
			<asp:Label ID="lbMensagem" Runat="server" CssClass="erro" Visible="False">Selecione o time que deseja adicionar ao Grupo.</asp:Label>
			<p>
				Informe o nome do time desejado:
				<asp:RequiredFieldValidator ID="rfvNome" Runat="server" ControlToValidate="tbNome" Display="Dynamic" ErrorMessage="Informe o nome do Time">*</asp:RequiredFieldValidator><br>
				<asp:TextBox ID="tbNome" Runat="server" Width="400px" MaxLength="100"></asp:TextBox>
				<asp:Button ID="btPesquisar" Runat="server" Text="Pesquisar" onclick="btPesquisar_Click"></asp:Button> 
				<asp:Button ID="btVoltar" Runat="server" Text="Voltar" CausesValidation="False" onclick="btCancelar_Click"></asp:Button>
			</p>
			<asp:Panel ID="pnTime" Runat="server" Visible="False">
				<p>
					Time:<br>
					<asp:DropDownList ID="ddlTime" Runat="server">
						<asp:ListItem Value="0">Selecione...</asp:ListItem>
					</asp:DropDownList>
				</p>
				<p>
					<asp:Button ID="btSalvar" Runat="server" Text="Salvar" CausesValidation="False" onclick="btSalvar_Click"></asp:Button>
					<asp:Button ID="btCancelar" Runat="server" Text="Cancelar" CausesValidation="False" onclick="btCancelar_Click"></asp:Button>
				</p>
			</asp:Panel>
		</div>
	</fieldset>
</div>