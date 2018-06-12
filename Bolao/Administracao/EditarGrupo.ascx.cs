namespace Bolao.Administracao
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	using W3.Library;

	/// <summary>
	///		Summary description for EditarBolao.
	/// </summary>
	public partial class EditarGrupo : BaseControl
	{
		public event EventHandler Salvar;
		public event EventHandler Cancelar;

		private Dados.Grupo grupo;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			grupo = new Dados.Grupo();

			lbMensagem.Visible = false;
		}

		private int CodBolao
		{
			get { return Convert.ToInt32("0" + Convert.ToString(ViewState["CodBolao"])); }
			set { ViewState["CodBolao"] = value; }
		}

		private int CodFase
		{
			get { return Convert.ToInt32("0" + Convert.ToString(ViewState["CodFase"])); }
			set { ViewState["CodFase"] = value; }
		}

		private int CodGrupo
		{
			get { return Convert.ToInt32("0" + Convert.ToString(ViewState["CodGrupo"])); }
			set { ViewState["CodGrupo"] = value; }
		}

		public void Inicializar(int codBolao, int codFase, int codGrupo)
		{
			this.CodBolao = codBolao;
			this.CodFase = codFase;
			this.CodGrupo = codGrupo;

			if (codGrupo == 0)
			{
				tbNome.Text = "";
				tbSigla.Text = "";
				lbOperacao.Text = "Incluir Grupo";
			}
			else
			{
				DataRow registro = grupo.BuscarGrupo(codBolao, codFase, codGrupo);

				if (registro != null)
				{
					tbNome.Text = "" + Convert.ToString(registro["nom_grupo"]);
					tbSigla.Text = "" + Convert.ToString(registro["sgl_grupo"]);
				}

				lbOperacao.Text = "Alterar Grupo";
			}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		protected void btSalvar_Click(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				int codBolao = this.CodBolao;
				int codFase = this.CodFase;
				int codGrupo = this.CodGrupo;

				try
				{
					grupo.SalvarGrupo(ref codGrupo, codBolao, codFase, tbNome.Text, tbSigla.Text);
					
					this.CodGrupo = codGrupo;

					if (this.Salvar != null) this.Salvar(this, e);
				}
				catch (Exception erro)
				{
					lbMensagem.Text = String.Format("<p>Erro ao salvar: {0}</p>", erro.Message);
					lbMensagem.Visible = true;
				}
			}
		}

		protected void btCancelar_Click(object sender, System.EventArgs e)
		{
			if (this.Cancelar != null) this.Cancelar(this, e);
		}
	}
}
