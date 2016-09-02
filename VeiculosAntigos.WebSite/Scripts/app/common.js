var VeiculosAntigosApp = {
    baseAddressApi: undefined
};

var Fabricante = function (id, nomeFabricante) {
    this.Id = id;
    this.NomeFabricante = nomeFabricante;
}

var TipoDeVeiculo = function (id, tipo) {
    this.Id = id;
    this.Tipo = tipo;
}

var Veiculo = function (id, descricao, ano, idTipo, tipo, idFabricante, fabricante) {
    this.Id = id;
    this.Descricao = descricao;
    this.Ano = ano;
    this.IdTipo = idTipo;
    this.IdFabricante = idFabricante;
    this.Fabricante = fabricante;
    this.Tipo = tipo;
}

function showWindow() {
    $(".win").css("display", "");
    $("input:text:first").focus();
}


function hideWindow() {
    $(".win").css("display", "none");
}