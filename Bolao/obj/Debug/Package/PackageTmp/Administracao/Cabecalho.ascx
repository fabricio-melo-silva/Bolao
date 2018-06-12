<%@ Control Language="c#" Inherits="Bolao.Administracao.Cabecalho" CodeBehind="Cabecalho.ascx.cs" %>
<div class="cabecalho">
	<div>
		<table cellpadding="0" cellspacing="0" border="0" width="100%" height="80">
			<tr>
				<td width="15">&nbsp;</td>
				<td class="bolao-nome">
					Administração
				</td>
				<td class="bolao-selecao">
				</td>
				<td width="15">&nbsp;</td>
			</tr>
		</table>
	</div>
	<div class="identificacao">
		<asp:Label ID="lbNomeUsuario" Runat="server"></asp:Label>
	</div>
</div>
<div class="menu">
	<asp:HyperLink ID="hlJogos" Runat="server" NavigateUrl="../Administracao/Jogo.aspx">Resultado dos Jogos</asp:HyperLink>
	<asp:HyperLink ID="hlBolao" Runat="server" NavigateUrl="../Administracao/Bolao.aspx">Cadastro de Bolão</asp:HyperLink>
	<asp:HyperLink ID="hlTime" Runat="server" NavigateUrl="../Administracao/Usuario.aspx">Cadastro de Usuário</asp:HyperLink>
	<asp:HyperLink ID="HyperLink1" Runat="server" NavigateUrl="../Administracao/Time.aspx">Cadastro de Time</asp:HyperLink>
	<asp:HyperLink ID="hlRanking" Runat="server" NavigateUrl="../Administracao/Ranking.aspx">Ranking</asp:HyperLink>
	<asp:HyperLink ID="hlGrupos" Runat="server" NavigateUrl="../Aposta/Default.aspx">Área de Apostas</asp:HyperLink>
	<asp:LinkButton ID="lbtSair" Runat="server" onclick="lbtSair_Click">Sair</asp:LinkButton>
</div>
