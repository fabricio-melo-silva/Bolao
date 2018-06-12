<%@ Control Language="c#" Inherits="Bolao.Aposta.Menu" CodeBehind="Menu.ascx.cs" %>

<div class="collapseMenu navbar-collapse collapse" id="collapseMenu">
	<asp:HyperLink ID="hlJogosColapsado" runat="server" NavigateUrl="Default.aspx" CssClass="text-menu" Text="Jogos"></asp:HyperLink>
	<asp:HyperLink ID="hlRankingColapsado" runat="server" NavigateUrl="Ranking.aspx" CssClass="text-menu" Text="Ranking"></asp:HyperLink>
	<asp:HyperLink ID="hlAdministracaoColapsado" runat="server" NavigateUrl="../Administracao/Jogo.aspx" CssClass="text-menu" Text="Admin"></asp:HyperLink>
	<asp:HyperLink ID="hlSairColapsado" runat="server" NavigateUrl="../Sair.aspx" CssClass="text-menu" Text="Sair"></asp:HyperLink>
</div>

<div class="banner-topo">
	<nav class="navbar">
		<div class="container">
			<div class="navbar-header nav-topo">
				<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#collapseMenu" aria-expanded="false" aria-controls="collapseMenu">
					<i class="fa fa-bars bt-hamburger"></i>
				</button>

<%--				<a class="navbar-brand" href="#">
					<img title="Bolão 2018" src="../Images/logomarca.png" />
				</a>--%>
			</div>

			<div id="navbar">
				<asp:HyperLink ID="hlJogos" runat="server" NavigateUrl="Default.aspx" CssClass="text-menu" Text="Jogos"></asp:HyperLink>
				<asp:HyperLink ID="hlRanking" runat="server" NavigateUrl="Ranking.aspx" CssClass="text-menu" Text="Ranking"></asp:HyperLink>
				<asp:HyperLink ID="hlAdministracao" runat="server" NavigateUrl="../Administracao/Jogo.aspx" CssClass="text-menu" Text="Admin"></asp:HyperLink>
				<asp:HyperLink ID="hlSair" runat="server" NavigateUrl="../Sair.aspx" CssClass="text-menu" Text="Sair"></asp:HyperLink>
			</div>
			<!--/.nav-collapse -->
		</div>
	</nav>
</div>
