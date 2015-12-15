$(document).ready(function () {
    $(function () {

        $("#txtQtdRemover").mask('9?99');
        $("#txtQtdAdicionar").mask('9?99');
        $("#txtDataInicialFinal").mask('99/99/9999 - 99/99/9999');


        var DatePicker = $('#txtDataInicialFinal').daterangepicker({
            language: "pt-BR"
        });

        //        DatePicker.on('blur', function () {
        //            DatePicker.datepicker('hide');
        //        });

        RegisterModal();

        $(".ui-dialog-titlebar").hide();

        $("#txtQtdRemover").change(function () {
            if ($(this).val() != '') {
                $("#lblQtdAposCadastroRemover").text(parseInt($("#lblQtdAtualRemover").text()) - parseInt($(this).val()));
            }
        });

        $("#txtQtdAdicionar").change(function () {
            if ($(this).val() != '') {
                $("#lblQtdAposCadastroAdicionar").text(parseInt($("#lblQtdAtualAdicionar").text()) + parseInt($(this).val()));
            }
        });
    });


    $("#btnSalvarRemover").click(function (e) {
        if ($("#txtQtdRemover").val() == '') {
            MessageBox('É necessário preencher a quantidade');
            return false;
        }
        if (parseInt($("#lblQtdAposCadastroRemover").text()) < 0) {
            MessageBox('A quantidade em estoque não pode ser menor que 0');
            return false;
        }
        return true;
    });

    $("#btnSalvarAdicionar").click(function (e) {
        if ($("#txtQtdAdicionar").val() == '') {
            MessageBox('É necessário preencher a quantidade');
            return false;
        }
        return true;
    });

    $("#btnPesquisarHistorico").click(function (e) {
        if ($('#txtDataInicialFinal').val() == '') {
            MessageBox('Entre com uma data válida');
            return false;
        }

        var Datas = $('#txtDataInicialFinal').val().split(" - ");

        if (!isDate(Datas[0]) || !isDate(Datas[1])) {
            MessageBox('Entre com uma data válida');
            return false;
        }
        return true;
    });
});
function CloseModal() {
    $("#txtQtdRemover").val('');
    $("#txtQtdAdicionar").val('');
    $("#txtMotivo").val('');
    $("#ModalRemover").dialog("close");
    $("#ModalHistorico").dialog("close");
    $("#ModalAdicionar").dialog("close");
}
function RegisterModal() {
    $("#ModalAdicionar").dialog({
        appendTo: "form",
        modal: true,
        width: 450,
        autoOpen: false,
        resizable: false,
        open: function () {
            $(this).parent().appendTo("form");
        }
    });
    $("#ModalRemover").dialog({
        appendTo: "form",
        modal: true,
        width: 450,
        autoOpen: false,
        resizable: false,
        open: function () {
            $(this).parent().appendTo("form");
        }
    });

    $("#ModalHistorico").dialog({
        appendTo: "form",
        modal: true,
        width: 600,
        autoOpen: false,
        resizable: false,
        open: function () {
            $(this).parent().appendTo("form");
        }
    });



}
function OpenModalRemover() {
    RegisterModal();
    CloseModal();
    $("#ModalRemover").dialog("open");
}
function OpenModalAdicionar() {
    RegisterModal();
    CloseModal();
    $("#ModalAdicionar").dialog("open");
}

function OpenModalHistorico() {
    RegisterModal();
    CloseModal();
    $("#ModalHistorico").dialog("open");
}

