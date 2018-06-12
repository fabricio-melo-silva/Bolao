//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\administracao\Stub_jogo_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'administracao\jogo.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using W3.Library;

namespace Bolao.Administracao
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class Jogo : BasePage
	{
		private Dados.Jogo jogo;
		private Dados.Fase fase;
		private Dados.Bolao bolao;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			Lib.Util.ValidarAdministrador(this.Context);
			
			bolao = new Dados.Bolao();
			jogo = new Dados.Jogo();
			fase = new Dados.Fase();

			if (!this.IsPostBack)
			{
				this.SortExpression = "num_jogo asc";
				this.CarregarBolao();
				this.CarregarFases();
				this.CarregarJogos();
			}
		}

		private string SortExpression
		{
			get { return Convert.ToString(ViewState["SortExpression"]); }
			set { ViewState["SortExpression"] = value; }
		}

		private void CarregarBolao()
		{
			DataTable lista = bolao.BuscarBolao();

			ddlBolao.DataSource = lista;
			ddlBolao.DataTextField = "dsc_bolao";
			ddlBolao.DataValueField = "cod_bolao";
			ddlBolao.DataBind();
		}

		private void CarregarFases()
		{
			int codBolao = Convert.ToInt32(ddlBolao.SelectedValue);

			DataTable fases = fase.BuscarFase(codBolao);

			ddlFase.DataSource = fases;
			ddlFase.DataTextField = "dsc_fase";
			ddlFase.DataValueField = "cod_fase";
			ddlFase.DataBind();
		}

		private void CarregarJogos()
		{
			int codBolao = Convert.ToInt32(ddlBolao.SelectedValue);
			int codFase = Convert.ToInt32(ddlFase.SelectedValue);
			int codGrupo = 0;
			string indTipoJogo = ddlJogos.SelectedValue;
			string filtro = "";

			switch (indTipoJogo.ToUpper())
			{
				case "N": filtro = "ind_realizado = 'N'"; break;
				case "R": filtro = "ind_realizado = 'S'"; break;
				default: filtro = ""; break;
			}

			DataView jogos = new DataView(jogo.BuscarJogo(codBolao, codFase, codGrupo));

			jogos.Sort = this.SortExpression;
			jogos.RowFilter = filtro;

			dgJogos.DataSource = jogos;
			dgJogos.DataBind();
			dgJogos.Visible = (jogos.Count > 0);
			
			lbMensagem.Visible = (jogos.Count == 0);
			lbMensagem.Text = "Não existem jogos para esta fase.";
			btSalvarResultado1.Visible = (jogos.Count > 0);
			btSalvarResultado2.Visible = (jogos.Count > 0);
		}

		protected void AtualizarRanking(object sender, EventArgs e) {
			int codBolao = Convert.ToInt32(ddlBolao.SelectedValue);
			Dados.Bolao bolao = new Dados.Bolao();

			try {
				bolao.GerarRanking(codBolao);
				lbMensagem.Visible = true;
				lbMensagem.Text = "Ranking atualizado!";
			}
			catch (Exception error) {
				lbMensagem.Visible = true;
				lbMensagem.Text = "Erro ao tentar atualizar o ranking: " + error.Message;
			}
		}

		protected void SalvarResultados(object sender, EventArgs e)
		{
			int codUsuario = Convert.ToInt32(Session["CodUsuario"]);

			try {
				bool sucesso = true;

				foreach (DataGridItem item in dgJogos.Items) {
					if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem) {
						TextBox tbGolsA = item.FindControl("tbGolsA") as TextBox;
						TextBox tbGolsB = item.FindControl("tbGolsB") as TextBox;
						CheckBox cbRealizado = item.FindControl("cbRealizado") as CheckBox;

						int codJogo = Convert.ToInt32(((HtmlInputHidden)item.FindControl("hdCodigoJogo")).Value);

						bool placarValido = true;
						bool placarVazio = false;

						int valor;

						if (String.IsNullOrEmpty(tbGolsA.Text) && String.IsNullOrEmpty(tbGolsB.Text)) placarVazio = true;
						else if (String.IsNullOrEmpty(tbGolsA.Text) || String.IsNullOrEmpty(tbGolsB.Text)) placarValido = false;
						else if (!Int32.TryParse(tbGolsA.Text, out valor) || !Int32.TryParse(tbGolsB.Text, out valor)) placarValido = false;
						else if (!(Convert.ToInt32(tbGolsA.Text) >= 0 && Convert.ToInt32(tbGolsB.Text) >= 0)) placarValido = false;

						item.CssClass = (placarValido) ? "" : "aposta-invalida";

						if (placarValido && !placarVazio) {
							int qtdGolsA = Convert.ToInt32(tbGolsA.Text);
							int qtdGolsB = Convert.ToInt32(tbGolsB.Text);

							jogo.SalvarResultadoJogo(codJogo, qtdGolsA, qtdGolsB, cbRealizado.Checked);
						}
						else if (!placarValido) {
							sucesso = false;
							lbMensagem.Visible = true;
							lbMensagem.Text = "ATENÇÃO: As linhas em vermelho indicam placares de jogos incorretos.<br><br>";
						}
					}
				}

				if (sucesso) {
					lbMensagem.Visible = true;
					lbMensagem.Text = "Resultados salvos com sucesso!";
				}
			}
			catch (Exception error) {
				lbMensagem.Visible = true;
				lbMensagem.Text = "Erro ao salvar os resultados: " + error.Message;
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
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
				TextBox tbGolsA = e.Item.FindControl("tbGolsA") as TextBox;
				TextBox tbGolsB = e.Item.FindControl("tbGolsB") as TextBox;
				CheckBox cbRealizado = e.Item.FindControl("cbRealizado") as CheckBox;
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
				string dscIconeA = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_icone_a"));
				string dscIconeB = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_icone_b"));
				string indRealizado = "" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ind_realizado"));

				bool realizado = indRealizado == "S";

				cbRealizado.Checked = realizado;
				hdCodigoJogo.Value = codJogo.ToString();
				lbNumeroJogo.Text = numJogo.ToString();
				lbData.Text = datJogo.ToString("dd/MM");
				lbHora.Text = datJogo.ToString("HH:mm");
				lbLocal.Text = dscLocal;
				lbGrupo.Text = nomGrupo;
				lbTimeA.Text = nomTimeA;
				lbTimeB.Text = nomTimeB;
				tbGolsA.Text = qtdGolsA;
				tbGolsB.Text = qtdGolsB;
				imgTimeA.ImageUrl = "../Images/Times/" + dscIconeA;
				imgTimeB.ImageUrl = "../Images/Times/" + dscIconeB;
			}
		}

		protected void ddlFase_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.CarregarJogos();
		}

		protected void ddlBolao_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.CarregarFases();
			this.CarregarJogos();
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

		protected void ddlJogos_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.CarregarJogos();
		}
	}
}
