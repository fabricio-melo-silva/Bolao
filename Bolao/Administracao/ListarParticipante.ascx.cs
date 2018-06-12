using Bolao.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using W3.Library;

namespace Bolao.Administracao {
	/// <summary>
	///		Summary description for ListarBolao.
	/// </summary>
	public partial class ListarParticipante : BaseControl {
		public event EventHandler Voltar;

		private Dados.Bolao bolao;


		protected System.Web.UI.WebControls.Button btIncluirFase;

		protected void Page_Load(object sender, System.EventArgs e) {
			bolao = new Dados.Bolao();

			ucEditarParticipante.Salvar += new EventHandler(this.SalvarParticipante);
			ucEditarParticipante.Cancelar += new EventHandler(this.CancelarEdicaoParticipante);

			if (!this.IsPostBack) {
				this.CarregarParticipantes();
			}
		}

		private int CodBolao {
			get {
				if (ViewState["CodBolao"] == null) ViewState["CodBolao"] = 0;
				return Convert.ToInt32(ViewState["CodBolao"]);
			}
			set { ViewState["CodBolao"] = value; }
		}

		public void Inicializar(int codBolao) {
			this.CodBolao = codBolao;
			this.CarregarParticipantes();
			this.CarregarBolao();
		}

		private void CarregarParticipantes() {
			var participantes = Participante.BuscarParticipantes(null, this.CodBolao, null);
			var usuarios = Dados.Usuario.BuscarUsuarios();

			var lista = from p in participantes
						join u in usuarios on p.CodigoUsuario equals u.CodigoUsuario
						orderby u.NomeUsuario
						select new {
							CodigoUsuario = p.CodigoUsuario,
							NomeUsuario = u.NomeUsuario,
							Email = u.Email,
							Administrador = p.Administrador,
							Pago = p.Pago
						};

			dgParticipante.DataSource = lista;
			dgParticipante.DataBind();
			dgParticipante.Visible = (participantes.Count > 0);

			lbMensagem.Visible = (participantes.Count == 0);
			lbMensagem.Text = "Não existe nenhum participante cadastrado.";
		}

		private void CarregarBolao() {
			DataRow registro = bolao.BuscarBolao(this.CodBolao);

			if (registro != null) {
				lbNomeBolao.Text = "" + Convert.ToString(registro["dsc_bolao"]);
			}
		}

		protected void AlterarParticipante(object sender, EventArgs e) {
			int codBolao = this.CodBolao;
			int codUsuario = Convert.ToInt32(((Button)sender).CommandArgument);

			pnListar.Visible = false;
			ucEditarParticipante.Visible = true;
			ucEditarParticipante.Inicializar(codBolao, codUsuario);
		}

		protected void ExcluirParticipante(object sender, EventArgs e) {
			int codUsuario = Convert.ToInt32(((Button)sender).CommandArgument);

			try {
				Participante.ExcluirParticipante(codUsuario, this.CodBolao);

				this.CarregarParticipantes();
			}
			catch (Exception erro) {
				lbMensagem.Text = String.Format("<p>Erro ao excluir: {0}</p>", erro.Message);
				lbMensagem.Visible = true;
			}
		}

		protected void SalvarParticipante(object sender, EventArgs e) {
			this.CarregarParticipantes();
			pnListar.Visible = true;
			ucEditarParticipante.Visible = false;
		}

		protected void CancelarEdicaoParticipante(object sender, EventArgs e) {
			pnListar.Visible = true;
			ucEditarParticipante.Visible = false;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) {
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
		private void InitializeComponent() {
			this.dgParticipante.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgParticipante_ItemDataBound);

		}
		#endregion

		private void dgParticipante_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e) {
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
				HyperLink hlEmail = e.Item.FindControl("hlEmail") as HyperLink;
				Label lbAdministrador = e.Item.FindControl("lbAdministrador") as Label;
				Label lbBolaoPago = e.Item.FindControl("lbBolaoPago") as Label;
				Button btAlterar = e.Item.FindControl("btAlterar") as Button;
				Button btExcluir = e.Item.FindControl("btExcluir") as Button;

				int codUsuario = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "CodigoUsuario"));
				string dscEmail = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Email"));
				string indAdministrador = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Administrador"));
				string indBolaoPago = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Pago"));

				hlEmail.Text = dscEmail;
				hlEmail.NavigateUrl = "mailto:" + dscEmail;
				lbAdministrador.Text = (indAdministrador == "S") ? "Sim" : "Não";
				lbBolaoPago.Text = (indBolaoPago == "S") ? "Sim" : "Não";

				btAlterar.CommandArgument = codUsuario.ToString();
				btExcluir.CommandArgument = codUsuario.ToString();
				btExcluir.Attributes["onclick"] = "return (confirm(\"Deseja realmente excluir este participante?\\nEsta operação não poderá ser desfeita.\"));";

				btAlterar.Attributes["title"] = "Alterar Participante";
				btExcluir.Attributes["title"] = "Excluir Participante";
			}
		}

		protected void btVoltar_Click(object sender, System.EventArgs e) {
			if (this.Voltar != null) this.Voltar(this, e);
		}

		protected void btIncluir_Click(object sender, EventArgs e) {
			int codBolao = this.CodBolao;
			int codUsuario = 0;

			pnListar.Visible = false;
			ucEditarParticipante.Visible = true;
			ucEditarParticipante.Inicializar(codBolao, codUsuario);
		}
	}
}
