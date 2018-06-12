using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using W3.Library;
using W3.Library.Utils;
using System.Collections.Generic;

namespace Bolao.Administracao {
	/// <summary>
	///		Summary description for EditarBolao.
	/// </summary>
	public partial class EditarUsuario : BaseControl {
		public event EventHandler Salvar;
		public event EventHandler Cancelar;

		protected void Page_Load(object sender, System.EventArgs e) {
			lbMensagem.Visible = false;
		}

		private int CodUsuario {
			get { return Convert.ToInt32("0" + Convert.ToString(ViewState["CodUsuario"])); }
			set { ViewState["CodUsuario"] = value; }
		}

		public void Inicializar(int codUsuario) {
			this.CodUsuario = codUsuario;

			if (codUsuario == 0) {
				tbNome.Text = "";
				tbEmail.Text = "";
				lbOperacao.Text = "Incluir Usuario";
				rblAtivo.SelectedValue = "S";
			}
			else {
				var usuario = Dados.Usuario.BuscarUsuario(codUsuario);

				if (usuario != null) {
					tbNome.Text = usuario.NomeUsuario;
					tbEmail.Text = usuario.Email;
					rblAtivo.SelectedValue = (usuario.Ativo ? "S" : "N");
				}

				lbOperacao.Text = "Alterar Usuário";
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

		protected void btSalvar_Click(object sender, System.EventArgs e) {
			if (Page.IsValid) {
				int codUsuario = this.CodUsuario;

				try {
					var usuario = new Dados.Usuario {
						CodigoUsuario = codUsuario,
						NomeUsuario = tbNome.Text,
						Email = tbEmail.Text,
						DataCadastro = DateTime.Now,
						DataModificacao = DateTime.Now,
						Senha = Lib.Util.GerarSenhaRandomica(),
						Ativo = "S".Equals(rblAtivo.SelectedValue, StringComparison.InvariantCultureIgnoreCase)
					};

					Dados.Usuario.Salvar(usuario, codUsuario == 0);

					this.CodUsuario = codUsuario;

					if (this.Salvar != null) this.Salvar(this, e);
				}
				catch (Exception erro) {
					lbMensagem.Text = String.Format("Erro ao salvar: {0}", erro.Message);
					lbMensagem.Visible = true;
				}
			}
		}

		protected void btCancelar_Click(object sender, System.EventArgs e) {
			if (this.Cancelar != null) this.Cancelar(this, e);
		}
	}
}
