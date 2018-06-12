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
		public string Apelido { get; set; }
		public DateTime DataCadastro { get; set; }
		public DateTime DataModificacao { get; set; }

		public Usuario() {
		}

		public static List<Usuario> BuscarUsuarios() {
			string sql =
				"select cod_usuario, nom_usuario, dsc_email, dsc_apelido, dat_cadastro, dat_modificacao " +
				"from usuario ";

			using (IDataReader reader = DatabaseUtil.Connector.BindSql(sql).AsDataReader()) {
				List<Usuario> lista = new List<Usuario>();

				if (reader.Read()) {
					lista.Add(
						new Usuario {
							CodigoUsuario = reader.GetValue<int>("cod_usuario"),
							NomeUsuario = reader.GetValue<string>("nom_usuario"),
							Email = reader.GetValue<string>("dsc_email"),
							Apelido = reader.GetValue<string>("dsc_apelido"),
							DataCadastro = reader.GetValue<DateTime>("dat_cadastro"),
							DataModificacao = reader.GetValue<DateTime>("dat_modificacao")
						}
					);
				}

				return lista;
			}

			return null;
		}

		public static Usuario BuscarUsuario(int codUsuario) {
			string sql =
				"select cod_usuario, nom_usuario, dsc_email, dsc_apelido, dat_cadastro, dat_modificacao " +
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
						Apelido = reader.GetValue<string>("dsc_apelido"),
						DataCadastro = reader.GetValue<DateTime>("dat_cadastro"),
						DataModificacao = reader.GetValue<DateTime>("dat_modificacao")
					};
				}
			}

			return null;
		}

		public static Usuario BuscarUsuario(string email, string senha) {
			string sql =
				"select cod_usuario, nom_usuario, dsc_email, dsc_apelido, dat_cadastro, dat_modificacao " +
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
						Apelido = reader.GetValue<string>("dsc_apelido"),
						DataCadastro = reader.GetValue<DateTime>("dat_cadastro"),
						DataModificacao = reader.GetValue<DateTime>("dat_modificacao")
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
			else {
				List<Participante> participantes = Participante.BuscarParticipantes(usuario.CodigoUsuario, null);

				if (participantes == null || participantes.Count == 0) {
					HttpContext.Current.Session["UsuarioValido"] = false;

					mensagem = "Não existe nenhum Bolão que você esteja participando.";

					return false;
				}
				else {
					Participante participante = participantes[0];

					HttpContext.Current.Session.Add("CodUsuario", usuario.CodigoUsuario);
					HttpContext.Current.Session.Add("CodBolao", participante.CodigoBolao);
					HttpContext.Current.Session.Add("NomUsuario", usuario.NomeUsuario);
					HttpContext.Current.Session.Add("IndAdministrador", participante.Administrador.Equals("S", StringComparison.InvariantCultureIgnoreCase));
					HttpContext.Current.Session.Add("UsuarioValido", true);

					mensagem = null;

					return true;
				}
			}
		}

		public bool EmailCadastrado(string dscEmail) {
			DataTable usuario = this.Connector.BindSql(
				"select count(*) from usuario where lower(dsc_email) = ?").
				ToParam("@Email", dscEmail.ToLower()).
				AsDataTable();

			return Convert.ToInt32(usuario.Rows[0][0]) > 0;
		}

		public bool ApelidoCadastrado(string dscApelido) {
			DataTable usuario = this.Connector.BindSql(
				"select count(*) from usuario where lower(dsc_apelido) = ?").
				ToParam("@Apelido", dscApelido.ToLower()).
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

		public DataRow Buscar(string dscEmail, string dscSenha) {
			DataTable usuario = this.Connector.BindSql(
				"select cod_usuario, nom_usuario, dsc_email, dsc_senha " +
				"from usuario " +
				"where lower(dsc_email) = ? and lower(dsc_senha) = ?").
				ToParam("@Email", dscEmail.ToLower()).
				ToParam("@Senha", dscSenha.ToLower()).
				AsDataTable();

			return (usuario.Rows.Count == 0) ? null : usuario.Rows[0];
		}

		public static int Salvar(Usuario usuario) {
			if (usuario.CodigoUsuario == 0) {
				SqlStatement sql = DatabaseUtil.Connector.BindSql(
					"insert into usuario (nom_usuario, dsc_email, dsc_senha, dsc_apelido, dat_cadastro, dat_modificacao) values (?, ?, dbo.fn_criptografar_senha(?), ?, getdate(), getdate())").
					ToParam("@Nome", usuario.NomeUsuario).
					ToParam("@Email", usuario.Email).
					ToParam("@Senha", usuario.Senha).
					ToParam("@Apelido", usuario.Apelido);

				sql.Execute();

				return Convert.ToInt32(sql.LastIdentifier("usuario", "cod_usuario"));
			}
			else {
				DatabaseUtil.Connector.BindSql(
					"update usuario set nom_usuario = ?, dsc_email = ?, dsc_senha = dbo.fn_criptografar_senha(?), dsc_apelido = ?, dat_modificacao = getdate() where cod_usuario = ?").
					ToParam("@Nome", usuario.NomeUsuario).
					ToParam("@Email", usuario.Email).
					ToParam("@Senha", usuario.Senha).
					ToParam("@Apelido", usuario.Apelido).
					ToParam("@Codigo", usuario.CodigoUsuario).
					Execute();

				return usuario.CodigoUsuario;
			}
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
	}
}
