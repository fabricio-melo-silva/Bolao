using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using W3.Library;

namespace Bolao.Administracao
{
	public partial class ListarTime : BaseControl
	{
		private Dados.Time time;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			time = new Dados.Time();

			ucEditarTime.Salvar += new EventHandler(this.SalvarTime);
			ucEditarTime.Cancelar += new EventHandler(this.CancelarEdicaoTime);

			if (!this.IsPostBack)
			{
				this.SortExpression = "nom_time asc";
				this.CarregarTimes();
			}
		}

		private string SortExpression
		{
			get { return Convert.ToString(ViewState["SortExpression"]); }
			set { ViewState["SortExpression"] = value; }
		}

		private void SalvarTime(object sender, EventArgs e)
		{
			ucEditarTime.Visible = false;
			pnListar.Visible = true;
			this.CarregarTimes();
		}

		private void CancelarEdicaoTime(object sender, EventArgs e)
		{
			ucEditarTime.Visible = false;
			pnListar.Visible = true;
		}

		public void CarregarTimes()
		{
			DataView times = new DataView(time.BuscarTime());

			times.Sort = this.SortExpression;

			dgTime.DataSource = times;
			dgTime.DataBind();
			dgTime.Visible = (times.Count > 0);
			
			lbMensagem.Visible = (times.Count == 0);
			lbMensagem.Text = "Não existe nenhum time cadastrado.";
		}

		protected void AlterarTime(object sender, EventArgs e)
		{
			int codTime = Convert.ToInt32(((Button)sender).CommandArgument);

			ucEditarTime.Inicializar(codTime);
			ucEditarTime.Visible = true;
			pnListar.Visible = false;
		}

		protected void ExcluirTime(object sender, EventArgs e)
		{
			int codTime = Convert.ToInt32(((Button)sender).CommandArgument);

			try
			{
				DataRow registro = time.BuscarTime(codTime);

				string nomePasta = Server.MapPath("..\\Images\\Times") + "\\";
				string dscIcone = "" + Convert.ToString(registro["dsc_icone"]);

				time.ExcluirTime(codTime);

				if (File.Exists(nomePasta + dscIcone)) File.Delete(nomePasta + dscIcone);

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
			this.dgTime.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgTime_SortCommand);
			this.dgTime.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgTime_ItemDataBound);

		}
		#endregion

		private void dgTime_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Image imgIcone = e.Item.FindControl("imgIcone") as Image;
				Button btAlterar = e.Item.FindControl("btAlterar") as Button;
				Button btExcluir = e.Item.FindControl("btExcluir") as Button;

				int codTime = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_time"));
				string dscIcone = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "dsc_icone"));

				imgIcone.ImageUrl = "../Images/Times/" + dscIcone;
				
				btAlterar.CommandArgument = codTime.ToString();
				btAlterar.Attributes["title"] = "Alterar Time";

				btExcluir.CommandArgument = codTime.ToString();
				btExcluir.Attributes["onclick"] = "return (confirm(\"Deseja realmente excluir este time?\\nEsta operação não poderá ser desfeita.\"));";
				btExcluir.Attributes["title"] = "Excluir Time";
			}
		}

		private void dgTime_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
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

			this.CarregarTimes();
		}

		protected void btIncluirTime_Click(object sender, System.EventArgs e)
		{
			ucEditarTime.Inicializar(0);
			ucEditarTime.Visible = true;
			pnListar.Visible = false;
		}
	}
}
