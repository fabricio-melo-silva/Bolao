using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using W3.Library;
using W3.Library.Utils;

namespace Bolao.Administracao
{
	/// <summary>
	///		Summary description for EditarBolao.
	/// </summary>
	public partial class EditarTime : BaseControl
	{
		public event EventHandler Salvar;
		public event EventHandler Cancelar;

		private Dados.Time time;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIcone;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			time = new Dados.Time();

			lbMensagem.Visible = false;
		}

		private int CodTime
		{
			get { return Convert.ToInt32("0" + Convert.ToString(ViewState["CodTime"])); }
			set { ViewState["CodTime"] = value; }
		}

		public void Inicializar(int codTime)
		{
			this.CodTime = codTime;

			if (codTime == 0)
			{
				tbNome.Text = "";
				tbSigla.Text = "";
				imgIcone.Visible = false;
				lbOperacao.Text = "Incluir Time";
			}
			else
			{
				DataRow registro = time.BuscarTime(codTime);

				if (registro != null)
				{
					tbNome.Text = "" + Convert.ToString(registro["nom_time"]);
					tbSigla.Text = "" + Convert.ToString(registro["sgl_time"]);
					imgIcone.Visible = true;
					imgIcone.ImageUrl = "../Images/Times/" + Convert.ToString(registro["dsc_icone"]);
				}

				lbOperacao.Text = "Alterar Time";
			}
		}

		protected void ValidarIcone(object sender, ServerValidateEventArgs e)
		{
			string nomeArquivo = (ifIcone.PostedFile != null) ? ifIcone.PostedFile.FileName.ToLower() : "";
			bool imagem = nomeArquivo.EndsWith(".gif") || nomeArquivo.EndsWith(".jpg") || nomeArquivo.EndsWith(".jpeg");

			if (this.CodTime == 0)
			{
				e.IsValid = (ifIcone.PostedFile != null) && imagem;
			}
			else
			{
				e.IsValid = (ifIcone.PostedFile == null) || imagem;
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

		protected void btSalvar_Click(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				int codTime = this.CodTime;

				string nomePasta = Server.MapPath("..\\Images\\Times") + "\\";
				string nomeArquivoNovo = ifIcone.PostedFile.FileName;
				string nomeArquivoVelho = (imgIcone.Visible) ? imgIcone.ImageUrl : "";
				string dscIcone;

				nomeArquivoVelho = nomePasta + nomeArquivoVelho.Substring(nomeArquivoVelho.LastIndexOf("/") + 1);

				if (ifIcone.PostedFile.ContentLength != 0)
				{
					nomeArquivoNovo = nomeArquivoNovo.Substring(nomeArquivoNovo.LastIndexOf("\\") + 1);
					nomeArquivoNovo = Arquivo.GerarNomeArquivoInexistente(nomePasta + nomeArquivoNovo);
				}
				else
				{
					nomeArquivoNovo = nomeArquivoVelho;
				}

				dscIcone = nomeArquivoNovo.Substring(nomeArquivoNovo.LastIndexOf("\\") + 1);

				try
				{
					time.SalvarTime(ref codTime, tbNome.Text, tbSigla.Text, dscIcone);
					
					this.CodTime = codTime;

					Arquivo.SalvarArquivo(nomeArquivoNovo, ref ifIcone);

					if (File.Exists(nomeArquivoVelho) && (nomeArquivoNovo != nomeArquivoVelho)) 
					{
						File.Delete(nomeArquivoVelho);
					}

					if (this.Salvar != null) this.Salvar(this, e);
				}
				catch (Exception erro)
				{
					lbMensagem.Text = String.Format("<p>Erro ao salvar: {0}</p>", erro.Message);
					lbMensagem.Visible = true;
				}
			}
		}

		protected void btCancelar_Click(object sender, System.EventArgs e)
		{
			if (this.Cancelar != null) this.Cancelar(this, e);
		}
	}
}
