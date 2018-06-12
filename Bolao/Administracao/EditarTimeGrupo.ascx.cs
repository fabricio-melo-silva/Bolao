namespace Bolao.Administracao
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.IO;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	using W3.Library;

	/// <summary>
	///		Summary description for EditarBolao.
	/// </summary>
	public partial class EditarTimeGrupo : BaseControl
	{
		public event EventHandler Salvar;
		public event EventHandler Cancelar;

		private Dados.Time time;

		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			time = new Dados.Time();
		}

		private int CodBolao
		{
			get 
			{
				if (ViewState["CodBolao"] == null) ViewState["CodBolao"] = 0;
				
				return Convert.ToInt32(ViewState["CodBolao"]);
			}
			set { ViewState["CodBolao"] = value; }
		}

		private int CodFase
		{
			get 
			{
				if (ViewState["CodFase"] == null) ViewState["CodFase"] = 0;
				
				return Convert.ToInt32(ViewState["CodFase"]);
			}
			set { ViewState["CodFase"] = value; }
		}

		private int CodGrupo
		{
			get 
			{
				if (ViewState["CodGrupo"] == null) ViewState["CodGrupo"] = 0;
				
				return Convert.ToInt32(ViewState["CodGrupo"]);
			}
			set { ViewState["CodGrupo"] = value; }
		}

		public void Inicializar(int codBolao, int codFase, int codGrupo)
		{
			this.CodBolao = codBolao;
			this.CodFase = codFase;
			this.CodGrupo = codGrupo;

			tbNome.Text = "";
			ddlTime.Items.Clear();
			pnTime.Visible = false;
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

		protected void btPesquisar_Click(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				DataTable times = time.BuscarTime(tbNome.Text);

				ddlTime.DataSource = times;
				ddlTime.DataValueField = "cod_time";
				ddlTime.DataTextField = "nom_time";
				ddlTime.DataBind();

				if (times.Rows.Count == 1) {
					ddlTime.Items[0].Selected = true;
				}
				else {
					ddlTime.Items.Insert(0, new ListItem("Selecione...", "0"));
				}

				pnTime.Visible = true;
			}
		}

		protected void btSalvar_Click(object sender, System.EventArgs e)
		{
			if (Convert.ToInt32(ddlTime.SelectedValue) == 0)
			{
				lbMensagem.Visible = true;
			}
			else
			{
				lbMensagem.Visible = false;

				int codTime = Convert.ToInt32(ddlTime.SelectedValue);
				int codGrupo = this.CodGrupo;

				time.AssociarTime(codTime, codGrupo);

				if (this.Salvar != null) this.Salvar(this, e);
			}
		}

		protected void btCancelar_Click(object sender, System.EventArgs e)
		{
			if (this.Cancelar != null) this.Cancelar(this, e);
		}
	}
}
