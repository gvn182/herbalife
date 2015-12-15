$(document).ready(function () {
    $('#TableProdutos').jtable({
        title: 'Produtos',
        paging: true, //Enables paging
        pageSize: 50, //Actually this is not needed since default value is 10.
        sorting: true, //Enables sorting
        defaultSorting: 'COD_Herbalife ASC', //Optional. Default sorting on first load.
        actions: {
            listAction: '/Produtos.aspx/ListaProdutos',
            createAction: '/Produtos.aspx/AddNewProduto',
            updateAction: '/Produtos.aspx/AtualizarProduto',
            deleteAction: '/Produtos.aspx/ExcluirProduto'
        },
        fields: {

            ID_Produto: {
                list: false,
                edit: false,
                key: true,
                title: 'ID'
            },
            COD_Herbalife:
            {
                title: 'Cod',
                width: '5%',
                edit: false
            },
            Descricao: {
                width: '35%',
                title: 'Descrição'
            },
            Preco: {
                width: '15%',
                title: 'Preço (R$)'
            },
            Gasto_Estimado:
            {
                width: '15%',
                title: 'Gasto est. (R$)'
            },
            Estoque_Minimo:
            {
                width: '15%',
                title: 'Est. minimo'
            },
            Afeta_Estoque:
            {
                width: '10%',
                title: 'Req. Estoque',
                options: '/Produtos.aspx/OpcoesEstoque'
            },
            PV: {
                width: '5%',
                title: 'PV'
            },
            UNID: {
                width: '5%',
                title: 'UN',
                list: false,
                options: '/Produtos.aspx/OpcoesUnidade'
            }

        },
        //Initialize validation logic when a form is created
        formCreated: function (event, data) {


            $("[name='Estoque_Minimo']").mask('9?9');
            $("[name='COD_Herbalife']").mask('9?999999');
            $("[name='Preco']").maskMoney({ showSymbol: false, allowZero: true, decimal: ",", thousands: "." });
            $("[name='Gasto_Estimado']").maskMoney({ showSymbol: false, allowZero: true, decimal: ",", thousands: "." });
            $("[name='PV']").mask("99,99");

            data.form.find('input[name="COD_Herbalife"]').addClass('validate[required,custom[integer]]');
            data.form.find('input[name="Descricao"]').addClass('validate[required]');
            data.form.find('input[name="Preco"]').addClass('validate[required]');
            data.form.find('input[name="PV"]').addClass('validate[required,custom[number]]');
            data.form.find('input[name="Estoque_Minimo"]').addClass('validate[required,custom[number]]');
            data.form.find('input[name="Gasto_Estimado"]').addClass('validate[required]');
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
        $('#TableProdutos').jtable('load', {
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

