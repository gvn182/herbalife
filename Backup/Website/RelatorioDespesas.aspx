<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="RelatorioDespesas.aspx.cs" Inherits="Front.RelatorioDespesas" EnableEventValidation="false" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="assets/code/RelatorioDespesas.js" type="text/javascript"></script>
    <div class="page-header">
        <h1>
            Relatorio <small><i class="icon-double-angle-right"></i>Despesas</small>
        </h1>
    </div>
    <div class="col-xs-12">
        <div class="filtering">
            <span class="input-icon">
                <input id="txtDataInicialFinal" runat="server" type="text" placeholder="Pesquisar ..."
                    class="nav-search-input input-large" name="date-range-picker" autocomplete="off" />
                <i class="icon-calendar nav-search-icon"></i></span>
            <asp:Button ID="btnPesquisar" runat="server" OnClick="BtnPesquisar_Click" class="btn-small btn-success"
                Text="Pesquisar" />
        </div>
        <asp:Button OnClick="btnExcel_Click" ID="btnExcel" CssClass="pull-right inline btn-medium btn-success" runat="server" Text="Exportar excel" Visible="false" />
        <div class="table-responsive">
            <asp:GridView ID="GdrPesquisa" CssClass="table table-striped table-bordered table-hover"
                runat="server" OnDataBound="GdrPesquisa_DataBound" >
                <EmptyDataTemplate>
                    <center>
                        <h3>
                            Não existe registros de despesas para o periodo informado.</h3>
                    </center>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
