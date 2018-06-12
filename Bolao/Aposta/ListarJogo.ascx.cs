namespace Bolao.Aposta
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
	///		Summary description for ListarJogo.
	/// </summary>
	public partial class ListarJogo : BaseControl
	{
		private Dados.Jogo jogo;
		private Dados.Fase fase;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			jogo = new Dados.Jogo();
			fase = new Dados.Fase();

			if (!this.IsPostBack)
			{
				this.SortExpression = "num_jogo asc";
				this.CarregarJogos();
			}
		}

		private string SortExpression
		{
			get { return Convert.ToString(ViewState["SortExpression"]); }
			set { ViewState["SortExpression"] = value; }
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

		private int CodGrupo {
			get {
				if (ViewState["CodGrupo"] == null) ViewState["CodGrupo"] = 0;
				return Convert.ToInt32(ViewState["CodGrupo"]);
			}
			set { ViewState["CodGrupo"] = value; }
		}

		private string FiltroJogo {
			get {
				if (ViewState["FiltroJogo"] == null) ViewState["FiltroJogo"] = "T";
				return Convert.ToString(ViewState["FiltroJogo"]);
			}
			set { ViewState["FiltroJogo"] = value; }
		}

		public void Inicializar(int codBolao, int codFase, int codGrupo, string filtroJogo)
		{
			this.CodBolao = codBolao;
			this.CodFase = codFase;
			this.CodGrupo = codGrupo;
			this.FiltroJogo = filtroJogo;
		}

		public void CarregarJogos(int codBolao, int codFase, int codGrupo, string filtroJogo)
		{
			this.Inicializar(codBolao, codFase, codGrupo, filtroJogo);
			this.CarregarJogos();
		}

		private void CarregarJogos()
		{
			int codBolao = this.CodBolao;
			int codFase = this.CodFase;
			int codGrupo = this.CodGrupo;
			string filtroJogo = this.FiltroJogo;
			int codUsuario = Convert.ToInt32(Session["CodUsuario"]);

			DataView jogos = new DataView(Dados.Aposta.BuscarAposta(codBolao, codFase, codGrupo, codUsuario, filtroJogo));

			jogos.Sort = this.SortExpression;

			bool haRegistros = jogos.Count > 0;

			dgJogos.DataSource = jogos;
			dgJogos.DataBind();
			dgJogos.Visible = haRegistros;
			
			lbMensagem.Visible = !haRegistros;
			lbMensagem.Text = "Não existem jogos para esta fase.";
			btApostar1.Visible = haRegistros;
			btApostar2.Visible = haRegistros;
		}

		protected void Apostar(object sender, EventArgs e)
		{
			int codUsuario = Convert.ToInt32(Session["CodUsuario"]);
			bool houveApostaValida = false;

			foreach (DataGridItem item in dgJogos.Items)
			{
				if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
				{
					TextBox tbGolsA = item.FindControl("tbGolsA") as TextBox;
					TextBox tbGolsB = item.FindControl("tbGolsB") as TextBox;
					
					int codJogo = Convert.ToInt32(((HtmlInputHidden)item.FindControl("hdCodigoJogo")).Value);

					bool apostaValida = true;
					bool apostaVazia = false;

					int valor;
					
					if (tbGolsA.Enabled && tbGolsB.Enabled)
					{
						if (String.IsNullOrEmpty(tbGolsA.Text) && String.IsNullOrEmpty(tbGolsB.Text)) apostaVazia = true;
						else if (String.IsNullOrEmpty(tbGolsA.Text) || String.IsNullOrEmpty(tbGolsB.Text)) apostaValida = false;
						else if (!Int32.TryParse(tbGolsA.Text, out valor) || !Int32.TryParse(tbGolsB.Text, out valor)) apostaValida = false;
						else if (!(Convert.ToInt32(tbGolsA.Text) >= 0 && Convert.ToInt32(tbGolsB.Text) >= 0)) apostaValida = false;

						item.CssClass = (apostaValida) ? "" : "aposta-invalida";

						if (apostaValida && !apostaVazia)
						{
							int qtdGolsA = Convert.ToInt32(tbGolsA.Text);
							int qtdGolsB = Convert.ToInt32(tbGolsB.Text);

							Dados.Aposta.SalvarAposta(codJogo, codUsuario, qtdGolsA, qtdGolsB, DateTime.Now);
							houveApostaValida = true;
						}
						else if (apostaVazia)
						{
							Dados.Aposta.ExcluirAposta(codJogo, codUsuario);
							houveApostaValida = true;
						}
						else if (!apostaValida)
						{
							lbMensagem.Visible = true;
							lbMensagem.Text = "ATENÇÃO: As linhas em vermelho indicam apostas com valores incorretos.<br><br>";
						}
					}
				}
			}

			if (houveApostaValida)
			{
				Page.RegisterStartupScript("Sucesso",
					"<script language=\"javascript\">\n" +
					"window.onload = function() { alert('As suas apostas foram salvas com sucesso!'); }\n" +
					"</script>");
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
			this.dgJogos.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgJogos_SortCommand);
			this.dgJogos.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgJogos_ItemDataBound);

		}
		#endregion

		private void dgJogos_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label lbNumeroJogo = e.Item.FindControl("lbNumeroJogo") as Label;
				Label lbData = e.Item.FindControl("lbData") as Label;
				Label lbHora = e.Item.FindControl("lbHora") as Label;
				Label lbLocal = e.Item.FindControl("lbLocal") as Label;
				Label lbGrupo = e.Item.FindControl("lbGrupo") as Label;
				Label lbTimeA = e.Item.FindControl("lbTimeA") as Label;
				Label lbTimeB = e.Item.FindControl("lbTimeB") as Label;
				Label lbGolsA = e.Item.FindControl("lbGolsA") as Label;
				Label lbGolsB = e.Item.FindControl("lbGolsB") as Label;
				Label lbPontuacao = e.Item.FindControl("lbPontuacao") as Label;
				TextBox tbGolsA = e.Item.FindControl("tbGolsA") as TextBox;
				TextBox tbGolsB = e.Item.FindControl("tbGolsB") as TextBox;
				System.Web.UI.WebControls.Image imgTimeA = e.Item.FindControl("imgTimeA") as System.Web.UI.WebControls.Image;
				System.Web.UI.WebControls.Image imgTimeB = e.Item.FindControl("imgTimeB") as System.Web.UI.WebControls.Image;
				HtmlInputHidden hdCodigoJogo = e.Item.FindControl("hdCodigoJogo") as HtmlInputHidden;

				int numJogo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "num_jogo"));
				int codJogo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_jogo"));
				DateTime datJogo = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "dat_jogo"));
				DateTime datLimite = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "dat_limite_aposta"));
				string dscLocal = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_local"));
				string nomGrupo = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "nom_grupo"));
				string nomTimeA = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "nom_time_a"));
				string nomTimeB = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "nom_time_b"));
				string qtdGolsA = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "qtd_gol_a"));
				string qtdGolsB = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "qtd_gol_b"));
				string qtdGolsApostaA = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "qtd_gol_aposta_a"));
				string qtdGolsApostaB = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "qtd_gol_aposta_b"));
				string dscIconeA = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_icone_a"));
				string dscIconeB = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_icone_b"));
				string indRealizado = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ind_realizado"));
				string indApurado = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ind_apurada"));
				string dscCriterio = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_criterio"));
				string vlrPontuacao = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "vlr_pontuacao"));

				bool realizado = indRealizado == "S";
				bool apurado = indApurado == "S";

				hdCodigoJogo.Value = codJogo.ToString();
				lbNumeroJogo.Text = numJogo.ToString();
				lbData.Text = datJogo.ToString("dd/MMM");
				lbHora.Text = datJogo.ToString("HH:mm");
				lbLocal.Text = dscLocal;
				lbGrupo.Text = nomGrupo;
				lbTimeA.Text = nomTimeA;
				lbTimeB.Text = nomTimeB;
				lbGolsA.Text = "&nbsp;" + qtdGolsA + "&nbsp;";
				lbGolsB.Text = "&nbsp;" + qtdGolsB + "&nbsp;";
				tbGolsA.Text = qtdGolsApostaA;
				tbGolsB.Text = qtdGolsApostaB;
				lbGolsA.Visible = realizado;
				lbGolsB.Visible = realizado;
				tbGolsA.Enabled = !realizado && (datLimite.CompareTo(DateTime.Now) > 0);
				tbGolsB.Enabled = !realizado && (datLimite.CompareTo(DateTime.Now) > 0);
				imgTimeA.ImageUrl = "../Images/Times/" + dscIconeA;
				imgTimeB.ImageUrl = "../Images/Times/" + dscIconeB;
				lbPontuacao.Text = apurado ? vlrPontuacao : "-";
				lbPontuacao.Attributes["title"] = apurado ? dscCriterio : (realizado ? "NÃO PONTUOU" : "");
				if (realizado) e.Item.CssClass = "jogo-realizado";
			}
		}

		private void dgJogos_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
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

			this.CarregarJogos();
		}
	}
}
