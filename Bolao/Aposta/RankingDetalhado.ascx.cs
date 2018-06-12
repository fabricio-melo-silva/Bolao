using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using W3.Library;

namespace Bolao.Aposta {
	public partial class RankingDetalhado : BaseRankingControl {
		private Dados.Jogo jogo;
		private Dados.Bolao bolao;

		protected void Page_Load(object sender, System.EventArgs e) {
			jogo = new Dados.Jogo();
			bolao = new Dados.Bolao();
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
			DataTable jogos = jogo.BuscarJogo(this.CodBolao, 0, 0);
			DataTable ranking = new DataTable();

			ranking.Columns.Add("Posição");
			ranking.Columns.Add("Participante");

			foreach (DataRow item in jogos.Rows) {
				int numJogo = Convert.ToInt32(item["num_jogo"]);
				string indRealizado = Convert.ToString(item["ind_realizado"]);

				if (indRealizado == "S") ranking.Columns.Add(numJogo.ToString());
			}

			ranking.Columns.Add("Total");

			foreach (DataRow item in pessoas.Rows) {
				DataRow novaLinha = ranking.NewRow();

				novaLinha["Posição"] = Convert.ToInt32(item["num_ranking"]);
				novaLinha["Participante"] = Convert.ToString(item["nom_usuario"]);

				DataTable apostas = Dados.Aposta.BuscarAposta(this.CodBolao, 0, 0, Convert.ToInt32(item["cod_usuario"]), "T");

				foreach (DataRow aposta in apostas.Rows) {
					int numJogo = Convert.ToInt32(aposta["num_jogo"]);
					string indRealizado = Convert.ToString(aposta["ind_realizado"]);

					if (indRealizado == "S") novaLinha[numJogo.ToString()] = Convert.ToInt32("0" + Convert.ToString(aposta["vlr_pontuacao"]));
				}

				novaLinha["Total"] = Convert.ToString(item["vlr_pontuacao"]);

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
			if (e.Item.ItemType == ListItemType.Header) {
				for (int i = 0; i < e.Item.Cells.Count; i++) {
					e.Item.Cells[i].Wrap = false;

					if (i == 0) {
						e.Item.Cells[i].Style["width"] = "25px";
						e.Item.Cells[i].Text = "";
					}
					else if ((i > 1) && (i != e.Item.Cells.Count - 1)) {
						e.Item.Cells[i].Width = new Unit("25px");

						int numJogo = Convert.ToInt32(e.Item.Cells[i].Text);

						DataRow registro = jogo.BuscarPlacarJogo(numJogo);

						e.Item.Cells[i].Style["cursor"] = "pointer";
						e.Item.Cells[i].Attributes["title"] = "Jogo " + numJogo.ToString() + ": " +
							Convert.ToString(registro["nom_time_a"]) + " " +
							Convert.ToString(registro["qtd_gol_a"]) + " x " +
							Convert.ToString(registro["qtd_gol_b"]) + " " +
							Convert.ToString(registro["nom_time_b"]);
					}
				}
			}
			else if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item) {
				e.Item.Cells[1].Style["text-align"] = "left";
				e.Item.Cells[1].Wrap = false;

				if (e.Item.Cells[1].Text == Convert.ToString(Session["NomUsuario"])) {
					e.Item.CssClass = "classificado";
				}
			}
		}
	}
}
