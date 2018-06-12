<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regras.aspx.cs" Inherits="Bolao.Aposta.Regras" %>

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
	<title>Bolão da Copa 2018 - Regras</title>
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
							<h1>Regulamento</h1>

							<h2>Objetivo</h2>

							<p>1. O presente Bolão da Copa 2018 tem caráter unicamente lúdico, sem fins lucrativos, com objetivo exclusivo de realizar a interação entre colegas através de um jogo de predição de placares dos jogos da Copa 2018, não se caracterizando como jogo de azar.</p>

							<p>2. Apenas para tornar mais atrativo o jogo, é cobrada uma taxa de participação no valor de R$ 20,00 por pessoa. Essa taxa será utilizada para o custeio das despesas de hospedagem do software do jogo, bem como para a premiação dos vencedores. </p>

							<p>3. O software do jogo, por sua vez, foi construído de forma particular e disponibilizado para a finalidade acima citada de forma 100% gratuita para os participantes. No entanto, ele não poderá ser copiado total ou parcialmente por nenhum participante.</p>

							<h2>Participação</h2>

							<p>4. Cada pessoa poderá participar uma única vez no Bolão da Copa 2018, mesmo que se disponha a pagar por uma segunda taxa de participação. Está autorizada a participação apenas dos funcionários LG, não sendo permitida a participação de outras pessoas ou ex-funcionários.</p>

							<p>5. Para efetivamente participar do Bolão da Copa 2018, o participante deverá informar seu nome, seu e-mail pessoal e efetuar o pagamento da taxa de participação para que possa ser cadastrado. Não utilize o e-mail corporativo no seu cadastro. O administrador ficará responsável por manter o valor arrecadado até o final da Copa 2018, efetuar o pagamento dos custos e realizar a premiação dos vencedores ao final da Copa.</p>

							<p>6. Vencerá o Bolão da Copa 2018 aquele participante que obtiver a maior quantidade de pontos, somando-se todas os pontos obtidos em suas apostas, que deverão ser feitas jogo a jogo. Também serão premiados os participantes que ficarem em 2o e 3o lugares. </p>

							<h2>Premiação</h2>

							<p>7. A premiação será calculada de acordo com a soma dos valores arrecadados conforme a fórmula abaixo:</p>

							<p><code>[Valor Total Líquido] = [Soma da taxa paga por cada participante] - [Custo de hospedagem na nuvem]</code></p>

							<p>
								Premiação 1o Lugar (50%) = [Valor Total Líquido] * 0,5<br />
								Premiação 2o Lugar (30%) = [Valor Total Líquido] * 0,3<br />
								Premiação 3o Lugar (20%) = [Valor Total Líquido] * 0,2
							</p>

							<h2>Pontuação</h2>

							<p>8. O participante deverá registrar uma aposta para cada jogo e, à medida em que os jogos forem acontecendo e os placares finais dos jogos serão registrados no sistema, ele receberá uma pontuação referente a cada aposta, conforme as regras abaixo:</p>

							<p>8.a. Acertou o placar exato da partida: <b>10 pontos</b></p>

							<p>
								<pre>Exemplo: Brasil x Costa Rica
Apostou 3 x 0
Placar do jogo: 3 x 0</pre>
							</p>

							<p>8.b. Acertou o time vitorioso e a diferença de gols: <b>6 pontos</b></p>

							<p>
								<pre>Exemplo: Brasil x Costa Rica
Apostou 2 x 0
Placar do jogo: 3 x 1</pre>
							</p>

							<p>8.c. Acertou o time vitorioso e os gols de um dos times: <b>4 pontos</b></p>

							<p>
								<pre>Exemplo: Brasil x Costa Rica
Apostou 2 x 0
Placar do jogo: 3 x 0</pre>
							</p>

							<p>8.d. Acertou apenas o time vitorioso: <b>2 pontos</b></p>

							<p>
								<pre>Exemplo: Brasil x Costa Rica
Apostou 2 x 0
Placar do jogo: 4 x 1</pre>
							</p>

							<p>8.e. Acertou que daria empate, mas errou por uma diferença de 1 gol: <b>6 pontos</b></p>

							<p>
								<pre>Exemplo: Brasil x Costa Rica
Apostou 2 x 2
Placar do jogo: 1 x 1</pre>
							</p>

							<p>8.f. Acertou que daria empate, mas errou por uma diferença de 2 gols ou mais: <b>4 pontos</b></p>

							<p>
								<pre>Exemplo: Brasil x Costa Rica
Apostou 2 x 2
Placar do jogo: 0 x 0</pre>
							</p>

							<p>8.g. Não acertou o vencedor e nem que daria empate, mas acertou os gols de um dos times: <b>1 ponto</b></p>

							<p>
								<pre>Exemplo: Brasil x Costa Rica
