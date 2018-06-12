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
	public class Ranking : BaseData
	{
		public Ranking()
		{
		}

		public DataTable BuscarJogo(int codBolao, int codFase, int codGrupo)
		{
			string condicao = "where a.cod_bolao = " + codBolao + " and ";

			if (codFase != 0) condicao += "cod_fase = " + codFase + " and ";
			if (codGrupo != 0) condicao += "cod_grupo = " + codGrupo + " and ";

			condicao = condicao.Substring(0, condicao.Length - 4);

			string sql =
				"select a.cod_jogo, a.cod_time_a, a.cod_time_b, " +
					"a.num_jogo, a.qtd_gol_a, a.qtd_gol_b, a.dat_jogo, a.dsc_local, a.ind_realizado, " +
					"b.dsc_fase, c.nom_grupo, d.nom_time as nom_time_a, e.nom_time as nom_time_b, " +
					"d.sgl_time as sgl_time_a, e.sgl_time as sgl_time_b, d.dsc_icone as dsc_icone_a, " +
					"e.dsc_icone as dsc_icone_b, f.qtd_gol_a, f.qtd_gol_b " +
				"from jogo a " +
					"inner join fase b on a.cod_fase = b.cod_fase " +
					"inner join grupo c on a.cod_grupo = c.cod_grupo " +
					"inner join time d on a.cod_time_a = d.cod_time " +
					"inner join time e on a.cod_time_b = e.cod_time " +
					"left join aposta f on a.cod_jogo = f.cod_jogo " + condicao;

			return this.Connector.BindSql(sql).AsDataTable();
		}
	}
}
