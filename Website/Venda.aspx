<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="Venda.aspx.cs" Inherits="Front.Venda" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="assets/js/jquery.knob.min.js"></script>
    <script src="assets/code/Venda.js" type="text/javascript"></script>
    <link rel="stylesheet" href="assets/css/jquery.gritter.css" />
    <script src="assets/js/jquery.gritter.min.js"></script>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="page-content">
        <div class="row">
            <div class="col-xs-12">
                <h3 class="header smaller lighter blue">
                    <i class="icon-male"></i>Informações gerais<a id="btnMapa" class="btn btn-sm btn-primary pull-right inline">
                        <i class="icon-bar-chart">&nbsp Fechamento diario</i> </a>
                        <a id="btnEstorno" class="btn btn-sm btn-danger pull-right inline">
                        <i class="icon-backward">&nbsp Estorno</i> </a>
                </h3>
                <div class="row">
                    <div class="form-horizontal col-xs-8">
                        <div class="form-group">
                            <label class="col-sm-1 control-label no-padding-right" for="form-field-1">
                                Data
                            </label>
                            <div class="col-xs-11">
                                <input type="text" id="txtData" title="Data lançamento" class="col-md-2" />
                                <span id="lblLancamentoRetroativo" class="red col-md-3">&nbsp Atenção!! Lançamento retroativo</span>
                            </div>
                        </div>
                        <div class="space-4">
                        </div>
                        <div class="form-group">
                            <label class="col-sm-1 control-label no-padding-right" for="form-field-2">
                                Cliente
                            </label>
                            <div class="col-sm-5">
                                <select class="chosen-select width-40" id="ddCliente" placeholder="Escolha um cliente">
                                </select>
                                <span class="help-inline"><span class="middle"><a href="javascript:$('#ModalCliente').dialog('open');">
                                    Novo cliente</a></span> </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-1 control-label no-padding-right" for="form-field-2">
                                Forma
                            </label>
                            <div class="col-sm-4">
                                <select class="width-40 chosen-select" id="ddPagamento" data-placeholder="Forma de pagamento">
                                    <option value="-1">Selecione...</option>
                                    <option value="Crédito">Crédito</option>
                                    <option value="Débito">Débito</option>
                                    <option value="Dinheiro">Dinheiro</option>
                                    <option value="Cartela">Cartela</option>
                                    <option value="Cheque">Cheque</option>
                                    <option value="Outro">Outro</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-1 control-label no-padding-right" for="form-field-2">
                                Obs
                            </label>
                            <div class="col-sm-9">
                                <div class="clearfix">
                                    <input type="text" name="observacao" class="col-xs-10 col-sm-5" id="txtObservacao" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4" style="height: 190px; overflow-y: auto;">
                        <table id="tbAcessos" style="font-size: 10px; display: none" class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <td>
                                        Ordem
                                    </td>
                                    <td>
                                        Status
                                    </td>
                                    <td>
                                        Nome
                                    </td>
                                    <td>
                                        R$
                                    </td>
                                </tr>
                            </thead>
                            <tbody id="tbBodyAcessos">
                            </tbody>
                        </table>
                    </div>
                </div>
                <h3 class="header smaller lighter blue">
                    <i class="icon-shopping-cart"></i>Produtos <a id="btnNovoProduto" class="btn btn-sm btn-success pull-right inline">
                        <i class="icon-plus">&nbsp Novo produto</i> </a>
                </h3>
                <div id="divProdutos" class="center">
                    <h3 class="green">
                        Inclua produtos!</h3>
                    Clique no botão novo produto para escolher o produto a ser vendido.
                </div>
                <div id="divProdutosAdicionados" style="display: none" class="table-responsive">
                    <table id="tbProdutos" class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th class="hidden">
                                </th>
                                <th>
                                    Produto
                                </th>
                                <th>
                                    Preço (R$)
                                </th>
                                <th>
                                    Desconto (%)
                                </th>
                                <th>
                                    Preço com desconto (R$)
                                </th>
                                <th>
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tbProdutosBody">
                        </tbody>
                    </table>
                </div>
                <!-- /.table-responsive -->
                <div id="divSalvar" style="display: none" class="pull-right">
                    <h2>
                        Valor Total: <a id="lblPrecoTotal"></a>
                    </h2>
                    <br />
                    <input id="btnSalvar" type="button" class="btn btn-info" value="Salvar" />
                </div>
            </div>
            <!-- /span -->
        </div>
        <div id="ModalProduto" style="display: none">
            <div class="page-content">
                <div class="page-header">
                    <h1>
                        Adicionar produto</h1>
                </div>
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                            Produto
                        </label>
                        <div class="col-sm-9">
                            <select class="chosen-select width-80" id="ddProduto" data-placeholder="Produto">
                                <option value="-1">Selecione...</option>
                            </select>
                            <span class="help-button" id="helpProduto" data-rel="popover" data-trigger="hover"
                                data-placement="left" data-content="More details." title="Popover on hover">!</span>
                            <%--<span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" title="Por favor, selecione o produto">!</span>--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                            Desconto
                        </label>
                        <div class="col-sm-9">
                            <input type="text" class="input" value="0" id="lblDesconto" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                            Qtd
                        </label>
                        <div class="col-sm-9">
                            <input type="text" class="input" value="0" id="lblQtd" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                            Preço (R$)
                        </label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblPreco" Text="0,00" Font-Size="Large" runat="server"></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                            Preço com desconto (R$)
                        </label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblPrecoDesconto" Text="0,00" Font-Size="Large" runat="server"></asp:Label></a>
                        </div>
                    </div>
                    <div class="clearfix form-actions">
                        <div class="col-md-offset-2 col-md-9">
                            <button onclick="CloseModal();" class="btn" type="reset">
                                <i class="icon-undo bigger-110"></i>Cancelar
                            </button>
                            &nbsp; &nbsp; &nbsp;
                            <button id="btnAddProduto" class="btn btn-success" type="button">
                                <i class="bigger-110"></i>Salvar
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ModalCaixa" style="display:none">
          <div class="page-content">
                <div class="page-header">
                    <h1>
                       Abertura de caixa</h1>
                </div>
                 <form id="formCaixa" class="form-horizontal">
                    <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                        Valor em caixa:
                    </label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="text" id="txtValorCaixa" name="valorcaixa" class="input width-60" />
                          <a id="btnAbrirCaixa" class="btn btn-sm btn-success inline">
                        <i class="icon-briefcase">&nbsp Abrir caixa</i> </a>
                        </div>
                    </div>
                </div>
                </form>
        
        </div>
        </div>
        <div id="ModalCliente" style="display: none">
            <div class="page-content">
                <div class="page-header">
                    <h1>
                        Novo Cliente</h1>
                </div>
                <form id="formCadastro" class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                        Nome
                    </label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="text" id="nome" name="nome" class="input width-80" />
                        </div>
                    </div>
                </div>
                <div class="space-4">
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                        Email
                    </label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="text" name="email" id="email" class="input width-80" />
                        </div>
                    </div>
                </div>
                <div class="space-4">
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                        Telefone
                    </label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="text" name="telefone" id="telefone" class="input width-80" value="0" />
                        </div>
                    </div>
                </div>
                <div class="space-4">
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                        Tipo
                    </label>
                    <div class="col-sm-9">
                        <select class="chosen-select" id="ddTipo" placeholder="Escolha um cliente">
                            <option value="Novo Cliente">Novo Cliente</option>
                            <option value="Indicação">Indicação</option>
                            <option value="Novo Distribuidor">Novo Distribuidor</option>
                        </select>
                    </div>
                </div>
                <div class="space-4">
                </div>
                <div id="divIndicacao" style="display: none" class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                        Indicação
                    </label>
                    <div class="col-sm-9">
                        <select class="width-40 chosen-select" name="indicacao" id="ddClienteIndicacao" placeholder="Escolha um cliente">
                            <option value=""></option>
                        </select>
                    </div>
                </div>
                <div class="space-4">
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="form-field-2">
                        Como soube
                    </label>
                    <div class="col-sm-9">
                        <select class="width-40 chosen-select" id="ddComoSoube" placeholder="Escolha um cliente">
                            <option value="Folheto">Folheto</option>
                            <option value="Internet">Internet</option>
                            <option value="Outro meio">Outro meio</option>
                        </select>
                    </div>
                </div>
                <div class="space-4">
                </div>
                <div class="clearfix form-actions">
                    <div class="col-md-offset-2 col-md-9">
                        <button onclick="CloseClienteModal();" class="btn" type="reset">
                            <i class="icon-undo bigger-110"></i>Cancelar
                        </button>
                        &nbsp; &nbsp; &nbsp;
                        <button id="btnAdicionarCliente" class="btn btn-success" type="button">
                            <i class="bigger-110"></i>Salvar
                        </button>
                    </div>
                </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
