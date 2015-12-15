<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Estoque.aspx.cs" ClientIDMode="Static"
    Inherits="Front.Estoque" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="assets/code/Estoque.js" type="text/javascript"></script>
    <div class="page-header">
        <h1>
            Estoque <small><i class="icon-double-angle-right"></i>Movimentação manual de estoque.</small>
        </h1>
    </div>
    <div class="page-content">
        <div class="row">
            <div class="col-xs-12">
                <div class="filtering">
                    <asp:HiddenField ID="HdnSelectedID" runat="server" />
                    <span class="input-icon">
                        <input id="txtFiltro" runat="server" type="text" placeholder="Pesquisar ..." class="nav-search-input"
                            name="nav-search-input" autocomplete="off" />
                        <i class="icon-search nav-search-icon"></i></span>
                    <button type="button" id="BtnPesquisar" runat="server" onserverclick="BtnPesquisar_Click"
                        class="btn-small btn-success">
                        <i class="icon-ok"></i>Pesquisar
                    </button>
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
                                    <asp:LinkButton ID="btnHistorico" CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="Historico" Style="font-size: 20px; text-decoration: none; cursor: pointer"
                                        OnClick="OnItemClick" runat="server">
                                <i class="icon-eye-open" title="Verificar movimentaçãoes"></i>
                                    </asp:LinkButton>
                                
                                
                                    <asp:LinkButton ID="btnAdicionar" CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="Adicionar" Style="font-size: 20px; text-decoration: none; color: Green;
                                        cursor: pointer" OnClick="OnItemClick" runat="server">
                                <i class="icon-plus" title="Adicionar quantidade ao estoque"></i>
                                    </asp:LinkButton>
                                
                                    <asp:LinkButton ID="btnRemover" CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="Remover" Style="font-size: 20px; text-decoration: none; color: Red;
                                        cursor: pointer" OnClick="OnItemClick" runat="server">
                                <i class="icon-minus" title="Remover quantidade produto"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div id="ModalAdicionar">
            <div class="page-content">
                <div class="page-header">
                    <h1 style="color: Green">
                        Aumentar estoque</h1>
                </div>
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Codigo:</label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblCodigoProdutoAdicionar" Font-Size="Large" runat="server" Text="0020"></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Produto:</label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblProdutoAdicionar" Font-Size="Large" runat="server" Text="Shake Baunilha Teste"></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Qtd. atual:</label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblQtdAtualAdicionar" Font-Size="Large" runat="server" Text="15"></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Qtd. após cadastro:</label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblQtdAposCadastroAdicionar" Font-Size="Large" runat="server" Text=""></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Qtd.:</label>
                        <div class="col-sm-9">
                            <input type="text" id="txtQtdAdicionar" runat="server" class="input-mini" />
                        </div>
                    </div>
                    <div class="clearfix form-actions">
                        <div class="col-md-offset-2
    col-md-9">
                            <button onclick="CloseModal();" class="btn" type="reset">
                                <i class="icon-undo
    bigger-110"></i>Cancelar
                            </button>
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnSalvarAdicionar" OnClick="btnSalvarAdicionar_Click" runat="server"
                                class="btn btn-info" Text="Salvar" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ModalRemover">
            <div class="page-content">
                <div class="page-header">
                    <h1 style="color: Red">
                        Diminuir estoque</h1>
                </div>
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-sm-3 control-label
    no-padding-right" for="txtCEP">
                            Codigo:</label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblCodigoProdutoRemover" Font-Size="Large" runat="server" Text="0020"></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Produto:</label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblProdutoRemover" Font-Size="Large" runat="server" Text="Shake Baunilha Teste"></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Qtd. atual:</label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblQtdAtualRemover" Font-Size="Large" runat="server" Text="15"></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Qtd. após cadastro:</label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblQtdAposCadastroRemover" runat="server" Font-Size="Large"></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Qtd.:*</label>
                        <div class="col-sm-9">
                            <input type="text" id="txtQtdRemover" runat="server" class="input-mini" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Motivo.:*</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddMotivo" runat="server">
                                <asp:ListItem Text="Consumo EVS" Value="Consumo EVS"></asp:ListItem>
                                <asp:ListItem Text="Perda" Value="Consumo EVS"></asp:ListItem>
                                <asp:ListItem Text="Uso
    pessoal" Value="Consumo EVS"></asp:ListItem>
                                <asp:ListItem Text="Acerto de invetário" Value="Consumo EVS"></asp:ListItem>
                                <asp:ListItem Text="Outro" Value="Consumo EVS"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix form-actions">
                        <div class="col-md-offset-2
    col-md-9">
                            <button onclick="CloseModal();" class="btn" type="reset">
                                <i class="icon-undo
    bigger-110"></i>Cancelar
                            </button>
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnSalvarRemover" OnClick="btnSalvarRemover_Click" runat="server"
                                class="btn btn-info" Text="Salvar" />
                        </div>
                    </div>
                </div>
                <%--</form>--%>
            </div>
        </div>
        <div id="ModalHistorico">
            <div class="pull-right inline" style="font-size: 22px">
                <i onclick="CloseModal();" style="cursor: pointer" class="icon-remove"></i>
            </div>
            <div class="page-content">
                <div class="page-header">
                    <h1>
                        Historico produto</h1>
                </div>
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Codigo:</label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblCodigoHistorico" Font-Size="Large" runat="server" Text="0020"></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Produto:</label>
                        <div class="col-sm-9">
                            <a>
                                <asp:Label ID="lblProdutoHistorico" Font-Size="Large" runat="server" Text="Shake Baunilha Teste"></asp:Label></a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Periodo:</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtDataInicialFinal" class="form-control" name="date-range-picker"
                                runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="txtCEP">
                            Filtro:</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddFiltro" CssClass="select2" runat="server">
                                <asp:ListItem Value="-1">Todos</asp:ListItem>
                                <asp:ListItem Value="0">Adição</asp:ListItem>
                                <asp:ListItem Value="1">Remoção</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnPesquisarHistorico" runat="server" class="btn-mini
    btn-info" Text="Pesquisar" OnClick="btnPesquisarHistorico_Click" />
                        </div>
                    </div>
                    <hr />
                    <div class="table-responsive">
                        <asp:GridView ID="GdrHistorico" CssClass="table
    table-striped table-bordered table-hover" runat="server" OnDataBound="GdrHistorico_DataBound">
                            <EmptyDataTemplate>
                                <h3>
                                    Não existe historico para o filtro informado</h3>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <%--</form>--%>
            </div>
        </div>
    </div>
</asp:Content>
