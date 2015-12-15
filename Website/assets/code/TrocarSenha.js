$(function () {

    if (getParameterByName('Reenvio') != '') {
        $("#txtSenhaAtual").val('old password');
        $("#txtSenhaAtual").addClass('disabled');
    }
    $('#btnSalvar').click(function () {
        if ($('#formTrocarSenha').valid()) {
            Salvar();
        }
        else {
            return false;
        }
    });



    $('#formTrocarSenha').validate({
        errorElement: 'div',
        errorClass: 'help-block',
        focusInvalid: false,
        rules: {
            senhaatual: {
                required: true
            },
            nova: {
                required: true
            },
            nova2: {
                required: true,
                equalTo: "#nova"
            }
        },

        messages: {
            senhaatual: {
                required: "Este campo é obrigatório"
            },
            nova: {
                required: "Este campo é obrigatório"
            },
            nova2: {
                required: "Este campo é obrigatório",
                equalTo: "As senhas devem ser iguais"
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

});

function Salvar() {
    var SenhaAtual = $('#txtSenhaAtual').val();
    var NovaSenha = $('#nova').val();

    PageMethods.Salvar(SenhaAtual, NovaSenha, OnSuccessSalvar, OnError);

}

function OnSuccessSalvar(data) {
    if (data == 1) {
        MessageBox('Senha alterada com sucesso');
        $('#txtSenhaAtual').val('');
        $('#nova').val('');
        $('#nova2').val('');
    }
    else {
        MessageBox('Senha atual inválida');
        $('#txtSenhaAtual').val('');
    }
}
function OnError(error) {
    alert(error.get_message());
}