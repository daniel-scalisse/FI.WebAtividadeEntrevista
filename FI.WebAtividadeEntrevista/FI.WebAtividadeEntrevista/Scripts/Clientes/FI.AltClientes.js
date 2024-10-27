$(document).ready(function () {
    if (obj) {
        $('#Nome').val(obj.Nome);
        $('#CEP').val(obj.CEP);
        $('#Email').val(obj.Email);
        $('#Sobrenome').val(obj.Sobrenome);
        $('#Nacionalidade').val(obj.Nacionalidade);
        $('#Estado').val(obj.Estado);
        $('#Cidade').val(obj.Cidade);
        $('#Logradouro').val(obj.Logradouro);
        $('#Telefone').val(obj.Telefone);
        $('#CPF').val(obj.CPF);
    }
})

function Excluir() {
    $.post(urlExc,
        { id: obj.Id },
        function (data, status) {
            if (status == "success") {
                if (data.Ok) {
                    setTimeout(function () { window.location.href = urlRetorno; }, 4000);
                }
                ModalDialog(data.Ok ? "Sucesso" : "Ops!", data.Msg, data.Ok);                    
            }
            else {
                ModalDialog("Ops!", "Erro ao excluir o registro!", false);
            }
        });
}