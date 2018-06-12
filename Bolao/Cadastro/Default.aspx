<%@ Page language="c#" Inherits="Bolao.Cadastro._Default" CodeBehind="Default.aspx.cs" %>
<%@ Reference Page="~/Default.aspx" %>
<%@ Register TagPrefix="uc" TagName="Cabecalho" Src="Cabecalho.ascx" %>
<!DOCTYPE html>

<html>
<head>
	<title>Cadastro</title>
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" href="../Css/Default.css">
</head>
<body>
	<form id="Form1" method="post" runat="server">
		<uc:Cabecalho id="ucCabecalho" runat="Server"></uc:Cabecalho>
		<div class="conteudo">
			<h1>Cadastro</h1>
			<asp:Panel ID="pnPasso1" Runat="server">
				<h5>Passo 1 de 2</h5>
				
				<p>Quero me cadastrar no bolão.</p>
				
				<p>
					<asp:RadioButtonList id="rblCadastrar" Runat="server" CssClass="sem-borda" RepeatLayout="Flow" RepeatDirection="Vertical" RepeatColumns="1">
						<asp:ListItem Value="0" Selected="True">Ainda não sou registrado no site</asp:ListItem>
						<asp:ListItem Value="1">Já sou um usuário registrado</asp:ListItem>
					</asp:RadioButtonList>
				</p>
				
				<p>
					<asp:Button id="btContinuar1" Runat="server" Text="Continuar >>" onclick="btContinuar1_Click"></asp:Button>
				</p>
			</asp:Panel>

			<asp:Panel ID="pnPasso2" Runat="server">
				<h5>Passo 2 de 2</h5>
				
				<asp:ValidationSummary id="vsPasso2" Runat="server"></asp:ValidationSummary>
				
				<p>
					Por favor, informe o seus dados:
				</p>
				
				<asp:Panel id="pnNaoRegistrado" Runat="server">
					<p>
						Nome:
						<asp:RequiredFieldValidator id="rfvNome" Runat="server" ErrorMessage="Informe o seu nome" Display="Dynamic" ControlToValidate="tbNome">*</asp:RequiredFieldValidator><br>
						<asp:TextBox id="tbNome" Runat="server" Width="400px" MaxLength="100"></asp:TextBox>
					</p>
				</asp:Panel>
				
				<asp:Panel id="pnRegistrado" Runat="server">
					<p>
						E-mail:
						<asp:RequiredFieldValidator id="rfvEmail" Runat="server" ErrorMessage="Informe o seu e-mail" Display="Dynamic" ControlToValidate="tbEmail">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator id="revEmail" runat="server" ErrorMessage="Informe um e-mail válido." ControlToValidate="tbEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator><asp:CustomValidator ID="cvEmail" Runat="server" ControlToValidate="tbEmail" Display="Dynamic" ErrorMessage="O email informado já está cadastrado." OnServerValidate="ValidarEmail">*</asp:CustomValidator><br>
						<asp:TextBox id="tbEmail" Runat="server" Width="400px" MaxLength="100"></asp:TextBox>
					</p>
					
					<p>
						Senha:
						<asp:RequiredFieldValidator id="rfvSenha" Runat="server" ErrorMessage="Informe a sua senha" Display="Dynamic" ControlToValidate="tbSenha">*</asp:RequiredFieldValidator><asp:CustomValidator ID="cvSenha" Runat="server" ControlToValidate="tbSenha" Display="Dynamic" ErrorMessage="O e-mail ou a senha informados não estão corretos." OnServerValidate="ValidarSenha">*</asp:CustomValidator><br>
						<asp:TextBox id="tbSenha" Runat="server" Width="120px" MaxLength="20" TextMode="Password"></asp:TextBox>
					</p>
					
					<p>
						Confirmação da senha:
						<asp:RequiredFieldValidator id="rfvConfirmacao" Runat="server" ErrorMessage="Informe corretamente a confirmação da senha" Display="Dynamic" ControlToValidate="tbConfirmacao">*</asp:RequiredFieldValidator><asp:CompareValidator id="cvConfirmacao" Runat="server" ErrorMessage="Informe a confirmação igual à senha" Display="Dynamic" ControlToValidate="tbConfirmacao" ControlToCompare="tbSenha" Operator="Equal">*</asp:CompareValidator><br>
						<asp:TextBox id="tbConfirmacao" Runat="server" Width="120px" MaxLength="20" TextMode="Password"></asp:TextBox>
					</p>
				</asp:Panel>
				
				<p>
					<asp:Button id="btContinuar2" Runat="server" Text="Continuar >>" onclick="btContinuar2_Click"></asp:Button>
				</p>
			</asp:Panel>
			
			<asp:Panel ID="pnPasso3" Runat="server">
				<h2>
					<asp:Label id="lbPasso3" Runat="server"></asp:Label>
				</h2>
				
				<p>
					Selecione o bolão para participar:
					<br><br>
					<asp:Label ID="lbMensagem" Runat="server" CssClass="erro" Visible="False"></asp:Label>
					<asp:DataGrid ID="dgBolao" Runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
						<HeaderStyle CssClass="cabecalho"></HeaderStyle>
						<ItemStyle CssClass="item"></ItemStyle>
						<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
						<Columns>
							<asp:TemplateColumn>
								<ItemStyle Width="20px" HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<input type="hidden" id="hdBolao" runat="server">
									<asp:CheckBox ID="cbBolao" Runat="server" CssClass="sem-borda"></asp:CheckBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:BoundColumn DataField="dsc_bolao" HeaderText="Bolão"></asp:BoundColumn>
							<asp:TemplateColumn HeaderText="Taxa de Participação (R$)">
								<ItemTemplate>
									<asp:Label ID="lbValor" Runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
				</p>
				
				<p>
					<asp:Button id="btConfirmar" Runat="server" Text="Confirmar" onclick="btConfirmar_Click"></asp:Button>
				</p>
			</asp:Panel>
			
			<asp:Panel ID="pnPasso4" Runat="server">
				<p>Sua participação foi solicitada com sucesso.</p>
				<p>O administrador irá confirmar a sua participação assim que o pagamento for efetuado.</p>
				<p>
					<asp:Button id="btOk" Runat="server" Text="Ok" onclick="btOk_Click"></asp:Button>
				</p>
			</asp:Panel>
		</div>
		&nbsp;
	</form>
</body>
</html>
