$(document).ready(function () {


    //        $("#txtQtdRemover").mask('9?99');
    //        $("#txtQtdAdicionar").mask('9?99');
    var DatePicker = $('#txtFiltro').daterangepicker({
        language: "pt-BR"
    });
    $("#txtFiltro").mask('99/99/9999 - 99/99/9999');
//    $("#txtFiltro").val(GetCurrentDate() + " - " + GetCurrentDate());


    //        DatePicker.on('blur', function () {
    //            DatePicker.datepicker('hide');
    //        });

    $("#BtnPesquisar").click(function (e) {
        if ($('#txtFiltro').val() == '') {
            MessageBox('Entre com uma data válida');
            return false;
        }

        var Datas = $('#txtFiltro').val().split(" - ");

        if (!isDate(Datas[0]) || !isDate(Datas[1])) {
            MessageBox('Entre com uma data válida');
            return false;
        }
        return true;
    });
});


