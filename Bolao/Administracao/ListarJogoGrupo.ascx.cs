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

	public partial class ListarJogoGrupo : BaseControl
	{
		public event EventHandler Voltar;

		private Dados.Grupo grupo;
		private Dados.Fase fase;
		private Dados.Jogo jogo;



		private Dados.Bolao bolao;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			grupo = new Dados.Grupo();
			fase = new Dados.Fase();
			bolao = new Dados.Bolao();
			jogo = new Dados.Jogo();

			ucEditarJogoGrupo.Salvar += new EventHandler(this.SalvarJogo);
			ucEditarJogoGrupo.Cancelar += new EventHandler(this.CancelarEdicaoJogo);

			if (!this.IsPostBack)
			{
				this.CarregarJogos();
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
			this.CarregarJogos();
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

		private void SalvarJogo(object sender, EventArgs e)
		{
			ucEditarJogoGrupo.Visible = false;
			pnListar.Visible = true;

			this.CarregarJogos();
		}

		private void CancelarEdicaoJogo(object sender, EventArgs e)
		{
			ucEditarJogoGrupo.Visible = false;
			pnListar.Visible = true;
		}

		public void CarregarJogos()
		{
			DataTable jogos = jogo.BuscarJogo(this.CodBolao, this.CodFase, this.CodGrupo);

			dgJogos.DataSource = jogos;
			dgJogos.DataBind();
			dgJogos.Visible = (jogos.Rows.Count > 0);
			
			lbMensagem.Visible = (jogos.Rows.Count == 0);
			lbMensagem.Text = "Não existe nenhum jogo cadastrado.";
		}

		protected void AlterarJogo(object sender, EventArgs e)
		{
			int codJogo = Convert.ToInt32(((Button)sender).CommandArgument);

			ucEditarJogoGrupo.Inicializar(this.CodBolao, this.CodFase, this.CodGrupo, codJogo);
			ucEditarJogoGrupo.Visible = true;
			pnListar.Visible = false;
		}

		protected void ExcluirJogo(object sender, EventArgs e)
		{
			int codJogo = Convert.ToInt32(((Button)sender).CommandArgument);

			try
			{
				jogo.ExcluirJogo(codJogo);

				this.CarregarJogos();
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
			this.dgJogos.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgJogos_ItemDataBound);

		}
		#endregion

		protected void btVoltar_Click(object sender, System.EventArgs e)
		{
			if (this.Voltar != null) this.Voltar(this, e);
		}

		protected void btIncluirJogo_Click(object sender, System.EventArgs e)
		{
			ucEditarJogoGrupo.Inicializar(this.CodBolao, this.CodFase, this.CodGrupo, 0);
			ucEditarJogoGrupo.Visible = true;
			pnListar.Visible = false;
		}

		private void dgJogos_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string codJogo = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cod_jogo"));
				string dscIconeA = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_icone_a"));
				string dscIconeB = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_icone_b"));
				string qtdGolA = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "qtd_gol_a"));
				string qtdGolB = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "qtd_gol_b"));
				DateTime datJogo = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "dat_jogo"));
				DateTime datLimite = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "dat_limite_aposta"));

				Image imgTimeA = e.Item.FindControl("imgTimeA") as Image;
				Image imgTimeB = e.Item.FindControl("imgTimeB") as Image;
				Label lbGolsA = e.Item.FindControl("lbGolsA") as Label;
				Label lbGolsB = e.Item.FindControl("lbGolsB") as Label;
				Label lbData = e.Item.FindControl("lbData") as Label;
				Label lbHora = e.Item.FindControl("lbHora") as Label;
				Label lbLimite = e.Item.FindControl("lbLimite") as Label;
				Button btAlterar = e.Item.FindControl("btAlterar") as Button;
				Button btExcluir = e.Item.FindControl("btExcluir") as Button;

				imgTimeA.ImageUrl = "../Images/Times/" + dscIconeA;
				imgTimeB.ImageUrl = "../Images/Times/" + dscIconeB;
				lbGolsA.Text = (qtdGolA == "") ? "-" : qtdGolA;
				lbGolsB.Text = (qtdGolB == "") ? "-" : qtdGolB;
				lbData.Text = datJogo.ToString("dd/MM");
				lbHora.Text = datJogo.ToString("HH:mm");
				lbLimite.Text = datLimite.ToString("dd/MM");
				btAlterar.CommandArgument = codJogo;
				btAlterar.Attributes["title"] = "Alterar Jogo";
				btExcluir.CommandArgument = codJogo;
				btExcluir.Attributes["title"] = "Excluir Jogo";
				btExcluir.Attributes["onclick"] = "return (confirm(\"Deseja realmente excluir este jogo?\\nEsta operação não poderá ser desfeita.\"));";
			}
		}
	}
}
