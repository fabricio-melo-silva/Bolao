<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LembreteSenha.aspx.cs" Inherits="Bolao.LembreteSenha" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="no-js">
<!--<![endif]-->

<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<title>Bolão da Copa 2018</title>
	<meta name="description" content="">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
	<link rel="stylesheet" href="css/main.css">
</head>

<body class="page-login">
	<!--[if lt IE 10]>
        <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
    <![endif]-->
	<form id="Form1" method="post" runat="server">
		<div class="login-content">
			<img class="logomarca" title="Bolão 2018" src="Images/Logomarca.png" />
			<div class="box-branco clearfix">
				<asp:ValidationSummary ID="vsLogin" runat="server" CssClass="validacao bg-danger" DisplayMode="BulletList"></asp:ValidationSummary>
				<asp:Label ID="lbMensagem" runat="server" CssClass="validacao bg-danger"></asp:Label>

				<div class="form-group">
					<label class="sr-only" for="InputEmail">Email address</label>
					<asp:TextBox ID="tbEmail" CssClass="form-control" placeholder="Email" runat="server" MaxLength="100" TextMode="Email"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="tbEmail" EnableClientScript="False" Display="None" ErrorMessage="Informe o seu e-mail"></asp:RequiredFieldValidator>
				</div>

				<asp:Button ID="btGerarNovaSenha" runat="server" CssClass="bt-padrao-grande center-block" Text="Gerar nova senha" OnClick="btGerarNovaSenha_Click"></asp:Button>
				<div class="checkbox text-align-center m-t-15 m-b-0">
					<a href="Default.aspx">Voltar</a>
				</div>
			</div>
		</div>
	</form>
	<script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</body>

</html>
