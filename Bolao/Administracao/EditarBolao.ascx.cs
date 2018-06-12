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
	public partial class EditarBolao : BaseControl
	{
		public event EventHandler Salvar;
		public event EventHandler Cancelar;

		private Dados.Bolao bolao;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			bolao = new Dados.Bolao();
			lbMensagem.Visible = false;

			if (!Page.IsPostBack)
			{
				tbValor.Attributes["onKeyUp"] = "javascript:NumericFormat(this, 2, ',', event);";
				tbValor.Attributes["onFocus"] = "javascript:NumericFormat(this, 2, ',', event);";
				tbValor.Attributes["onBlur"] = "javascript:NumericFormat(this, 2, ',', event);";
			}
		}

		private int CodBolao
		{
			get { return Convert.ToInt32("0" + Convert.ToString(ViewState["CodBolao"])); }
			set { ViewState["CodBolao"] = value; }
		}

		public void Inicializar(int codBolao)
		{
			this.CodBolao = codBolao;

			if (codBolao == 0)
			{
				tbDescricao.Text = "";
				tbValor.Text = "";
				ddlStatus.SelectedIndex = 0;
				lbOperacao.Text = "Incluir Bolão";
			}
			else
			{
				DataRow registro = bolao.BuscarBolao(codBolao);

				if (registro != null)
				{
					tbDescricao.Text = "" + Convert.ToString(registro["dsc_bolao"]);
					tbValor.Text = Lib.Util.DoubleToString(Convert.ToDouble(registro["vlr_bolao"]), 2, true);
					ddlStatus.SelectedIndex = -1;
					ddlStatus.Items.FindByValue("" + Convert.ToString(registro["ind_status"])).Selected = true;
				}

				lbOperacao.Text = "Alterar Bolão";
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
				double vlrBolao = Convert.ToDouble(tbValor.Text);

				try
				{
					bolao.SalvarBolao(ref codBolao, tbDescricao.Text, ddlStatus.SelectedValue, vlrBolao);
					
					this.CodBolao = codBolao;

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
		
		protected void ValidarValor(object sender, ServerValidateEventArgs e)
		{
			double valor;
			e.IsValid = Double.TryParse(tbValor.Text, out valor);
		}
	}
}
