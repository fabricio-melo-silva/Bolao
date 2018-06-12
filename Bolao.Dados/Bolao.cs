using System;
using System.Data;
using System.Web;
using W3.Library;
using W3.Library.Data;

namespace Bolao.Dados {
	public class Bolao : BaseData {
		public int CodigoBolao { get; set; }
		public string Descricao { get; set; }
		public string Status { get; set; }
		public DateTime? DataRanking { get; set; }
		public float ValorBolao { get; set; }

		public Bolao() {
		}

		public DataTable BuscarBolao() {
			return this.Connector.BindSql(
				"select cod_bolao, dsc_bolao, ind_status, dat_ranking, vlr_bolao " +
				"from bolao " +
				"order by cod_bolao desc").
				AsDataTable();
		}

		public DataTable BuscarBolao(string indStatus) {
			return this.Connector.BindSql(
				"select cod_bolao, dsc_bolao, ind_status, dat_ranking, vlr_bolao " +
				"from bolao " +
				"where ind_status = ?").
				ToParam("@Status", indStatus).
				AsDataTable();
		}

		public DataRow BuscarBolao(int codBolao) {
			DataTable lista = this.Connector.BindSql(
				"select cod_bolao, dsc_bolao, ind_status, dat_ranking, vlr_bolao " +
				"from bolao " +
				"where cod_bolao = ?").
				ToParam("@Bolao", codBolao).
				AsDataTable();

			return (lista.Rows.Count == 0) ? null : lista.Rows[0];
		}

		public void SalvarBolao(ref int codBolao, string dscBolao, string indStatus, double vlrBolao) {
			SqlStatement sql;

			if (codBolao == 0) {
				sql = this.Connector.BindSql(
					"insert into bolao (dsc_bolao, ind_status, vlr_bolao) " +
					"values (?, ?, ?)").
					ToParam("@Descricao", dscBolao).
					ToParam("@Status", indStatus).
					ToParam("@Valor", vlrBolao);

				sql.Execute();

				codBolao = Convert.ToInt32(sql.LastIdentifier("bolao", "cod_bolao"));
			}
			else {
				this.Connector.BindSql(
					"update bolao set " +
						"dsc_bolao = ?, " +
						"ind_status = ?, " +
						"vlr_bolao = ? " +
					"where cod_bolao = ?").
					ToParam("@Descricao", dscBolao).
					ToParam("@Status", indStatus).
					ToParam("@Valor", vlrBolao).
					ToParam("@Codigo", codBolao).
					Execute();
			}
		}

		public void ExcluirBolao(int codBolao) {
			this.Connector.BindSql("delete from bolao where cod_bolao = ?").ToParam("@Codigo", codBolao).Execute();
		}

		public string DescricaoStatus(string indStatus) {
			string descricao;

			switch (indStatus.ToUpper()) {
				case "N": descricao = "Não-iniciado"; break;
				case "A": descricao = "Em andamento"; break;
				case "F": descricao = "Finalizado"; break;
				default: descricao = "-"; break;
			}

			return descricao;
		}

		public DataTable BuscarRanking(int codBolao) {
			string sql =
				"select u.cod_usuario, u.nom_usuario, p.num_ranking, p.vlr_pontuacao " +
				"from usuario u " +
					"inner join participante p on u.cod_usuario = p.cod_usuario " +
				"where p.cod_bolao = ? and " +
					"p.ind_bolao_pago = 'S' " +
				"order by p.vlr_pontuacao desc, p.num_ranking asc, u.nom_usuario asc";

			return this.Connector.BindSql(sql).
				ToParam("@Bolao", codBolao).
				AsDataTable();
		}

		public void GerarRanking(int? codBolao) {
			this.Connector.BeginTransaction();
			try {
				this.Connector.BindSql("sp_ranking", CommandType.StoredProcedure).
					ToParam("@Bolao", codBolao).
					Execute();

				this.Connector.CommitTransaction();
			}
			catch (Exception e) {
				this.Connector.RollbackTransaction();
				throw e;
			}
		}
	}
}
