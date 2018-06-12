using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using W3.Library.Data;

namespace Bolao.Dados {
	public class Participante {
		public int CodigoUsuario { get; set; }
		public int CodigoBolao { get; set; }
		public int Pontuacao { get; set; }
		public int PosicaoRanking { get; set; }
		public string Administrador { get; set; }
		public string Pago { get; set; }

		public static Participante BuscarParticipante(int codigoUsuario, int codigoBolao) {
			List<Participante> lista = Participante.BuscarParticipantes(codigoUsuario, codigoBolao);

			if (lista != null && lista.Count > 0) {
				return lista[0];
			}

			return null;
		}

		public static List<Participante> BuscarParticipantes(int? codigoUsuario, int? codigoBolao) {
			string sql =
				"declare " +
					"@codUsuario int = ?, " +
					"@codBolao int = ? " +
				"select p.cod_usuario, p.cod_bolao, p.vlr_pontuacao, p.num_ranking, p.ind_administrador, p.ind_bolao_pago " +
				"from participante p " +
					"inner join bolao b on b.cod_bolao = p.cod_bolao " +
				"where (@codUsuario = -1 or p.cod_usuario = @codUsuario) " +
					"and (@codBolao = -1 or p.cod_bolao = @codBolao) " +
				"order by cod_bolao asc ";

			using (IDataReader reader = DatabaseUtil.Connector.BindSql(sql).
				ToParam("@codUsuario", (codigoUsuario.HasValue ? codigoUsuario.Value : -1)).
				ToParam("@codBolao", (codigoBolao.HasValue ? codigoBolao.Value : -1)).
				AsDataReader()) {
				List<Participante> lista = new List<Participante>();

				while (reader.Read()) {
					lista.Add(
						new Participante {
							CodigoUsuario = reader.GetValue<int>("cod_usuario"),
							CodigoBolao = reader.GetValue<int>("cod_bolao"),
							Pontuacao = reader.GetValue<int>("vlr_pontuacao"),
							PosicaoRanking = reader.GetValue<int>("num_ranking"),
							Administrador = reader.GetValue<string>("ind_administrador"),
							Pago = reader.GetValue<string>("ind_bolao_pago")
						});
				}

				return lista;
			}

			return null;
		}

		//public static void SalvarParticipante(Participante participante) {
		//	string indAdministrador = administrador ? "S" : "N";
		//	string indBolaoPago = bolaoPago ? "S" : "N";

		//	HttpContext.Current.BindSql("update participante set ind_administrador = ?, ind_bolao_pago = ? where cod_usuario = ? and cod_bolao = ?").
		//		ToParam("@Administrador", indAdministrador).
		//		ToParam("@BolaoPago", indBolaoPago).
		//		ToParam("@Usuario", codUsuario).
		//		ToParam("@Bolao", codBolao).
		//		Execute();
		//}

		//public DataRow BuscarParticipante(int codBolao, int codUsuario) {
		//	DataTable lista = this.Connector.BindSql(
		//		"select u.cod_usuario, u.nom_usuario, u.dsc_email, p.ind_administrador, p.ind_bolao_pago " +
		//		"from participante p inner join usuario u on p.cod_usuario = u.cod_usuario " +
		//		"where p.cod_bolao = ? and u.cod_usuario = ? " +
		//		"order by u.nom_usuario asc").
		//		ToParam("@Bolao", codBolao).
		//		ToParam("@Usuario", codUsuario).
		//		AsDataTable();

		//	return (lista.Rows.Count == 0) ? null : lista.Rows[0];
		//}

		public static void ExcluirParticipante(int codUsuario, int codBolao) {
			DatabaseUtil.Connector.BindSql("delete from participante where cod_usuario = ? and cod_bolao = ?").
				ToParam("@Usuario", codUsuario).
				ToParam("@Bolao", codBolao).
				Execute();
		}

		public static void SalvarParticipante(Participante participante) {
			try {
				DatabaseUtil.Connector.BeginTransaction();

				int qtdRegistro = Convert.ToInt32(DatabaseUtil.Connector.BindSql("select count(*) from participante where cod_usuario = ? and cod_bolao = ?").
					ToParam("@Usuario", participante.CodigoUsuario).
					ToParam("@Bolao", participante.CodigoBolao).
					AsValue());

				if (qtdRegistro == 0) {
					DatabaseUtil.Connector.BindSql(
						"insert into participante " +
							"(cod_usuario, cod_bolao, vlr_pontuacao, num_ranking, ind_administrador, ind_bolao_pago, dat_cadastro, dat_modificacao) " +
						"values (?, ?, ?, ?, ?, ?, getdate(), getdate())").
						ToParam("@Usuario", participante.CodigoUsuario).
						ToParam("@Bolao", participante.CodigoBolao).
						ToParam("@Pontuacao", participante.Pontuacao).
						ToParam("@Ranking", participante.PosicaoRanking).
						ToParam("@Administrador", participante.Administrador).
						ToParam("@Pago", participante.Pago).
						Execute();
				}
				else {
					DatabaseUtil.Connector.BindSql(
						"update participante set " +
							"vlr_pontuacao = ?, num_ranking = ?, ind_administrador = ?, ind_bolao_pago = ?, dat_modificacao = getdate() " +
						"where cod_usuario = ? and cod_bolao = ?").
						ToParam("@Pontuacao", participante.Pontuacao).
						ToParam("@Ranking", participante.PosicaoRanking).
						ToParam("@Administrador", participante.Administrador).
						ToParam("@Pago", participante.Pago).
						ToParam("@Usuario", participante.CodigoUsuario).
						ToParam("@Bolao", participante.CodigoBolao).
						Execute();

				}

				DatabaseUtil.Connector.CommitTransaction();
			}
			catch (Exception erro) {
				DatabaseUtil.Connector.RollbackTransaction();
				throw erro;
			}
		}

	}
}