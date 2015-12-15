//  Criado por: Edson Henrique
//  Site: www.edsonhenrique.com.br
//  Email: meuemail@edsonhenrique.com.br



namespace Uol.PagSeguro.Resources
{
    /// <summary>
    /// Classe que substitui o XML, contendo as informações de configuração.
    /// </summary>
    public static class PagSeguroConfig
    {
        /// <summary>
        /// Account Credential Email
        /// </summary>
        public const string AccountCredentialEmail = "";

        /// <summary>
        /// Account Credential Token
        /// </summary>
        public const string AccountCredentialToken = "";

        /// <summary>
        /// URL de Envio de Configuração de Pagamento
        /// </summary>
        //public const string PaymentLink = "https://ws.sandbox.pagseguro.uol.com.br/v2/checkout";
        
        ///// <summary>
        ///// URL de Acesso para pagamento de Pagamento, ?code= + codigo gerado
        ///// </summary>
        //public const string PaymentRedirectLink = "https://sandbox.pagseguro.uol.com.br/v2/checkout/payment.html";
        
        ///// <summary>
        ///// URL de Acesso a dados de Pagamento já cadastrado
        ///// </summary>
        //public const string NotificationLink = "https://ws.sandbox.pagseguro.uol.com.br/v2/transactions/notifications";

        public const string PaymentLink = "https://ws.pagseguro.uol.com.br/v2/checkout";

        /// <summary>
        /// URL de Acesso para pagamento de Pagamento, ?code= + codigo gerado
        /// </summary>
        public const string PaymentRedirectLink = "https://pagseguro.uol.com.br/v2/checkout/payment.html";

        /// <summary>
        /// URL de Acesso a dados de Pagamento já cadastrado
        /// </summary>
        public const string NotificationLink = "https://ws.pagseguro.uol.com.br/v2/transactions/notifications";
        
        /// <summary>
        /// URL de acesso a dados de Pagamentos por Range de data.
        /// </summary>
        public const string Search = "https://ws.pagseguro.uol.com.br/v2/transactions";

        /// <summary>
        /// Versão da Biblioteca
        /// </summary>
        public const string LibVersion = "2.0.3";

        /// <summary>
        /// URL Encode
        /// </summary>
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";

        /// <summary>
        /// Encoding Type
        /// </summary>
        public const string Encoding = "ISO-8859-1";

        /// <summary>
        /// Tempo limite para Requisição
        /// </summary>
        public const int RequestTimeout = 10000;
    }
}
