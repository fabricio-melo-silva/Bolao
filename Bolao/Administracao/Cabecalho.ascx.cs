using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Bolao.Administracao {
	/// <summary>
	///		Summary description for Cabecalho.
	/// </summary>
	public partial class Cabecalho : System.Web.UI.UserControl {
		protected System.Web.UI.WebControls.Label lbNomeBolao;
		protected System.Web.UI.WebControls.DropDownList ddlBolao;

		protected void Page_Load(object sender, System.EventArgs e) {
			if (!Page.IsPostBack) {
				this.MarcarMenu();
			}
		}

		private void MarcarMenu() {
			string selecionado = "selecionado";

			if (this.Page is Bolao) hlBolao.CssClass = selecionado;
			else if (this.Page is Jogo) hlJogos.CssClass = selecionado;
			else if (this.Page is Usuario) hlTime.CssClass = selecionado;
			else if (this.Page is Ranking) hlRanking.CssClass = selecionado;
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

		}
		#endregion

		protected void lbtSair_Click(object sender, System.EventArgs e) {
			Session.Abandon();
			Response.Redirect("../Default.aspx");
		}
	}
}
