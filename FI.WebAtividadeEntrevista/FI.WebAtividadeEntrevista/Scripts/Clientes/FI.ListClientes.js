$(document).ready(function () {

    if (document.getElementById("gridClientes"))
        $('#gridClientes').jtable({
            title: 'Clientes',
            paging: true, //Enable paging
            pageSize: 5, //Set page size (default: 10)
            sorting: true, //Enable sorting
            defaultSorting: 'Nome ASC', //Set default sorting
            actions: {
                listAction: urlClienteList,
            },
            fields: {
                Nome: {
                    title: 'Nome',
                    width: '41%'
                },
                Email: {
                    title: 'Email',
                    width: '31%'
                },
                CPF: {
                    title: 'CPF',
                    width: '13%',
                    listClass: 'text-center'
                },
                Alterar: {
                    title: '',
                    width: '5%',
                    listClass: 'text-center',
                    display: function (data) {
                        return "<button onclick=window.location.href='" + urlAlteracao + "/" + data.record.Id + "' class='btn btn-primary btn-sm'>Alterar</button>";
                    }
                }
            }
        });

    //Load student list from server
    if (document.getElementById("gridClientes"))
        $('#gridClientes').jtable('load');
})