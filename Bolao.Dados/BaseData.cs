namespace Bolao.Dados {
	public class BaseData {
		protected W3.Library.Data.DatabaseConnector Connector {
			get {
				return W3.Library.Data.DatabaseUtil.Connector;
			}
		}

		protected System.Web.HttpContext Context {
			get {
				return System.Web.HttpContext.Current;
			}
		}
	}
}
