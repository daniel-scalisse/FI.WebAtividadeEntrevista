var acaoBenef;
var idBenef;

function SalvarBenef() {
    $.ajax({
        url: acaoBenef == 'I' ? urlIncBenef : urlAltBenef,
        method: "POST",
        data: {
            Id: idBenef,
            CPF: $('#CPFBenef').val(),
            Nome: $('#NomeBenef').val(),
            IdCliente: obj.Id
        },
        error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON, false);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
        success:
            function (r) {
                if (r.Ok) {
                    if (acaoBenef == 'A') {
                        var trB = document.getElementById("trBenef" + idBenef);
                        trB.cells[0].innerHTML = r.CPF;
                        trB.cells[1].innerHTML = $('#NomeBenef').val();
                        $('#btSalvarBenef').html("Incluir");
                        acaoBenef = 'I';
                    }
                    else
                        AddBenef(r.Id, r.CPF, $('#NomeBenef').val())

                    $('#CPFBenef').val("");
                    $('#NomeBenef').val("");
                    idBenef = 0;
                }

                ModalDialog(r.Ok ? "Sucesso" : "Ops!", r.Msg, r.Ok);
            }
    });
}

function GetBenefById(id) {
    $.post(urlGetBenef,
        { id: id },
        function (data, status) {
            if (status == "success") {
                idBenef = id;
                $('#CPFBenef').val(data.CPF);
                $('#NomeBenef').val(data.Nome);
                $('#btSalvarBenef').html("Salvar");
                acaoBenef = 'A';
            }
            else {
                ModalDialog("Ops!", "Erro ao buscar o registro!", false);
            }
        });
}

function AddBenef(id, cpf, nome) {
    var t = document.getElementById("tbBenef");
    var qtC = 0, qtR = t.rows.length;
    var r, c, obj;
    r = t.insertRow(qtR);
    r.setAttribute("id", "trBenef" + id);

    c = r.insertCell(qtC++);
    c.innerHTML = cpf;

    c = r.insertCell(qtC++);
    c.innerHTML = nome;

    c = r.insertCell(qtC++);
    c.className = "text-right";
    obj = document.createElement("button");
    obj.setAttribute("type", "button");
    obj.className = "btn btn-sm btn-info";
    obj.innerHTML = "Alterar";
    obj.onclick = function () { GetBenefById(id) };
    c.appendChild(obj);

    c = r.insertCell(qtC++);
    c.className = "text-right";
    obj = document.createElement("button");
    obj.setAttribute("type", "button");
    obj.className = "btn btn-sm btn-danger";
    obj.innerHTML = "Excluir";
    obj.onclick = function () { ExcluirBenef(id) };
    c.appendChild(obj);
}

function OpenDivBenef() {
    idBenef = 0;
    acaoBenef = 'I';
    $('#CPFBenef').val("");
    $('#NomeBenef').val("");
    $.post(urlListBenef,
        { idCliente: obj.Id },
        function (data, status) {
            if (status == "success") {
                var t = document.getElementById("tbBenef");
                if (t) {
                    var q = t.rows.length;
                    while (q > 1)
                        t.deleteRow(--q);
                }
                for (var i = 0; i < data.Records.length; i++)
                    AddBenef(data.Records[i].Id, data.Records[i].CPF, data.Records[i].Nome)
                $('#divBenef').modal('show');
            }
            else {
                ModalDialog("Ops!", "Erro ao buscar o registro!", false);
            }
        });
}

function ExcluirBenef(id) {
    $.post(urlExcBenef,
        { id: id },
        function (data, status) {
            if (status == "success") {
                var r = document.getElementById("trBenef" + id);
                r.parentNode.removeChild(r);
                ModalDialog(data.Ok ? "Sucesso" : "Ops!", data.Msg, data.Ok);
            }
            else {
                ModalDialog("Ops!", "Erro ao excluir o registro!", false);
            }
        });
}