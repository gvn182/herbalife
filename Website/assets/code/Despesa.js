$(document).ready(function () {
    $('#TableDespesas').jtable({
        title: 'Despesas',
        paging: true, //Enables paging
        pageSize: 50, //Actually this is not needed since default value is 10.
        sorting: true, //Enables sorting
        defaultSorting: 'Descricao ASC', //Optional. Default sorting on first load.
        actions: {
            listAction: '/Despesas.aspx/ListaDespesas',
            createAction: '/Despesas.aspx/AddNewDespesa',
            updateAction: '/Despesas.aspx/AtualizarDespesa',
            deleteAction: '/Despesas.aspx/ExcluirDespesa'
        },
        fields: {

            Codigo: {
                list: false,
                edit: false,
                key: true,
                title: 'ID'
            },
            Data: {
                width: '25%',
                title: 'Data'
            },
            Descricao: {
                width: '50%',
                title: 'Descrição'
            },
            ValorTotal: {
                width: '25%',
                title: 'Valor (R$)'
            }

        },
        //Initialize validation logic when a form is created
        formCreated: function (event, data) {

            $("[name='Data']").val(GetCurrentDate());
            $("[name='Data']").mask('99/99/9999');
            $("[name='ValorTotal']").maskMoney({ showSymbol: false, decimal: ",", thousands: "." });

            data.form.find('input[name="Data"]').addClass('validate[required,custom[dateFormat]]');
            data.form.find('input[name="Descricao"]').addClass('validate[required]');
            data.form.find('input[name="ValorTotal"]').addClass('validate[required]');
            data.form.validationEngine();
        },
        //Validate form when it is being submitted
        formSubmitting: function (event, data) {
            return data.form.validationEngine('validate');
        },
        //Dispose validation logic when form is closed
        formClosed: function (event, data) {
            data.form.validationEngine('hide');
            data.form.validationEngine('detach');
        }
    });


    $('#btnPesquisar').click(function (e) {
        e.preventDefault();
        $('#TableDespesas').jtable('load', {
            filtro: $('#filtro').val()
        });
    });
    $('#filtro').keydown(function (event) {
        if (event.keyCode == 13) {
            $('#btnPesquisar').click();
            event.preventDefault();
            return false;
        }
    });
    //Load student list from server
    $('#btnPesquisar').click();

});

