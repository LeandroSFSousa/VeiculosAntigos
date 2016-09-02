$(document).ready(function () {
    VeiculosAntigosApp.TipoDeVeiculo.Init();
});

VeiculosAntigosApp.TipoDeVeiculo = function () {
    function events() {
        $("#btnAdd").click(function () {
            fillForm('', '');
            showWindow();
        });

        $("#btnOK").click(function () {
            var data = new TipoDeVeiculo($("form #Id").val(), $("form #Tipo").val());

            if (data.Id === '') {
                $.post(baseAddressApi, data, function (result) {
                    addRow(result);
                    hideWindow();
                }).error(function (result) {
                    alert(result.responseJSON.ExceptionMessage);
                });
            }
            else {
                $.ajax({
                    type: "POST",
                    url: baseAddressApi + "/Put/" + data.Id,
                    data: data,
                }).success(function (e) {
                    location.reload();
                });
            }
        });

        $("#btnCancel").click(function () {
            hideWindow();
        });

        $("#btnRefresh").click(function () {
            location.reload();
        });
    }

    function fillForm(id, tipo) {
        $("form #Id").val(id);
        $("form #Tipo").val(tipo);
    }

    function getItemRow(sender) {
        var row = getRow(sender);
        return new TipoDeVeiculo(row.find('#Id').html(), row.find('#Tipo').html());
    }

    function getRow(sender) {
        return sender.parents('tr:first');
    }

    function addRow(model) {
        var table = document.getElementById("tblTipoDeVeiculo");
        var row = table.insertRow(1);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        cell1.innerHTML = '<span id="Id">' + model.Id + '</span>';
        cell2.innerHTML = '<span id="Tipo">' + model.Tipo + '</span>';
        cell3.innerHTML = '<div>' +
                            '<button class="btn btn-primary btnEdit" onclick="VeiculosAntigosApp.TipoDeVeiculo.EditOnClick(this)">Editar</button>  ' +
                            '<button class="btn btn-primary btnRemove" onclick="VeiculosAntigosApp.TipoDeVeiculo.RemoveOnClick(this)">Excluir</button> ' +
                          '</div>';
    }

    function removeRow(row) {
        var table = document.getElementById("tblTipoDeVeiculo");
        table.deleteRow(row.index() + 1);
    }

    function editOnClick(e) {
        var data = getItemRow($(e))
        showWindow();
        fillForm(data.Id, data.Tipo);
    }

    function removeOnClick(e) {
        var row = $(e);
        var data = getItemRow(row)
        var option = confirm('Deseja remover "' + data.Tipo + '"?');
        if (option == 1) {
            $.ajax({
                type: "POST",
                url: baseAddressApi + "/Delete/" + data.Id,
            }).success(function (e) {
                removeRow(getRow(row));
            })

        }
    }

    return {
        Init: function () {
            baseAddressApi = VeiculosAntigosApp.BaseAddressApi + 'api/TipoDeVeiculo';
            events();
        },
        EditOnClick: editOnClick,
        RemoveOnClick: removeOnClick
    }
}();
