<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Front.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="utf-8" />
    <meta name="description" content="User login page" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- basic styles -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <!--[if IE 7]>
		  <link rel="stylesheet" href="assets/css/font-awesome-ie7.min.css" />
		<![endif]-->
    <!-- page specific plugin styles -->
    <!-- fonts -->
    <link rel="stylesheet" href="assets/css/ace-fonts.css" />
    <!-- ace styles -->
    <link href="assets/css/uncompressed/ace.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="assets/css/ace-rtl.min.css" />
    <link rel="stylesheet" href="assets/css/ace-skins.min.css" />
    <script src="assets/js/jquery-2.0.3.min.js" type="text/javascript"></script>
    <script src="assets/code/Login.js" type="text/javascript"></script>
    <script src="assets/code/Util.js" type="text/javascript"></script>
    <!--[if lte IE 8]>
		  <link rel="stylesheet" href="assets/css/ace-ie.min.css" />
		<![endif]-->
    <script src="assets/js/ace-extra.min.js"></script>
    <!-- inline styles related to this page -->
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
		<script src="assets/js/html5shiv.js"></script>
		<script src="assets/js/respond.min.js"></script>
		<![endif]-->
    <script src="assets/js/bootstrap.min.js"></script>


    <!-- /.main-container -->
    <!-- basic scripts -->
    <!-- basic scripts -->
    <!--[if !IE]> -->
    <script type="text/javascript">
        window.jQuery || document.write("<script src='assets/js/jquery-2.0.3.min.js'>" + "<" + "/script>");
    </script>
    <!-- <![endif]-->
    <!--[if IE]>
<script type="text/javascript">
 window.jQuery || document.write("<script src='assets/js/jquery-1.10.2.min.js'>"+"<"+"/script>");
