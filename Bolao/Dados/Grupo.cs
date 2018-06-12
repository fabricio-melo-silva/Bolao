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
	public class Grupo : BaseData
	{
		public Grupo()
		{
		}

		public DataTable BuscarGrupo(int codBolao, int codFase)
		{
			return this.Connector.BindSql(
				"select cod_grupo, sgl_grupo, nom_grupo " +
				"from grupo " +
				"where cod_bolao = ? and cod_fase = ?").
				ToParam("@Bolao", codBolao).
				ToParam("@Fase", codFase).
				AsDataTable();
		}

		public DataRow BuscarGrupo(int codBolao, int codFase, int codGrupo)
		{
			string sql = "select cod_grupo, sgl_grupo, nom_grupo from grupo where cod_bolao = ? and cod_fase = ? and cod_grupo = ?";

			DataTable lista = this.Connector.BindSql(sql).
				ToParam("@Bolao", codBolao).
				ToParam("@Fase", codFase).
				ToParam("@Grupo", codGrupo).
				AsDataTable();

			return (lista.Rows.Count == 0) ? null : lista.Rows[0];
		}

		public void SalvarGrupo(ref int codGrupo, int codBolao, int codFase, string nomGrupo, string sglGrupo)
		{
			if (codGrupo == 0)
			{
				SqlStatement sql = this.Connector.BindSql(
					"insert into grupo (cod_bolao, cod_fase, nom_grupo, sgl_grupo) values (?, ?, ?, ?)").
					ToParam("@Bolao", codBolao).
					ToParam("@Fase", codFase).
					ToParam("@Nome", nomGrupo).
					ToParam("@Sigla", sglGrupo);
					
				sql.Execute();

				codGrupo = Convert.ToInt32(sql.LastIdentifier("grupo", "cod_grupo"));
			}
			else
			{
				this.Connector.BindSql(
					"update grupo set " +
						"cod_bolao = ?, " +
						"cod_fase = ?, " +
						"nom_grupo = ?, " +
						"sgl_grupo = ? " +
					"where cod_grupo = ?").
					ToParam("@Bolao", codBolao).
					ToParam("@Fase", codFase).
					ToParam("@Nome", nomGrupo).
					ToParam("@Sigla", sglGrupo).
					ToParam("@Grupo", codGrupo).
					Execute();
			}
		}

		public void ExcluirGrupo(int codGrupo)
		{
			this.Connector.BindSql("delete from grupo where cod_grupo = ?").ToParam("@Grupo", codGrupo).Execute();
		}
	}
}
