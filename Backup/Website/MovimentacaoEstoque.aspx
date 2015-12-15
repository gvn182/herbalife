<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="MovimentacaoEstoque.aspx.cs" Inherits="Front.MovimentacaoEstoque"
    EnableEventValidation="false" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="assets/code/MovimentacaoEstoque.js" type="text/javascript"></script>
    <div class="page-header">
        <h1>
            Relatorio <small><i class="icon-double-angle-right"></i>Movimentação estoque</small>
        </h1>
    </div>
    <div class="col-xs-12">
        <div class="form-horizontal">
            <div class="form-group">
                <label class="col-sm-1 control-label no-padding-right" for="form-field-1">
                    Data
                </label>
                <div class="col-xs-11">
                    <span class="input-icon">
                        <input id="txtDataInicialFinal" runat="server" type="text" placeholder="Pesquisar ..."
                            class="nav-search-input input-large" name="date-range-picker" autocomplete="off" />
                        <i class="icon-calendar nav-search-icon"></i></span>
                </div>
            </div>
            <div class="space-4">
            </div>
            <div class="form-group">
                <label class="col-sm-1 control-label no-padding-right" for="form-field-2">
                    Produto
                </label>
                <div class="col-sm-5">
                    <select class="chosen-select width-40" id="ddProduto" runat="server">
                    </select>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-1 control-label no-padding-right" for="form-field-2">
                    Status
                </label>
                <div class="col-sm-5">
                    <asp:DropDownList ID="ddFiltro" CssClass="select2" runat="server">
                        <asp:ListItem Value="-1">Todos</asp:ListItem>
                        <asp:ListItem Value="0">Adição</asp:ListItem>
                        <asp:ListItem Value="1">Remoção</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-1 control-label no-padding-right" for="form-field-2">
                </label>
                <div class="col-sm-7 pull-right">
                    <asp:Button ID="btnPesquisar" runat="server" OnClick="BtnPesquisar_Click" class="btn-small btn-success"
                        Text="Pesquisar" />
                </div>
            </div>
        </div>
        
        <asp:Button OnClick="btnExcel_Click" ID="btnExcel" CssClass="pull-right inline btn-medium btn-success" runat="server" Text="Exportar excel" Visible="false" />
        <div class="table-responsive">
            <asp:GridView ID="GdrPesquisa" CssClass="table table-striped table-bordered table-hover"
                runat="server" OnDataBound="GdrPesquisa_DataBound" OnRowDataBound="GdrPesquisa_RowDataBound">
                <EmptyDataTemplate>
                    <center>
                        <h3>
                            Não existe registros de vendas para o periodo informado.</h3>
                    </center>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
