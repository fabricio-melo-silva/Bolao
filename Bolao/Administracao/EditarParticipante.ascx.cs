using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using W3.Library;
using Bolao.Dados;

namespace Bolao.Administracao {
	/// <summary>
	///		Summary description for EditarBolao.
	/// </summary>
	public partial class EditarParticipante : BaseControl {
		public event EventHandler Salvar;
		public event EventHandler Cancelar;

		protected void Page_Load(object sender, System.EventArgs e) {
			lbMensagem.Visible = false;
		}

		private int CodBolao {
			get { return Convert.ToInt32("0" + Convert.ToString(ViewState["CodBolao"])); }
			set { ViewState["CodBolao"] = value; }
		}

		private int CodUsuario {
			get { return Convert.ToInt32("0" + Convert.ToString(ViewState["CodUsuario"])); }
			set { ViewState["CodUsuario"] = value; }
		}

		public void Inicializar(int codBolao, int codUsuario) {
			this.CodBolao = codBolao;
			this.CodUsuario = codUsuario;

			if (codUsuario == 0) {
				CarregarUsuarios();

				lbNome.Visible = false;
				ddlUsuario.Visible = true;

				hlEmail.Text = "-";
				rblAdministrador.SelectedIndex = 1;
				rblBolaoPago.SelectedIndex = 1;
				lbOperacao.Text = "Incluir Participante";
			}
			else {
				lbNome.Visible = true;
				ddlUsuario.Visible = false;

				Participante participante = Participante.BuscarParticipante(codUsuario, codBolao, null);
				Dados.Usuario usuario = Dados.Usuario.BuscarUsuario(codUsuario);

				if (participante != null && usuario != null) {
					lbNome.Text = "" + usuario.NomeUsuario;
					hlEmail.Text = "" + usuario.Email;
					hlEmail.NavigateUrl = "mailto:" + usuario.Email;

					string indAdministrador = "" + participante.Administrador;
					string indBolaoPago = "" + participante.Pago;

					rblAdministrador.SelectedIndex = -1;
					rblBolaoPago.SelectedIndex = -1;

					rblAdministrador.Items.FindByValue(indAdministrador).Selected = true;
					rblBolaoPago.Items.FindByValue(indBolaoPago).Selected = true;
				}

				lbOperacao.Text = "Alterar Participante";
			}
		}

		private void CarregarUsuarios() {
			var usuarios = Dados.Usuario.BuscarUsuarios();
			var participantes = Dados.Participante.BuscarParticipantes(null, CodBolao, null);

			var lista = from u in usuarios
						join p in participantes on u.CodigoUsuario equals p.CodigoUsuario
						into temp
						from t in temp.DefaultIfEmpty()
						where t == null
						select u;

			ddlUsuario.DataSource = lista;
			ddlUsuario.DataValueField = "CodigoUsuario";
			ddlUsuario.DataTextField = "NomeUsuario";
			ddlUsuario.DataBind();

			ddlUsuario.Items.Insert(0, new ListItem("Selecione...", "0"));
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
				try {
					int codUsuario = this.CodUsuario;

					Dados.Participante participante;

					if (ddlUsuario.Visible) {
						codUsuario = Convert.ToInt32(ddlUsuario.SelectedValue);

						participante = new Participante {
							CodigoUsuario = codUsuario,
							CodigoBolao = this.CodBolao,
							Pago = rblBolaoPago.SelectedValue,
							Pontuacao = 0,
							PosicaoRanking = 0,
							Administrador = rblAdministrador.SelectedValue,
						};
					}
					else {
						participante = Participante.BuscarParticipante(codUsuario, this.CodBolao, null);

						participante.Administrador = rblAdministrador.SelectedValue;
						participante.Pago = rblBolaoPago.SelectedValue;
					}

					Participante.SalvarParticipante(participante);

					if (this.Salvar != null) this.Salvar(this, e);
				}
				catch (Exception erro) {
					lbMensagem.Text = String.Format("<p>Erro ao salvar: {0}</p>", erro.Message);
					lbMensagem.Visible = true;
				}
			}
		}

		protected void btCancelar_Click(object sender, System.EventArgs e) {
			if (this.Cancelar != null) {
				this.Cancelar(this, e);
			}
		}

		protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e) {
			int codUsuario = Convert.ToInt32(ddlUsuario.SelectedValue);

			if (codUsuario == 0) {
				hlEmail.Text = "-";
			}
			else {
				Dados.Usuario usuario = Dados.Usuario.BuscarUsuario(codUsuario);

				hlEmail.Text = "" + usuario.Email;
				hlEmail.NavigateUrl = "mailto:" + usuario.Email;
			}
		}
	}
}
