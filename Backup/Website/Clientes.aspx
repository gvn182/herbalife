﻿<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="Clientes.aspx.cs" Inherits="Front.Clientes" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="assets/css/jquery-ui-1.10.3.full.min.css" rel="stylesheet" type="text/css" />
    <script src="assets/js/json2.js" type="text/javascript"></script>
    <script src="assets/js/jquery.jtable.js" type="text/javascript"></script>
    <script src="assets/js/jquery.jtable.pt-BR.js" type="text/javascript"></script>
    <script src="assets/js/jquery.jtable.aspnetpagemethods.js" type="text/javascript"></script>
    <link href="assets/js/validationEngine/validationEngine.jquery.css" rel="stylesheet"
        type="text/css" />
    <script src="assets/js/validationEngine/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="assets/js/validationEngine/jquery.validationEngine-pt.js" type="text/javascript"></script>
    <script src="assets/code/Cliente.js" type="text/javascript"></script>
    <link href="assets/css/themes/lightcolor/blue/jtable.css" rel="stylesheet" type="text/css" />
    <div class="page-header">
      <h1>
            Clientes <small><i class="icon-double-angle-right"></i> disponibilizados para vendas de produtos e acessos</small>
        </h1>
    </div>
    <div class="page-content">
        <div class="row">
            <div class="filtering">
                <form>
                <span class="input-icon">
                    <input id="filtro" type="text" placeholder="Pesquisar ..." class="nav-search-input"
                        id="nav-search-input" autocomplete="off" />
                    <i class="icon-search nav-search-icon"></i></span>
                <button id="btnPesquisar" class="btn-small btn-success">
                    <i class="icon-ok"></i>Pesquisar
                </button>
                </form>
            </div>
            <div id="TableClientes">
            </div>
        </div>
    </div>
</asp:Content>
