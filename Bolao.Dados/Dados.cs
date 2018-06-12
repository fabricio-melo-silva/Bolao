using System;
using System.Data;
using System.Web;
using W3.Library;
using W3.Library.Data;

namespace Bolao.Dados
{
	public class Dados : BaseData
	{
		public Dados()
		{
		}

		public DataTable BuscarTime()
		{
			return this.Connector.BindSql("select cod_time, nom_time, sgl_time, dsc_icone from time order by nom_time").AsDataTable();
		}

		public DataTable BuscarJogo()
		{
			return this.Connector.BindSql(
				"select a.cod_jogo, a.dat_jogo, a.dsc_local, " + 
					"a.cod_time_a, ta.nom_time as nom_time_a, " +
					"a.cod_time_b, tb.nom_time as nom_time_b, " +
					"a.qtd_gol_a, a.qtd_gol_b, a.cod_grupo, " +
					"g.nom_grupo, a.cod_fase, f.dsc_fase " +
				"from jogo a " +
					"left join time ta on a.cod_time_a = ta.cod_time " +
					"left join time tb on a.cod_time_b = tb.cod_time " +
					"left join grupo g on a.cod_grupo = g.cod_grupo " +
					"left join fase f on a.cod_fase = f.cod_fase " +
				"order by a.cod_jogo").
				AsDataTable();
		}

//		public void EditarJogo(
//			ref int codTime, 
//			int codFase, 
//			int codGrupo,
//			int codTimeA,
//			int codTimeB,
//			int numJogo,
//			int qtdGolA,
//			int qtdGolB,
//			datetimestring sglTime, string dscIcone)
//		{
//			if (codTime == 0)
//			{
//				this.Connector.BindSql("insert into time (nom_time, sgl_time, dsc_icone
//			}
//		}
	}
}
