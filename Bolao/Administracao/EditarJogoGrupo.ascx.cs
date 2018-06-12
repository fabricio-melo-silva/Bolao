namespace Bolao.Administracao {
	using System;
	using System.Data;
	using System.Drawing;
	using System.IO;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using W3.Library;
	using System.Globalization;

	/// <summary>
	///		Summary description for EditarBolao.
	/// </summary>
	public partial class EditarJogoGrupo : BaseControl {
		public event EventHandler Salvar;
		public event EventHandler Cancelar;

		private Dados.Jogo jogo;
		private Dados.Time time;


		protected void Page_Load(object sender, System.EventArgs e) {
			jogo = new Dados.Jogo();
			time = new Dados.Time();
		}

		private int CodBolao {
			get {
				if (ViewState["CodBolao"] == null) ViewState["CodBolao"] = 0;

				return Convert.ToInt32(ViewState["CodBolao"]);
			}
			set { ViewState["CodBolao"] = value; }
		}

		private int CodFase {
			get {
				if (ViewState["CodFase"] == null) ViewState["CodFase"] = 0;

				return Convert.ToInt32(ViewState["CodFase"]);
			}
			set { ViewState["CodFase"] = value; }
		}

		private int CodGrupo {
			get {
				if (ViewState["CodGrupo"] == null) ViewState["CodGrupo"] = 0;

				return Convert.ToInt32(ViewState["CodGrupo"]);
			}
			set { ViewState["CodGrupo"] = value; }
		}

		private int CodJogo {
			get {
				if (ViewState["CodJogo"] == null) ViewState["CodJogo"] = 0;

				return Convert.ToInt32(ViewState["CodJogo"]);
			}
			set { ViewState["CodJogo"] = value; }
		}

		public void Inicializar(int codBolao, int codFase, int codGrupo, int codJogo) {
			this.CodBolao = codBolao;
			this.CodFase = codFase;
			this.CodGrupo = codGrupo;
			this.CodJogo = codJogo;
			this.CarregarJogo();
		}

		private void CarregarTimes() {
			DataTable times = time.BuscarTimeGrupo(this.CodGrupo);

			ddlTimeA.DataSource = times;
			ddlTimeA.DataTextField = "nom_time";
			ddlTimeA.DataValueField = "cod_time";
			ddlTimeA.DataBind();

			ddlTimeB.DataSource = times;
			ddlTimeB.DataTextField = "nom_time";
			ddlTimeB.DataValueField = "cod_time";
			ddlTimeB.DataBind();
		}

		private void CarregarJogo() {
			this.CarregarTimes();

			if (this.CodJogo == 0) {
				ddlTimeA.Items.Insert(0, new ListItem("Selecione...", "0"));
				ddlTimeB.Items.Insert(0, new ListItem("Selecione...", "0"));
				tbData.Text = "";
				tbHora.Text = "";
				tbLocal.Text = "";
				tbNumeroJogo.Text = "";
				tbDataLimite.Text = "";
				lbOperacao.Text = "Incluir Jogo";
			}
			else {
				var jogo = Dados.Jogo.BuscarJogo(this.CodJogo);

				ddlTimeA.SelectedIndex = -1;
				ddlTimeB.SelectedIndex = -1;
				ddlTimeA.Items.FindByValue(jogo.CodigoTimeA.ToString()).Selected = true;
				ddlTimeB.Items.FindByValue(jogo.CodigoTimeB.ToString()).Selected = true;
				tbData.Text = jogo.DataJogo.ToString("dd/MM/yyyy");
				tbHora.Text = jogo.DataJogo.ToString("HH:mm");
				tbLocal.Text = jogo.Local;
				tbNumeroJogo.Text = jogo.NumeroJogo.ToString();
				tbDataLimite.Text = jogo.DataLimite.ToString("dd/MM/yyyy");
				lbOperacao.Text = "Alterar Jogo";
			}
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

		}
		#endregion

		protected void btSalvar_Click(object sender, System.EventArgs e) {
			if (Page.IsValid) {
				int codJogo = this.CodJogo;

				DateTime datJogo = Convert.ToDateTime(tbData.Text + " " + tbHora.Text + ":00", CultureInfo.GetCultureInfo("pt-BR"));
				DateTime datLimiteAposta = Convert.ToDateTime(tbDataLimite.Text + " 23:59:59", CultureInfo.GetCultureInfo("pt-BR"));

				jogo.SalvarJogo(ref codJogo, this.CodGrupo, this.CodFase, this.CodBolao,
					Convert.ToInt32(ddlTimeA.SelectedValue), Convert.ToInt32(ddlTimeB.SelectedValue),
					Convert.ToInt32(tbNumeroJogo.Text), datJogo, tbLocal.Text, datLimiteAposta);

				if (this.Salvar != null) this.Salvar(this, e);
			}
		}

		protected void btCancelar_Click(object sender, System.EventArgs e) {
			if (this.Cancelar != null) this.Cancelar(this, e);
		}
	}
}
