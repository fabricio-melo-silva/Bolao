<%@ Control Language="c#" Inherits="Bolao.Cadastro.Cabecalho" CodeBehind="Cabecalho.ascx.cs" %>
<div class="cabecalho">
	<div>
		<table cellpadding="0" cellspacing="0" border="0" width="100%" height="42">
			<tr>
				<td width="15">&nbsp;</td>
				<td class="bolao-nome">
					<span class="bolao">Bolão</span>
				</td>
				<td class="bolao-selecao">
				</td>
				<td width="15">&nbsp;</td>
			</tr>
		</table>
	</div>
	<div class="identificacao">
	</div>
</div>
<div class="menu">
	<asp:LinkButton ID="lbtVoltar" Runat="server" onclick="lbtVoltar_Click">Voltar</asp:LinkButton>
	<asp:LinkButton ID="lbtSair" Runat="server" onclick="lbtSair_Click">Sair</asp:LinkButton>
</div>
