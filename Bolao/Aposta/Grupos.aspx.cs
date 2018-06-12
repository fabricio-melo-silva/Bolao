//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\aposta\Stub_grupos_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'aposta\grupos.aspx' was also modified to refer to the new class name.
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

namespace Bolao.Aposta
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class Grupos : BasePage
	{
		private Dados.Time time;
		private Dados.Fase fase;
		private Dados.Jogo jogo;
		private Dados.Grupo grupo;

		
		protected System.Web.UI.WebControls.DataGrid dgJogos;
		protected System.Web.UI.WebControls.Button btApostar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Lib.Util.ValidarLogin(this.Context);

			time = new Bolao.Dados.Time();
			fase = new Bolao.Dados.Fase();
			jogo = new Bolao.Dados.Jogo();
			grupo = new Bolao.Dados.Grupo();

			if (!this.IsPostBack)
			{
				this.CarregarFases();
				this.CarregarGrupos();

				ucListarJogo.Visible = false;
			}
		}

		private int CodBolao
		{
			get { return Convert.ToInt32(Session["CodBolao"]); }
		}

		private void CarregarFases()
		{
			int codFaseDefault = 0;

			DataTable fases = fase.BuscarFase(this.CodBolao);
			DataView visao = new DataView(fases);

			visao.RowFilter = "ind_status = 'A'";

			if (visao.Count > 0)
			{
				codFaseDefault = Convert.ToInt32(visao[0]["cod_fase"]);
			}

			visao.RowFilter = "ind_status in ('A', 'F')";

			ddlFase.DataSource = visao;
			ddlFase.DataTextField = "dsc_fase";
			ddlFase.DataValueField = "cod_fase";
			ddlFase.DataBind();

			if (codFaseDefault != 0)
			{
				ddlFase.SelectedIndex = -1;
				ddlFase.Items.FindByValue(codFaseDefault.ToString()).Selected = true;
			}
		}

		private void CarregarGrupos()
		{
			int codBolao = this.CodBolao;
			int codFase = Convert.ToInt32(ddlFase.SelectedValue);

			DataTable dtGrupos = grupo.BuscarGrupo(codBolao, codFase);

			ddlGrupo.DataSource = dtGrupos;
			ddlGrupo.DataValueField = "cod_grupo";
			ddlGrupo.DataTextField = "nom_grupo";
			ddlGrupo.DataBind();

			ddlGrupo.Items.Insert(0, new ListItem("Selecione...", "0"));
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
			this.dgTimes.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgTimes_ItemDataBound);

		}
		#endregion

		private void CarregarTimes()
		{
			int codGrupo = Convert.ToInt32(ddlGrupo.SelectedValue);

			DataTable dtTimes = time.BuscarTimeGrupo(codGrupo);

			dgTimes.DataSource = dtTimes;
			dgTimes.DataBind();
		}

		private void CarregarJogos()
		{			
			int codBolao = Convert.ToInt32("0" + Session["CodBolao"]);
			int codFase = Convert.ToInt32("0" + ddlFase.SelectedValue);
			int codGrupo = Convert.ToInt32("0" + ddlGrupo.SelectedValue);
			string filtroJogo = "T";
			
			ucListarJogo.CarregarJogos(codBolao, codFase, codGrupo, filtroJogo);
			ucListarJogo.Visible = true;
		}

		private void dgTimes_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string indClassificado = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ind_classificado"));

				if (indClassificado == "S")
				{
					e.Item.CssClass = "classificado";
				}
			}
		}

		protected void ddlFase_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.CarregarGrupos();

			ucListarJogo.Visible = false;
		}

		protected void ddlGrupo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.CarregarTimes();
			this.CarregarJogos();
		}
	}
}
