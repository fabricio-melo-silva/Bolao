//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\aposta\Stub_ranking_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'aposta\ranking.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using W3.Library;

namespace Bolao.Aposta {
	public partial class Ranking : BasePage {

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

		protected void Page_Load(object sender, System.EventArgs e) {
			Lib.Util.ValidarLogin(this.Context);

			if (!this.IsPostBack) {
				this.CarregarCabecalho();
				this.CarregarControle("ucRankingResumido");
			}

			this.InformarDataRanking();
		}

		private void CarregarCabecalho() {
			ucMenu.Inicializar(Menu.LinkMenu.Ranking);
			ucCabecalho.Inicializar();
		}

		private void InformarDataRanking() {
			Dados.Bolao bolao = new Dados.Bolao();

			DataRow linha = bolao.BuscarBolao(this.CodBolao);

			if (linha != null) {
				string dscBolao = Convert.ToString(linha["dsc_bolao"]);
				DateTime? datRanking = null;

				if (linha["dat_ranking"] != null && linha["dat_ranking"] != DBNull.Value) {
					datRanking = Convert.ToDateTime(linha["dat_ranking"]);

					lbDataUltimaAtualizacao.Text = String.Format("O ranking foi atualizado em {0} às {1}.",
						datRanking.Value.ToString("dd/MM/yyyy"),
						datRanking.Value.ToString("HH:mm:ss")
					);
				}
				else {
					lbDataUltimaAtualizacao.Text = "O ranking ainda não foi atualizado.";
				}
			}
			else {
				lbDataUltimaAtualizacao.Text = "Erro ao tentar recuperar o código do bolão atual.";
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
		}
		#endregion

		protected void ddlTipoRanking_SelectedIndexChanged(object sender, System.EventArgs e) {
			string userControlName = ddlTipoRanking.SelectedItem.Value;
			CarregarControle(userControlName);
		}

		private void CarregarControle(string userControlName) {
			ucRankingResumido.Visible = ucRankingDetalhado.Visible = ucRankingPorFase.Visible = false;

			Control userControl = this.FindControl(userControlName);

			userControl.Visible = true;

			((BaseRankingControl)userControl).Inicializar(this.CodBolao);
		}
	}
}
