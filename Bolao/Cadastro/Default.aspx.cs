using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


using W3.Library;
using Bolao.Dados;
using Bolao.Lib;

namespace Bolao.Cadastro
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : BasePage
	{
		private Dados.Usuario usuario;
		private Dados.Bolao bolao;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			bolao = new Dados.Bolao();
			usuario = new Dados.Usuario();

			if (!this.IsPostBack)
			{
				if (this.UsuarioLogado)
				{
					this.CodUsuario = Convert.ToInt32(Session["CodUsuario"]);
					this.LimparFormulario(3);
					this.ExibirPainel(pnPasso3);
					this.CarregarBolao();
				}
				else
				{
					this.LimparFormulario(1);
					this.ExibirPainel(pnPasso1);
				}
			}
		}

		private bool UsuarioLogado
		{
			get 
			{
				if (Session["UsuarioValido"] == null)
				{
					return false;
				}
				else
				{
					return Convert.ToBoolean(Session["UsuarioValido"]);
				}
			}
		}

		private bool UsuarioCadastrado
		{
			get { return rblCadastrar.SelectedValue == "1"; }
		}

		private int CodUsuario
		{
			get
			{
				if (ViewState["CodUsuario"] == null) ViewState["CodUsuario"] = 0;

				return Convert.ToInt32(ViewState["CodUsuario"]);
			}
			set { ViewState["CodUsuario"] = value; }
		}

		private DataTable ListaCompletaBolao
		{
			get { return (DataTable)ViewState["ListaCompletaBolao"]; }
			set { ViewState["ListaCompletaBolao"] = value; }
		}

		private DataTable ListaRestritaBolao
		{
			get { return (DataTable)ViewState["ListaRestritaBolao"]; }
			set { ViewState["ListaRestritaBolao"] = value; }
		}

		private void ExibirPainel(Panel painel)
		{
			pnPasso1.Visible = pnPasso1.Equals(painel);
			pnPasso2.Visible = pnPasso2.Equals(painel);
			pnPasso3.Visible = pnPasso3.Equals(painel);
			pnPasso4.Visible = pnPasso4.Equals(painel);
		}

		private void LimparFormulario(int passo)
		{
			switch (passo)
			{
				case 1:
					rblCadastrar.SelectedIndex = 0;
					break;
				case 2:
					tbNome.Text = "";
					tbEmail.Text = "";
					tbSenha.Text = "";
					tbConfirmacao.Text = "";
					break;
//				case 3:
//					dgBolao.DataSource = null;
//					dgBolao.DataBind();
//					break;
			}
		}

		private void CadastrarUsuario()
		{
			Usuario usuario = new Usuario {
				CodigoUsuario = 0,
				NomeUsuario = tbNome.Text,
				Email = tbEmail.Text,
				Senha = tbSenha.Text
			};

			this.CodUsuario = Usuario.Salvar(usuario, true);

			Email.SendMail(usuario.Email, usuario.NomeUsuario, "Cadastro no Bolão", "<h1>Cadastro no Bolão<h1>");
		}

		private void RecuperarUsuario()
		{
			DataRow registro = usuario.Buscar(tbEmail.Text, tbSenha.Text);

			this.CodUsuario = Convert.ToInt32(registro["cod_usuario"]);
		}

		private void CarregarBolao()
		{
			// Busca todos os bolões cadastrados já iniciados
			this.ListaCompletaBolao = bolao.BuscarBolao("A");

			// Busca os bolões que o usuário participa
			this.ListaRestritaBolao = usuario.BuscarBolao(this.CodUsuario);

			// Exclui da primeira lista aqueles que o usuário já participa
			dgBolao.DataSource = this.ListaCompletaBolao;
			dgBolao.DataBind();
		}

		private void CadastrarParticipante()
		{
			foreach (DataGridItem item in dgBolao.Items)
			{
				if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
				{
					HtmlInputHidden hdBolao = item.FindControl("hdBolao") as HtmlInputHidden;
					CheckBox cbBolao = item.FindControl("cbBolao") as CheckBox;

					if (item.Enabled && cbBolao.Checked)
					{
						int codBolao = Convert.ToInt32("0" + hdBolao.Value);

						Participante.SalvarParticipante(new Participante {
							CodigoBolao = codBolao,
							CodigoUsuario = this.CodUsuario,
							Pontuacao = 0,
							PosicaoRanking = 0,
							Administrador = "N",
							Pago = "N"
						});
					}
				}
			}
		}

		protected void ValidarEmail(object sender, ServerValidateEventArgs e)
		{
			if (this.UsuarioCadastrado)
			{
				e.IsValid = Usuario.EmailCadastrado(tbEmail.Text);
				((CustomValidator)sender).ErrorMessage = "O e-mail informado não foi encontrado.";
			}
			else
			{
				e.IsValid = !Usuario.EmailCadastrado(tbEmail.Text);
				((CustomValidator)sender).ErrorMessage = "O e-mail informado já está cadastrado.";
			}
		}

		protected void ValidarSenha(object sender, ServerValidateEventArgs e)
		{
			if (this.UsuarioCadastrado)
			{
				e.IsValid = usuario.SenhaCorreta(tbEmail.Text, tbSenha.Text);
			}
			else
			{
				e.IsValid = true;
			}
		}

		private bool UsuarioParticipante(int codBolao)
		{
			DataView visao = new DataView(this.ListaRestritaBolao);

			visao.RowFilter = String.Format("cod_bolao = {0}", codBolao);

			return visao.Count > 0;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
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
		private void InitializeComponent()
		{    
			this.dgBolao.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgBolao_ItemDataBound);

		}
		#endregion

		protected void btContinuar1_Click(object sender, System.EventArgs e)
		{
			this.LimparFormulario(2);
			this.ExibirPainel(pnPasso2);

			pnNaoRegistrado.Visible = rblCadastrar.SelectedValue == "0";
		}

		protected void btContinuar2_Click(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (!this.UsuarioCadastrado)
				{
					this.CadastrarUsuario();
				}
				else if (!this.UsuarioLogado)
				{
					this.RecuperarUsuario();
				}

				this.LimparFormulario(3);
				this.ExibirPainel(pnPasso3);
				this.CarregarBolao();
			}
		}

		protected void btConfirmar_Click(object sender, System.EventArgs e)
		{
			this.CadastrarParticipante();
			this.ExibirPainel(pnPasso4);
		}

		protected void btOk_Click(object sender, System.EventArgs e)
		{
			string paginaRetorno = "";

			if (this.UsuarioLogado) paginaRetorno = "../Aposta/Default.aspx";
			else paginaRetorno = "../Default.aspx";

			Response.Redirect(paginaRetorno, true);
		}

		private void dgBolao_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				int codBolao = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_bolao"));
				double vlrBolao = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "vlr_bolao"));

				bool participante = this.UsuarioParticipante(codBolao);

				CheckBox cbBolao = e.Item.FindControl("cbBolao") as CheckBox;
				Label lbValor = e.Item.FindControl("lbValor") as Label;
				HtmlInputHidden hdBolao = e.Item.FindControl("hdBolao") as HtmlInputHidden;

				hdBolao.Value = codBolao.ToString();
				cbBolao.Checked = participante;
				cbBolao.Enabled = !participante;
				lbValor.Text = Lib.Util.DoubleToString(vlrBolao, 2, true);
				e.Item.Enabled = !participante;
			}
		}
	}
}
