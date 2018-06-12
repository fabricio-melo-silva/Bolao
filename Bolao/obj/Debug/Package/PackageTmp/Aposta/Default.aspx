<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bolao.Aposta._Default" %>

<%@ Register TagPrefix="uc" TagName="Menu" Src="Menu.ascx" %>
<%@ Register TagPrefix="uc" TagName="Cabecalho" Src="Cabecalho.ascx" %>

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
	<title>Bolão da Copa 2018 - Jogos</title>
	<meta name="description" content="">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
	<link rel="stylesheet" href="../Css/Main.css">
	<script lang="javascript" src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
	<script lang="javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
	<script lang="javascript" src="../Lib/lib_ajax.js"></script>
	<script lang="javascript">
		function salvarPlacar(obj, codJogo, qtdGolsA, qtdGolsB) {
			ajax("../API/Aposta/Post", { "CodigoJogo": codJogo, "QuantidadeGolA": qtdGolsA, "QuantidadeGolB": qtdGolsB },
				function (data) {
					if (typeof (data.ApostaValida) != 'undefined') {
						var codJogo = obj.attr("jogo");
						var h4 = $('h4[jogo="' + codJogo + '"]');

						if (data.ApostaValida) {
							h4.attr("class", "texto-dataHoraLocal");
						}
						else {
							h4.attr("class", "texto-dataHoraLocal status-invalido");
						}
					}
				},
				function (xtr, status, error) {
					if (typeof (xtr.responseJSON.Message) != 'undefined') {
						if (xtr.responseJSON.Message == 'Logoff') {
							document.localtion.href = '../Sair.aspx';
						}
						else {
							alert('Não foi possível salvar o placar.\n\n Motivo: ' + xtr.responseJSON.Message);
							obj.val('');
						}
					}
					else {
						alert('Não foi possível salvar o placar');
						obj.val('');
					}
				}
			);
		}

		function recuperarDados(obj) {
			var placarTimeA = null;
			var placarTimeB = null;
			var outroTextbox = null;
			var codJogo = obj.attr("jogo");

			if (obj.attr("time") == "A") {
				outroTextbox = obj.parent().find('input[time="B"]');
				placarTimeA = obj.val();
				placarTimeB = outroTextbox.val();
			}
			else if (obj.attr("time") == "B") {
				outroTextbox = obj.parent().find('input[time="A"]');
				placarTimeB = obj.val();
				placarTimeA = outroTextbox.val();
			}

			salvarPlacar(obj, codJogo, placarTimeA, placarTimeB);
		}

		$(document).ready(function () {
			$('input[jogo!=""]').each(function () {
				$(this).keypress(function (e) {
					if (!(e.keyCode >= 48 && e.keyCode <= 57)) {
						return false;
					}
				});

				$(this).on("keyup", function (e) {
					recuperarDados($(this));
				});

				$(this).on("change", function () {
					recuperarDados($(this));
				})
			});
		});
	</script>
</head>
<body class="page-jogos">
	<form id="form1" runat="server">
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
									<label for="filtrarFase" class="texto-padrao">Filtrar Fase:</label>
									<asp:DropDownList ID="ddlFiltroFase" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroFase_SelectedIndexChanged">
									</asp:DropDownList>
								</div>
								<div class="form-group">
									<label for="filtrarFase" class="texto-padrao">Filtrar Jogos:</label>
									<asp:DropDownList ID="ddlFiltroJogo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroJogo_SelectedIndexChanged">
										<asp:ListItem Value="T">Todos</asp:ListItem>
										<asp:ListItem Value="N">Somente jogos não realizados</asp:ListItem>
										<asp:ListItem Value="R">Somente jogos já realizados</asp:ListItem>
									</asp:DropDownList>
								</div>
							</div>
						</div>
						<asp:Label ID="lbMensagem" runat="server"></asp:Label>
						<asp:Panel ID="pnLegenda" runat="server" CssClass="clearfix">
							<span class="legenda-label">
								<span class="legenda-indicador"></span>Resultado não computado</span>
							<span class="legenda-label">
								<span class="legenda-indicador legenda-indicador-verde"></span>Resultado computado</span>
							<span class="legenda-label">
								<span class="legenda-indicador legenda-indicador-vermelho"></span>Aposta inválida</span>
						</asp:Panel>
						<div class="clearfix">
							<asp:Repeater ID="rpJogos" runat="server" OnItemDataBound="rpJogos_ItemDataBound">
								<ItemTemplate>
									<asp:Literal ID="ltFase" runat="server"></asp:Literal>
									<asp:Panel ID="pnJogo" runat="server" CssClass="aposta even row">
										<input type="hidden" id="hdCodigoJogo" runat="server" name="hdCodigoJogo">
										<asp:Literal ID="ltDataHoraLocalGrupo" runat="server"></asp:Literal>
										<div class="aposta-content">
											<div class="aposta-xs-content hidden-sm hidden-md hidden-lg">
												<div class="nome-pais col-xs-6 hidden-sm hidden-md hidden-lg">
													<asp:Image ID="imgTimeAPequeno" runat="server" CssClass="img-bandeira" />
													<asp:Label ID="lbTimeAPequeno" runat="server" CssClass="clearfix"></asp:Label>
												</div>
												<div style="width: 30px">&nbsp;</div>
												<div class="nome-pais col-xs-6 dir hidden-sm hidden-md hidden-lg">
													<asp:Image ID="imgTimeBPequeno" runat="server" CssClass="img-bandeira" />
													<asp:Label ID="lbTimeBPequeno" runat="server" CssClass="clearfix"></asp:Label>
												</div>
											</div>
											<div class="nome-pais hidden-xs">
												<asp:Label ID="lbTimeA" runat="server"></asp:Label>
												<asp:Image ID="imgTimeA" runat="server" CssClass="img-bandeira" />
											</div>
											<div class="form-aposta">
												<asp:TextBox ID="tbPlacarTimeA" runat="server" CssClass="campo-placar" TextMode="Phone" MaxLength="2" />
												<span class="span-vs">X</span>
												<asp:TextBox ID="tbPlacarTimeB" runat="server" CssClass="campo-placar" TextMode="Phone" MaxLength="2" />
											</div>
											<div class="nome-pais dir hidden-xs">
												<asp:Image ID="imgTimeB" runat="server" CssClass="img-bandeira" />
												<asp:Label ID="lbTimeB" runat="server"></asp:Label>
											</div>
										</div>
										<asp:Panel ID="pnPlacarFinal" runat="server" CssClass="placar-final-content">
											<div class="placar-final">
												<div class="tag-placar-final">
													<asp:Literal ID="ltResultadoTimeA" runat="server"></asp:Literal>
												</div>
												<span>Placar Final</span>
												<div class="tag-placar-final">
													<asp:Literal ID="ltResultadoTimeB" runat="server"></asp:Literal>
												</div>
											</div>
											<asp:Literal ID="ltPontuacaoJogo" runat="server"></asp:Literal>
											<asp:Label ID="lbDescricaoPontuacao" runat="server" CssClass="tag-sumario"></asp:Label>
										</asp:Panel>
									</asp:Panel>
								</ItemTemplate>
							</asp:Repeater>
						</div>
					</div>
				</div>
				<!-- fim miolo -->
			</div>
		</div>
	</form>
</body>

</html>
