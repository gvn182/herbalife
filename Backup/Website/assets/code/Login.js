$(document).ready(function () {

    $('#login-box').keypress(function (e) {
        {
            if (e.which == 13) {
                $('#btnEntrar').click();
            }
        }
    });


    $("#btnSalvarLogin").click(function () {
        if ($('#RegisterForm').valid()) {
            $(this).addClass('disabled');
            CreateNewUser();
        }
    });
    $("#btnEntrar").click(function () {
        if ($('#usuarioLogin').val() != '' && $('#senhaLogin').val() != '') {
            $(this).addClass('disabled');
            CheckUser();
        }
        else {
            MessageBox('Preencha o usuario e a senha');
        }
    });
    $("#btnReenviarSenha").click(function () {
        if ($('#formReenviar').valid()) {
            $(this).addClass('disabled');
            ReenviaSenha();
        }

    });

});


function ReenviaSenha() {
    PageMethods.EsqueciMinhaSenha($('#txtReenviarSenhaEmail').val(), OnSuccessReenviar, OnErrorCheck);
}
function OnSuccessReenviar(data) {
    if (data) {
        show_box('reenvio-box');
    }
    else {
        MessageBox('Não foi encontrado nenhum usuário com este email');
        $("#btnReenviarSenha").removeClass('disabled');
    }
}
function CheckUser() {
    PageMethods.CheckUser($('#usuarioLogin').val(), $('#senhaLogin').val(), OnSuccessCheck, OnErrorCheck);
}
function OnSuccessCheck(response) {
    switch (response) {
        case -1: show_box('reemail-box'); break;
        case -2: MessageBox('Login ou senha inválido.');
            $('#senhaLogin').val('');
            $("#btnEntrar").removeClass('disabled');
            break;
        default: window.location = 'Venda.aspx'; break;
    }
}
function OnErrorCheck(error) {
    alert(error.get_message());
}
function CreateNewUser() {
    PageMethods.NovoUsuario($('#usuario').val(), $('#senha').val(), $('#email').val(), $('#telefone').val(), OnSuccessCreate, OnErrorCreate);
}
function OnSuccessCreate(response) {

    switch (response) {
        case -1: show_box('reemail-box'); break;
        case -2: MessageBox('Usuario já cadastrado no sistema, favor escolher outro usuario'); $('#btnSalvarLogin').removeClass('disabled'); break;
        default: show_box('confirm-box'); $('#EmailEnviado').html($('#email').val()); break;
    }
}
function OnErrorCreate(error) {
    alert(error.get_message());
}