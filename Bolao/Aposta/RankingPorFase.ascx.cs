using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using W3.Library;

namespace Bolao.Aposta {
	public partial class RankingPorFase : BaseRankingControl {
		private Dados.Jogo jogo;
		private Dados.Bolao bolao;
		private Dados.Fase fase;

		protected void Page_Load(object sender, System.EventArgs e) {
			jogo = new Dados.Jogo();
			bolao = new Dados.Bolao();
			fase = new Dados.Fase();
		}

		private int CodBolao {
			get {
				if (ViewState["CodBolao"] == null) ViewState["CodBolao"] = 0;
				return Convert.ToInt32(ViewState["CodBolao"]);
			}
			set { ViewState["CodBolao"] = value; }
		}

		public override void Inicializar(int codBolao) {
			this.CodBolao = codBolao;
			this.CarregarRanking();
		}

		public void CarregarRanking() {
			DataTable pessoas = bolao.BuscarRanking(this.CodBolao);
			DataTable fases = fase.BuscarFase(this.CodBolao);
			DataTable ranking = new DataTable();

			ranking.Columns.Add("Posição");
			ranking.Columns.Add("Participante");

			foreach (DataRow item in fases.Rows) {
				ranking.Columns.Add(Convert.ToString(item["dsc_fase"]));
			}

			ranking.Columns.Add("Total");

			foreach (DataRow item in pessoas.Rows) {
				DataRow novaLinha = ranking.NewRow();

				novaLinha["Posição"] = Convert.ToInt32(item["num_ranking"]);
				novaLinha["Participante"] = Convert.ToString(item["nom_usuario"]);

				DataTable apostas = Dados.Aposta.BuscarResultadoPorFase(this.CodBolao, Convert.ToInt32(item["cod_usuario"]));

				int somaPontuacao = 0;

				foreach (DataRow aposta in apostas.Rows) {
					string dscFase = Convert.ToString(aposta["dsc_fase"]);
					int vlrPontuacao = Convert.ToInt32(aposta["vlr_pontuacao"]);

					novaLinha[dscFase] = vlrPontuacao;
					somaPontuacao += vlrPontuacao;
				}

				novaLinha["Total"] = somaPontuacao;

				ranking.Rows.Add(novaLinha);
			}

			dgRanking.DataSource = ranking;
			dgRanking.DataBind();
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.dgRanking.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgRanking_ItemDataBound);

		}
		#endregion

		private void dgRanking_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e) {
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item) {
				e.Item.Cells[0].Style["width"] = "25px";
				e.Item.Cells[1].Style["text-align"] = "left";
				e.Item.Cells[1].Wrap = false;

				if (e.Item.Cells[1].Text == Convert.ToString(Session["NomUsuario"])) {
					e.Item.CssClass = "classificado";
				}
			}
			else if (e.Item.ItemType == ListItemType.Header) {
				e.Item.Cells[0].Text = "";
			}
		}
	}
}
