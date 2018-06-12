<%@ Control Language="c#" Inherits="Bolao.Administracao.ListarTimeGrupo" CodeBehind="ListarTimeGrupo.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="EditarTimeGrupo" Src="EditarTimeGrupo.ascx" %>
<asp:Panel ID="pnListar" Runat="server">
	<h1>Times do Grupo</h1>
	<p>
		<b>Bolão:</b>
		<asp:Label ID="lbNomeBolao" Runat="server"></asp:Label>
		<br><br>
		<b>Fase:</b>
		<asp:Label ID="lbNomeFase" Runat="server"></asp:Label>
		<br><br>
		<b>Grupo:</b>
		<asp:Label ID="lbNomeGrupo" Runat="server"></asp:Label>
	</p>
	<p>
		<asp:Button ID="btIncluirTime" Runat="server" Text="Incluir Time" onclick="btIncluirTime_Click"></asp:Button>
		<asp:Button ID="btVoltar" Runat="server" Text="Voltar" onclick="btVoltar_Click"></asp:Button>
	</p>
	<p>
		<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existe nenhum time cadastrado.</asp:Label>
		<asp:DataGrid ID="dgTime" Runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
			<HeaderStyle CssClass="cabecalho"></HeaderStyle>
			<ItemStyle CssClass="item"></ItemStyle>
			<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
			<Columns>
				<asp:BoundColumn DataField="nom_time" HeaderText="Time" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
				<asp:BoundColumn DataField="sgl_time" HeaderText="Sigla" ItemStyle-Width="50px"></asp:BoundColumn>
				<asp:BoundColumn DataField="qtd_ponto" HeaderText="PG" ItemStyle-Width="30px"></asp:BoundColumn>
				<asp:BoundColumn DataField="qtd_vitoria" HeaderText="V" ItemStyle-Width="30px"></asp:BoundColumn>
				<asp:BoundColumn DataField="qtd_empate" HeaderText="E" ItemStyle-Width="30px"></asp:BoundColumn>
				<asp:BoundColumn DataField="qtd_derrota" HeaderText="D" ItemStyle-Width="30px"></asp:BoundColumn>
				<asp:BoundColumn DataField="qtd_gol_feito" HeaderText="GF" ItemStyle-Width="30px"></asp:BoundColumn>
				<asp:BoundColumn DataField="qtd_gol_sofrido" HeaderText="GC" ItemStyle-Width="30px"></asp:BoundColumn>
				<asp:BoundColumn DataField="qtd_saldo_gol" HeaderText="SC" ItemStyle-Width="30px"></asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Ícone">
					<ItemTemplate>
						<asp:Image ID="imgIcone" Runat="server"></asp:Image>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Excluir">
					<ItemTemplate>
						<asp:Button ID="btExcluir" Runat="server" Text="E" CssClass="botao" OnClick="ExcluirTime"></asp:Button>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</asp:Panel>
<uc:EditarTimeGrupo id="ucEditarTimeGrupo" runat="Server" Visible="False"></uc:EditarTimeGrupo>
