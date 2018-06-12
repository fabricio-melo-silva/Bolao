<%@ Page Language="c#" Inherits="Bolao.Aposta.Ranking" CodeBehind="Ranking.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="Menu" Src="Menu.ascx" %>
<%@ Register TagPrefix="uc" TagName="Cabecalho" Src="Cabecalho.ascx" %>
<%@ Register TagPrefix="uc" TagName="RankingResumido" Src="RankingResumido.ascx" %>
<%@ Register TagPrefix="uc" TagName="RankingDetalhado" Src="RankingDetalhado.ascx" %>
<%@ Register TagPrefix="uc" TagName="RankingPorFase" Src="RankingPorFase.ascx" %>
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
	<title>Bolão da Copa 2018 - Ranking</title>
	<meta name="description" content="">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
	<link rel="stylesheet" href="../Css/Main.css">
	<script lang="javascript" src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
	<script lang="javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body class="page-jogos">
	<form id="form2" runat="server">
		<!--[if lt IE 10]>
			<p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
		<![endif]-->

		<uc:Menu ID="ucMenu" runat="Server"></uc:Menu>

		<div class="container" role="main">
			<uc:Cabecalho ID="ucCabecalho" runat="Server"></uc:Cabecalho>

			<div class="row">
				<!-- Miolo -->
				<div class="col-md-12 clearfix">
					<div class="box-branco clearfix box-miolo">
						<div class="clearfix">
							<div class="form-inline">
								<div class="form-group">
									<label class="texto-padrao">Ranking:</label>
									<asp:DropDownList ID="ddlTipoRanking" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoRanking_SelectedIndexChanged" CssClass="form-control">
										<asp:ListItem Value="ucRankingResumido" Selected="true">Resumido</asp:ListItem>
										<asp:ListItem Value="ucRankingDetalhado">Detalhado</asp:ListItem>
										<asp:ListItem Value="ucRankingPorFase">Por Fase</asp:ListItem>
									</asp:DropDownList>
								</div>
							</div>
						</div>

						<div class="clearfix">
							<div class="table-responsive">
								<uc:RankingResumido ID="ucRankingResumido" runat="Server"></uc:RankingResumido>
								<uc:RankingDetalhado ID="ucRankingDetalhado" runat="Server" Visible="False"></uc:RankingDetalhado>
								<uc:RankingPorFase ID="ucRankingPorFase" runat="Server" Visible="False"></uc:RankingPorFase>
							</div>
							<asp:Label ID="lbDataUltimaAtualizacao" runat="server" CssClass="m-t-15 db" Style="font-style: italic"></asp:Label>
						</div>
					</div>
				</div>
			</div>
		</div>
	</form>
</body>
</html>
