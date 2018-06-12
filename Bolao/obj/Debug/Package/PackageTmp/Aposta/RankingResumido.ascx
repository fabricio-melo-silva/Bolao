<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RankingResumido.ascx.cs" Inherits="Bolao.Aposta.RankingResumido" %>
<div class="m-t-30">
	<asp:Label ID="lbMensagem" runat="server" Visible="False"></asp:Label>
	<asp:DataGrid ID="dgUsuarios" runat="server" AutoGenerateColumns="False" AllowSorting="True" CssClass="resultado table table-striped table-hover" CellSpacing="0" BorderWidth="0px">
		<HeaderStyle CssClass="cabecalho" />
		<AlternatingItemStyle CssClass="alternado" />
		<Columns>
			<asp:BoundColumn DataField="num_ranking" SortExpression="num_ranking" ItemStyle-Width="25px"></asp:BoundColumn>
			<asp:BoundColumn DataField="nom_usuario" SortExpression="nom_usuario" HeaderText="Participante"></asp:BoundColumn>
			<asp:BoundColumn DataField="vlr_pontuacao" SortExpression="vlr_pontuacao" HeaderText="Pontuação"></asp:BoundColumn>
		</Columns>
	</asp:DataGrid>
</div>
