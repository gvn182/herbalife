$(function () {
    $('#btnSalvar').attr('disabled', 'disabled');
    $("#txtCEP").change(function () {
        GetCEP();
    });

    //    $("#chkFisica").change(function () {
    //        if (this.checked) {

    //            $('#divFisica').show();
    //            $('#divJuridica').hide();

    //        }
    //        else {
    //            $('#divFisica').hide();
    //            $('#divJuridica').show();

    //        }
    //        ClearPessoa();
    //    });

    //    $("#chkJuridica").change(function () {
    //        if (this.checked) {
    //            $('#divFisica').hide();
    //            $('#divJuridica').show();
    //        }
    //        else {
    //            $('#divFisica').hide();
    //            $('#divJuridica').show();
    //        }
    //        ClearPessoa();
    //    });

    $('#chkPolitica').click(function () {
        if (this.checked) {
            $('#btnSalvar').removeAttr('disabled');
        }
        else {
            $('#btnSalvar').attr('disabled', 'disabled');
        }
    });
    $('#btnSalvar').click(function () {
        if ($('#formCadastro').valid()) {
            if ($('#txtCPF').val() != "")
                if (!ValidarCPF($('#txtCPF').val())) {
                    MessageBox('Digite um cpf valido!');
                    return false;
                }
            //            if ($('#txtCNPJ').val() != "")
            //                if (!ValidarCNPJ($('#txtCNPJ').val())) {
            //                    MessageBox('Digite um CNPJ valido!');
            //                    return false;
            //                }
            $(this).addClass('disabled');
            AddDetalhes();
        }
    });
});
function GetCEP() {
    var CEP = $('#txtCEP').val();
    if(CEP != '')
    PageMethods.GetEndereco(CEP, OnSucessGetCEP, OnErrorGETCEP);
}
function OnSucessGetCEP(response) {
    if (response != null) {
        var Logradouro = response.LOGRADOURO;
        var Bairro = response.BAIRRO;
        var Cidade = response.CIDADE;
        var Estado = response.ESTADO;
        $('#txtBairro').val(Bairro);
        $('#txtCidade').val(Cidade);
        $('#ddEstado').val(Estado);
        $('#txtEndereco').val(Logradouro);
    }
}
function OnErrorGETCEP(error) {
    alert(error.get_message());
}
function AddDetalhes() {
    var Nome = $('#txtNome').val();
    var CEP = $('#txtCEP').val();
    var Endereco = $('#txtEndereco').val();
    var Numero = $('#txtNumero').val();
    var Complemento = $('#txtComplemento').val();
    var Bairro = $('#txtBairro').val();
    var Cidade = $('#txtCidade').val();
    var Estado = $('#ddEstado').val();
    var RG = $('#txtRG').val();
    var CPF = $('#txtCPF').val();
//    var CNPJ = $('#txtCNPJ').val();
    var Telefone = $('#txtTelefone').val();
    var Celular = $('#txtCelular').val();
    var Nivel = $('#ddNivel').val();

    PageMethods.AddDetalhes(Nome, CEP, Endereco, Numero, Complemento, Bairro, Cidade, Estado, RG, CPF, Telefone, Celular,Nivel, OnSuccessAddDetalhes, OnErrorAddDetalhes);
}
function OnSuccessAddDetalhes() {
    window.location = "Produtos.aspx";
}
function OnErrorAddDetalhes() {

}
function ClearPessoa() {
    $('#txtRG').val("");
    $('#txtCPF').val("");
    $('#txtCNPJ').val("");
}