Apostou 2 x 2
Placar do jogo: 3 x 2</pre>
							</p>

							<p>8.h. Não acertou nada no placar, ou não registrou a aposta no jogo: <b>0 pontos</b></p>

							<p>
								<pre>Exemplo: Brasil x Costa Rica
Apostou 2 x 2
Placar do jogo: 1 x 0</pre>
							</p>

							<p>9. A apuração das apostas será feita jogo a jogo, e todos os jogos da Copa 2018 serão contabilizados, mesmo das fases eliminatórias, inclusive a final e o jogo que define o 3o colocado da Copa.</p>

							<p>10. Nas fases eliminatórias (oitavas de finais em diante), o sistema irá considerar somente o placar obtido durante o tempo regular de jogo, ou seja, durante os 90 minutos + acréscimos. Serão desconsiderados os gols marcados durante a prorrogação, acréscimos da prorrogação, e nas eventuais cobranças de penaltis.</p>

							<p>
								<pre>Exemplo: Brasil x França
Apostou 2 x 1
Placar ao final dos 90 minutos de jogo: <b>1 x 1</b>
Placar ao final da prorrogação: 2 x 1</pre>
							</p>

							<p>
								<cite>Neste exemplo, o placar a ser considerado será o de "1 x 1" para apurar a pontuação da aposta.
									<br />
									Ou seja, o participante ganhará 1 ponto por ter errado o placar mas acertado os gols de um dos times.
								</cite>
							</p>

							<p>11. Os participantes terão até o dia anterior ao da partida para registrarem sua aposta. Por isso, é importante que o participante fique atento para registrar suas apostas com antecedência para não deixar de pontuar. Caso o participante queira mudar sua aposta, ele poderá fazê-lo livremente, por quantas vezes quiser, até o dia anterior ao da partida em questão.</p>

							<p>12. Como os placares dos jogos serão registrados manualmente ao final de cada partida, poderá haver algum atraso no cômputo da pontuação dos jogos. </p>

							<p>13. À medida em que os placares reais dos jogos vão sendo registrados, o sistema fará o cálculo da pontuação para todas as apostas dos jogos já realizados, e fará também o ranqueamento dos participantes, conforme a seguinte regra de ordenação:</p>

							<code>
								<ol>

									<li>Maior quantidade de pontos</li>
									<li>Maior quantidade de vezes em que obteve 10 pontos</li>
									<li>Maior quantidade de vezes em que obteve 6 pontos</li>
									<li>Maior quantidade de vezes em que obteve 4 pontos</li>
									<li>Maior quantidade de vezes em que obteve 2 pontos</li>
									<li>Maior quantidade de vezes em que obteve 1 pontos</li>
								</ol>
							</code>

							<p>14. Na hipótese de, ao final do Bolão da Copa 2018, tivermos dois ou mais participantes exatamente empatados nas primeiras posições do ranking, o desempate será feito por sorteio - que deverá ser presenciado por pelo menos 2 outros participantes. </p>

							<p>15. Em caso de desistência do participante, por qualquer motivo, a taxa de participação não será devolvida, e continuará sendo considerada no cálculo da premiação final.</p>

							<p>16. Caso haja indisponibilidade do sistema, dependendo do tempo de indisponibilidade, pode-se ter que desconsiderar algum jogo do cômputo geral, mas isso só será feito em último caso e comunicado a todos os participantes. Caso se perceba que não há mais condições de se manter o sistema funcionando, o jogo será cancelado e as taxas de participação serão devolvidas.</p>

							<p>17. O sistema será responsável por gerar trilhas de auditoria para cada acesso dos participantes, gerando o log das apostas e suas mudanças, de forma a que se tenha comprovação do que de fato foi feito por cada participante.</p>

							<p>18. As senhas dos participantes serão criptografadas e o administrador do sistema não possuirá nenhuma forma de acessar diretamente o banco de dados do sistema, que ficará sob gestão exclusiva do Moisés Mitrioni, que hoje garante a segurança dos ambientes dos clientes LG HCM na nuvem.</p>

							<p>19. Na ocorrência de qualquer problema, gentileza informar o ocorrido pelo email <a href="mailto:subscribe.bolaocopa2018@gmail.com">subscribe.bolaocopa2018@gmail.com</a>, com o máximo de detalhes possível. Caso seja percebido que o problema gerou impactos em mais participantes, a comunicação do impacto e a resolução do problema se dará através do e-mail cadastrado no sistema.</p>

							<h3>Boa sorte!</h3>
						</div>
					</div>
				</div>
			</div>
		</div>
	</form>
</body>
</html>
