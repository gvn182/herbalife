using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Constants;
using DAL;
using Util;

namespace BLL
{
    public class PagamentoBLL
    {
        //AccountCredentials credentials = new AccountCredentials("gvn182@gmail.com", "2FDEAB08184642F3B841250B6CD09511");
        AccountCredentials credentials = new AccountCredentials(PSeguroUtil.Email, PSeguroUtil.Token);
        
        TransacaoDAL TransDAL = new TransacaoDAL();
        UsuarioBLL UserBLL = new UsuarioBLL();

        public Uri NovoPagamento(TB_USUARIO CurrentUser, int Meses, String Valor)
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Items.Add(new Item("0001", "Assinatura HBMAX " + Meses + " Meses", 1, Convert.ToDecimal(Valor)));
            payment.Reference = CurrentUser.ID.ToString() + "," + Meses;

            payment.Shipping = new Shipping();
            payment.Shipping.ShippingType = ShippingType.NotSpecified;
            //payment.RedirectUri = new Uri("hbmax.com/PagSeguro.aspx?Novo=" + CurrentUser.ID);
            Uri paymentRedirectUri = payment.Register(credentials);
            return paymentRedirectUri;
        }

        public void NovaTransacao(Transaction Trans)
        {
            String ID = Trans.Reference.Split(',')[0];
            int Meses = int.Parse(Trans.Reference.Split(',')[1]);
            TransDAL.AddNew(DateTime.Now, Trans.Code, Trans.Reference, Trans.TransactionStatus);
            if (Trans.TransactionStatus == 3) //Pago
            {
                UserBLL.LiberaSistema(int.Parse(ID), Meses);
            }
        }
    }
}
