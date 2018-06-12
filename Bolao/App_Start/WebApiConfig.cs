using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace Bolao.App_Start {
	public class WebApiConfig {
		public static void Register(HttpConfiguration config) {

			// Web API routes
			config.MapHttpAttributeRoutes();

			var route = RouteTable.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			route.RouteHandler = new MyHttpControllerRouteHandler();

			//GlobalConfiguration.Configuration.Filters.Add(new W3.Framework.API.W3AuthenticationWebApiFilter());

			//SwaggerConfig.Register();
		}
	}
	public class MyHttpControllerHandler : HttpControllerHandler, IRequiresSessionState {
		public MyHttpControllerHandler(RouteData routeData) : base(routeData) {
		}
	}
	public class MyHttpControllerRouteHandler : HttpControllerRouteHandler {
		protected override IHttpHandler GetHttpHandler(RequestContext requestContext) {
			return new MyHttpControllerHandler(requestContext.RouteData);
		}
	}
}