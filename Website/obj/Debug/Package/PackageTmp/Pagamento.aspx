<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="Pagamento.aspx.cs" Inherits="Front.Pagamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="assets/js/jquery.gritter.min.js"></script>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="page-header">
        <h1>
            Assinatura <small><i class="icon-double-angle-right"></i>Renovar assinatura</small>
        </h1>
    </div>
    <div class="row">
        <div class="col-xs-6 col-sm-3 pricing-box">
            <div class="widget-box">
                <div class="widget-header header-color-dark">
                    <h5 class="bigger lighter">
                        1 Mês</h5>
                </div>
                <div class="widget-body">
                    <div class="widget-main">
                        <ul class="list-unstyled spaced2">
                            <li><i class="icon-ok green"></i>Acesso Total ao sistema </li>
                            <li><i class="icon-ok green"></i>1 Mês de acesso ao sistema </li>
                            <li><i class="icon-ok green"></i>0% de desconto </li>
                        </ul>
                        <hr />
                        <div class="price">
                            R$ 39,90 <small></small>
                        </div>
                    </div>
                    <div>
                        <button id="btnUmMes" runat="server" type="button" onserverclick="btnUmMes_Click" class="btn btn-block btn-inverse"><i class="icon-shopping-cart bigger-110">
                        </i><span>Pagar</span> </button>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 pricing-box">
            <div class="widget-box">
                <div class="widget-header header-color-orange">
                    <h5 class="bigger lighter">
                        3 Mêses</h5>
                </div>
                <div class="widget-body">
                    <div class="widget-main">
                        <ul class="list-unstyled spaced2">
                            <li><i class="icon-ok green"></i>Acesso Total ao sistema </li>
                            <i class="icon-ok green"></i>3 Mêses de acesso ao sistema </li>
                            <li><i class="icon-ok green"></i>5% de desconto </li>
                        </ul>
                        <hr />
                        <div class="price">
                            R$ 113,70
                        </div>
                    </div>
                    <div>
                        <button id="btnTresMeses" runat="server" type="button" onserverclick="btnTresMeses_Click" class="btn btn-block btn-warning"><i class="icon-shopping-cart bigger-110">
                        </i><span>Pagar</span> </button>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 pricing-box">
            <div class="widget-box">
                <div class="widget-header header-color-blue">
                    <h5 class="bigger lighter">
                        6 Mêses</h5>
                </div>
                <div class="widget-body">
                    <div class="widget-main">
                        <ul class="list-unstyled spaced2">
                            <li><i class="icon-ok green"></i>Acesso Total ao sistema </li>
                            <i class="icon-ok green"></i>6 Mêses de acesso ao sistema </li>
                            <li><i class="icon-ok green"></i>10% de desconto </li>
                        </ul>
                        <hr />
                        <div class="price">
                            R$ 215,45
                        </div>
                    </div>
                    <div>
                         <button id="btnSeisMeses" runat="server" type="button" onserverclick="btnSeisMeses_Click" class="btn btn-block btn-primary"><i class="icon-shopping-cart bigger-110">
                        </i><span>Pagar</span> </button>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 pricing-box">
            <div class="widget-box">
                <div class="widget-header header-color-green">
                    <h5 class="bigger lighter">
                        1 Ano</h5>
                </div>
                <div class="widget-body">
                    <div class="widget-main">
                        <ul class="list-unstyled spaced2">
                            <li><i class="icon-ok green"></i>Acesso Total ao sistema </li>
                            <i class="icon-ok green"></i>1 Ano de acesso ao sistema </li>
                            <li><i class="icon-ok green"></i>15% de desconto </li>
                        </ul>
                        <hr />
                        <div class="price">
                            R$ 406,90
                        </div>
                    </div>
                    <div>
                        <button id="btnUmAno" runat="server" type="button" onserverclick="btnUmAno_Click" class="btn btn-block btn-success"><i class="icon-shopping-cart bigger-110">
                        </i><span>Pagar</span> </button>
                    </div>
                </div>
            </div>
        </div>
        <!-- INICIO CODIGO PAGSEGURO -->
        <br />
        <br />
<center>
<a href='https://pagseguro.uol.com.br' target='_blank'><img alt='Logotipos de meios de pagamento do PagSeguro' src='https://p.simg.uol.com.br/out/pagseguro/i/banners/pagamento/todos_estatico_550_100.gif' title='Este site aceita pagamentos com Visa, MasterCard, Diners, American Express, Hipercard, Aura, Elo, PLENOCard, PersonalCard, BrasilCard, FORTBRASIL, Cabal, Mais!, Avista, Grandcard, Sorocred, Bradesco, Itaú, Banco do Brasil, Banrisul, Banco HSBC, saldo em conta PagSeguro e boleto.' border='0'></a>
</center>
<!-- FINAL CODIGO PAGSEGURO -->
        </div>
</asp:Content>
