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
using Bolao.Dados;

namespace Bolao {
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : BasePage {
		protected void Page_Load(object sender, System.EventArgs e) {
			if (!this.IsPostBack) { 
				this.EscreverMensagem("Informe seu e-mail e sua senha");
			}
			else {
				this.OcultarMensagem();
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

		protected void btEntrar_Click(object sender, System.EventArgs e) {
			if (this.IsValid) {
				string mensagem = null;

				if (Usuario.EfetuarLogin(tbEmail.Text, tbSenha.Text, ref mensagem)) {
					Response.Redirect("Aposta", true);
				}
				else {
					this.EscreverMensagem(mensagem);
				}
			}
		}

		private void EscreverMensagem(string mensagem) {
			lbMensagem.Visible = true;
			lbMensagem.Text = "<ul class=\"validacao bg-danger\"><li>" + mensagem + "</li></ul>";
		}

		private void OcultarMensagem() {
			lbMensagem.Visible = false;
		}
	}
}
