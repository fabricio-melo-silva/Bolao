using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using W3.Library;

namespace Bolao.Aposta {
	public partial class _Default : BasePage {
		private int CodUsuario {
			get {
				return Convert.ToInt32(Session["CodUsuario"]);
			}
		}

		private int CodBolao {
			get {
				return Convert.ToInt32(Session["CodBolao"]);
			}
		}

		private int CodFiltroFase {
			get {
				return Convert.ToInt32(ddlFiltroFase.SelectedValue);
			}
		}

		private string CodFiltroJogo {
			get {
				return Convert.ToString(ddlFiltroJogo.SelectedValue);
			}
		}

		private bool IndPago {
			get {
				return Convert.ToBoolean(Session["IndPago"]);
			}
		}

		private int CodFaseAuxiliar { get; set; }

		protected void Page_Load(object sender, EventArgs e) {
			Lib.Util.ValidarLogin(this.Context);

			if (!this.IsPostBack) {
				this.Inicializar();
			}
		}

		private void Inicializar() {
			this.CarregarCabecalho();
			this.CarregarFiltros();
			this.CarregarJogos();
		}

		private void CarregarCabecalho() {
			ucMenu.Inicializar(Menu.LinkMenu.Aposta);
			ucCabecalho.Inicializar();
		}

		private void CarregarFiltros() {
			int codFaseDefault = 0;

			var fases = Dados.Fase.ListarFase(this.CodBolao);

			var fasesAbertas = from f in fases
							   where "A".Equals(f.Status, StringComparison.InvariantCultureIgnoreCase)
							   select f.CodigoFase;

			var fasesAbertasFinalizadas = from f in fases
										  where "A".Equals(f.Status, StringComparison.InvariantCultureIgnoreCase)
										     || "F".Equals(f.Status, StringComparison.InvariantCultureIgnoreCase)
										  select f;

			if (fasesAbertas.Count() > 0) {
				codFaseDefault = fasesAbertas.FirstOrDefault<int>();
			}

			ddlFiltroFase.DataSource = fasesAbertasFinalizadas;
			ddlFiltroFase.DataTextField = "NomeFase";
			ddlFiltroFase.DataValueField = "CodigoFase";
			ddlFiltroFase.DataBind();

			if (codFaseDefault != 0) {
				ddlFiltroFase.SelectedIndex = -1;
				ddlFiltroFase.Items.FindByValue(codFaseDefault.ToString()).Selected = true;
			}
		}

