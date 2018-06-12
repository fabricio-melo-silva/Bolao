using System;
using System.Data;
using System.Web;
using W3.Library;
using W3.Library.Data;

namespace Bolao.Dados
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Jogo : BaseData
	{
		public Jogo()
		{
		}

		public DataTable BuscarJogo(int codBolao, int codFase, int codGrupo)
		{
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

		public DataRow BuscarJogo(int codJogo)
		{
			DataTable lista = this.Connector.BindSql(
				"select a.cod_time_a, a.cod_time_b, a.num_jogo, a.qtd_gol_a, " +
					"a.qtd_gol_b, a.dat_jogo, a.dsc_local, a.dat_limite_aposta " +
				"from jogo a " +
				"where a.cod_jogo = ?").
				ToParam("@Jogo", codJogo).
				AsDataTable();

			return (lista.Rows.Count == 0) ? null : lista.Rows[0];
		}

		public DataRow BuscarPlacarJogo(int numJogo)
		{
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

		public DataTable BuscarAposta(int codBolao, int codFase, int codGrupo, int codUsuario, string filtroJogo)
		{
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

			return this.Connector.BindSql(sql).AsDataTable();
		}

		public DataTable BuscarResultadoPorFase(int codBolao, int codUsuario)
		{
			string sql =
				"select a.cod_fase, b.dsc_fase, sum(coalesce(g.vlr_pontuacao, 0)) as vlr_pontuacao " +
				"from jogo a " +
					"inner join fase b on a.cod_fase = b.cod_fase " +
					"left join aposta f on a.cod_jogo = f.cod_jogo and f.cod_usuario = " + codUsuario + " " +
					"left join criterio g on f.cod_criterio = g.cod_criterio " +
				"where a.cod_bolao = " + codBolao + " " +
				"group by a.cod_fase, b.dsc_fase " +
				"order by a.cod_fase, b.dsc_fase ";

			return this.Connector.BindSql(sql).AsDataTable();
		}

		public void SalvarJogo(ref int codJogo, int codGrupo, int codFase, int codBolao, int codTimeA, int codTimeB,
			int numJogo, DateTime datJogo, string dscLocal, DateTime datLimiteAposta)
		{
			if (codJogo == 0)
			{
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
			else
			{
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

		public void ExcluirJogo(int codJogo)
		{
			this.Connector.BindSql("delete from jogo where cod_jogo = ?").
				ToParam("@Jogo", codJogo).
				Execute();
		}

		public void SalvarResultadoJogo(int codJogo, int qtdGolsA, int qtdGolsB, bool indRealizado)
		{
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

		public void SalvarAposta(int codJogo, int codUsuario, int qtdGolsA, int qtdGolsB, DateTime datAposta)
		{
			string indApurada = "N";
			string sql = "";

			DataTable aposta = this.Connector.BindSql("select count(*) from aposta where cod_usuario = ? and cod_jogo = ?").ToParam("@Usuario", codUsuario).ToParam("@Jogo", codJogo).AsDataTable();

			if (Convert.ToInt32(aposta.Rows[0][0]) == 0)
			{
				sql = "insert into aposta (cod_usuario, cod_jogo, qtd_gol_a, qtd_gol_b, dat_aposta, ind_apurada) values (?, ?, ?, ?, ?, ?)";

				this.Connector.BindSql(sql).
					ToParam("@Usuario", codUsuario).
					ToParam("@Jogo", codJogo).
					ToParam("@GolsA", qtdGolsA).
					ToParam("@GolsB", qtdGolsB).
					ToParam("@Data", datAposta).
					ToParam("@Apurada", indApurada).
					Execute();
			}
			else
			{
				sql = "update aposta set qtd_gol_a = ?, qtd_gol_b = ?, dat_aposta = ? where cod_usuario = ? and cod_jogo = ?";

				this.Connector.BindSql(sql).
					ToParam("@GolsA", qtdGolsA).
					ToParam("@GolsB", qtdGolsB).
					ToParam("@Data", datAposta).
					ToParam("@Usuario", codUsuario).
					ToParam("@Jogo", codJogo).
					Execute();
			}
		}

		public void ExcluirAposta(int codJogo, int codUsuario)
		{
			this.Connector.BindSql("delete from aposta where cod_usuario = ? and cod_jogo = ?").
				ToParam("@Usuario", codUsuario).
				ToParam("@Jogo", codJogo).
				Execute();
		}
	}
}
