using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using W3.Library;

namespace Bolao.Aposta {
	public abstract class BaseRankingControl : BaseControl {
		public abstract void Inicializar(int codBolao);
	}
}