</script>
<![endif]-->
    <script type="text/javascript">
        if ("ontouchend" in document) document.write("<script src='assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
    </script>
    <script src="assets/js/bootstrap.min.js"></script>
    <!-- page specific plugin scripts -->
    <script src="assets/js/jquery.validate.min.js"></script>
    <script src="assets/js/bootbox.min.js"></script>
    <script src="assets/js/jquery.maskedinput.min.js"></script>
    <script src="assets/js/select2.min.js"></script>
    <!-- ace scripts -->
    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>
    
    <!-- inline scripts related to this page -->
    <script type="text/javascript">

        jQuery(function ($) {


            //documentation : http://docs.jquery.com/Plugins/Validation/validate


            $.mask.definitions['~'] = '[+-]';
            $('#telefone').mask('(99) 9999-9999?9');
            //            jQuery.validator.addMethod("phone", function (value, element) {
            //                return this.optional(element) || /^\(\d{3}\) \d{3}\-\d{4}( x\d{1,6})?$/.test(value);
            //            }, "Enter a valid phone number.");

            //            jQuery.validator.addMethod("cpf", function (value, element) {
            //                return this.optional(element) || /^\d{3}\.\d{3}\.\d{3}-\d{2}$/.test(value);
            //            }, "Insira um cpf válido.");



            $('#formReenviar').validate({
                errorElement: 'div',
                errorClass: 'help-block',
                focusInvalid: true,
                rules: {
                    txtReenviarSenhaEmail: {
                        required: true,
                        email:true
                    }
                },

                messages: {
                    txtReenviarSenhaEmail:
                        {
                            required: "Por favor preencha o login",
                            email: "Por favor utilize um email válido"
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



            $('#ValidaLogin').validate({
                errorElement: 'div',
                errorClass: 'help-block',
                focusInvalid: true,
                rules: {
                    usuarioLogin: {
                        required: true
                    },
                    senhaLogin: {
                        required: true
                    }

                },

                messages: {
                    usuarioLogin:
                        {
                            required: "Por favor preencha o login"
                        },
                    senhaLogin:
                        {
                            required: "Por favor preencha a senha"
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


            $('#RegisterForm').validate({
                errorElement: 'div',
                errorClass: 'help-block',
                focusInvalid: true,
                rules: {
                    phone: {
                        required: true
                    },
                    usuario: {
                        required: true
                    },
                    senha: {
                        required: true
                    },
                    senha2:
                    {
                        required: true,
                        equalTo: "#senha"
                    },
                    email: {
                        required: true,
                        email: true
                    }

                },

                messages: {
                    phone:
                        {
                            required: "Este campo é obrigatório"
                        },
                    usuario:
                        {
                            required: "Este campo é obrigatório"
                        },
                    senha: {
                        required: "Este campo é obrigatório"
                    },
                    senha2: {
                        required: "Este campo é obrigatório",
                        equalTo: "As senhas devem ser iguais"
                    },
                    email:
                        {
                            email: "Por favor preencha um e-mail valido",
                            required: "Este campo é obrigatório"
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



        })

    </script>
    <!-- inline scripts related to this page -->
    <script type="text/javascript">
        function show_box(id) {
            $('.widget-box.visible').removeClass('visible');
            $('#' + id).addClass('visible');
        }
      
    </script>

    <!-- page specific plugin scripts -->
</head>
<body class="login-layout">
    
    <form id="MainForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="main-container">
        <div class="main-content">
            <div class="row">
                <div class="col-sm-10 col-sm-offset-1">
                    <div class="login-container">
                        <div class="center">
                            <h1>
                                <img src="assets/img/logo.png"  />
                            </h1>
                        </div>
                        <div class="space-6">
                        </div>
                        <div class="position-relative">
                            <div id="login-box" class="login-box visible widget-box no-border">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <h4 class="header blue lighter bigger">
                                            <i class="icon-coffee green"></i>Por favor efetue login
                                        </h4>
                                        <div class="space-6">
                                        </div>
                                        <form id="ValidaLogin">
                                        <fieldset>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">
                                                    <input type="text" id="usuarioLogin" name="usuarioLogin" class="form-control" placeholder="Usuario" />
                                                    <i class="icon-user"></i></span>
                                            </label>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">
                                                    <input type="password" id="senhaLogin" name="senhaLogin" class="form-control" placeholder="Senha" />
                                                    <i class="icon-lock"></i></span>
                                            </label>
                                            <div class="space">
                                            </div>
                                            <div class="clearfix">
                                                <label class="inline">
                                                    <input type="checkbox" class="ace" />
                                                    <span class="lbl">Lembrar</span>
                                                </label>
                                                <input id="btnEntrar" type="button" value="Entrar" class="width-35 pull-right btn btn-sm btn-primary" />
                                            </div>
                                            <div class="space-4">
                                            </div>
                                        </fieldset>
                                        </form>
                                    </div>
                                    <!-- /widget-main -->
                                    <div class="toolbar clearfix">
                                        <div>
                                            <a href="#" onclick="show_box('forgot-box'); return false;" class="forgot-password-link">
                                                <i class="icon-arrow-left"></i>Esqueci minha senha</a>
                                        </div>
                                        <div>
                                            <a href="#" onclick="show_box('signup-box'); return false;" class="user-signup-link">
                                                Cadastrar <i class="icon-arrow-right"></i></a>
                                        </div>
                                    </div>
                                </div>
                                <!-- /widget-body -->
                            </div>
                            <div id="confirm-box" class="forgot-box widget-box no-border">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <h3 class="header green lighter">
                                            Obrigado!
                                        </h3>
                                        <h4 class="header blue lighter bigger">
                                            <i class="icon-envelope blue"></i>Por favor confirme o cadastro no email recebido
                                            no endereço: <strong><label id='EmailEnviado' class="control-label"></label></strong>
                                        </h4>
                                        <!-- /widget-main -->
                                    </div>
                                </div>
                                <!-- /widget-body -->
                            </div>
                               <div id="reemail-box" class="forgot-box widget-box no-border">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <h3 class="header green lighter">
                                            Ops..!
                                        </h3>
                                        <h4 class="header blue lighter bigger">
                                            <i class="icon-envelope blue"></i>Já existe um cadastro com esse usuario aguardando confirmação. O email de confirmação foi enviado novamente.
                                        </h4>
                                        <!-- /widget-main -->
                                    </div>
                                </div>
                                <!-- /widget-body -->
                            </div>
                            <div id="reenvio-box" class="forgot-box widget-box no-border">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <h3 class="header green lighter">
                                            Sucesso..!
                                        </h3>
                                        <h4 class="header blue lighter bigger">
                                            <i class="icon-envelope blue"></i>Foi lhe enviado um email com as instruções para redefinir sua senha, por favor siga as instruções contidas no email.
                                        </h4>
                                        <!-- /widget-main -->
                                    </div>
                                </div>
                                <!-- /widget-body -->
                            </div>
                            <!-- /login-box -->
                            <div id="forgot-box" class="forgot-box widget-box no-border">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <h4 class="header red lighter bigger">
                                            <i class="icon-key"></i>Resetar senha
                                        </h4>
                                        <div class="space-6">
                                        </div>
                                        <p>
                                            Entre com seu email para receber instruções
                                        </p>
                                        <form id="formReenviar">
                                        <fieldset>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">
                                                    <input type="text" id="txtReenviarSenhaEmail" name="txtReenviarSenhaEmail" class="form-control" placeholder="Email" />
                                                    <i class="icon-envelope"></i></span>
                                            </label>
                                            <div class="clearfix">
                                                <input id="btnReenviarSenha" type="button" value="Enviar"  class="width-35 pull-right btn btn-sm btn-danger" />
                                            </div>
                                        </fieldset>
                                        </form>
                                    </div>
                                    <!-- /widget-main -->
                                    <div class="toolbar center">
                                        <a href="#" onclick="show_box('login-box'); return false;" class="back-to-login-link">
                                            Voltar para o login <i class="icon-arrow-right"></i></a>
                                    </div>
                                </div>
                                <!-- /widget-body -->
                            </div>
                            <!-- /forgot-box -->
                            <div id="signup-box" class="signup-box widget-box no-border">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <h4 class="header green lighter bigger">
                                            <i class="icon-group blue"></i>Cadastro de novo usuário
                                        </h4>
                                        <div class="space-6">
                                        </div>
                                        <p>
                                            Entre com seus dados para começar:
                                        </p>
                                        <form id="RegisterForm">
                                        <fieldset>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">
                                                    <input type="text" id="usuario" name="usuario" class="form-control" placeholder="Usuario"
                                                        runat="server" />
                                                    <i class="icon-user"></i></span>
                                            </label>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">
                                                    <input type="password" id="senha" name="senha" class="form-control" placeholder="Senha" />
                                                    <i class="icon-lock"></i></span>
                                            </label>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">
                                                    <input type="password" id="senha2" name="senha2" class="form-control" placeholder="Repetir senha" />
                                                    <i class="icon-retweet"></i></span>
                                            </label>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">
                                                    <input type="email" id="email" name="email" class="form-control" placeholder="Email" />
                                                    <i class="icon-envelope"></i></span>
                                            </label>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">
                                                    <input type="text" id="telefone" name="phone" class="form-control" placeholder="Telefone" />
                                                    <i class="icon-phone"></i></span>
                                            </label>
                                            <div class="space-24">
                                            </div>
                                            <div class="clearfix">
                                                <button type="reset" class="width-30 pull-left btn btn-sm">
                                                    <i class="icon-refresh"></i>Limpar
                                                </button>
                                                <input id="btnSalvarLogin" type="button" value="Salvar" class="width-65 pull-right btn btn-sm btn-success" />
                                            </div>
                                        </fieldset>
                                        </form>
                                    </div>
                                    <div class="toolbar center">
                                        <a href="#" onclick="show_box('login-box'); return false;" class="back-to-login-link">
                                            <i class="icon-arrow-left"></i>Voltar para o login </a>
                                    </div>
                                    <!-- /signup-box -->
                                </div>
                                <!-- /position-relative -->
                            </div>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->
                </div>
            </div>
        </div>
    </div>
    
    </form>
</body>
</html>
