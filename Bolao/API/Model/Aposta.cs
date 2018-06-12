using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bolao.API.Model {
	public class Aposta {
		[Required]
		public string CodigoJogo { get; set; }
		public string QuantidadeGolA { get; set; }
		public string QuantidadeGolB { get; set; }
	}
}