namespace Bolao.Administracao
{
	using System;
	using System.Data;
	using System.IO;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	using W3.Library;

	public partial class ListarTimeGrupo : BaseControl
	{
		public event EventHandler Voltar;

		private Dados.Time time;
		private Dados.Grupo grupo;
		private Dados.Fase fase;
		private Dados.Bolao bolao;



		protected void Page_Load(object sender, System.EventArgs e)
		{
			time = new Dados.Time();
			grupo = new Dados.Grupo();
			fase = new Dados.Fase();
			bolao = new Dados.Bolao();

			ucEditarTimeGrupo.Salvar += new EventHandler(this.SalvarTime);
			ucEditarTimeGrupo.Cancelar += new EventHandler(this.CancelarEdicaoTime);

			if (!this.IsPostBack)
			{
				this.CarregarTimes();
			}
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
			this.CarregarTimes();
			this.CarregarGrupo();
			this.CarregarBolao();
			this.CarregarFase();
		}

		private void CarregarBolao()
		{
			DataRow registro = bolao.BuscarBolao(this.CodBolao);

			if (registro != null)
			{
				lbNomeBolao.Text = "" + Convert.ToString(registro["dsc_bolao"]);
			}
		}

		private void CarregarFase()
		{
			DataRow registro = fase.BuscarFase(this.CodBolao, this.CodFase);

			if (registro != null)
			{
				lbNomeFase.Text = "" + Convert.ToString(registro["dsc_fase"]);
			}
		}

		private void CarregarGrupo()
		{
			DataRow registro = grupo.BuscarGrupo(this.CodBolao, this.CodFase, this.CodGrupo);

			if (registro != null)
			{
				lbNomeGrupo.Text = "" + Convert.ToString(registro["nom_grupo"]);
			}
		}

		private void SalvarTime(object sender, EventArgs e)
		{
			ucEditarTimeGrupo.Visible = false;
			pnListar.Visible = true;
			this.CarregarTimes();
		}

		private void CancelarEdicaoTime(object sender, EventArgs e)
		{
			ucEditarTimeGrupo.Visible = false;
			pnListar.Visible = true;
		}

		public void CarregarTimes()
		{
			DataTable times = time.BuscarTimeGrupo(this.CodGrupo);

			dgTime.DataSource = times;
			dgTime.DataBind();
			dgTime.Visible = (times.Rows.Count > 0);
			
			lbMensagem.Visible = (times.Rows.Count == 0);
			lbMensagem.Text = "Não existe nenhum time cadastrado.";
		}

//		protected void AlterarTime(object sender, EventArgs e)
//		{
//			int codTime = Convert.ToInt32(((Button)sender).CommandArgument);
//
//			ucEditarTime.Inicializar(codTime);
//			ucEditarTime.Visible = true;
//			pnListar.Visible = false;
//		}
//
		protected void ExcluirTime(object sender, EventArgs e)
		{
			int codTime = Convert.ToInt32(((Button)sender).CommandArgument);

			try
			{
				time.DesassociarTime(codTime, this.CodGrupo);

				this.CarregarTimes();
			}
			catch (Exception erro)
			{
				lbMensagem.Text = String.Format("<p>Erro ao excluir: {0}</p>", erro.Message);
				lbMensagem.Visible = true;
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
			this.dgTime.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgTime_ItemDataBound);

		}
		#endregion

		private void dgTime_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Image imgIcone = e.Item.FindControl("imgIcone") as Image;
				Button btExcluir = e.Item.FindControl("btExcluir") as Button;

				int codTime = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_time"));
				string dscIcone = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_icone"));

				imgIcone.ImageUrl = "../Images/Times/" + dscIcone;
				btExcluir.CommandArgument = codTime.ToString();
				btExcluir.Attributes["onclick"] = "return (confirm(\"Deseja realmente excluir este time deste grupo?\\nEsta operação não poderá ser desfeita.\"));";
				btExcluir.Attributes["title"] = "Excluir Time do Grupo";
			}
		}

		protected void btIncluirTime_Click(object sender, System.EventArgs e)
		{
			ucEditarTimeGrupo.Inicializar(this.CodBolao, this.CodFase, this.CodGrupo);
			ucEditarTimeGrupo.Visible = true;
			pnListar.Visible = false;
		}

		protected void btVoltar_Click(object sender, System.EventArgs e)
		{
			if (this.Voltar != null) this.Voltar(this, e);
		}
	}
}