		private void CarregarJogos() {
			int codUsuario = this.CodUsuario;
			int codBolao = this.CodBolao;
			int codFase = this.CodFiltroFase;
			int codGrupo = 0;
			string filtroJogo = this.CodFiltroJogo;

			this.CodFaseAuxiliar = 0;

			DataView jogos = new DataView(Dados.Aposta.BuscarApostaBolao(codBolao, codFase, codGrupo, codUsuario, filtroJogo));

			bool haRegistros = jogos.Count > 0;

			rpJogos.DataSource = jogos;
			rpJogos.DataBind();
			rpJogos.Visible = pnLegenda.Visible = haRegistros;

			lbMensagem.Visible = !haRegistros;
			lbMensagem.Text = "<br /><br />Não existem jogos para esta fase.";
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) {
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {

		}
		#endregion

		protected void rpJogos_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
				Label lbTimeA = e.Item.FindControl("lbTimeA") as Label;
				Label lbTimeB = e.Item.FindControl("lbTimeB") as Label;
				Label lbTimeAPequeno = e.Item.FindControl("lbTimeAPequeno") as Label;
				Label lbTimeBPequeno = e.Item.FindControl("lbTimeBPequeno") as Label;
				Label lbDescricaoPontuacao = e.Item.FindControl("lbDescricaoPontuacao") as Label;
				Literal ltResultadoTimeA = e.Item.FindControl("ltResultadoTimeA") as Literal;
				Literal ltResultadoTimeB = e.Item.FindControl("ltResultadoTimeB") as Literal;
				Literal ltPontuacaoJogo = e.Item.FindControl("ltPontuacaoJogo") as Literal;
				TextBox tbPlacarTimeA = e.Item.FindControl("tbPlacarTimeA") as TextBox;
				TextBox tbPlacarTimeB = e.Item.FindControl("tbPlacarTimeB") as TextBox;
				System.Web.UI.WebControls.Image imgTimeA = e.Item.FindControl("imgTimeA") as System.Web.UI.WebControls.Image;
				System.Web.UI.WebControls.Image imgTimeB = e.Item.FindControl("imgTimeB") as System.Web.UI.WebControls.Image;
				System.Web.UI.WebControls.Image imgTimeAPequeno = e.Item.FindControl("imgTimeAPequeno") as System.Web.UI.WebControls.Image;
				System.Web.UI.WebControls.Image imgTimeBPequeno = e.Item.FindControl("imgTimeBPequeno") as System.Web.UI.WebControls.Image;
				HtmlInputHidden hdCodigoJogo = e.Item.FindControl("hdCodigoJogo") as HtmlInputHidden;
				Literal ltFase = e.Item.FindControl("ltFase") as Literal;
				Literal ltDataHoraLocalGrupo = e.Item.FindControl("ltDataHoraLocalGrupo") as Literal;
				Panel pnJogo = e.Item.FindControl("pnJogo") as Panel;
				Panel pnPlacarFinal = e.Item.FindControl("pnPlacarFinal") as Panel;

				int numJogo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "num_jogo"));
				int codJogo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_jogo"));
				int codFase = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_fase"));
				DateTime datJogo = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "dat_jogo"));
				DateTime datLimite = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "dat_limite_aposta"));
				string dscLocal = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_local"));
				string dscFase = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_fase"));
				string nomGrupo = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "nom_grupo"));
				string nomTimeA = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "nom_time_a"));
				string nomTimeB = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "nom_time_b"));
				string qtdGolsA = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "qtd_gol_a"));
				string qtdGolsB = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "qtd_gol_b"));
				string qtdGolsApostaA = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "qtd_gol_aposta_a"));
				string qtdGolsApostaB = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "qtd_gol_aposta_b"));
				string dscIconeA = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_icone_a"));
				string dscIconeB = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_icone_b"));
				string indRealizado = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ind_realizado"));
				string indApurado = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ind_apurada"));
				string dscCriterio = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_criterio"));
				string vlrPontuacao = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "vlr_pontuacao"));

				bool realizado = indRealizado == "S";
				bool apurado = indApurado == "S";

				if (codFase != this.CodFaseAuxiliar) {
					this.CodFaseAuxiliar = codFase;
					ltFase.Text = String.Format("<h3 class=\"titulo-h3-conteudo\">{0}</h3>", dscFase);
				}
				else {
					ltFase.Visible = false;
				}
				
				ltDataHoraLocalGrupo.Text = String.Format("<h4 class=\"texto-dataHoraLocal{0}\" jogo=\"{1}\">Jogo {2} - {3}h - {4}<span class=\"label-grupo\">{5}</span></h4>",
					(realizado ? (String.IsNullOrEmpty(qtdGolsApostaA) || String.IsNullOrEmpty(qtdGolsApostaB) ? " status-invalido" : " status-computado") : ""),
					codJogo.ToString(),
					numJogo.ToString(),
					datJogo.ToString("dd/MMM HH:mm"),
					dscLocal,
					nomGrupo
				);

				pnJogo.CssClass = (e.Item.ItemType == ListItemType.Item) ? "aposta even row" : "aposta row";
				pnPlacarFinal.Visible = apurado || realizado;

				 hdCodigoJogo.Value = codJogo.ToString();
				lbTimeA.Text = lbTimeAPequeno.Text = nomTimeA;
				lbTimeB.Text = lbTimeBPequeno.Text = nomTimeB;
				ltResultadoTimeA.Text = qtdGolsA;
				ltResultadoTimeB.Text = qtdGolsB;
				ltPontuacaoJogo.Text = String.IsNullOrEmpty(vlrPontuacao) ? "<span class=\"span-pontos-ganhos\">Não pontuou</span>" : String.Format("<span class=\"span-pontos-ganhos\">Voce ganhou <b>{0}</b> ponto(s)</span>", vlrPontuacao);
				lbDescricaoPontuacao.Text = String.IsNullOrEmpty(dscCriterio) ? "Placar inválido" : dscCriterio;
				tbPlacarTimeA.Text = qtdGolsApostaA;
				tbPlacarTimeB.Text = qtdGolsApostaB;
				tbPlacarTimeA.Attributes["min"] = "0";
				tbPlacarTimeB.Attributes["min"] = "0";
				tbPlacarTimeA.Attributes["time"] = "A";
				tbPlacarTimeB.Attributes["time"] = "B";
				tbPlacarTimeA.Attributes["onpaste"] = "return false";
				tbPlacarTimeB.Attributes["onpaste"] = "return false";
				tbPlacarTimeA.Attributes["jogo"] = codJogo.ToString();
				tbPlacarTimeB.Attributes["jogo"] = codJogo.ToString();
				tbPlacarTimeA.Enabled = !realizado && (datLimite.CompareTo(DateTime.Now) > 0) && IndPago;
				tbPlacarTimeB.Enabled = !realizado && (datLimite.CompareTo(DateTime.Now) > 0) && IndPago;
				imgTimeA.ImageUrl = imgTimeAPequeno.ImageUrl = "../Images/Times/" + dscIconeA;
				imgTimeB.ImageUrl = imgTimeBPequeno.ImageUrl = "../Images/Times/" + dscIconeB;
			}
		}

		protected void ddlFiltroFase_SelectedIndexChanged(object sender, EventArgs e) {
			this.CarregarJogos();
		}

		protected void ddlFiltroJogo_SelectedIndexChanged(object sender, EventArgs e) {
			this.CarregarJogos();
		}
	}
}