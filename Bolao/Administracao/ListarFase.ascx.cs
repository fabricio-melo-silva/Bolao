namespace Bolao.Administracao
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	using W3.Library;

	/// <summary>
	///		Summary description for ListarBolao.
	/// </summary>
	public partial class ListarFase : BaseControl
	{
		public event EventHandler Voltar;

		private Dados.Fase fase;
		private Dados.Bolao bolao;



		protected void Page_Load(object sender, System.EventArgs e)
		{
			fase = new Dados.Fase();
			bolao = new Dados.Bolao();

			ucEditarFase.Salvar += new EventHandler(this.SalvarFase);
			ucEditarFase.Cancelar += new EventHandler(this.CancelarEdicaoFase);
			ucListarGrupo.Voltar += new EventHandler(this.VoltarGrupo);

			if (!this.IsPostBack)
			{
				this.CarregarFases();
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

		protected void SalvarFase(object sender, EventArgs e)
		{
			pnListar.Visible = true;
			ucEditarFase.Visible = false;
			this.CarregarFases();
		}

		protected void CancelarEdicaoFase(object sender, EventArgs e)
		{
			pnListar.Visible = true;
			ucEditarFase.Visible = false;
		}

		protected void VoltarGrupo(object sender, EventArgs e)
		{
			ucListarGrupo.Visible = false;
			pnListar.Visible = true;
		}

		public void Inicializar(int codBolao)
		{
			this.CodBolao = codBolao;
			this.CarregarFases();
			this.CarregarBolao();
		}

		private void CarregarFases()
		{
			DataView fases = new DataView(fase.BuscarFase(this.CodBolao));

			dgFase.DataSource = fases;
			dgFase.DataBind();
			dgFase.Visible = (fases.Count > 0);
			
			lbMensagem.Visible = (fases.Count == 0);
			lbMensagem.Text = "Não existe nenhum bolão cadastrado.";
		}

		private void CarregarBolao()
		{
			DataRow registro = bolao.BuscarBolao(this.CodBolao);

			if (registro != null)
			{
				lbNomeBolao.Text = "" + Convert.ToString(registro["dsc_bolao"]);
			}
		}

		protected void AlterarFase(object sender, EventArgs e)
		{
			int codBolao = this.CodBolao;
			int codFase = Convert.ToInt32(((Button)sender).CommandArgument);

			pnListar.Visible = false;
			ucEditarFase.Visible = true;
			ucEditarFase.Inicializar(codBolao, codFase);
		}

		protected void ExcluirFase(object sender, EventArgs e)
		{
			int codFase = Convert.ToInt32(((Button)sender).CommandArgument);

			try
			{
				fase.ExcluirFase(codFase);
				this.CarregarFases();
			}
			catch (Exception erro)
			{
				lbMensagem.Text = String.Format("<p>Erro ao excluir: {0}</p>", erro.Message);
				lbMensagem.Visible = true;
			}
		}

		protected void ExibirGrupo(object sender, EventArgs e)
		{
			int codBolao = this.CodBolao;
			int codFase = Convert.ToInt32(((Button)sender).CommandArgument);

			ucListarGrupo.Visible = true;
			ucListarGrupo.Inicializar(codBolao, codFase);
			pnListar.Visible = false;
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
			this.dgFase.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgFase_ItemDataBound);

		}
		#endregion

		private void dgFase_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label lbStatus = e.Item.FindControl("lbStatus") as Label;
				Label lbTipo = e.Item.FindControl("lbTipo") as Label;
				Button btGrupos = e.Item.FindControl("btGrupos") as Button;
				Button btAlterar = e.Item.FindControl("btAlterar") as Button;
				Button btExcluir = e.Item.FindControl("btExcluir") as Button;

				int codFase = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_fase"));
				string indStatus = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ind_status"));
				string indTipoFase = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ind_tipo_fase"));

				lbStatus.Text = fase.DescricaoStatus(indStatus);
				lbTipo.Text = fase.DescricaoTipo(indTipoFase);

				btGrupos.CommandArgument = codFase.ToString();
				btAlterar.CommandArgument = codFase.ToString();
				btExcluir.CommandArgument = codFase.ToString();
				btExcluir.Attributes["onclick"] = "return (confirm(\"Deseja realmente excluir esta fase?\\nEsta operação não poderá ser desfeita.\"));";

				btGrupos.Attributes["title"] = "Listar Grupos da Fase";
				btAlterar.Attributes["title"] = "Alterar Fase";
				btExcluir.Attributes["title"] = "Excluir Fase";
			}
		}

		protected void btIncluirFase_Click(object sender, System.EventArgs e)
		{
			pnListar.Visible = false;
			ucEditarFase.Visible = true;
			ucEditarFase.Inicializar(this.CodBolao, 0);
		}

		protected void btVoltar_Click(object sender, System.EventArgs e)
		{
			if (this.Voltar != null) this.Voltar(this, e);
		}
	}
}
