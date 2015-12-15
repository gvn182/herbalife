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
function RowClick(Index) {

    var table = document.getElementById('GdrPesquisa');
    var row = table.rows[parseInt(Index) + 1];
    var Data = row.cells[0].innerHTML;
    window.open('FechamentoDiarioDetalhe.aspx?Data=' + Data, '_blank');

}
function RowClickDetalhe(Index) {

    var table = document.getElementById('GdrPesquisa');
    var row = table.rows[parseInt(Index) + 1];
    var Data = row.cells[0].innerHTML;
    var Cliente = row.cells[2].innerHTML;
    window.open('VendasItemCliente.aspx?Data=' + Data + '&Cliente=' + Cliente, '_blank');

}
