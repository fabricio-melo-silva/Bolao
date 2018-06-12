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
	public partial class ListarGrupo : BaseControl
	{
		public event EventHandler Voltar;

		private Dados.Grupo grupo;
		private Dados.Bolao bolao;
		private Dados.Fase fase;



		protected void Page_Load(object sender, System.EventArgs e)
		{
			grupo = new Dados.Grupo();
			bolao = new Dados.Bolao();
			fase = new Dados.Fase();

			ucEditarGrupo.Salvar += new EventHandler(this.SalvarGrupo);
			ucEditarGrupo.Cancelar += new EventHandler(this.CancelarEdicaoGrupo);
			ucListarTimeGrupo.Voltar += new EventHandler(this.VoltarListaTime);
			ucListarJogoGrupo.Voltar += new EventHandler(this.VoltarListaJogo);

			if (!this.IsPostBack)
			{
				this.CarregarGrupos();
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

		protected void SalvarGrupo(object sender, EventArgs e)
		{
			pnListar.Visible = true;
			ucEditarGrupo.Visible = false;
			this.CarregarGrupos();
		}

		protected void CancelarEdicaoGrupo(object sender, EventArgs e)
		{
			pnListar.Visible = true;
			ucEditarGrupo.Visible = false;
		}

		protected void VoltarListaTime(object sender, EventArgs e)
		{
			pnListar.Visible = true;
			ucListarTimeGrupo.Visible = false;
		}

		protected void VoltarListaJogo(object sender, EventArgs e)
		{
			pnListar.Visible = true;
			ucListarJogoGrupo.Visible = false;
		}

		public void Inicializar(int codBolao, int codFase)
		{
			this.CodBolao = codBolao;
			this.CodFase = codFase;
			this.CarregarGrupos();
			this.CarregarBolao();
			this.CarregarFase();
		}

		private void CarregarGrupos()
		{
			DataView grupos = new DataView(grupo.BuscarGrupo(this.CodBolao, this.CodFase));

			dgGrupo.DataSource = grupos;
			dgGrupo.DataBind();
			dgGrupo.Visible = (grupos.Count > 0);
			
			lbMensagem.Visible = (grupos.Count == 0);
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

		private void CarregarFase()
		{
			DataRow registro = fase.BuscarFase(this.CodBolao, this.CodFase);

			if (registro != null)
			{
				lbNomeFase.Text = "" + Convert.ToString(registro["dsc_fase"]);
			}
		}

		protected void ExibirTimes(object sender, EventArgs e)
		{
			int codBolao = this.CodBolao;
			int codFase = this.CodFase;
			int codGrupo = Convert.ToInt32(((Button)sender).CommandArgument);

			ucListarTimeGrupo.Visible = true;
			ucListarTimeGrupo.Inicializar(codBolao, codFase, codGrupo);
			pnListar.Visible = false;
		}

		protected void ExibirJogos(object sender, EventArgs e)
		{
			int codBolao = this.CodBolao;
			int codFase = this.CodFase;
			int codGrupo = Convert.ToInt32(((Button)sender).CommandArgument);

			ucListarJogoGrupo.Visible = true;
			ucListarJogoGrupo.Inicializar(codBolao, codFase, codGrupo);
			pnListar.Visible = false;
		}

		protected void AlterarGrupo(object sender, EventArgs e)
		{
			int codBolao = this.CodBolao;
			int codFase = this.CodFase;
			int codGrupo = Convert.ToInt32(((Button)sender).CommandArgument);

			pnListar.Visible = false;
			ucEditarGrupo.Visible = true;
			ucEditarGrupo.Inicializar(codBolao, codFase, codGrupo);
		}

		protected void ExcluirGrupo(object sender, EventArgs e)
		{
			int codGrupo = Convert.ToInt32(((Button)sender).CommandArgument);

			try
			{
				grupo.ExcluirGrupo(codGrupo);
				this.CarregarGrupos();
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
			this.dgGrupo.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgGrupo_ItemDataBound);

		}
		#endregion

		private void dgGrupo_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Button btTimes = e.Item.FindControl("btTimes") as Button;
				Button btJogos = e.Item.FindControl("btJogos") as Button;
				Button btAlterar = e.Item.FindControl("btAlterar") as Button;
				Button btExcluir = e.Item.FindControl("btExcluir") as Button;

				int codGrupo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_grupo"));

				btTimes.CommandArgument = codGrupo.ToString();
				btJogos.CommandArgument = codGrupo.ToString();
				btAlterar.CommandArgument = codGrupo.ToString();
				btExcluir.CommandArgument = codGrupo.ToString();
				btExcluir.Attributes["onclick"] = "return (confirm(\"Deseja realmente excluir este grupo?\\nEsta operação não poderá ser desfeita.\"));";

				btTimes.Attributes["title"] = "Listar Times do Grupo";
				btJogos.Attributes["title"] = "Listar Jogos do Grupo";
				btAlterar.Attributes["title"] = "Alterar Grupo";
				btExcluir.Attributes["title"] = "Excluir Grupo";
			}
		}

		protected void btIncluirGrupo_Click(object sender, System.EventArgs e)
		{
			pnListar.Visible = false;
			ucEditarGrupo.Visible = true;
			ucEditarGrupo.Inicializar(this.CodBolao, this.CodFase, 0);
		}

		protected void btVoltar_Click(object sender, System.EventArgs e)
		{
			if (this.Voltar != null) this.Voltar(this, e);
		}
	}
}
