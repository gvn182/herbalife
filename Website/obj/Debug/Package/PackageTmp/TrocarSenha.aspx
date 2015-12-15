<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="TrocarSenha.aspx.cs" Inherits="Front.TrocarSenha" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="assets/code/TrocarSenha.js" type="text/javascript"></script>
    <div class="page-content">
        <div class="page-header">
            <h1>
                Alteração de senha <small><i class="icon-double-angle-right"></i>Efetue a alteração da sua senha
                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div id="formPrincipal" class="col-xs-12">
             <form class="form-horizontal" id="formTrocarSenha">
                  <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
                </asp:ScriptManager>
              <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtNome">
                        Senha atual
                    </label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="password" name="senhaatual"  id="txtSenhaAtual" class="col-xs-10 col-sm-5" />
                        </div>
                    </div>
                </div>
                 <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtNome">
                        Nova senha
                    </label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="password" name="nova" id="nova" class="col-xs-10 col-sm-5" />
                        </div>
                    </div>
                </div>
                 <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right" for="txtNome">
                        Confirmação
                    </label>
                    <div class="col-sm-9">
                        <div class="clearfix">
                            <input type="password" name="nova2" id="nova2" class="col-xs-10 col-sm-5" />
                        </div>
                    </div>
                </div>
                   <div class="clearfix form-actions">
                    <div class="col-md-offset-3 col-md-9">
                        <button class="btn" type="reset">
                            <i class="icon-undo bigger-110"></i>Limpar
                        </button>
                        &nbsp; &nbsp; &nbsp;
                         <input id="btnSalvar" type="button" value="Salvar" class="btn btn-info" />
                    </div>
                </div>
                <div class="hr hr-24">
                </div>
             </form>
            </div>
        </div>
    </div>
</asp:Content>
