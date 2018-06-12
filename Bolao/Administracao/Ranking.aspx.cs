//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\administracao\Stub_ranking_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'administracao\ranking.aspx' was also modified to refer to the new class name.
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
	public partial class Ranking : BasePage
	{
		private Dados.Bolao bolao;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			Lib.Util.ValidarAdministrador(this.Context);

			bolao = new Dados.Bolao();

			if (!this.IsPostBack)
			{
				this.SortExpression = "num_ranking asc";
				this.CarregarBolao();
				this.CarregarRanking();
			}
		}

		private void CarregarBolao()
		{
			DataTable lista = bolao.BuscarBolao();

			ddlBolao.DataSource = lista;
			ddlBolao.DataTextField = "dsc_bolao";
			ddlBolao.DataValueField = "cod_bolao";
			ddlBolao.DataBind();
		}

		private string SortExpression
		{
			get { return Convert.ToString(ViewState["SortExpression"]); }
			set { ViewState["SortExpression"] = value; }
		}

		private void CarregarRanking()
		{
			int codBolao = Convert.ToInt32(ddlBolao.SelectedValue);

			DataView ranking = new DataView(bolao.BuscarRanking(codBolao));

			ranking.Sort = this.SortExpression;

			dgUsuarios.DataSource = ranking;
			dgUsuarios.DataBind();
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
			this.dgUsuarios.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgUsuarios_SortCommand);

		}
		#endregion

		private void dgUsuarios_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
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

			this.CarregarRanking();
		}

		protected void ddlBolao_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.CarregarRanking();
		}
	}
}
