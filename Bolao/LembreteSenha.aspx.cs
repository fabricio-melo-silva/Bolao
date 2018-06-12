using Bolao.Dados;
using Bolao.Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using W3.Library.Data;

namespace Bolao {
	public partial class LembreteSenha : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			if (!this.IsPostBack) {
				this.EscreverMensagem("Informe seu e-mail");
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

		protected void btGerarNovaSenha_Click(object sender, EventArgs e) {
			if (this.IsValid) {
				if (!Email.MailAddressIsValid(tbEmail.Text)) {
					this.EscreverMensagem("E-mail informado não é válido");
					return;
				}

				string novaSenha = Util.GerarSenhaRandomica();
				string subject = Email.GetMailSubject(Email.MailBodyType.ChangePassword);
				string body = Email.GetMailBody(Email.MailBodyType.ChangePassword, novaSenha);

				bool novaSenhaOk = false;

				try {
					DatabaseUtil.Connector.BeginTransaction();

					if (Usuario.EmailCadastrado(tbEmail.Text)) {
						Usuario.AlterarSenha(tbEmail.Text, novaSenha);

						if (Email.SendMail(tbEmail.Text, String.Empty, subject, body)) {
							novaSenhaOk = true;
							this.EscreverMensagem("Você receberá um e-mail com sua nova senha");
						}
					}

					DatabaseUtil.Connector.CommitTransaction();
				}
				catch (Exception erro) {
					DatabaseUtil.Connector.RollbackTransaction();

					if (Convert.ToBoolean(ConfigurationManager.AppSettings["Debug"])) {
						throw erro;
					}
				}

				if (!novaSenhaOk) {
					this.EscreverMensagem("Não foi possível enviar sua nova senha por e-mail");
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