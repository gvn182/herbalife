using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uol.PagSeguro.Service;
using Uol.PagSeguro.Domain;
using BLL;
using Util;

namespace Front
{
    public partial class Integracao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PagamentoBLL BLLPagamento = new PagamentoBLL();
            //Response.AppendHeader("Access-Control-Allow-Origin", "https://sandbox.pagseguro.uol.com.br");
            String NotCode = Request.Form["notificationCode"];
            //AccountCredentials credentials = new AccountCredentials("gvn182@gmail.com", "2FDEAB08184642F3B841250B6CD09511");
            AccountCredentials credentials = new AccountCredentials(PSeguroUtil.Email, PSeguroUtil.Token);
            
            if (NotCode != null)
            {
                Transaction Trans = NotificationService.CheckTransaction(credentials, NotCode);
                BLLPagamento.NovaTransacao(Trans);
            }
        }
    }
}