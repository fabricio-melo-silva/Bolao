using System;
using System.Web;
using System.Web.Http;

namespace Bolao.API {
	public class ApostaController : ApiController {
		[ActionName("Post")]
		[HttpPost]
		public IHttpActionResult Post(Model.Aposta aposta) {
			int codUsuario;
			int codJogo;
			int golsA;
			int golsB;
			int? qtdGolsA = null;
			int? qtdGolsB = null;

			if (!Int32.TryParse(aposta.CodigoJogo, out codJogo)) {
				return BadRequest("Código do jogo é inválido");
			}

			if (!String.IsNullOrEmpty(aposta.QuantidadeGolA)) { 
				if (!Int32.TryParse(aposta.QuantidadeGolA, out golsA)) {
					return BadRequest("Quantidade de gols do time A é inválido");
				}
				qtdGolsA = golsA;
			}

			if (!String.IsNullOrEmpty(aposta.QuantidadeGolB)) {
				if (!Int32.TryParse(aposta.QuantidadeGolB, out golsB)) {
					return BadRequest("Quantidade de gols do time B é inválido");
				}
				qtdGolsB = golsB;
			}

			// Verificar se existe sessão
			if (HttpContext.Current == null || HttpContext.Current.Session == null) {
				return BadRequest("Logoff");
			}

			var context = HttpContext.Current;

			// Verificar se o usuário está logado
			if (!Convert.ToBoolean(context.Session["UsuarioValido"])) {
				return BadRequest("Logoff");
			}

			// Verificar se o usuário é o mesmo da sessão
			codUsuario = Convert.ToInt32(context.Session["CodUsuario"]);

			// Verificar se o jogo existe
			var jogo = Dados.Jogo.BuscarJogo(codJogo);

			if (jogo == null) {
				return BadRequest("Jogo é inválido");
			}

			// Verificar se o jogo já foi realizado
			if (jogo.Realizado) {
				return BadRequest("Jogo já foi realizado");
			}

			// Verificar se já passou a data limite
			if (jogo.DataLimite < DateTime.Now) {
				return BadRequest("Data limite atingida");
			}

			// Verificar se o usuário está ativo
			var usuario = Dados.Usuario.BuscarUsuario(codUsuario);

			if (!usuario.Ativo) {
				return BadRequest("Usuario encontra-se inativo");
			}

			// Verificar se participante pagou o bolão
			var participante = Dados.Participante.BuscarParticipante(codUsuario, jogo.CodigoBolao, "A");

			if (participante == null) {
				return BadRequest("Participante não encontrado");
			}
			else if (!"S".Equals(participante.Pago)) {
				return BadRequest("Participante não efetuou o pagamento da taxa");
			}

			// Salvar a aposta
			try {
				bool apostaValida = Dados.Aposta.SalvarAposta(codJogo, codUsuario, qtdGolsA, qtdGolsB, DateTime.Now);

				return Ok(
					new {
						ApostaValida = apostaValida
					});
			}
			catch (Exception e) {
				return InternalServerError(new Exception("Erro ao tentar salvar a aposta"));
			}
		}
	}
}