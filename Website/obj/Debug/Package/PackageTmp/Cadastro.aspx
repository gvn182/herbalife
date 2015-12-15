<%@ Page Title="Continuação cadastro" Language="C#" MasterPageFile="~/Master.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="Front.Cadastro"
    ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="assets/code/Cadastro.js" type="text/javascript"></script>
    <script>

        $(function () {



            $('#txtCEP').mask('99999-999');
            $('#txtRG').mask('99.999.999');
            $('#txtCPF').mask('999.999.999-99');
            $('#txtTelefone').mask('(99) 9999-9999');
            $('#txtCelular').mask('(99) 9999-9999?9');
            $('#txtCNPJ').mask('99.999.999/9999-99');



            $('#formCadastro').validate({
                errorElement: 'div',
                errorClass: 'help-block',
                focusInvalid: false,
                rules: {
                    nome: {
                        required: true
                    },
                    cep: {
                        required: true
                    },
                    endereco: {
                        required: true
                    },
                    numero: {
                        required: true
                    },
                    bairro: {
                        required: true
                    },
                    cidade: {
                        required: true
                    },
                    rg: {
                        required: true
                    },
                    cpf: {
                        required: true
                    },
                    telefone: {
                        required: true
                    },
                    cnpj: {
                        required: true
                    },
                    estado: {
                        required: true
                    }
                },

                messages: {
                    nome: {
                        required: "Este campo é obrigatório"
                    },
                    cep: {
                        required: "Este campo é obrigatório"
                    },
                    endereco: {
                        required: "Este campo é obrigatório"
                    },
                    numero: {
                        required: "Este campo é obrigatório"
                    },
                    bairro: {
                        required: "Este campo é obrigatório"
                    },
                    cidade: {
                        required: "Este campo é obrigatório"
                    },
                    rg: {
                        required: "Este campo é obrigatório"
                    },
                    cpf: {
                        required: "Este campo é obrigatório"
                    },
                    telefone: {
                        required: "Este campo é obrigatório"
                    },
                    cnpj: {
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

        });

    </script>
    <div class="page-content">
        <div class="page-header">
            <h1>
                Informações do espaço <small><i class="icon-double-angle-right"></i>Estamos quase lá!
                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div id="divConfirmCadastro" class="col-xs-12">
                <!-- PAGE CONTENT BEGINS -->
                <form class="form-horizontal" id="formCadastro">
                <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
                </asp:ScriptManager>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtNome">
                        Nome
                    </label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="text" name="nome" id="txtNome" class="col-xs-10 col-sm-5" />
                        </div>
                    </div>
                </div>
                                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="nivel">
                        Nivel</label>
                    <div class="col-xs-12 col-sm-9">
                        <select id="ddNivel" name="nivel" class="select2">
                            <option value="Distribuidor independente">Distribuidor independente</option>
                            <option value="Consultor sênior">Consultor sênior</option>
                            <option value="Construtor de sucesso">Construtor de sucesso</option>
                            <option value="Produtor qualificado">Produtor qualificado</option>
                            <option value="Supervisor">Supervisor</option>
                        </select>
                    </div>
                </div>
                <div class="space-4">
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                        CEP</label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="text" name="cep" id="txtCEP" class="input-sm" />
                        </div>
                    </div>
                </div>
                <div class="space-4">
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtEndereco">
                        Endereço</label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="text" name="endereco" class="col-xs-10 col-sm-5" id="txtEndereco" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtNumero">
                        Numero</label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input class="input-sm" name="numero" type="text" id="txtNumero" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtComplemento">
                        Complemento</label>
                    <div class="col-sm-9">
                        <input type="text" class="col-xs-10 col-sm-5" id="txtComplemento" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtBairro">
                        Bairro</label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="text" name="bairro" class="col-xs-10 col-sm-5" id="txtBairro" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtCidade">
                        Cidade</label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="text" name="cidade" class="col-xs-10 col-sm-5" id="txtCidade" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="ddEstado">
                        Estado</label>
                    <div class="col-xs-12 col-sm-9">
                        <select id="ddEstado" name="estado" class="select2">
                            <option value="SP">São Paulo</option>
                            <option value="AC">Acre</option>
                            <option value="AL">Alagoas</option>
                            <option value="AP">Amapá</option>
                            <option value="AM">Amazonas</option>
                            <option value="BA">Bahia</option>
                            <option value="CE">Ceará</option>
                            <option value="DF">Distrito Federal</option>
                            <option value="ES">Espirito Santo</option>
                            <option value="GO">Goiás</option>
                            <option value="MA">Maranhão</option>
                            <option value="MT">Mato Grosso</option>
                            <option value="MS">Mato Grosso do Sul</option>
                            <option value="MG">Minas Gerais</option>
                            <option value="PA">Pará</option>
                            <option value="PB">Paraiba</option>
                            <option value="PR">Paraná</option>
                            <option value="PE">Pernambuco</option>
                            <option value="PI">Piauí</option>
                            <option value="RJ">Rio de Janeiro</option>
                            <option value="RN">Rio Grande do Norte</option>
                            <option value="RS">Rio Grande do Sul</option>
                            <option value="RO">Rondônia</option>
                            <option value="RR">Roraima</option>
                            <option value="SC">Santa Catarina</option>
                            <option value="SE">Sergipe</option>
                            <option value="TO">Tocantis</option>
                        </select>
                    </div>
                </div>
             <%--   <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right">
                    </label>
                    <div class="radio">
                        <label>
                            <input id="chkFisica" name="form-field-radio" checked="checked" type="radio" class="ace" />
                            <span class="lbl">Pessoa Fisica</span>
                        </label>
                        <label>
                            <input id="chkJuridica" name="form-field-radio" type="radio" class="ace" />
                            <span class="lbl">Pessoa Juridica</span>
                        </label>
                    </div>
                </div>--%>
                <div id="divFisica">
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtRG">
                            RG</label>
                        <div class="col-sm-9">
                            <div class="clearfix">
                                <input type="text" name="rg" class="col-xs-10 col-sm-5" id="txtRG" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCPF">
                            CPF</label>
                        <div class="col-sm-9">
                            <div class="clearfix">
                                <input type="text" name="cpf" class="col-xs-10 col-sm-5" id="txtCPF" />
                            </div>
                        </div>
                    </div>
                </div>
               <%-- <div id="divJuridica" style="display: none">
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCNPJ">
                            CNPJ</label>
                        <div class="col-sm-9">
                            <div class="clearfix">
                                <input type="text" name="cnpj" class="col-xs-10 col-sm-5" id="txtCNPJ" />
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtTelefone">
                        Telefone</label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="icon-phone"></i></span>
                                <input type="text" name="telefone" class="col-xs-10 col-sm-4" id="txtTelefone" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtCelular">
                        Celular</label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="icon-phone"></i></span>
                                <input type="text" class="col-xs-10 col-sm-4" id="txtCelular" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtCelular">
                        </label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <div class="input-group">
                                <input id="chkPolitica" type="checkbox" value="" /><a href="termo/TERMOS%20E%20CONDIÇÕES%20GERAIS%20PARA%20ADESÃO%20DE%20USO%20DE%20SOFTWARE.pdf" target="_blank">Termo de adesão</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix form-actions">
                    <div class="col-md-offset-3 col-md-9">
                        <button class="btn" type="reset">
                            <i class="icon-undo bigger-110"></i>Reset
                        </button>
                        &nbsp; &nbsp; &nbsp;
                        <input id="btnSalvar" type="button" value="Salvar" class="btn btn-info" />
                    </div>
                </div>
                <div class="hr hr-24">
                </div>
                </form>
            </div>
            <div id="divPagamento" style="display: none" class="col-xs-12">
                <div class="widget-body">
                    <div class="widget-main">
                        <h4 class="header blue lighter bigger">
                            Tudo pronto! agora prossiga com o pagamento no site do pagseguro!
                        </h4>
                        <button class="btn btn-info" type="button">
                            <i class="icon-ok bigger-110"></i>Assinar com pagseguro
                        </button>
                        <!-- /widget-main -->
                    </div>
                </div>
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </div>
    <!-- /.page-content -->
</asp:Content>
