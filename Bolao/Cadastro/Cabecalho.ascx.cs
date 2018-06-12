namespace Bolao.Cadastro
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Cabecalho.
	/// </summary>
	public partial class Cabecalho : System.Web.UI.UserControl
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			bool exibirSair = false;

			if (Session["UsuarioValido"] != null)
			{
				exibirSair = Convert.ToBoolean(Session["UsuarioValido"]);
			}

			lbtSair.Visible = exibirSair;
		}

		private bool UsuarioLogado
		{
			get 
			{
				if (Session["UsuarioValido"] == null)
				{
					return false;
				}
				else
				{
					return Convert.ToBoolean(Session["UsuarioValido"]);
				}
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

		}
		#endregion

		protected void lbtSair_Click(object sender, System.EventArgs e)
		{
			Session.Abandon();
			Response.Redirect("../Default.aspx", true);
		}

		protected void lbtVoltar_Click(object sender, System.EventArgs e)
		{
			if (this.UsuarioLogado)
			{
				Response.Redirect("../Aposta/Default.aspx");
			}
			else
			{
				Response.Redirect("../Default.aspx");
			}
		}
	}
}
