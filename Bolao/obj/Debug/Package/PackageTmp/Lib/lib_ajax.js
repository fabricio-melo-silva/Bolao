function ajax(servico, parametros, fnSucesso, fnErro, fnSessaoInvalida) {
	if ((fnSucesso != null) && (typeof (fnSucesso) != 'function')) {
		alert('fnFuncao inválida. Formato: ajax(servico, parametros, fnSucesso, fnErro, fnSessaoInvalida) ');
		return;
	}

	if ((parametros != null) && (typeof (parametros) == 'object')) {
		var pNovo = '{';

		for (var p in parametros) {
			pNovo += '"' + p + '":"' + parametros[p] + '", ';
		}

		parametros = pNovo.substr(0, pNovo.length - 2) + '}';
	}

	var urlx = (servico.indexOf("/") != -1) ? servico : (opcoes.urlServico + "/" + servico);

	parametros = (parametros) ? parametros : '{}';

	var retorno;

	$.ajaxSetup({ cache: true });

	$.ajax({
		url: urlx,
		type: "post",
		data: parametros,
		dataType: "json",
		async: (fnSucesso != null),
		contentType: "application/json; charset=utf-8",
		cache: true,
		success: function (data) {
			//debugger;
			//montaLoading(false, "Aguarde Carregando...");
			retorno = (data.d) ? data.d : data;

			if (fnSucesso != null) {
				fnSucesso(retorno);
			}
		},
		error: function (xtr, status, error) {
			//debugger;
			var ehUmErroDeConexao = (xtr.statusText != '') && (xtr.statusText.indexOf('NetworkError') !== -1) && (xtr.statusText.indexOf('send') !== -1) && (xtr.statusText.indexOf('XMLHttpRequest') !== -1);
			if (xtr.status == 0 || ehUmErroDeConexao) {
				return;
			}
			else if (xtr.status != 0) {
				if (status === "abort" && error === "abort") {
					return;
				}

				var retornoJson = xtr.responseJSON;

				if (retornoJson == "Logoff") {
					documento.location.href = "../Sair.aspx";
				}

				if (typeof (fnErro) == 'function') {
					fnErro(xtr, status, error);
				}
				else {
					document.write(error.message + '<br /><br />' + xtr.responseText);
				}
			}
		}
	});

	return retorno;
}
