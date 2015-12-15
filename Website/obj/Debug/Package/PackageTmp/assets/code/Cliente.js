$(document).ready(function () {
    $('#TableClientes').jtable({
        title: 'Clientes',
        paging: true, //Enables paging
        pageSize: 50, //Actually this is not needed since default value is 10.
        sorting: true, //Enables sorting
        defaultSorting: 'Nome ASC', //Optional. Default sorting on first load.
        actions: {
            listAction: '/Clientes.aspx/ListaClientes',
            createAction: '/Clientes.aspx/AddNewCliente',
            updateAction: '/Clientes.aspx/AtualizarCliente',
            deleteAction: '/Clientes.aspx/ExcluirCliente'
        },
        fields: {

            ID_Cliente: {
                list: false,
                edit: false,
                key: true,
                title: 'ID'
            },
            Codigo:
            {
                create: false,
                edit: false,
                title: 'Cod',
                width: '5%'
            },
            Nome:
            {
                title: 'Nome'
            },
            Email: {
                title: 'Email'
            },
            Telefone: {
                title: 'Telefone'
            },
            Tipo: {
                title: 'Tipo',
                options: '/Clientes.aspx/OpcoesTipo'
            },
            Indicacao: {
                title: 'Indicação',
                options: '/Clientes.aspx/OpcoesClientes'
            },
            ComoSoube:
            {
                title: 'Como soube',
                options: '/Clientes.aspx/OpcoesComoSoube'
            },
            DataVisita:
            {
                list: false,
                edit: false,
                create: false
            }

        },
        //Initialize validation logic when a form is created
        formCreated: function (event, data) {

            $("[name='Tipo'] option[value='Repetidor']").remove();
            //$("[name='Indicacao']").chosen();

            var container = $("[name='Indicacao']").parent().parent();
            if ($("[name='Tipo']").val() != "Indicação") {
                container.hide();
            }
            else {
                container.show();
            }

            $("[name='Telefone']").mask('(99) 9999-9999?9');
            $("[name='DataVisita']").mask('99/99/9999');

            $("[name='Tipo']").change(function () {
                var container = $("[name='Indicacao']").parent().parent();
                if ($(this).val() == "Indicação") {
                    container.show();
                }
                else {
                    container.hide();
                }
            });


            data.form.find('input[name="Email"]').addClass('validate[required,custom[email]]');
            data.form.find('input[name="DataVisita"]').addClass('validate[required,custom[dateFormat]]');
            data.form.find('input[name="Telefone"]').addClass('validate[required]');
            data.form.find('input[name="Nome"]').addClass('validate[required]');
            data.form.validationEngine();



        },
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
        $('#TableClientes').jtable('load', {
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

