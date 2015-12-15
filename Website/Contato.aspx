<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contato.aspx.cs" Inherits="Front.Contato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contato</title>
    <meta charset="utf-8">
    <meta name="format-detection" content="telephone=no" />
    <link rel="icon" href="images/favicon.ico">
    <link rel="shortcut icon" href="images/favicon.ico" />
    <link rel="stylesheet" href="css/form.css">
    <link rel="stylesheet" href="css/style.css">
    <script src="js/jquery.js"></script>
    <script src="js/jquery-migrate-1.2.1.js"></script>
    <script src="js/script.js"></script>
    <script src="js/superfish.js"></script>
    <script src="js/TMForm.js"></script>
    <script src="js/jquery.ui.totop.js"></script>
    <script src="js/jquery.equalheights.js"></script>
    <script src="js/jquery.mobilemenu.js"></script>
    <script src="js/jquery.easing.1.3.js"></script>
    <!--[if lt IE 8]>
       <div style=' clear: both; text-align:center; position: relative;'>
         <a href="http://windows.microsoft.com/en-US/internet-explorer/products/ie/home?ocid=ie6_countdown_bannercode">
           <img src="http://storage.ie6countdown.com/assets/100/images/banners/warning_bar_0000_us.jpg" border="0" height="42" width="820" alt="You are using an outdated browser. For a faster, safer browsing experience, upgrade for free today." />
         </a>
      </div>
      <![endif]-->
    <!--[if lt IE 9]>
      <script src="js/html5shiv.js"></script>
      <link rel="stylesheet" media="screen" href="css/ie.css">


    <![endif]-->
    <script>
        $(document).ready(function () {
            $().UItoTop({ easingType: 'easeOutQuart' });
        });
    </script>
</head>
<body runat="server">
    <div class="main_color">
        <!--==============================header=================================-->
        <div class="container_12">
            <div class="grid_12">
                <header>
        <h1>
        <a href="index.html">
          <img src="images/image002.png" alt="HBMAX">
        </a>
      </h1>
<div class="menu_block ">
  <nav class="horizontal-nav full-width horizontalNav-notprocessed">
    <ul class="sf-menu">
         <li><a href="index.html">Home</a></li>
         <li><a href="http://hbmax.com/Login.aspx" target="_blank">Cadastre-se</a></li>
         <!--<li><a href="http://hbmax.com/Login.aspx">Login</a></li>-->
         <li class="current"><a href="#">Contato</a></li>
       </ul>
    </nav>
   <div class="clear"></div>
</div>
<div class="clear"></div>
</header>
            </div>
        </div>
        <!--==============================Content=================================-->
        <div class="content">
            <div class="container_12">
                <div class="grid_6">
                    <h3>
                        Entre em contato</h3>
                    <form id="form" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
                    </asp:ScriptManager>
                    <div class="success_wrapper">
                        <div class="success-message">
                            Email enviado com sucesso</div>
                    </div>
                    <label class="name">
                        <input type="text" placeholder="Nome*:" data-constraints="@Required @JustLetters" />
                        <span class="empty-message">* É necessário preencher seu nome.</span> <span class="error-message">
                            * Preencha um nome válido.</span>
                    </label>
                    <label class="email">
                        <input type="text" placeholder="E-mail*:" data-constraints="@Required @Email" />
                        <span class="empty-message">* Preencha seu email</span> <span class="error-message">
                            * Preencha um email válido.</span>
                    </label>
                    <label class="message">
                        <textarea placeholder="Mensagem*:" data-constraints="@Required"></textarea>
                        <span class="empty-message">* É necessário preencher a descrição.</span>
                    </label>
                    <div>
                        <div class="clear">
                        </div>
                        <div class="btns">
                            <a href="#" data-type="reset" class="btn">Limpar</a> <a href="#" data-type="submit"
                                class="btn">Enviar</a></div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
        <!--==============================footer=================================-->
    </div>
    <footer>   
    <div class="container_12">
      <div class="grid_12">
     <!-- <div class="socials">
          <a href="#"></a>
          <a href="#" class="rss"></a>
          <a href="#" class="tw"></a>
          <a href="#" class="gp"></a>
        </div>-->
        <div class="copy">
        <strong>HBMAX  </strong>  &copy; <span id="copyright-year"></span> |  
        <a href="politica/Política%20de%20privacidade.pdf" target="_blank">Politica de privacidade</a> Sistema para Distribuidores Independentes Herbalife. <br /> Não possui nenhum vínculo com a Herbalife no Brasil ou no mundo. Todos os direitos reservados.
        
          
        </div>
      </div>
    </div>  
</footer>
</body>
</html>
