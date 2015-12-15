$(function () {

    $("#divProdutos").show();
    $("#divProdutosAdicionados").hide();
    $("#divSalvar").hide();
    $("#helpProduto").hide();
    $("#txtData").prop('readonly', 'true');
    $(".knob").knob();
    $('#ddTipo').chosen();
    $('#ddComoSoube').chosen();

    $('#lblQtd').ace_spinner({ value: 1, min: 1, max: 1000, step: 1, btn_up_class: 'btn-info', btn_down_class: 'btn-info' });
    $('#lblDesconto').ace_spinner({ value: 0.00, min: 0.00, max: 1000.00, step: 5.00, btn_up_class: 'btn-info', btn_down_class: 'btn-info', numberFormat: "n" }).on('change', function () {
        AtualizaDesconto();
    });
    $('#lblDesconto').blur(function () {
        var Desconto = $('#lblDesconto').val();
        if (Desconto == '') {
            $('#lblDesconto').val('0.00');
            AtualizaDesconto();
        }

        if (parseFloat(Desconto) > 100) {
            $('#lblDesconto').val('100');
            AtualizaDesconto();
        }
    });
    $("#ModalCliente").dialog({
        modal: true,
        width: 800,
        autoOpen: false,
        resizable: false
    });
    $("#ModalCaixa").dialog({
        modal: true,
        width: 800,
        autoOpen: false,
        resizable: false,
        closeOnEscape: false
    });
    $("#ModalProduto").dialog({
        modal: true,
        width: 800,
        autoOpen: false,
        resizable: false
    });
    $(".ui-dialog-titlebar").hide();

    $("#btnNovoProduto").click(function () {
        $("#ModalProduto").dialog("open");

    });


    GetClientes();
    GetProdutos();
    $('#ddCliente').change(function () {
        if ($(this).val() != "-1") {
            GetCliente($(this).val());
        }
    });
    $('#ddTipo').change(function () {
        if ($(this).val() != "Indicação") {
            $('#divIndicacao').hide();
        }
        else {
            $('#divIndicacao').show();
        }
    });


    $('#ddPagamento').chosen();
    $('#ddProduto').change(function () {
        if ($(this).val() != "-1") {
            GetValorProduto();
        }
    });


    $('#btnEstorno').click(function () {
        window.open('Estorno.aspx?Data=' + $("#txtData").val(), '_blank');
    });
    $('#btnMapa').click(function () {
        window.open('FechamentoDiario.aspx?Data=' + $("#txtData").val(), '_blank');
    });

    $('#btnAddProduto').click(function () {
        if ($('#ddProduto').val() != "-1") {

            AddProduto();
            $("#ModalProduto").dialog("close");
            $("#helpProduto").hide();
        }
        else {
            $("#helpProduto").show();
        }
    });
    $("#txtData").datepicker({
        format: 'dd/mm/yyyy',
        startDate: '-15d',
        endDate: '0d' // there's no convenient "right now" notation yet
    });

    $("#txtData").val(GetCurrentDate());
    GetAcessos();
    CheckAberturaCaixa();
    $("#lblLancamentoRetroativo").hide();

    $("#txtData").change(function () {
        if ($("#txtData").val() != GetCurrentDate()) {
            $("#lblLancamentoRetroativo").show();
        }
        else {
            $("#lblLancamentoRetroativo").hide();
        }
        CheckAberturaCaixa();
        GetAcessos();
    });
    function CheckAberturaCaixa() {
        PageMethods.CheckCaixa($("#txtData").val(), OnSucessCheckCaixa, OnError);
    }
    function OnSucessCheckCaixa(data) {
        if (data.Result != "TRUE") {
            $("#ModalCaixa").dialog("open");
        }

    }
    $('#btnSalvar').click(function () {
        $(this).prop('disabled', true);
        SalvarVenda();
        $(this).prop('disabled', false);
    });


    $('#txtValorCaixa').maskMoney({ showSymbol: false, decimal: ",", thousands: ".", allowZero: true });
    $('#telefone').mask('(99) 9999-9999?9');


    $('#formCadastro').validate({
        errorElement: 'div',
        errorClass: 'help-block',
        focusInvalid: false,
        rules: {
            nome: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            telefone: {
                required: true
            },
            indicacao: {
                required: true
            }
        },

        messages: {
            nome: {
                required: "Este campo é obrigatório"
            },
            email: {
                required: "Este campo é obrigatório",
                email: "Por favor preencha um e-mail valido"
            },
            telefone: {
                required: "Este campo é obrigatório"
            },
            indicacao: {
                required: "Por favor selecione um cliente"
            }
        },
        highlight: function (e) {
            $(e).closest('.form-group').removeClass('has-info').addClass('has-error');
        },

        success: function (e) {
            $(e).closest('.form-group').removeClass('has-error').addClass('has-info');
            $(e).remove();
        },

        errorPlacement: function (error, element) {
            if (element.is(':checkbox') || element.is(':radio')) {
                var controls = element.closest('div[class*="col-"]');
                if (controls.find(':checkbox,:radio').length > 1) controls.append(error);
                else error.insertAfter(element.nextAll('.lbl:eq(0)').eq(0));
            }
            else if (element.is('.select2')) {
                error.insertAfter(element.siblings('[class*="select2-container"]:eq(0)'));
            }
            else if (element.is('.chosen-select')) {
                error.insertAfter(element.siblings('[class*="chosen-container"]:eq(0)'));
            }
            else error.insertAfter(element.parent());
        }

    });

    $('#formCaixa').validate({
        errorElement: 'div',
        errorClass: 'help-block',
        focusInvalid: false,
        rules: {
            valorcaixa: {
                required: true
            }
        },

        messages: {
            valorcaixa: {
                required: "Preencha o valor, caso não haja dinheiro no caixa, preencher com 0,00"
            }
        },
        highlight: function (e) {
            $(e).closest('.form-group').removeClass('has-info').addClass('has-error');
        },

        success: function (e) {
            $(e).closest('.form-group').removeClass('has-error').addClass('has-info');
            $(e).remove();
        },

        errorPlacement: function (error, element) {
            if (element.is(':checkbox') || element.is(':radio')) {
                var controls = element.closest('div[class*="col-"]');
                if (controls.find(':checkbox,:radio').length > 1) controls.append(error);
                else error.insertAfter(element.nextAll('.lbl:eq(0)').eq(0));
            }
            else if (element.is('.select2')) {
                error.insertAfter(element.siblings('[class*="select2-container"]:eq(0)'));
            }
            else if (element.is('.chosen-select')) {
                error.insertAfter(element.siblings('[class*="chosen-container"]:eq(0)'));
            }
            else error.insertAfter(element.parent());
        }

    });

    $('#btnAdicionarCliente').click(function () {
        if ($('#formCadastro').valid()) {
            AddCliente();
        }

    });
    $('#btnAbrirCaixa').click(function () {
        if ($('#formCaixa').valid()) {
            AbrirCaixa();
        }
    });
});
function AbrirCaixa() {
    PageMethods.AbreCaixa($("#txtData").val(), $("#txtValorCaixa").val(), OnSucessAbrirCaixa, OnError);
}
function OnSucessAbrirCaixa() {
    $("#ModalCaixa").dialog("close");
}
function GetAcessos() {
    PageMethods.GetAcessos($("#txtData").val(), OnSuccessAcessos, OnError);
}
function OnSuccessAcessos(data) {
    $("#tbAcessos > tbody").html("");

    if (data.length == 0)
        $('#tbAcessos').hide();

    for (var i = 0; i < data.length-1; i++) {
        AddItemAcesso(data[i].Ordem, data[i].Status, data[i].Nome, data[i].Valor);
    }
}
function AddItemAcesso(Ordem, Status, Nome, Valor) {
    var NewLine = "<tr><td>" + Ordem + "</td> <td>" + Status + "</td> <td>" + Nome + "</td> <td>" + Valor + "</td></tr>";
    $('#tbBodyAcessos').prepend(NewLine);
    $('#tbAcessos').show();
    
}
function AddCliente() {
    var Cliente = new Object;
    Cliente.ComoSoube = $("#ddComoSoube").val();
    Cliente.Nome = $("#nome").val();
    Cliente.Email = $("#email").val();
    Cliente.Telefone = $("#telefone").val();
    Cliente.Tipo = $("#ddTipo").val();
    Cliente.Indicacao = $("#ddClienteIndicacao").val();
    Cliente.ID_Cliente = '';
    Cliente.Codigo = '';
    Cliente.DataVisita = '';

    PageMethods.AddNovoCliente(Cliente, OnSucessAddCliente, OnError);
}
function OnSucessAddCliente(data) {
    GetClientes();
    CloseClienteModal();
}
function CloseClienteModal() {
    $("#ModalCliente").dialog("close");
    $('#nome').val('');
    $('#email').val('');
    $('#telefone').val('');
    $('#ddTipo').val('Novo cliente');
    $('#divIndicacao').hide();
    $('#ddTipo').trigger("chosen:updated");
    $('#ddComoSoube').val('Folheto');
    $('#ddComoSoube').trigger("chosen:updated");

        

}
function SalvarVenda() {
    if ($("#ddCliente").val() == "-1") {
        MessageBox('Por favor, selecione o cliente');
        return;
    }
    if ($("#ddPagamento").val() == "-1") {
        MessageBox('Por favor, selecione a forma de pagamento');
        return;
    }
    var ListaProdutos = new Array();
    var table = document.getElementById('tbProdutos');
    var rowLength = table.rows.length;

    for (var i = 1; i < rowLength; i += 1) {
        var row = table.rows[i];
        var ID_Produto = row.cells[0].innerHTML;
        var Descricao = row.cells[1].innerHTML;
        var Preco = row.cells[2].innerHTML;
        var Desconto = row.cells[3].innerHTML;
        var PrecoComDesconto = row.cells[4].innerHTML;
        var Produto = new Object();

        Produto.ID_Produto = ID_Produto;
        Produto.Produto = Descricao;
        Produto.Preco = Preco;
        Produto.Desconto = Desconto;
        Produto.PrecoComDesconto = PrecoComDesconto;
        ListaProdutos[i - 1] = Produto;
    }
    
    var Data = $("#txtData").val();
    var ID_Cliente = $("#ddCliente").val();
    var Pagamento = $("#ddPagamento").val();
    var Obs = $("#txtObservacao").val();
    PageMethods.SalverVenda(Data, ID_Cliente, Pagamento, Obs, ListaProdutos, OnSucessSalvarVenda, OnError);

}
function OnSucessSalvarVenda(data) {
    //{ Status = "FAIL", Message = "Quantidade em estoque do produto [" + item.Descricao + "] Indisponivel" };
    if (data.Status == "FAIL") {
        MessageBox(data.Message, "Falha ao concluir a venda");
        return;
    }
    else {
        //object ResultadoSUCCESS = new { Status = "SUCCESS", Cliente = NovaVenda.TB_CLIENTE.Nome, QtdProdutos = LstProdutos.Count, ValorTotal = NovaVenda.Total_Produtos };
         var table = document.getElementById('tbAcessos');
        var rowLength = table.rows.length;
        AddItemAcesso(rowLength, data.S, data.Nome, data.Valor);
        AddGritter(data.Nome, data.Valor);
        ResetPage();
        
    }

}
function GetCliente(ID) {
    PageMethods.GetAcessosCliente(ID, OnSucessGetAcessosCliente, OnError);
}
function OnSucessGetAcessosCliente(data) {
    if (parseInt(data.Acessos) == 2) {
        MessageBox("Esse é o terceiro acesso do(a) Sr(a)  <b>" + data.Nome + "</b>, pergunte como se ele está se sentindo!", "Atenção!");
    }
}
function ResetPage() {
    $('#ddPagamento').val("-1");
    $('#ddPagamento').trigger("chosen:updated");
    $('#ddCliente').val("-1");
    $('#ddCliente').trigger("chosen:updated");
    $('#ddProduto').val("-1");
    $('#ddProduto').trigger("chosen:updated");
    $('#lblPreco').text("0,00");
    $('#lblDesconto').val("0");
    $('#lblQtd').val("1");
    $("#lblPrecoDesconto").text("0,00");
    $("#txtObservacao").val('');

    while (RowIndex > 1) {
        RemoveProduto(1);
    }
    AtualizaValorTotal();
}
function CloseModal() {
    $("#ModalProduto").dialog("close");
}
function AddGritter(NomeCliente, ValorTotal) {
    $.gritter.add({
        title: 'Venda efetuada com sucesso!',
        text: NomeCliente + "<br/>" + "R$ " + ValorTotal + "<br/>",
        image: 'assets/images/avatar1.png',
        class_name: 'gritter-info gritter-success'
    
    });
}

