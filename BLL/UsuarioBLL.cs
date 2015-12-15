using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Util;

namespace BLL
{
    public class UsuarioBLL
    {
        ProdutoBLL ProdutoBLL = new ProdutoBLL();
        UsuarioDAL DAL = new UsuarioDAL();
        Email EmailUtl = new Email();
        EnderecoDAL EnderecoDAL = new EnderecoDAL();
        SecurityUTL SecUtl = new SecurityUTL();
        
        public int Add(String Usuario, String Senha, String Email, String Telefone)
        {
            TB_USUARIO Usr = DAL.GetBy(Usuario);
            if (Usr == null)
            {
                int ID_User = DAL.Add(new TB_USUARIO
                  {
                      Data_Criacao = DateTime.Now,
                      Email = Email,
                      Email_Confirmado = false,
                      Login = Usuario,
                      Senha = Senha.GetHashCode().ToString(),
                      Telefone = Telefone,
                      Trocar_Senha = false
                  });
                EnviaEmailConfirmacao(ID_User, Email);
                return ID_User;
            }
            else if (!Usr.Email_Confirmado)
            {
                EnviaEmailConfirmacao(Usr.ID, Usr.Email);
                return -1;
            }
            return -2;
        }
        public void ReenviaConfirmacao(TB_USUARIO Usr)
        {
            EnviaEmailConfirmacao(Usr.ID, Usr.Email);
        }
        private void EnviaEmailConfirmacao(int ID_User, String Email)
        {
            String Mensagem = @"<img src='www.hbmax.com/assets/img/logo.png' /> <br/>
                                <h2>Olá, parabéns pela escolha do Sistema Inteligente de Gestão Vida Saudável. <br/> 
                                Por favor, clique no link abaixo para confirmar seu e-mail, concluir o seu cadastro e começar a </br> utilizar os recursos: <br/>
                                http://www.hbmax.com/Cadastro.aspx?Confirmacao=" + SecUtl.Encrypting(ID_User.ToString()) + @"<br/><br/>
                                Agradecemos seu interesse. Seja bem vindo! </h2><br/><br/>
                                <h2><bold> HBMAX ::: Central de cadastros</bold></h2>";
                    

            EmailUtl.SendEmail("Confirmacao", Email, "Confirmação de cadastro", Mensagem);
        }
        public TB_USUARIO GetBy(String Usuario, String Senha)
        {
            return DAL.GetBy(Usuario.ToUpper(), Senha.GetHashCode().ToString());
        }
        public TB_USUARIO GetBy(int ID_Usuario, String Senha)
        {
            return DAL.GetBy(ID_Usuario, Senha.GetHashCode().ToString());
        }
        public TB_USUARIO GetBy(int ID_Usuario)
        {
            return DAL.GetBy(ID_Usuario);
        }
        public int ChangePass(String Usuario, String Senha, String NovaSenha)
        {
            TB_USUARIO User = DAL.GetBy(Usuario.ToUpper(), Senha.GetHashCode().ToString());
            if (User != null)
            {
                User.Senha = NovaSenha.GetHashCode().ToString();
                DAL.Update();
                return 1;
            }
            return 0;
        }
        public void ValidaEmail(int User_ID)
        {
            TB_USUARIO User = DAL.GetBy(User_ID);
            User.Email_Confirmado = true;
            DAL.Update();
        }
        public TB_ENDERECO GetEndereco(string CEP)
        {
            CEP = CEP.Replace("-", String.Empty);
            return EnderecoDAL.GetEnderecoBy(int.Parse(CEP));
        }
        public void AddDetalhes(String ID_Usuario, String Nome, String CEP, String Endereco, String Numero, String Complemento, String Bairro, String Cidade, String Estado, String RG, String CPF, String Telefone, String Celular, String Nivel)
        {
            TB_USUARIO User = DAL.GetBy(int.Parse(ID_Usuario));
            List<TB_PRODUTO_DEFAULT> ProdutoDefault = ProdutoBLL.GetAllProdutoDefault();

            List<TB_PRODUTO> Produtos = ProdutoDefault.Select(x => new TB_PRODUTO
            {
                COD_HERBALIFE = x.COD_HERBALIFE,
                DESCRICAO = x.DESCRICAO,
                ID_EMPRESA = int.Parse(ID_Usuario),
                PRECO = x.PRECO,
                PV = x.PV,
                ESTOQUE_MINIMO = x.AFETA_ESTOQUE ? 3 : 0,
                QTD_ESTOQUE = 0,
                GASTO_ESTIMADO = GetPrecoEstimado(Nivel, x.PRECO),
                UNID = x.UNID,
                AFETA_ESTOQUE = x.AFETA_ESTOQUE
            }).ToList<TB_PRODUTO>();

            ProdutoBLL.AddList(Produtos);
            DAL.AddDetalhe(new TB_DETALHE_USUARIO
            {
                ID_USUARIO = int.Parse(ID_Usuario),
                Bairro = Bairro,
                Celuar = Celular,
                CEP = CEP.Replace("-", String.Empty),
                Cidade = Cidade,
                Complemento = Complemento,
                CPF = CPF,
                Endereco = Endereco,
                Estado = Estado,
                Nome_Razao = Nome,
                Numero = Numero,
                RG = RG,
                Telefone = Telefone,
                Nivel_Qualificacao = Nivel
            });

            DAL.AddAcesso(new TB_ACESSO { ID_USUARIO = int.Parse(ID_Usuario), DataInicio = DateTime.Now, DataFinal = DateTime.Now.AddMonths(1) });

            String Mensagem = @"<img src='www.hbmax.com/assets/img/logo.png' /> <br/>
                               <p>Olá " + Nome + @"</p>
                               <p>Seja bem vindo ao sistema HBMAX de gestão inteligente de Espaços de Vida Saudável.</p>
                               <p>Com o sistema você poderá controlar de forma simples seus resultados, seu estoque e suas vendas clientes.</p>
                               <p>Utilize todos os recursos disponíveis nesta versão. Em breve estaremos disponibilizando novas versões com muito mais recursos.</p>                                
                               <p>Seu período gratuito de utilização é de 30 dias. Neste período cadastre seus clientes, faça os lançamentos diariamente de vendas e conheça todos os benefícios do seu negócio com um controle prático e rápido no dia-a-dia.</p>
                                
                               <p>Em caso de dúvidas e sugestões estamos a disposição,</p>
                               <h2><bold> HBMAX ::: Central de cadastros</bold></h2>";


            EmailUtl.SendEmail("Contato", User.Email, "Bem vindo!", Mensagem);
        }

