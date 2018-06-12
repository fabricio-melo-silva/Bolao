<%@ Control Language="c#" Inherits="Bolao.Administracao.EditarJogoGrupo" CodeBehind="EditarJogoGrupo.ascx.cs" %>
<h1>Cadastro de Jogo</h1>
<div>
	<fieldset>
		<legend><asp:Label ID="lbOperacao" Runat="server">Incluir Jogo</asp:Label></legend>
		<div class="conteudo-fieldset">
			<asp:ValidationSummary ID="vsJogo" Runat="server" CssClass="erro"></asp:ValidationSummary>
			<p>
				Número do Jogo:
				<asp:RequiredFieldValidator ID="rfvNumeroJogo" Runat="server" ControlToValidate="tbNumeroJogo" Display="Dynamic" ErrorMessage="Informe o número do jogo">*</asp:RequiredFieldValidator><asp:CompareValidator ID="cvNumeroJogo" Runat="server" ControlToValidate="tbNumeroJogo" Display="Dynamic" ErrorMessage="Informe um valor inteiro para o número do jogo" Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator><br>
				<asp:TextBox ID="tbNumeroJogo" Runat="server" MaxLength="8" Width="80px"></asp:TextBox>
			</p>
			<p>
				Confronto:
				<asp:CompareValidator ID="cvTimeA" Runat="server" ControlToValidate="ddlTimeA" Display="Dynamic" ErrorMessage="Selecione o primeiro time do confronto" Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator><asp:CompareValidator ID="cvTimeB" Runat="server" ControlToValidate="ddlTimeB" Display="Dynamic" ErrorMessage="Selecione o segundo time do confronto" Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator><br>
				<asp:DropDownList ID="ddlTimeA" Runat="server"></asp:DropDownList>
				&nbsp;x&nbsp;
				<asp:DropDownList ID="ddlTimeB" Runat="server"></asp:DropDownList>
			</p>
			<p>
				Data do Jogo:
				<asp:RequiredFieldValidator ID="rfvData" Runat="server" ControlToValidate="tbData" Display="Dynamic" ErrorMessage="Informe a data do jogo">*</asp:RequiredFieldValidator><asp:CompareValidator ID="cvData" Runat="server" ControlToValidate="tbData" Display="Dynamic" ErrorMessage="Informe um valor correto para a data do jogo" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator><br>
				<asp:TextBox ID="tbData" Runat="server" MaxLength="10" Width="80px"></asp:TextBox>
			</p>
			<p>
				Horário do Jogo:
				<asp:RequiredFieldValidator ID="rfvHora" Runat="server" ControlToValidate="tbHora" Display="Dynamic" ErrorMessage="Informe a hora do jogo">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revHora" Runat="server" ControlToValidate="tbHora" Display="Dynamic" ErrorMessage="Informe um valor válido para a hora do jogo" ValidationExpression="((2[0-3])|([0-1][0-9])):[0-5][0-9]">*</asp:RegularExpressionValidator><br>
				<asp:TextBox ID="tbHora" Runat="server" MaxLength="5" Width="80px"></asp:TextBox>
			</p>
			<p>
				Local do Jogo:
				<asp:RequiredFieldValidator ID="rfvLocal" Runat="server" ControlToValidate="tbLocal" Display="Dynamic" ErrorMessage="Informe o local do jogo">*</asp:RequiredFieldValidator><br>
				<asp:TextBox ID="tbLocal" Runat="server" MaxLength="100" Width="200px"></asp:TextBox>
			</p>
			<p>
				Data Limite para Aposta:
				<asp:RequiredFieldValidator ID="rfvDataLimite" Runat="server" ControlToValidate="tbDataLimite" Display="Dynamic" ErrorMessage="Informe a data limite para apostas">*</asp:RequiredFieldValidator><asp:CompareValidator ID="cvDataLimite" Runat="server" ControlToValidate="tbDataLimite" Display="Dynamic" ErrorMessage="Informe um valor correto para a data limite para apostas" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator><br>
				<asp:TextBox ID="tbDataLimite" Runat="server" MaxLength="10" Width="80px"></asp:TextBox>
			</p>
			<p>
				<asp:Button ID="btSalvar" Runat="server" Text="Salvar" onclick="btSalvar_Click"></asp:Button>
				<asp:Button ID="btCancelar" Runat="server" Text="Cancelar" CausesValidation="False" onclick="btCancelar_Click"></asp:Button>
			</p>
		</div>
	</fieldset>
</div>
