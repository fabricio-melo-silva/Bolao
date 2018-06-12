<%@ Control Language="c#" Inherits="Bolao.Administracao.EditarTime" CodeBehind="EditarTime.ascx.cs" %>
<h1>Cadastro de Time</h1>
<div>
	<fieldset>
		<legend><asp:Label ID="lbOperacao" Runat="server">Incluir Time</asp:Label></legend>
		<div class="conteudo-fieldset">
			<asp:ValidationSummary ID="vsGrupo" Runat="server" CssClass="erro"></asp:ValidationSummary>
			<asp:Label ID="lbMensagem" Runat="server" CssClass="erro" Visible="False"></asp:Label>
			<p>
				Nome:
				<asp:RequiredFieldValidator ID="rfvNome" Runat="server" ControlToValidate="tbNome" Display="Dynamic" ErrorMessage="Informe o nome do Time">*</asp:RequiredFieldValidator><br>
				<asp:TextBox ID="tbNome" Runat="server" Width="400px" MaxLength="100"></asp:TextBox>
			</p>
			<p>
				Sigla:
				<asp:RequiredFieldValidator ID="rfvSigla" Runat="server" ControlToValidate="tbSigla" Display="Dynamic" ErrorMessage="Informe a sigla do Time">*</asp:RequiredFieldValidator><br>
				<asp:TextBox ID="tbSigla" Runat="server" Width="100px" MaxLength="5"></asp:TextBox>
			</p>
			<p>
				Ícone:
				<asp:CustomValidator ID="cvIcone" Runat="server" ControlToValidate="ifIcone" Display="Dynamic" ErrorMessage="Informe a imagem de ícone do time no formato GIF ou JPG" OnServerValidate="ValidarIcone">*</asp:CustomValidator><br>
				<input type="file" id="ifIcone" name="ifIcone" runat="server" style="width: 400px"><br>
				<asp:Image ID="imgIcone" Runat="server"></asp:Image>
			</p>
			<p>
				<asp:Button ID="btSalvar" Runat="server" Text="Salvar" onclick="btSalvar_Click"></asp:Button>
				<asp:Button ID="btCancelar" Runat="server" Text="Cancelar" CausesValidation="False" onclick="btCancelar_Click"></asp:Button>
			</p>
		</div>
	</fieldset>
</div>