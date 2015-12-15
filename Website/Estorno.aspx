<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Estorno.aspx.cs" ClientIDMode="Static"
    Inherits="Front.Estorno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="assets/code/Estorno.js" type="text/javascript"></script>
    <div class="page-header">
        <h1>
            Estorno <small><i class="icon-double-angle-right"></i>Cancelamento de vendas.</small>
        </h1>
    </div>
    <div class="page-content">
        <div class="row">
            <div class="col-xs-12">
                <div class="filtering">
                    <span class="input-icon">
                       <input id="txtFiltro" runat="server" type="text" placeholder="Pesquisar ..."
                            class="nav-search-input input-large" name="date-range-picker" autocomplete="off" />
                        <i class="icon-search nav-search-icon"></i></span>
                             <asp:Button ID="BtnPesquisar" runat="server" OnClick="BtnPesquisar_Click" class="btn-small btn-success"  Text="Pesquisar" />
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GdrPesquisa" CssClass="table table-striped table-bordered table-hover"
                        runat="server" AllowPaging="True" AllowSorting="True" OnDataBound="GdrPesquisa_DataBound"
                        OnPageIndexChanging="GdrPesquisa_PageIndexChanging" OnSorting="GdrConteudo_Sorting"
                        PageSize="50">
                        <Columns>
                            <asp:TemplateField HeaderText="Ações" HeaderStyle-ForeColor="#438EB9" HeaderStyle-Font-Bold="true"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEstornar" CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="Estorno" OnClientClick="if(confirm('Tem certeza que deseja estornar a venda?')) return true; else return false;" Style="font-size: 20px; text-decoration: none; cursor: pointer"
                                        OnClick="OnItemClick" runat="server"><i class="icon-backward" title="Estornar"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
