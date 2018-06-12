<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cabecalho.ascx.cs" Inherits="Bolao.Aposta.Cabecalho" %>
<div class="row m-t-negativo">
	<div class="col-md-12 clearfix">
		<div class="box-branco clearfix box-info-header">
			<div class="col-sm-6">
				<div class="row">
					<div class="col-md-12 col-xs-12 p-zero">
						<asp:Label ID="lbNomeUsuario" runat="server" CssClass="text-nome-usuario col-xs-12 p-zero"></asp:Label><br /><br />
					</div>
				</div>
			</div>
			<div class="col-sm-6">
				<div class="row">
					<div class="col-md-6 col-xs-6 p-zero">
						<i class="fa fa-star ico-grande"></i>&nbsp;
						<asp:Label ID="lbPontuacao" runat="server" CssClass="label-menor"></asp:Label>
					</div>
					<div class="col-md-6 col-xs-6 p-zero">
						<i class="fa fa-trophy ico-grande"></i>&nbsp;
						<asp:Label ID="lbRankingUsuario" runat="server" CssClass="label-menor"></asp:Label>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
