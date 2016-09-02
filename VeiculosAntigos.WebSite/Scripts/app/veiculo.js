$(document).ready(function () {
    VeiculosAntigosApp.Veiculo.Init();
});

VeiculosAntigosApp.Veiculo = function () {
    function events() {
        $("#btnAdd").click(function () {
            fillForm(null);
            showWindow();
        });

        $("#btnOK").click(function () {
            var data = new Veiculo($("form #Id").val(), $("form #Descricao").val(),
                $("form #Ano").val(), $("form #IdTipo").val(), $("form #IdTipo option:selected").html(),
                $("form #IdFabricante").val(), $("form #IdFabricante option:selected").html());

            if (data.Id === '') {
                $.post(baseAddressApi, data, function (result) {
                    debugger;
                    data.Id = result.Id;
                    addRow(data);
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

    function fillForm(model) {
        if (model == null) {
            $("form #Id").val('');
            $("form #Descricao").val('');
            $("form #Ano").val('');
            $("form #IdTipo").selectedIndex = 0;
            $("form #IdFabricante").selectedIndex = 0;
        }
        else {
            $("form #Id").val(model.Id);
            $("form #Descricao").val(model.Descricao);
            $("form #Ano").val(model.Ano);
            $("form #IdTipo").val(model.IdTipo);
            $("form #IdFabricante").val(model.IdFabricante);
        }
    }

    function getItemRow(sender) {
        var row = getRow(sender);
        return new Veiculo(row.find('#Id').html(), row.find('#Descricao').html(), 
            row.find('#Ano').html(), row.find('#IdTipo').html(), row.find('#Tipo').html(),
            row.find('#IdFabricante').html(), row.find('#Fabricante').html());
    }

    function getRow(sender) {
        return sender.parents('tr:first');
    }

    function addRow(model) {
        var table = document.getElementById("tblVeiculo");
        var row = table.insertRow(1);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        var cell4 = row.insertCell(3);
        var cell5 = row.insertCell(4);
        cell1.innerHTML = '<span id="Descricao">' + model.Descricao + '</span>';
        cell2.innerHTML = '<span id="IdFabricante" style="display:none;">' + model.IdFabricante + '</span>' +
                          '<span id="Fabricante">' + model.Fabricante + '</span>'
        cell3.innerHTML = '<span id="IdTipo" style="display:none;">' + model.IdTipo + '</span>' +
                          '<span id="Tipo">' + model.Tipo + '</span>'
        cell4.innerHTML = '<span id="Ano">' + model.Ano + '</span>';
        cell5.innerHTML = '<div>' +
                            '<span id="Id" style="display:none;">' + model.Id + '</span>' +
                            '<button class="btn btn-primary btnEdit" onclick="VeiculosAntigosApp.Veiculo.EditOnClick(this)">Editar</button>  ' +
                            '<button class="btn btn-primary btnRemove" onclick="VeiculosAntigosApp.Veiculo.RemoveOnClick(this)">Excluir</button> ' +
                          '</div>';
    }

    function removeRow(row) {
        var table = document.getElementById("tblVeiculo");
        table.deleteRow(row.index() + 1);
    }

    function editOnClick(e) {
        var data = getItemRow($(e))
        fillForm(data);
        showWindow();
    }

    function removeOnClick(e) {
        var row = $(e);
        var data = getItemRow(row)
        var option = confirm('Deseja remover "' + data.Descricao + '"?');
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
            baseAddressApi = VeiculosAntigosApp.BaseAddressApi + 'api/Veiculo';
            events();
        },
        EditOnClick: editOnClick,
        RemoveOnClick: removeOnClick
    }
}();