        private decimal GetPrecoEstimado(String Nivel, decimal Preco)
        {
            decimal Percent = 1;
            switch (Nivel)
            {
                case "Distribuidor independente": Percent = 0.25m; break;
                case "Construtor de sucesso": Percent = 0.35m; break;
                case "Produtor qualificado": Percent = 0.42m; break;
                case "Supervisor": Percent = 0.50m; break;
            }
            return Preco - (Preco * Percent);
        }

        public TB_DETALHE_USUARIO GetDetailByIdUsuario(int ID_Usuario)
        {
            return DAL.GetDetalheByID_Usuario(ID_Usuario);
        }

        
        public bool EsqueciSenha(string Email)
        {
            TB_USUARIO User = DAL.GetByEmail(Email);
            if (User != null)
            {
                String Mensagem = @"<img src='www.hbmax.com/assets/img/logo.png' /> <br/>
                                <h2>Olá, você solicitou o reenvio de sua senha. <br/> 
                                Por favor, clique no link abaixo para redefinir a sua senha.<br/>
                                http://www.hbmax.com/TrocarSenha.aspx?Reenvio=" + SecUtl.Encrypting(User.ID.ToString()) + @"<br/><br/></h2><br/><br/>
                                <h2><bold> HBMAX ::: Central de cadastros</bold></h2>";


                EmailUtl.SendEmail("Reenvio", Email, "Reenvio de senha", Mensagem);
                return true;
            }
            return false;
        }

        public int ChangePass(string ID_Usuario, string NovaSenha)
        {
            TB_USUARIO User = DAL.GetBy(int.Parse(ID_Usuario));
            if (User != null)
            {
                User.Senha = NovaSenha.GetHashCode().ToString();
                DAL.Update();
                return 1;
            }
            return 0;
        }

        internal void LiberaSistema(int ID, int Meses)
        {
            TB_USUARIO AcessUser = DAL.GetBy(ID);
            AcessUser.TB_ACESSO.FirstOrDefault().DataFinal = AcessUser.TB_ACESSO.FirstOrDefault().DataFinal.AddMonths(Meses);
            DAL.Update();
        }
    }
}
