using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using W3.Library;
using System.Linq;

namespace Bolao.Administracao
{
	public partial class ListarUsuario : BaseControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ucEditarUsuario.Salvar += new EventHandler(this.SalvarUsuario);
			ucEditarUsuario.Cancelar += new EventHandler(this.CancelarEdicaoUsuario);

			if (!this.IsPostBack)
			{
				this.SortExpression = "NomeUsuario asc";
				this.CarregarUsuarios();
			}
		}

		private string SortExpression
		{
			get { return Convert.ToString(ViewState["SortExpression"]); }
			set { ViewState["SortExpression"] = value; }
		}

		private void SalvarUsuario(object sender, EventArgs e)
		{
			ucEditarUsuario.Visible = false;
			pnListar.Visible = true;
			this.CarregarUsuarios();
		}

		private void CancelarEdicaoUsuario(object sender, EventArgs e)
		{
			ucEditarUsuario.Visible = false;
			pnListar.Visible = true;
		}

		public void CarregarUsuarios()
		{
			var usuarios = from x in Dados.Usuario.BuscarUsuarios()
						   orderby x.NomeUsuario
						   select x;

			dgUsuario.DataSource = usuarios;
			dgUsuario.DataBind();
			dgUsuario.Visible = (usuarios.Count() > 0);
			
			lbMensagem.Visible = (usuarios.Count() == 0);
			lbMensagem.Text = "Não existe nenhum usuário cadastrado.";
		}

		protected void AlterarUsuario(object sender, EventArgs e)
		{
			int codUsuario = Convert.ToInt32(((Button)sender).CommandArgument);

			ucEditarUsuario.Inicializar(codUsuario);
			ucEditarUsuario.Visible = true;
			pnListar.Visible = false;
		}

		protected void AtivarInativar(object sender, EventArgs e)
		{
			int codUsuario = Convert.ToInt32(((Button)sender).CommandArgument);

			try {
				Dados.Usuario.AtivarInativar(codUsuario);

				this.CarregarUsuarios();
			}
			catch (Exception erro) {
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
			this.dgUsuario.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgUsuario_SortCommand);
			this.dgUsuario.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgUsuario_ItemDataBound);

		}
		#endregion

		private void dgUsuario_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label lbDataCadastro = e.Item.FindControl("lbDataCadastro") as Label;
				Label lbAtivo = e.Item.FindControl("lbAtivo") as Label;
				Button btAlterar = e.Item.FindControl("btAlterar") as Button;
				Button btAtivarInativar = e.Item.FindControl("btAtivarInativar") as Button;

				int codUsuario = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "CodigoUsuario"));
				DateTime datCadastro = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "DataCadastro"));
				bool ativo = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Ativo"));

				lbDataCadastro.Text = datCadastro.ToString("dd/MM/yyyy HH:mm:ss");
				lbAtivo.Text = (ativo ? "Sim" : "Não");
				
				btAlterar.CommandArgument = codUsuario.ToString();
				btAlterar.Attributes["title"] = "Alterar Usuário";

				btAtivarInativar.CommandArgument = codUsuario.ToString();

				if (ativo) {
					btAtivarInativar.Attributes["onclick"] = "return (confirm(\"Deseja realmente inativar este usuário?\"));";
					btAtivarInativar.Attributes["title"] = "Inativar usuário";
					btAtivarInativar.Text = "I";
				}
				else {
					btAtivarInativar.Attributes["onclick"] = "return (confirm(\"Deseja realmente ativar este usuário?\"));";
					btAtivarInativar.Attributes["title"] = "Ativar usuário";
					btAtivarInativar.Text = "A";
				}
			}
		}

		private void dgUsuario_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
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

			this.CarregarUsuarios();
		}

		protected void btIncluirUsuario_Click(object sender, System.EventArgs e)
		{
			ucEditarUsuario.Inicializar(0);
			ucEditarUsuario.Visible = true;
			pnListar.Visible = false;
		}
	}
}
