//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\administracao\Stub_time_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'administracao\time.aspx' was also modified to refer to the new class name.
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
	public partial class Time : BasePage
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Lib.Util.ValidarAdministrador(this.Context);
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

		}
		#endregion
	}
}
