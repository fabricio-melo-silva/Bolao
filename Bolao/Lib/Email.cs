using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Bolao.Lib {
	public class Email {
		public const string TAG_NOME = "<NOME>";
		public const string TAG_BOLAO = "<BOLAO>";

		public enum MailBodyType {
			Welcome,
			ChangePassword,
			RankingUpdated
		};

		private static string GetProperty(string propertyKey) {
			return ConfigurationManager.AppSettings[propertyKey];
		}

		private static string MailFromName {
			get {
				return GetProperty("MailFromName");
			}
		}

		private static string MailFromAddress {
			get {
				return GetProperty("MailFromAddress");
			}
		}

		private static string MailFromPassword {
			get {
				return GetProperty("MailFromPassword");
			}
		}

		private static string Smtp {
			get {
				return GetProperty("Smtp");
			}
		}

		private static int SmtpPort {
			get {
				return Convert.ToInt32(GetProperty("SmtpPort"));
			}
		}

		private static bool SmtpTLS {
			get {
				return Convert.ToBoolean(GetProperty("SmtpTLS"));
			}
		}

		public static string GetMailSubject(MailBodyType type, params string[] optionalParameters) {
			string body = String.Empty;

			switch (type) {
				case MailBodyType.Welcome:
					body = "Bolão da Copa 2018 - Bem-vindo!";
					break;
				case MailBodyType.ChangePassword:
					body = "Bolão da Copa 2018 - Nova senha";
					break;
			}

			return body;
		}

		public static string GetMailBody(MailBodyType type, params string[] optionalParameters) {
			string body = String.Empty;

			switch (type) {
				case MailBodyType.Welcome:
					body = "<h2>Olá!</h2><p>Seja bem vindo ao bolão!</p>";
					break;
				case MailBodyType.ChangePassword:
					body = String.Format("<h2>Olá!</h2><p>Sua nova senha para acessar o bolão é: {0}<p><p>Acesse: <a href=\"{1}\">{1}</a>", optionalParameters[0], GetProperty("Url"));
					break;
			}

			return body;
		}

		public static bool MailAddressIsValid(string mailAddress) {
			try {
				var address = new MailAddress(mailAddress);
				return true;
			}
			catch {
				return false;
			}
		}

		public static bool SendMail(string mailTo, string nameTo, string subject, string body) {
			if (!MailAddressIsValid(Email.MailFromAddress) || !MailAddressIsValid(mailTo)) {
				return false;
			}

			//try {
				var fromAddress = new MailAddress(Email.MailFromAddress, Email.MailFromName);
				var toAddress = new MailAddress(mailTo, nameTo);

				var smtp = new SmtpClient {
					Host = Email.Smtp,
					Port = Email.SmtpPort,
					EnableSsl = Email.SmtpTLS,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(Email.MailFromAddress, Email.MailFromPassword)
				};

				using (var message = new MailMessage(fromAddress, toAddress) {
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				}) {
					smtp.Send(message);
				}

				return true;
			//}
			//catch {
			//	return false;
			//}
		}
	}
}