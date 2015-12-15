$(document).ready(function () {
    $(function () {

        $("#txtDataInicialFinal").mask('99/99/9999 - 99/99/9999');
        

        var DatePicker = $('#txtDataInicialFinal').daterangepicker({
            language: "pt-BR"
        });

        $('#btnPesquisar').click(function () {


            var DataInicial = $('#txtDataInicialFinal').val().split(' - ')[0];
            var DataFinal = $('#txtDataInicialFinal').val().split(' - ')[1];

            if (isDate(DataInicial) && isDate(DataFinal)) {
                return true;
            }
            else {
                MessageBox("Preencha uma data valida");
                return false;
            }
        });

    })

});

