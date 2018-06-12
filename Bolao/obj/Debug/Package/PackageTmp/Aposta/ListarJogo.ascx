<%@ Control Language="c#" Inherits="Bolao.Aposta.ListarJogo" CodeBehind="ListarJogo.ascx.cs" %>
<div>
	<p>
		<asp:Button ID="btApostar1" Runat="server" Text="Salvar Placares" OnClick="Apostar"></asp:Button>
	</p>
	<p>
		<asp:Label ID="lbMensagem" Runat="server" Visible="False" CssClass="aviso">Não existem jogos para esta fase.</asp:Label>
		<asp:DataGrid ID="dgJogos" Runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="resultado" CellPadding="4" CellSpacing="0" BorderWidth="0px">
			<HeaderStyle CssClass="cabecalho"></HeaderStyle>
			<ItemStyle CssClass="item"></ItemStyle>
			<AlternatingItemStyle CssClass="alternado"></AlternatingItemStyle>
			<Columns>
				<asp:TemplateColumn HeaderText="Jogo" SortExpression="num_jogo">
					<ItemTemplate>
						<input type="hidden" id="hdCodigoJogo" runat="server" NAME="hdCodigoJogo">
						<asp:Label ID="lbNumeroJogo" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:Image ID="imgTimeA" Runat="server"></asp:Image><br />
						<asp:Label ID="lbTimeA" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn>
					<ItemStyle Wrap="False" VerticalAlign="middle"></ItemStyle>
					<ItemTemplate>
						<asp:Label ID="lbGolsA" Runat="server" CssClass="resultado-jogo"></asp:Label><asp:TextBox ID="tbGolsA" Runat="server" MaxLength="2" CssClass="aposta-jogo"></asp:TextBox>
						x
						<asp:TextBox ID="tbGolsB" Runat="server" MaxLength="2" CssClass="aposta-jogo"></asp:TextBox><asp:Label ID="lbGolsB" Runat="server" CssClass="resultado-jogo"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:Image ID="imgTimeB" Runat="server"></asp:Image><br />
						<asp:Label ID="lbTimeB" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Data" SortExpression="dat_jogo">
					<ItemTemplate>
						<asp:Label ID="lbData" Runat="server"></asp:Label><br />
						<asp:Label ID="lbHora" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Local" SortExpression="dsc_local">
					<ItemTemplate>
						<asp:Label ID="lbLocal" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Grupo" SortExpression="nom_grupo">
					<ItemTemplate>
						<asp:Label ID="lbGrupo" Runat="server"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Pontos">
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
					<ItemTemplate>
						<asp:Label ID="lbPontuacao" Runat="server" CssClass="pontuacao"></asp:Label>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
	<p>
		<asp:Button ID="btApostar2" Runat="server" Text="Salvar Placares" OnClick="Apostar"></asp:Button>
	</p>
	<br />
	<div>
		<span>Legenda:</span><br />
		<div class="legendaNormal">Jogo cujo resultado ainda não foi computado</div>
		<div class="legendaRealizado">Jogo cujo resultado foi computado</div>
		<div class="legendaInvalido">Aposta inválida</div>
	</div>
</div>