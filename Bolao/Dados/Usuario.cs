using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using W3.Library;
using W3.Library.Data;

namespace Bolao.Dados {
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Usuario : BaseData {
		public int CodigoUsuario { get; set; }
		public string NomeUsuario { get; set; }
		public string Email { get; set; }
		public string Senha { get; set; }
		public DateTime DataCadastro { get; set; }
		public DateTime DataModificacao { get; set; }
		public bool Ativo { get; set; }

		public Usuario() {
		}

		public static List<Usuario> BuscarUsuarios() {
			string sql =
				"select cod_usuario, nom_usuario, dsc_email, dat_cadastro, dat_modificacao, ind_ativo " +
				"from usuario " +
				"order by nom_usuario ";

			using (IDataReader reader = DatabaseUtil.Connector.BindSql(sql).AsDataReader()) {
				List<Usuario> lista = new List<Usuario>();

				while (reader.Read()) {
					lista.Add(
						new Usuario {
							CodigoUsuario = reader.GetValue<int>("cod_usuario"),
							NomeUsuario = reader.GetValue<string>("nom_usuario"),
							Email = reader.GetValue<string>("dsc_email"),
							DataCadastro = reader.GetValue<DateTime>("dat_cadastro"),
							DataModificacao = reader.GetValue<DateTime>("dat_modificacao"),
							Ativo = "S".Equals(reader.GetValue<string>("ind_ativo"), StringComparison.InvariantCultureIgnoreCase)
						}
					);
				}

				return lista;
			}
		}

		public static Usuario BuscarUsuario(int codUsuario) {
			string sql =
				"select cod_usuario, nom_usuario, dsc_email, dat_cadastro, dat_modificacao, ind_ativo " +
				"from usuario " +
				"where cod_usuario = ? ";

			using (IDataReader reader = DatabaseUtil.Connector.BindSql(sql).
				ToParam("@Codigo", codUsuario).
				AsDataReader()) {

				if (reader.Read()) {
					return new Usuario {
						CodigoUsuario = reader.GetValue<int>("cod_usuario"),
						NomeUsuario = reader.GetValue<string>("nom_usuario"),
						Email = reader.GetValue<string>("dsc_email"),
						DataCadastro = reader.GetValue<DateTime>("dat_cadastro"),
						DataModificacao = reader.GetValue<DateTime>("dat_modificacao"),
						Ativo = "S".Equals(reader.GetValue<string>("ind_ativo"), StringComparison.InvariantCultureIgnoreCase)
					};
				}
			}

			return null;
		}

		public static Usuario BuscarUsuario(string email, string senha) {
			string sql =
				"select cod_usuario, nom_usuario, dsc_email, dat_cadastro, dat_modificacao, ind_ativo " +
				"from usuario " +
				"where lower(dsc_email) = ? " +
					"and dsc_senha = dbo.fn_criptografar_senha(?)";

			using (IDataReader reader = DatabaseUtil.Connector.BindSql(sql).
				ToParam("@Email", email).
				ToParam("@Senha", senha).
				AsDataReader()) {

				if (reader.Read()) {
					return new Usuario {
						CodigoUsuario = reader.GetValue<int>("cod_usuario"),
						NomeUsuario = reader.GetValue<string>("nom_usuario"),
						Email = reader.GetValue<string>("dsc_email"),
						DataCadastro = reader.GetValue<DateTime>("dat_cadastro"),
						DataModificacao = reader.GetValue<DateTime>("dat_modificacao"),
						Ativo = "S".Equals(reader.GetValue<string>("ind_ativo"), StringComparison.InvariantCultureIgnoreCase)
					};
				}
			}

			return null;
		}

		public static bool EfetuarLogin(string dscEmail, string dscSenha, ref string mensagem) {
			Usuario usuario = Usuario.BuscarUsuario(dscEmail, dscSenha);

			if (usuario == null) {
				HttpContext.Current.Session["UsuarioValido"] = false;

				mensagem = "O e-mail ou a senha estão inválidos";

				return false;
			}
			else if (!usuario.Ativo) {
				HttpContext.Current.Session["UsuarioValido"] = false;

				mensagem = "O usuário encontra-se inativo";

				return false;
			}
			else {
				List<Participante> participantes = Participante.BuscarParticipantes(usuario.CodigoUsuario, null, "A");

				if (participantes == null || participantes.Count == 0) {
					HttpContext.Current.Session["UsuarioValido"] = false;

					mensagem = "Não existe nenhum Bolão que você esteja participando.";

					return false;
				}
				else {
					Participante participante = participantes[0];

					Lib.Util.InicializarSessao(participante, usuario, HttpContext.Current);
					Lib.Util.GravarLogAcesso();

					mensagem = null;

					return true;
				}
			}
		}

		public static bool EmailCadastrado(string dscEmail) {
			DataTable usuario = DatabaseUtil.Connector.BindSql(
				"select count(*) from usuario where lower(dsc_email) = ? and ind_ativo = 'S'").
				ToParam("@Email", dscEmail.Trim().ToLower()).
				AsDataTable();

			return Convert.ToInt32(usuario.Rows[0][0]) > 0;
		}

		public bool SenhaCorreta(string dscEmail, string dscSenha) {
			DataTable usuario = this.Connector.BindSql(
				"select count(*) from usuario where lower(dsc_email) = ? and lower(dsc_senha) = ?").
				ToParam("@Email", dscEmail.ToLower()).
				ToParam("@Senha", dscSenha.ToLower()).
				AsDataTable();

			return Convert.ToInt32(usuario.Rows[0][0]) > 0;
		}

		public static void AlterarSenha(string dscEmail, string novaSenha) {
			DatabaseUtil.Connector.BindSql(
				"update usuario set dsc_senha = dbo.fn_criptografar_senha(?) where dsc_email = ?").
				ToParam("@Senha", novaSenha).
				ToParam("@Email", dscEmail.Trim().ToLower()).
				Execute();
		}

		public DataRow Buscar(string dscEmail, string dscSenha) {
			DataTable usuario = this.Connector.BindSql(
				"select cod_usuario, nom_usuario, dsc_email, dsc_senha, ind_ativo " +
				"from usuario " +
				"where lower(dsc_email) = ? and lower(dsc_senha) = ?").
				ToParam("@Email", dscEmail.ToLower()).
				ToParam("@Senha", dscSenha.ToLower()).
				AsDataTable();

			return (usuario.Rows.Count == 0) ? null : usuario.Rows[0];
		}

		public static int Salvar(Usuario usuario, bool salvarSenha) {
			int codUsuario;

			if (usuario.CodigoUsuario == 0) {
				SqlStatement sql = DatabaseUtil.Connector.BindSql(
					"insert into usuario (nom_usuario, dsc_email, dat_cadastro, dat_modificacao, ind_ativo) values (?, ?, getdate(), getdate(), ?)").
					ToParam("@Nome", usuario.NomeUsuario).
					ToParam("@Email", usuario.Email).
					ToParam("@Ativo", (usuario.Ativo ? "S" : "N"));

				sql.Execute();

				codUsuario = Convert.ToInt32(sql.LastIdentifier("usuario", "cod_usuario"));
			}
			else {
				DatabaseUtil.Connector.BindSql(
					"update usuario set nom_usuario = ?, dsc_email = ?, dat_modificacao = getdate(), ind_ativo = ? where cod_usuario = ?").
					ToParam("@Nome", usuario.NomeUsuario).
					ToParam("@Email", usuario.Email).
					ToParam("@Ativo", (usuario.Ativo ? "S" : "N")).
					ToParam("@Codigo", usuario.CodigoUsuario).
					Execute();

				codUsuario = usuario.CodigoUsuario;
			}

			if (salvarSenha) {
				DatabaseUtil.Connector.BindSql(
					"update usuario set dsc_senha = dbo.fn_criptografar_senha(?) where cod_usuario = ?").
					ToParam("@Senha", usuario.Senha).
					ToParam("@Codigo", codUsuario).
					Execute();
			}

			return codUsuario;
		}

		public DataTable BuscarBolao(int codUsuario) {
			return this.Connector.BindSql(
				"select b.cod_bolao, b.dsc_bolao, b.ind_status, b.dat_ranking, b.vlr_bolao " +
				"from bolao b " +
					"inner join participante p on b.cod_bolao = p.cod_bolao " +
				"where b.ind_status in ('A') and " +
					"p.cod_usuario = ?").
				ToParam("@Usuario", codUsuario).
				AsDataTable();
		}

		public static void AtivarInativar(int codUsuario) {
			DatabaseUtil.Connector.BindSql("update usuario set ind_ativo = case when ind_ativo = 'S' then 'N' else 'S' end where cod_usuario = ?").
				ToParam("@codUsuario", codUsuario).
				Execute();
		}
	}
}
