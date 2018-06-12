using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bolao.Aposta {
	public partial class Cabecalho : System.Web.UI.UserControl {
		private int CodUsuario {
			get {
				return Convert.ToInt32(Session["CodUsuario"]);
			}
		}

		private int CodBolao {
			get {
				return Convert.ToInt32(Session["CodBolao"]);
			}
		}

		protected void Page_Load(object sender, EventArgs e) {

		}

		public void Inicializar() {
			var usuario = Dados.Usuario.BuscarUsuario(CodUsuario);
			var participante = Dados.Participante.BuscarParticipante(CodUsuario, CodBolao, "A");

			lbNomeUsuario.Text = usuario.NomeUsuario;

			if (participante.PosicaoRanking == 0) {
				lbPontuacao.Text = "0 pontos";
				lbRankingUsuario.Text = "-";
			}
			else {
				if (participante.Pontuacao == 1) {
					lbPontuacao.Text = "1 ponto";
				}
				else {
					lbPontuacao.Text = participante.Pontuacao.ToString() + " pontos";
				}

				lbRankingUsuario.Text = participante.PosicaoRanking.ToString() + "º lugar";
			}
		}
	}
}