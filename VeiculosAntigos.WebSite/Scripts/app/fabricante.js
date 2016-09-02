$(document).ready(function () {
    VeiculosAntigosApp.Fabricante.Init();
});

VeiculosAntigosApp.Fabricante = function () {
    function events() {
        $("#btnAdd").click(function () {
            fillForm('', '');
            showWindow();
        });

        $("#btnOK").click(function () {
            var data = new Fabricante($("form #Id").val(), $("form #NomeFabricante").val());

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

    function fillForm(id, nomeFabricante) {
        $("form #Id").val(id);
        $("form #NomeFabricante").val(nomeFabricante);
    }

    function getItemRow(sender) {
        var row = getRow(sender);
        return new Fabricante(row.find('#Id').html(), row.find('#NomeFabricante').html());
    }

    function getRow(sender) {
        return sender.parents('tr:first');
    }

    function addRow(model) {
        var table = document.getElementById("tblFabricante");
        var row = table.insertRow(1);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        cell1.innerHTML = '<span id="Id">' + model.Id + '</span>';
        cell2.innerHTML = '<span id="NomeFabricante">' + model.NomeFabricante + '</span>';
        cell3.innerHTML = '<div>' +
                            '<button class="btn btn-primary btnEdit" onclick="VeiculosAntigosApp.Fabricante.EditOnClick(this)">Editar</button>  ' +
                            '<button class="btn btn-primary btnRemove" onclick="VeiculosAntigosApp.Fabricante.RemoveOnClick(this)">Excluir</button> ' +
                          '</div>';
    }

    function removeRow(row) {
        var table = document.getElementById("tblFabricante");
        table.deleteRow(row.index() + 1);
    }

    function editOnClick(e) {
        var data = getItemRow($(e))
        showWindow();
        fillForm(data.Id, data.NomeFabricante);
    }

    function removeOnClick(e) {
        var row = $(e);
        var data = getItemRow(row)
        var option = confirm('Deseja remover "' + data.NomeFabricante + '"?');
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
        Init: function(){
            baseAddressApi = VeiculosAntigosApp.BaseAddressApi + 'api/Fabricante';
            events();
        },
        EditOnClick: editOnClick,
        RemoveOnClick: removeOnClick
    }
}();
