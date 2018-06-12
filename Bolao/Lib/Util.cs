using System;
using System.Globalization;
using System.Web;
using System.Net;
using System.Net.Mail;
using Bolao.Dados;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using W3.Library.Data;
using System.Xml;
using System.Text;
using System.Linq;

namespace Bolao.Lib {
	public class Util {
		public Util() {
		}

		public static void InicializarSessao(Dados.Participante participante, Dados.Usuario usuario, HttpContext context) {
			if (participante != null && usuario != null && context != null) {
				context.Session.Add("CodUsuario", usuario.CodigoUsuario);
				context.Session.Add("CodBolao", participante.CodigoBolao);
				context.Session.Add("NomUsuario", usuario.NomeUsuario);
				context.Session.Add("IndPago", "S".Equals(participante.Pago));
				context.Session.Add("IndAdministrador", participante.Administrador.Equals("S", StringComparison.InvariantCultureIgnoreCase));
				context.Session.Add("UsuarioValido", true);
			}
		}

		public static void GravarLogAcesso() {
			List<string> logKeys = new List<string>(new string[] { "HTTP_USER_AGENT", "HTTP_REFERER", "QUERY_STRING", "REMOTE_ADDR" });

			StringBuilder xml = new StringBuilder();

			using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(xml)) {
				writer.WriteStartDocument();
				writer.WriteStartElement("Dados");

				foreach (string key in HttpContext.Current.Request.ServerVariables.AllKeys) {
					if (logKeys.Contains(key)) {
						writer.WriteStartElement("Item");
						writer.WriteAttributeString("Key", key);
						writer.WriteCData(HttpContext.Current.Request.ServerVariables[key]);
						writer.WriteEndElement();
					}
				}

				writer.WriteEndElement();
				writer.WriteEndDocument();
			}

			string atributo = xml.ToString().Replace("utf-16", "utf-8");

			DatabaseUtil.Connector.BindSql("insert into log_acesso (cod_usuario, dat_acesso, dsc_atributo) values (?, ?, ?) ").
				ToParam("@Usuario", Convert.ToInt32(HttpContext.Current.Session["CodUsuario"])).
				ToParam("@Data", DateTime.Now).
				ToParam("@Atributo", atributo).
				Execute();
		}

		public static void SimularLogin(HttpContext context) {
			int codUsuario = 1;
			Usuario usuario = Usuario.BuscarUsuario(codUsuario);
			List<Participante> participantes = Dados.Participante.BuscarParticipantes(codUsuario, null, "A");

			if (usuario != null && participantes.Count > 1) {
				InicializarSessao(participantes[0], usuario, context);
			}
		}

		public static void ValidarLogin(HttpContext contexto) {
			bool usuarioValido = false;

			if (contexto.Session["UsuarioValido"] != null) {
				usuarioValido = Convert.ToBoolean(contexto.Session["UsuarioValido"]);
			}

			if (!usuarioValido) {
				contexto.Response.Write("<script>window.top.document.location.href = '../Sair.aspx';</script>");
				contexto.Response.End();
			}
		}

		public static void ValidarAdministrador(HttpContext contexto) {
			bool administrador = false;

			Util.ValidarLogin(contexto);

			if (contexto.Session["IndAdministrador"] != null) {
				administrador = Convert.ToBoolean(contexto.Session["IndAdministrador"]);
			}

			if (!administrador) {
				contexto.Response.Write("<script>window.top.document.location.href = '../Sair.aspx';</script>");
				contexto.Response.End();
			}
		}

		public static string DoubleToString(double floatValue, int decimalDigits, bool showGroupSeparator) {
			NumberFormatInfo format = new NumberFormatInfo();

			if (showGroupSeparator) {
				format.NumberGroupSeparator = ".";
				format.NumberGroupSizes = new int[] { 3 };
			}

			format.NumberDecimalDigits = decimalDigits;
			format.NumberDecimalSeparator = ",";

			return floatValue.ToString("N", format);
		}

		public static string GerarSenhaRandomica() {
			string novaSenha = Guid.NewGuid().ToString();

			novaSenha = novaSenha.Substring(0, novaSenha.IndexOf("-"));

			return novaSenha;
		}
	}
}