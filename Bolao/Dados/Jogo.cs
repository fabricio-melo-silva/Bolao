using System;
using System.Data;
using System.Web;
using W3.Library;
using W3.Library.Data;

namespace Bolao.Dados {
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Jogo : BaseData {
		public int CodigoJogo { get; set; }
		public int CodigoFase { get; set; }
		public int CodigoBolao { get; set; }
		public int CodigoGrupo { get; set; }
		public int CodigoTimeA { get; set; }
		public int CodigoTimeB { get; set; }
		public int NumeroJogo { get; set; }
		public int? QuantidadeGolsA { get; set; }
		public int? QuantidadeGolsB { get; set; }
		public DateTime DataJogo { get; set; }
		public string Local { get; set; }
		public string Estadio { get; set; }
		public bool Realizado { get; set; }
		public DateTime DataLimite { get; set; }

		public Jogo() {
		}

		public DataTable BuscarJogo(int codBolao, int codFase, int codGrupo) {
			string condicao = "where a.cod_bolao = " + codBolao + " and ";

			if (codFase != 0) condicao += "a.cod_fase = " + codFase + " and ";
			if (codGrupo != 0) condicao += "a.cod_grupo = " + codGrupo + " and ";

			condicao = condicao.Substring(0, condicao.Length - 4);

			string sql =
				"select a.cod_jogo, a.cod_time_a, a.cod_time_b, a.num_jogo, a.qtd_gol_a, " +
					"a.qtd_gol_b, a.dat_jogo, a.dsc_local, a.ind_realizado, b.dsc_fase, " +
					"c.nom_grupo, d.nom_time as nom_time_a, e.nom_time as nom_time_b, " +
					"d.sgl_time as sgl_time_a, e.sgl_time as sgl_time_b, d.dsc_icone as dsc_icone_a, " +
					"e.dsc_icone as dsc_icone_b, a.dat_limite_aposta " +
				"from jogo a " +
					"inner join fase b on a.cod_fase = b.cod_fase " +
					"inner join grupo c on a.cod_grupo = c.cod_grupo " +
					"inner join time d on a.cod_time_a = d.cod_time " +
					"inner join time e on a.cod_time_b = e.cod_time " +
				condicao +
				"order by a.num_jogo asc";

			return this.Connector.BindSql(sql).AsDataTable();
		}

		public static Jogo BuscarJogo(int codJogo) {
			string sql =
				"select a.cod_fase, a.cod_bolao, a.cod_grupo, a.cod_time_a, a.cod_time_b, " +
					"a.num_jogo, a.qtd_gol_a, a.qtd_gol_b, a.dat_jogo, a.dsc_local, " + 
					"a.dsc_estadio, a.ind_realizado, a.dat_limite_aposta " +
				"from jogo a " +
				"where a.cod_jogo = ?";

			using (IDataReader reader = DatabaseUtil.Connector.BindSql(sql).ToParam("@Jogo", codJogo).AsDataReader()) {
				if (reader.Read()) {
					return new Jogo {
						CodigoJogo = codJogo,
						CodigoFase = reader.GetValue<int>("cod_fase"),
						CodigoBolao = reader.GetValue<int>("cod_bolao"),
						CodigoGrupo = reader.GetValue<int>("cod_grupo"),
						CodigoTimeA = reader.GetValue<int>("cod_time_a"),
						CodigoTimeB = reader.GetValue<int>("cod_time_b"),
						NumeroJogo = reader.GetValue<int>("num_jogo"),
						QuantidadeGolsA = reader.GetValue<int?>("qtd_gol_a"),
						QuantidadeGolsB = reader.GetValue<int?>("qtd_gol_b"),
						DataJogo = reader.GetValue<DateTime>("dat_jogo"),
						DataLimite = reader.GetValue<DateTime>("dat_limite_aposta"),
						Local = reader.GetValue<string>("dsc_local"),
						Estadio = reader.GetValue<string>("dsc_estadio"),
						Realizado = "S".Equals(reader.GetValue<string>("ind_realizado"), StringComparison.InvariantCultureIgnoreCase)
					};
				}
			}

			return null;
		}

		public DataRow BuscarPlacarJogo(int numJogo) {
			DataTable lista = this.Connector.BindSql(
				"select a.num_jogo, a.qtd_gol_a, a.qtd_gol_b, a.dat_jogo, " +
					"b.nom_time as nom_time_a, b.sgl_time as sgl_time_a, " +
					"c.nom_time as nom_time_b, c.sgl_time as sgl_time_b " +
				"from jogo a " +
					"left join time b on a.cod_time_a = b.cod_time " +
					"left join time c on a.cod_time_b = c.cod_time " +
				"where a.num_jogo = ?").
				ToParam("@Jogo", numJogo).
				AsDataTable();

			return (lista.Rows.Count == 0) ? null : lista.Rows[0];
		}

		public void SalvarJogo(ref int codJogo, int codGrupo, int codFase, int codBolao, int codTimeA, int codTimeB,
			int numJogo, DateTime datJogo, string dscLocal, DateTime datLimiteAposta) {
			if (codJogo == 0) {
				SqlStatement sql = this.Connector.BindSql(
					"insert into jogo " +
						"(cod_grupo, cod_fase, cod_bolao, cod_time_a, cod_time_b, " +
						"num_jogo, dat_jogo, dsc_local, dat_limite_aposta, ind_realizado) " +
					"values " +
						"(?, ?, ?, ?, ?, ?, ?, ?, ?, ?)").
					ToParam("@Grupo", codGrupo).
					ToParam("@Fase", codFase).
					ToParam("@Bolao", codBolao).
					ToParam("@TimeA", codTimeA).
					ToParam("@TimeB", codTimeB).
					ToParam("@Numero", numJogo).
					ToParam("@Data", datJogo).
					ToParam("@Local", dscLocal).
					ToParam("@Limite", datLimiteAposta).
					ToParam("@Realizado", "N");

				sql.Execute();

				codJogo = Convert.ToInt32(sql.LastIdentifier("jogo", "cod_jogo"));
			}
			else {
				this.Connector.BindSql(
					"update jogo set " +
						"cod_time_a = ?, " +
						"cod_time_b = ?, " +
						"num_jogo = ?, " +
						"dat_jogo = ?, " +
						"dsc_local = ?, " +
						"dat_limite_aposta = ? " +
					"where cod_jogo = ?").
					ToParam("@TimeA", codTimeA).
					ToParam("@TimeB", codTimeB).
					ToParam("@Numero", numJogo).
					ToParam("@Data", datJogo).
					ToParam("@Local", dscLocal).
					ToParam("@Limite", datLimiteAposta).
					ToParam("@Codigo", codJogo).
					Execute();
			}
		}

		public void ExcluirJogo(int codJogo) {
			this.Connector.BindSql("delete from jogo where cod_jogo = ?").
				ToParam("@Jogo", codJogo).
				Execute();
		}

		public void SalvarResultadoJogo(int codJogo, int qtdGolsA, int qtdGolsB, bool indRealizado) {
			string realizado = (indRealizado) ? "S" : "N";
			string sql = "";

			sql = "update jogo set qtd_gol_a = ?, qtd_gol_b = ?, ind_realizado = ? where cod_jogo = ?";

			this.Connector.BindSql(sql).
				ToParam("@GolsA", qtdGolsA).
				ToParam("@GolsB", qtdGolsB).
				ToParam("@Realizado", realizado).
				ToParam("@Jogo", codJogo).
				Execute();
		}
	}
}
