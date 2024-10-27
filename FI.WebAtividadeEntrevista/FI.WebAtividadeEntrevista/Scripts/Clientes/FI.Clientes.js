$(document).ready(function () {
    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "CPF": $(this).find("#CPF").val()
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
                    ModalDialog(r.Ok ? "Sucesso" : "Ops!", r.Msg, r.Ok);
                    if (r.Ok) {
                        if (acao == 'I')
                            $('#formCadastro')[0].reset();
                        //else if (acao == 'A')
                        //    window.location.href = urlRetorno;
                    }
                }
        });
    })
})

function ModalDialog(titulo, texto, success) {
    var random = Math.random().toString().replace('.', '');
    var cls = success ? "text-success" : "text-danger";
    var texto = '<div id="' + random + '" class="modal fade ' + cls + '">' +
        '        <div class="modal-dialog">' +
        '            <div class="modal-content">' +
        '                <div class="modal-header">' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>' +
        '                    <h4 class="modal-title">' + titulo + '</h4>' +
        '                </div>' +
        '                <div class="modal-body">' +
        '                    <p>' + texto + '</p>' +
        '                </div>' +
        '                <div class="modal-footer">' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>' +
        '                </div>' +
        '            </div><!-- /.modal-content -->' +
        '  </div><!-- /.modal-dialog -->' +
        '</div> <!-- /.modal -->';

    $('body').append(texto);
    $('#' + random).modal('show');
}

$(document).keydown(function (event) {
    if (event.which == 27) {
        if ($('#divBenef').is(':visible'))
            $('#divBenef').modal('hide');
    }
});