var RowIndex = 1;
function AddProduto() {
    var Produto = $('#ddProduto option:selected').text();
    var ProdutoID = $('#ddProduto').val();
    var Preco = $('#lblPreco').text();
    var Desconto = $('#lblDesconto').val();
    var PrecoDesconto = $("#lblPrecoDesconto").text();
    var Qtd = $('#lblQtd').val();
    if (parseInt(Qtd) <= 0)
        Qtd = 1;

    for (var i = 0; i < Qtd; i++) {
        var NewLine = "<tr> <td class='hidden'>" + ProdutoID + "</td> <td>" + Produto + "</td> <td>" + Preco + "</td> <td>" + Desconto + "</td> <td>" + PrecoDesconto + "</td> <td><a href='#' onClick='RemoveProduto(" + RowIndex + ");' class='tooltip-error' data-rel='tooltip' title='Remover produto'><span class='red'><i class='icon-trash bigger-120'></i></span></a></td></tr>";
        $('#tbProdutosBody').append(NewLine);
        RowIndex++;
    }
    

    $('#ddProduto').val("-1");
    $('#ddProduto').trigger("chosen:updated");
    $('#lblPreco').text("0,00");
    $('#lblDesconto').val("0");
    $("#lblPrecoDesconto").text("0,00");
    $('#lblQtd').val("1");
    AtualizaValorTotal();

    $("#divProdutos").hide();
    $("#divProdutosAdicionados").show();
    $("#divSalvar").show();
}
function AtualizaValorTotal() {
    var table = document.getElementById('tbProdutos');

    var rowLength = table.rows.length;
    var Total = 0;
    for (var i = 1; i < rowLength; i += 1) {
        var row = table.rows[i];
        var Valor = row.cells[4].innerHTML;
        Total += parseFloat(Valor.replace(',','.'));

    }
    $('#lblPrecoTotal').text("R$ " + Total.toFixed(2).replace('.', ','));
}
function RemoveProduto(Index) {

    $("#tbProdutos tr:eq(" + Index + ")").remove();
    AtualizaValorTotal();
    RowIndex--;

    if (RowIndex <= 1) {
        $("#divProdutos").show();
        $("#divProdutosAdicionados").hide();
        $("#divSalvar").hide();
    }
}
function AtualizaDesconto() {
    var Preco = parseFloat($("#lblPreco").text());
    var Desconto = parseFloat($("#lblDesconto").val());
    var Valor;
    if (Desconto > 0) {
        Valor = Preco - (Preco * Desconto / 100);
        $("#lblPrecoDesconto").text(Valor.toFixed(2).replace('.', ','));
    }
    else
        $("#lblPrecoDesconto").text($("#lblPreco").text());

    if (Desconto > 100) {
        $("#lblPrecoDesconto").text('0,00');
    }
}
function GetValorProduto() {
    PageMethods.GetValorProduto($('#ddProduto').val(), OnSucessGetValorProduto, OnError);
}
function OnSucessGetValorProduto(data) {
    if (data.Requer_Estoque) {
        var EstoqueAtual = parseInt(data.Qtd_Estoque);
        var EstoqueMinimo = parseInt(data.Qtd_Minima);

        if (EstoqueAtual <= 0) {
            jAlert("Produto não disponivel no estoque!", "Atenção!");
            $('#btnAddProduto').prop('disabled', true);
            return;
        }
        else {
            $('#btnAddProduto').prop('disabled', false);
        }
        if (EstoqueAtual <= EstoqueMinimo) {
            jAlert("O Produto atingiu o estoque minimo", "Atenção!");
        }
    }
    else {
        $('#btnAddProduto').prop('disabled', false);
    }
    $("#lblPreco").text(data.Preco);
    AtualizaDesconto();
}

function GetProdutos() {
    PageMethods.GetAllProdutos(OnSucessProduto, OnError);
}
function OnSucessProduto(data) {
    for (var i = 0; i < data.length; i++) {
        $('#ddProduto').append("<option value='" + data[i].Value + "'>" + data[i].Text + "</option>");
    }
    $('#ddProduto').chosen();
}
function GetClientes() {
    PageMethods.GetAllClientes(OnSucessCliente, OnError);
}
function OnSucessCliente(data) {
    $('#ddCliente').empty();
    $('#ddClienteIndicacao').empty();

    $('#ddCliente').append('<option value="-1">Selecione...</option>');
    for (var i = 0; i < data.length; i++) {
        $('#ddCliente').append("<option value='" + data[i].Value + "'>" + data[i].Text + "</option>");
        $('#ddClienteIndicacao').append("<option value='" + data[i].Value + "'>" + data[i].Text + "</option>");
    }
    $("#ddCliente").chosen();
    $("#ddClienteIndicacao").chosen();
    $('#ddCliente').trigger("chosen:updated");
    $('#ddClienteIndicacao').trigger("chosen:updated");

}
function OnError(error) {
    alert(error.get_message());
}


