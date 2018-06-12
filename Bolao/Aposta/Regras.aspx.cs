using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bolao.Aposta {
	public partial class Regras : System.Web.UI.Page {

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

		protected void Page_Load(object sender, EventArgs e) {
			Lib.Util.ValidarLogin(this.Context);

			if (!this.IsPostBack) {
				this.CarregarCabecalho();
			}
		}

		private void CarregarCabecalho() {
			ucMenu.Inicializar(Menu.LinkMenu.Regras);
			ucCabecalho.Inicializar();
		}
	}
}