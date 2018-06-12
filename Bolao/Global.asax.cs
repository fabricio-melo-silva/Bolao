using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.Routing;
using System.Web.Hosting;
using System.Web.Http;
using W3.Library.Data;
using System.Configuration;

namespace Bolao 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			//Configura as rotas das web apis situadas na pasta API/Controller
			GlobalConfiguration.Configure(Bolao.App_Start.WebApiConfig.Register);
		}

		protected void Session_Start(Object sender, EventArgs e)
		{
			Session["CodBolao"] = 1;
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{
			DatabaseUtil.CloseConnector();
		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{
			//HttpContext context = HttpContext.Current;
			//Exception lastError = context.Server.GetLastError();

			//if (lastError == null) return;

			//if (lastError is System.Web.HttpUnhandledException && lastError.InnerException != null) {
			//	lastError = lastError.InnerException;
			//}

			//bool isHttpException = (lastError is HttpException);
			//bool isW3GenericException = (lastError is W3GenericException);
			//bool isNotFoundException = isHttpException && ((HttpException)lastError).GetHttpCode() == (int)HttpStatusCode.NotFound;
			//bool isDatabaseVersionException = IsDatabaseVersionException(lastError);

			//string errorMessage = lastError.Message;

			//long? errorId = null;

			//if (!isNotFoundException) {
			//	errorId = Log.LogErro.WriteLog(lastError, context);
			//}

			//context.Server.ClearError();

			//try {
			//	DatabaseUtil.CloseConnector();
			//}
			//catch {
			//}

			//if (lastError != null) {
			//	string errorPage = ConfigurationManager.AppSettings["ErroPage"];

			//	if (!errorPage.StartsWith("~/")) errorPage = "~/" + errorPage;

			//	if (isHttpException) {
			//		if (isNotFoundException) {
			//			context.Response.Write("Resource not found: " + context.Request.Url.AbsoluteUri);
			//			context.Response.End();
			//		}
			//		else if (!String.IsNullOrEmpty(errorPage)) {
			//			errorPage += "?Id=" + (errorId.HasValue ? errorId.Value.ToString() : String.Empty);
			//			context.Server.Transfer(errorPage);
			//		}
			//	}
			//	else if (isDatabaseVersionException) {
			//		context.Response.Write(errorMessage);
			//		context.Response.End();
			//	}
			//	else if (context.CurrentHandler is System.Web.UI.Page) {
			//		if (!String.IsNullOrEmpty(errorPage)) {
			//			errorPage += "?Id=" + (errorId.HasValue ? errorId.Value.ToString() : String.Empty);
			//			context.Server.Transfer(errorPage);
			//		}
			//		else if (context.Request.IsLocal) {
			//			context.Response.Write("<p><b>Error Id: </b>" + (errorId.HasValue ? errorId.Value.ToString() : "-") + "</br>");
			//			context.Response.Write("<p><b>Error Message: </b>" + lastError.Message + "</br>");
			//			context.Response.Write("<p><b>Error Source: </b>" + lastError.Source + "</br>");
			//			context.Response.Write("<p><b>Error Stack Trace: </b></ br>" + lastError.StackTrace + "</br>");
			//			context.Response.End();
			//		}
			//		else {
			//			context.Response.Write("<p><b>Error Id: </b>" + (errorId.HasValue ? errorId.Value.ToString() : "-") + "</br>");
			//			context.Response.End();
			//		}
			//	}
			//	else {
			//		if (isW3GenericException) {
			//			context.Response.Write(
			//				String.Format("{{\"OK\": false, \"CodLogErro\": \"{0}\", \"Redirect\": \"{1}\"}}",
			//					errorId.Value.ToString(),
			//					(!String.IsNullOrEmpty(errorPage) ? VirtualPathUtility.ToAbsolute(errorPage) + "?Id=" + errorId.Value.ToString() : String.Empty)));
			//			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			//			context.Response.End();
			//		}
			//		else if (context.CurrentHandler is IHttpHandler) {
			//			if (context.Request.IsLocal) {
			//				context.Response.Write("<p><b>Error Id: </b>" + (errorId.HasValue ? errorId.Value.ToString() : "-") + "</br>");
			//				context.Response.Write("<p><b>Error Message: </b>" + lastError.Message + "</br>");
			//				context.Response.Write("<p><b>Error Source: </b>" + lastError.Source + "</br>");
			//				context.Response.Write("<p><b>Error Stack Trace: </b></ br>" + lastError.StackTrace + "</br>");
			//				context.Response.End();
			//			}
			//			else {
			//				context.Response.Write("<p><b>Error Id: </b>" + (errorId.HasValue ? errorId.Value.ToString() : "-") + "</br>");
			//				context.Response.End();
			//			}
			//		}
			//	}
			//}
		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

