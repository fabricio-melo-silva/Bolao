<%@ Control Language="c#" Inherits="Bolao.Aposta.RankingPorFase" CodeBehind="RankingPorFase.ascx.cs" %>
<div class="m-t-30">
	<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existem participantes ranqueados no Bolão.</asp:Label>
	<asp:DataGrid ID="dgRanking" Runat="server" AutoGenerateColumns="True" CssClass="resultado table table-striped table-hover" CellSpacing="0" BorderWidth="0px">
		<HeaderStyle Wrap="False" CssClass="cabecalho"></HeaderStyle>
		<ItemStyle Wrap="False"></ItemStyle>
		<AlternatingItemStyle Wrap="False" CssClass="alternado"></AlternatingItemStyle>
	</asp:DataGrid>
</div>
