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
	public partial class ListarBolao : BaseControl
	{
		private Dados.Bolao bolao;



		protected void Page_Load(object sender, System.EventArgs e)
		{
			bolao = new Dados.Bolao();

			ucEditarBolao.Salvar += new EventHandler(this.SalvarBolao);
			ucEditarBolao.Cancelar += new EventHandler(this.CancelarEdicaoBolao);
			ucListarFase.Voltar += new EventHandler(this.VoltarFase);
			ucListarParticipante.Voltar += new EventHandler(this.VoltarParticipante);

			if (!this.IsPostBack)
			{
				this.SortExpression = "dsc_bolao asc";
				this.CarregarBoloes();
			}
		}

		private string SortExpression
		{
			get { return Convert.ToString(ViewState["SortExpression"]); }
			set { ViewState["SortExpression"] = value; }
		}

		private void SalvarBolao(object sender, EventArgs e)
		{
			ucEditarBolao.Visible = false;
			pnListar.Visible = true;
			this.CarregarBoloes();
		}

		private void CancelarEdicaoBolao(object sender, EventArgs e)
		{
			ucEditarBolao.Visible = false;
			pnListar.Visible = true;
		}

		private void VoltarFase(object sender, EventArgs e)
		{
			ucListarFase.Visible = false;
			pnListar.Visible = true;
		}

		private void VoltarParticipante(object sender, EventArgs e)
		{
			ucListarParticipante.Visible = false;
			pnListar.Visible = true;
		}

		public void CarregarBoloes()
		{
			DataView boloes = new DataView(bolao.BuscarBolao());

			boloes.Sort = this.SortExpression;

			dgBolao.DataSource = boloes;
			dgBolao.DataBind();
			dgBolao.Visible = (boloes.Count > 0);
			
			lbMensagem.Visible = (boloes.Count == 0);
			lbMensagem.Text = "Não existe nenhum bolão cadastrado.";
		}

		protected void AlterarBolao(object sender, EventArgs e)
		{
			int codBolao = Convert.ToInt32(((Button)sender).CommandArgument);

			ucEditarBolao.Inicializar(codBolao);
			ucEditarBolao.Visible = true;
			pnListar.Visible = false;
		}

		protected void ExcluirBolao(object sender, EventArgs e)
		{
			int codBolao = Convert.ToInt32(((Button)sender).CommandArgument);

			try
			{
				bolao.ExcluirBolao(codBolao);
				this.CarregarBoloes();
			}
			catch (Exception erro)
			{
				lbMensagem.Text = String.Format("<p>Erro ao excluir: {0}</p>", erro.Message);
				lbMensagem.Visible = true;
			}
		}

		protected void ExibirFases(object sender, EventArgs e)
		{
			int codBolao = Convert.ToInt32(((Button)sender).CommandArgument);

			ucListarFase.Inicializar(codBolao);
			ucListarFase.Visible = true;
			pnListar.Visible = false;
		}

		protected void ExibirParticipantes(object sender, EventArgs e)
		{
			int codBolao = Convert.ToInt32(((Button)sender).CommandArgument);

			ucListarParticipante.Inicializar(codBolao);
			ucListarParticipante.Visible = true;
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
			this.dgBolao.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgBolao_SortCommand);
			this.dgBolao.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgBolao_ItemDataBound);

		}
		#endregion

		private void dgBolao_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label lbStatus = e.Item.FindControl("lbStatus") as Label;
				Label lbValor = e.Item.FindControl("lbValor") as Label;
				Label lbDataRanking = e.Item.FindControl("lbDataRanking") as Label;
				Button btFases = e.Item.FindControl("btFases") as Button;
				Button btParticipantes = e.Item.FindControl("btParticipantes") as Button;
				Button btAlterar = e.Item.FindControl("btAlterar") as Button;
				Button btExcluir = e.Item.FindControl("btExcluir") as Button;

				int codBolao = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_bolao"));
				string indStatus = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ind_status"));
				double vlrBolao = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "vlr_bolao"));
				string datRanking = ""  + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dat_ranking"));

				lbStatus.Text = bolao.DescricaoStatus(indStatus);
				lbValor.Text = "R$ " + Lib.Util.DoubleToString(vlrBolao, 2, true);

				if (datRanking != "")
				{
					lbDataRanking.Text = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "dat_ranking")).ToString("dd/MM/yyyy HH:mm:ss");
				}
				else
				{
					lbDataRanking.Text = "-";
				}

				btFases.CommandArgument = codBolao.ToString();
				btParticipantes.CommandArgument = codBolao.ToString();
				btAlterar.CommandArgument = codBolao.ToString();
				btExcluir.CommandArgument = codBolao.ToString();
				btExcluir.Attributes["onclick"] = "return (confirm(\"Deseja realmente excluir este bolão?\\nEsta operação não poderá ser desfeita.\"));";

				btFases.Attributes["title"] = "Listar Fases do Bolão";
				btAlterar.Attributes["title"] = "Alterar Bolão";
				btExcluir.Attributes["title"] = "Excluir Bolão";
			}
		}

		private void dgBolao_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (!this.SortExpression.ToLower().StartsWith(e.SortExpression.ToLower()))
			{
				this.SortExpression = e.SortExpression + " asc";
			}
			else if (this.SortExpression.ToLower().EndsWith(" asc"))
			{
				this.SortExpression = this.SortExpression.Replace(" asc", " desc");
			}
			else
			{
				this.SortExpression = this.SortExpression.Replace(" desc", " asc");
			}

			this.CarregarBoloes();
		}

		protected void btIncluirBolao_Click(object sender, System.EventArgs e)
		{
			ucEditarBolao.Inicializar(0);
			ucEditarBolao.Visible = true;
			pnListar.Visible = false;
		}

		protected void btAtualizarRanking_Click(object sender, System.EventArgs e)
		{
			bolao.GerarRanking(null);
			this.CarregarBoloes();
		}
	}
}
