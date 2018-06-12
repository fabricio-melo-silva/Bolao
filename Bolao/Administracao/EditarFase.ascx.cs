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
	public partial class EditarFase : BaseControl
	{
		public event EventHandler Salvar;
		public event EventHandler Cancelar;

		private Dados.Fase fase;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			fase = new Dados.Fase();

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

		public void Inicializar(int codBolao, int codFase)
		{
			this.CodBolao = codBolao;
			this.CodFase = codFase;

			if (codFase == 0)
			{
				tbDescricao.Text = "";
				ddlStatus.SelectedIndex = 0;
				rblTipo.SelectedIndex = 0;
				lbOperacao.Text = "Incluir Fase";
			}
			else
			{
				DataRow registro = fase.BuscarFase(codBolao, codFase);

				if (registro != null)
				{
					tbDescricao.Text = "" + Convert.ToString(registro["dsc_fase"]);
					ddlStatus.SelectedIndex = -1;
					ddlStatus.Items.FindByValue("" + Convert.ToString(registro["ind_status"])).Selected = true;
					rblTipo.SelectedIndex = -1;
					rblTipo.Items.FindByValue("" + Convert.ToString(registro["ind_tipo_fase"])).Selected = true;
				}

				lbOperacao.Text = "Alterar Fase";
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

				try
				{
					fase.SalvarFase(ref codFase, codBolao, tbDescricao.Text, rblTipo.SelectedValue, ddlStatus.SelectedValue);
					
					this.CodFase = codFase;

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
