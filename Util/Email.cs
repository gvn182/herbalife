using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace Util
{
    public class Email
    {
        public void SendEmail(String WebConfigParameter, String To, String Titulo, String Mensagem)
        {

            SmtpClient client = new SmtpClient();
            String From = ConfigurationManager.AppSettings[WebConfigParameter + "From"].ToString();
            String Pass = ConfigurationManager.AppSettings[WebConfigParameter + "Senha"].ToString();
            String SMTP = ConfigurationManager.AppSettings["SMTP"].ToString();
            String PortaSMTP = ConfigurationManager.AppSettings["PortaSMTP"].ToString();
            client.Credentials = new System.Net.NetworkCredential(From, Pass);
            MailMessage mail = new MailMessage(From, To);

            client.Port = int.Parse(PortaSMTP);
            client.Host = SMTP;
            mail.Subject = Titulo;
            mail.Body = Mensagem;
            mail.IsBodyHtml = true;
            client.Send(mail);

        }
    }
}
