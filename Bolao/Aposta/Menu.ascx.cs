using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using W3.Library;
using Bolao.Dados;

namespace Bolao.Aposta {
	/// <summary>
	///		Summary description for Cabecalho.
	/// </summary>
	public partial class Menu : BaseControl {
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

		private bool Administrador {
			get {
				return Convert.ToBoolean(Session["IndAdministrador"]);
			}
		}

		public enum LinkMenu { Aposta, Ranking, Regras }

		protected void Page_Load(object sender, System.EventArgs e) {
			if (!Page.IsPostBack) {
			}
		}

		public void Inicializar(LinkMenu menuSelecionado) {
			string cssAtivo = "text-menu active";
			string cssInativo = "text-menu";

			hlAdministracao.Visible = hlAdministracaoColapsado.Visible = this.Administrador;
			hlJogos.CssClass = hlJogosColapsado.CssClass = cssInativo;
			hlRegras.CssClass = hlRegrasColapsado.CssClass = cssInativo;
			hlRanking.CssClass = hlRankingColapsado.CssClass = cssInativo;

			switch (menuSelecionado) {
				case LinkMenu.Aposta:
					hlJogos.CssClass = hlJogosColapsado.CssClass = cssAtivo;
					break;
				case LinkMenu.Ranking:
					hlRanking.CssClass = hlRankingColapsado.CssClass = cssAtivo;
					break;
				case LinkMenu.Regras:
					hlRegras.CssClass = hlRegrasColapsado.CssClass = cssAtivo;
					break;
			}
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
			Response.Redirect("../Sair.aspx");
		}
	}
}
