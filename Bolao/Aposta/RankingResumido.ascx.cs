using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bolao.Aposta {
	public partial class RankingResumido : BaseRankingControl {
		private string SortExpression {
			get { return Convert.ToString(ViewState["SortExpression"]); }
			set { ViewState["SortExpression"] = value; }
		}

		private int CodBolao {
			get {
				return Convert.ToInt32(Session["CodBolao"]);
			}
		}

		protected void Page_Load(object sender, EventArgs e) {
			if (!this.IsPostBack) {
				this.SortExpression = "vlr_pontuacao desc";
			}
		}

		public override void Inicializar(int codBolao) {
			Dados.Bolao bolao = new Dados.Bolao();

			DataView ranking = new DataView(bolao.BuscarRanking(codBolao));

			ranking.Sort = this.SortExpression;

			dgUsuarios.DataSource = ranking;
			dgUsuarios.DataBind();
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
			this.dgUsuarios.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgUsuarios_SortCommand);
			this.dgUsuarios.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgUsuarios_ItemDataBound);

		}
		#endregion

		private void dgUsuarios_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e) {
			if (!this.SortExpression.ToLower().StartsWith(e.SortExpression.ToLower())) {
				this.SortExpression = e.SortExpression + " asc";
			}
			else if (this.SortExpression.ToLower().EndsWith(" asc")) {
				this.SortExpression = this.SortExpression.Replace(" asc", " desc");
			}
			else {
				this.SortExpression = this.SortExpression.Replace(" desc", " asc");
			}

			this.Inicializar(this.CodBolao);
		}

		private void dgUsuarios_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e) {
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
				if (e.Item.Cells[1].Text == Convert.ToString(Session["NomUsuario"])) {
					e.Item.CssClass = "classificado";
				}
			}
		}
	}
}