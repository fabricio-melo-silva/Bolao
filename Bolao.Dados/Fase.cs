using System;
using System.Data;
using System.Web;
using W3.Library;
using W3.Library.Data;

namespace Bolao.Dados {
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Fase : BaseData {
		public Fase() {
		}

		public DataTable BuscarFase(int codBolao) {
			string sql = "select cod_fase, dsc_fase, ind_tipo_fase, ind_status from fase where cod_bolao = ?";

			return this.Connector.BindSql(sql).ToParam("@Bolao", codBolao).AsDataTable();
		}

		public DataRow BuscarFase(int codBolao, int codFase) {
			string sql = "select cod_fase, dsc_fase, ind_tipo_fase, ind_status from fase where cod_bolao = ? and cod_fase = ?";

			DataTable lista = this.Connector.BindSql(sql).ToParam("@Bolao", codBolao).ToParam("@Fase", codFase).AsDataTable();

			return (lista.Rows.Count == 0) ? null : lista.Rows[0];
		}

		public void SalvarFase(ref int codFase, int codBolao, string dscFase, string indTipoFase, string indStatus) {
			if (codFase == 0) {
				SqlStatement sql = this.Connector.BindSql("insert into fase (cod_bolao, dsc_fase, ind_tipo_fase, ind_status) values (?, ?, ?, ?)").
					ToParam("@Bolao", codBolao).
					ToParam("@Descricao", dscFase).
					ToParam("@Tipo", indTipoFase).
					ToParam("@Status", indStatus);

				sql.Execute();

				codFase = Convert.ToInt32(sql.LastIdentifier("fase", "cod_fase"));
			}
			else {
				this.Connector.BindSql(
					"update fase set " +
						"cod_bolao = ?, " +
						"dsc_fase = ?, " +
						"ind_tipo_fase = ?, " +
						"ind_status = ? " +
					"where cod_fase = ?").
					ToParam("@Bolao", codBolao).
					ToParam("@Descricao", dscFase).
					ToParam("@Tipo", indTipoFase).
					ToParam("@Status", indStatus).
					ToParam("@Fase", codFase).
					Execute();
			}
		}

		public void ExcluirFase(int codFase) {
			this.Connector.BindSql("delete from fase where cod_fase = ?").ToParam("@Fase", codFase).Execute();
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

		public string DescricaoTipo(string indTipoFase) {
			string descricao;

			switch (indTipoFase.ToUpper()) {
				case "G": descricao = "Grupos"; break;
				case "E": descricao = "Eliminatória"; break;
				default: descricao = "-"; break;
			}

			return descricao;
		}
	}
}
