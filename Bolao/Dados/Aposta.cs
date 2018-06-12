using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using W3.Library.Data;

namespace Bolao.Dados {
	public class Aposta {
		public static DataTable BuscarAposta(int codBolao, int codFase, int codGrupo, int codUsuario, string filtroJogo) {
			string condicao = "where a.cod_bolao = " + codBolao + " and ";

			if (codFase != 0) condicao += "a.cod_fase = " + codFase + " and ";
			if (codGrupo != 0) condicao += "a.cod_grupo = " + codGrupo + " and ";
			if (!String.IsNullOrEmpty(filtroJogo) && filtroJogo != "T") {
				switch (filtroJogo) {
					case "N":
						condicao += "a.ind_realizado = 'N' and ";
						break;
					case "R":
						condicao += "a.ind_realizado = 'S' and ";
						break;
				}
			}

			condicao = condicao.Substring(0, condicao.Length - 4);

			string sql =
				"select a.cod_jogo, a.cod_time_a, a.cod_time_b, a.num_jogo, a.qtd_gol_a, " +
					"a.qtd_gol_b, a.dat_jogo, a.dsc_local, a.ind_realizado, b.dsc_fase, " +
					"c.nom_grupo, d.nom_time as nom_time_a, e.nom_time as nom_time_b, " +
					"d.sgl_time as sgl_time_a, e.sgl_time as sgl_time_b, d.dsc_icone as dsc_icone_a, " +
					"e.dsc_icone as dsc_icone_b, f.qtd_gol_a as qtd_gol_aposta_a, " +
					"f.qtd_gol_b as qtd_gol_aposta_b, g.vlr_pontuacao, f.cod_criterio, " +
					"g.dsc_criterio, f.ind_apurada, a.dat_limite_aposta " +
				"from jogo a " +
					"inner join fase b on a.cod_fase = b.cod_fase " +
					"inner join grupo c on a.cod_grupo = c.cod_grupo " +
					"inner join time d on a.cod_time_a = d.cod_time " +
					"inner join time e on a.cod_time_b = e.cod_time " +
					"left join aposta f on a.cod_jogo = f.cod_jogo and f.cod_usuario = " + codUsuario + " " +
					"left join criterio g on f.cod_criterio = g.cod_criterio " +
				condicao +
				"order by a.num_jogo asc";

			return DatabaseUtil.Connector.BindSql(sql).AsDataTable();
		}

		public static DataTable BuscarApostaBolao(int codBolao, int codFase, int codGrupo, int codUsuario, string filtroJogo) {
			string condicao = "where a.cod_bolao = " + codBolao + " and ";

			if (codFase != 0) condicao += "a.cod_fase = " + codFase + " and ";
			if (codGrupo != 0) condicao += "a.cod_grupo = " + codGrupo + " and ";
			if (!String.IsNullOrEmpty(filtroJogo) && filtroJogo != "T") {
				switch (filtroJogo) {
					case "N":
						condicao += "a.ind_realizado = 'N' and ";
						break;
					case "R":
						condicao += "a.ind_realizado = 'S' and ";
						break;
				}
			}

			condicao = condicao.Substring(0, condicao.Length - 4);

			string sql =
				"select a.cod_jogo, a.cod_time_a, a.cod_time_b, a.num_jogo, a.qtd_gol_a, " +
					"a.qtd_gol_b, a.dat_jogo, a.dsc_local, a.ind_realizado, b.cod_fase, b.dsc_fase, " +
					"c.nom_grupo, d.nom_time as nom_time_a, e.nom_time as nom_time_b, " +
					"d.sgl_time as sgl_time_a, e.sgl_time as sgl_time_b, d.dsc_icone as dsc_icone_a, " +
					"e.dsc_icone as dsc_icone_b, f.qtd_gol_a as qtd_gol_aposta_a, " +
					"f.qtd_gol_b as qtd_gol_aposta_b, g.vlr_pontuacao, f.cod_criterio, " +
					"g.dsc_criterio, f.ind_apurada, a.dat_limite_aposta " +
				"from jogo a " +
					"inner join fase b on a.cod_fase = b.cod_fase " +
					"inner join grupo c on a.cod_grupo = c.cod_grupo " +
					"inner join time d on a.cod_time_a = d.cod_time " +
					"inner join time e on a.cod_time_b = e.cod_time " +
					"left join aposta f on a.cod_jogo = f.cod_jogo and f.cod_usuario = " + codUsuario + " " +
					"left join criterio g on f.cod_criterio = g.cod_criterio " +
				condicao +
				"order by a.num_jogo asc";

			return DatabaseUtil.Connector.BindSql(sql).AsDataTable();
		}

		public static DataTable BuscarResultadoPorFase(int codBolao, int codUsuario) {
			string sql =
				"select a.cod_fase, b.dsc_fase, sum(coalesce(g.vlr_pontuacao, 0)) as vlr_pontuacao " +
				"from jogo a " +
					"inner join fase b on a.cod_fase = b.cod_fase " +
					"left join aposta f on a.cod_jogo = f.cod_jogo and f.cod_usuario = " + codUsuario + " " +
					"left join criterio g on f.cod_criterio = g.cod_criterio " +
				"where a.cod_bolao = " + codBolao + " " +
				"group by a.cod_fase, b.dsc_fase " +
				"order by a.cod_fase, b.dsc_fase ";

			return DatabaseUtil.Connector.BindSql(sql).AsDataTable();
		}

		public static bool SalvarAposta(int codJogo, int codUsuario, int? qtdGolsA, int? qtdGolsB, DateTime datAposta) {
			string indApurada = "N";
			string sql = "";

			DataTable aposta = DatabaseUtil.Connector.BindSql("select count(*) from aposta where cod_usuario = ? and cod_jogo = ?").ToParam("@Usuario", codUsuario).ToParam("@Jogo", codJogo).AsDataTable();

			if (Convert.ToInt32(aposta.Rows[0][0]) == 0) {
				sql = "insert into aposta (cod_usuario, cod_jogo, qtd_gol_a, qtd_gol_b, dat_aposta, ind_apurada) values (?, ?, ?, ?, ?, ?)";

				DatabaseUtil.Connector.BindSql(sql).
					ToParam("@Usuario", codUsuario).
					ToParam("@Jogo", codJogo).
					ToParam("@GolsA", qtdGolsA).
					ToParam("@GolsB", qtdGolsB).
					ToParam("@Data", datAposta).
					ToParam("@Apurada", indApurada).
					Execute();
			}
			else {
				sql = "update aposta set qtd_gol_a = ?, qtd_gol_b = ?, dat_aposta = ? where cod_usuario = ? and cod_jogo = ?";

				DatabaseUtil.Connector.BindSql(sql).
					ToParam("@GolsA", qtdGolsA).
					ToParam("@GolsB", qtdGolsB).
					ToParam("@Data", datAposta).
					ToParam("@Usuario", codUsuario).
					ToParam("@Jogo", codJogo).
					Execute();
			}

			return (qtdGolsA.HasValue && qtdGolsB.HasValue) || (!qtdGolsA.HasValue && !qtdGolsB.HasValue);
		}

		public static void ExcluirAposta(int codJogo, int codUsuario) {
			DatabaseUtil.Connector.BindSql("delete from aposta where cod_usuario = ? and cod_jogo = ?").
				ToParam("@Usuario", codUsuario).
				ToParam("@Jogo", codJogo).
				Execute();
		}
	}
}