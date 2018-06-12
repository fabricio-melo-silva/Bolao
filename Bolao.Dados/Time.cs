using System;
using System.Data;
using System.Web;
using W3.Library;
using W3.Library.Data;

namespace Bolao.Dados
{
	public class Time : BaseData
	{
		public Time()
		{
		}

		public DataTable BuscarTime()
		{
			return this.Connector.BindSql(
				"select cod_time, nom_time, sgl_time, dsc_icone " +
				"from time").
				AsDataTable();
		}

		public DataRow BuscarTime(int codTime)
		{
			DataTable lista = this.Connector.BindSql(
				"select cod_time, nom_time, sgl_time, dsc_icone " +
				"from time " +
				"where cod_time = ?").
				ToParam("@Time", codTime).
				AsDataTable();

			return (lista.Rows.Count == 0) ? null : lista.Rows[0];
		}

		public DataTable BuscarTime(string nomTime)
		{
			return this.Connector.BindSql(
				"select cod_time, nom_time, sgl_time, dsc_icone " +
				"from time " +
				"where nom_time like '%" + nomTime.Replace("'", "''") + "%'").
				AsDataTable();
		}

		public DataTable BuscarTimeGrupo(int codGrupo)
		{
			return this.Connector.BindSql(
				"select a.cod_time, a.nom_time, a.sgl_time, a.dsc_icone, " +
					"b.qtd_vitoria, b.qtd_empate, b.qtd_derrota, " +
					"b.qtd_ponto, b.qtd_gol_feito, b.qtd_gol_sofrido, " +
					"(b.qtd_gol_feito - b.qtd_gol_sofrido) as qtd_saldo_gol, b.ind_classificado " +
				"from time a " +
					"inner join time_grupo b on a.cod_time = b.cod_time " +
				"where b.cod_grupo = ? " + 
				"order by b.qtd_ponto desc, b.qtd_vitoria desc, (b.qtd_gol_feito - b.qtd_gol_sofrido) desc, b.qtd_gol_feito desc, a.nom_time").
				ToParam("@Grupo", codGrupo).
				AsDataTable();
		}

		public void SalvarTime(ref int codTime, string nomTime, string sglTime, string dscIcone)
		{
			SqlStatement sql;

			if (codTime == 0)
			{
				sql = this.Connector.BindSql(
					"insert into time (nom_time, sgl_time, dsc_icone) " +
					"values (?, ?, ?)").
					ToParam("@Nome", nomTime).
					ToParam("@Sigla", sglTime).
					ToParam("@Icone", dscIcone);

				sql.Execute();

				codTime = Convert.ToInt32(sql.LastIdentifier("time", "cod_time"));
			}
			else
			{
				this.Connector.BindSql(
					"update time set " +
						"nom_time = ?, " +
						"sgl_time = ?, " +
						"dsc_icone = ? " +
					"where cod_time = ?").
					ToParam("@Nome", nomTime).
					ToParam("@Sigla", sglTime).
					ToParam("@Icone", dscIcone).
					ToParam("@Codigo", codTime).
					Execute();
			}
		}

		public void AssociarTime(int codTime, int codGrupo)
		{
			this.Connector.BeginTransaction();

			try
			{
				this.DesassociarTime(codTime, codGrupo);

				this.Connector.BindSql(
					"insert into time_grupo " +
					"(cod_time, cod_grupo, qtd_vitoria, qtd_empate, qtd_derrota, qtd_ponto, qtd_gol_feito, qtd_gol_sofrido, ind_classificado) " +
					"values (?, ?, ?, ?, ?, ?, ?, ?, ?)").
					ToParam("@Time", codTime).
					ToParam("@Grupo", codGrupo).
					ToParam("@Vitorias", 0).
					ToParam("@Empates", 0).
					ToParam("@Derrotas", 0).
					ToParam("@Pontos", 0).
					ToParam("@GolsFeitos", 0).
					ToParam("@GolsSofridos", 0).
					ToParam("@Classificado", "N").
					Execute();

				this.Connector.CommitTransaction();
			}
			catch (Exception e)
			{
				this.Connector.RollbackTransaction();
				throw e;
			}
		}

		public void DesassociarTime(int codTime, int codGrupo)
		{
			this.Connector.BindSql(
				"delete from time_grupo " +
				"where cod_time = ? and cod_grupo = ?").
				ToParam("@Time", codTime).
				ToParam("@Grupo", codGrupo).
				Execute();
		}

		public void ExcluirTime(int codTime)
		{
			this.Connector.BindSql("delete from time where cod_time = ?").ToParam("@Codigo", codTime).Execute();
		}
	}
}
