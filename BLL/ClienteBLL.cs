using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Util;

namespace BLL
{
    
    public class ClienteBLL
    {
        Email EmailUTL = new Email();
        UsuarioBLL BLLUser = new UsuarioBLL();
        ClienteDAL DAL = new ClienteDAL();
        public class ClienteREP
        {
            public string ID_Cliente { get; set; }
            public string Codigo { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string DataVisita { get; set; }
            public string ComoSoube { get; set; }
            public string Tipo { get; set; }
            public string Indicacao { get; set; }
        }
        public List<TB_CLIENTE> GetAll(string ID_Empresa)
        {
            return DAL.GetAll(int.Parse(ID_Empresa), "Nome", false);
        }
        public object GetAll(string ID_Empresa, string filter, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            String Sort = jtSorting.Split(' ')[0];
            bool Desc = jtSorting.Split(' ')[1] == "ASC" ? false : true;

            List<ClienteREP> Lista = DAL.GetAll(int.Parse(ID_Empresa), Sort, Desc).Select(x => new ClienteREP
            {
               Telefone = x.Telefone,
               Nome = x.Nome,
               ID_Cliente = x.ID.ToString(),
               Email = x.Email,
               DataVisita = x.DataVisita.ToString("dd/MM/yyyy"),
               ComoSoube = x.ComoSoube, 
               Codigo = x.Codigo.ToString(),
               Tipo =  x.Tipo,
               Indicacao = x.Indicacao.ToString()
            }).ToList<ClienteREP>();

            if (filter != String.Empty)
            {
                filter = filter.ToUpper();
                Lista = Lista.Where(x => x.Nome.ToUpper().Contains(filter)).ToList();
                return new { Result = "OK", Records = Lista, TotalRecordCount = Lista.Count };
            }
            int ListCount = Lista.Count;
            Lista = Lista.Skip(jtStartIndex).Take(jtPageSize).ToList();
            return new { Result = "OK", Records = Lista, TotalRecordCount = ListCount };
        }

        public int Delete(string ID_Produto)
        {
            TB_CLIENTE Cli = DAL.GetBy(int.Parse(ID_Produto));
            DAL.Delete(Cli);
            return 1;
        }
        public ClienteREP Add(string ID_Usuario, string ComoSoube, string Email, string Nome, string Telefone, string Tipo, string Ind)
        {
            TB_CLIENTE Cli = new TB_CLIENTE();
            Cli.ComoSoube = ComoSoube;
            Cli.DataVisita = DateTime.Now;
            Cli.Email = Email;
            Cli.ID_Usuario = int.Parse(ID_Usuario);
            Cli.Telefone = Telefone;
            Cli.Nome = Nome;
            Cli.Tipo = Tipo;
            Cli.Codigo = DAL.GetLastCode(int.Parse(ID_Usuario)) + 1;
            if (Tipo.Equals("Indicação"))
                Cli.Indicacao = int.Parse(Ind);
            Cli.ID = DAL.Add(Cli);
            TB_DETALHE_USUARIO User = BLLUser.GetDetailByIdUsuario(int.Parse(ID_Usuario));

            String Mensagem = @"<p>Olá, " + Cli.Nome + @"</p> 
                                <p>Queremos agradecer sua visita em nosso Espaço de Vida Saudável e esperamos que tenha gostado do atendimento e dos nossos produtos. </p> 
                                <p>Agradecemos se puder nos indicar para outras pessoas de seu relacionamento que, assim como você, buscam uma alimentação mais equilibrada.  </p> 
                                <p>Caso necessite de informações estamos a disposição.  </p>  <br/>

                               <p> Muito sucesso para você.  </p>  <br/>
                    
                               <p> Espaço Vida Saudável Herbalife </p>
                               <p> " + User.Nome_Razao + " - " + User.Telefone + @"</p>
                                <p><i> Conheça a linha 24 horas da Herbalife, especialmente desenvolvida para pessoas que praticam atividades físicas. </i> </p>";


            EmailUTL.SendEmail("Contato", Email, "Bem vindo ao Espaço Vida Saudável Herbalife", Mensagem);


            return new ClienteREP { ComoSoube = Cli.ComoSoube, Telefone = Cli.Telefone, Nome = Cli.Nome, Email = Cli.Email, DataVisita = Cli.DataVisita.ToString("dd/MM/yyyy"), ID_Cliente = ID_Usuario, Indicacao = Cli.Indicacao.HasValue ? Cli.Indicacao.Value.ToString() : String.Empty, Tipo = Cli.Tipo, Codigo = Cli.Codigo.ToString() };
        }

        public int Update(string ID_Usuario, string ID, string ComoSoube, string Email, string Nome, string Telefone, string Tipo, string Indicacao)
        {
            TB_CLIENTE Cli = DAL.GetBy(int.Parse(ID));

            Cli.ComoSoube = ComoSoube;
            Cli.Email = Email;
            Cli.ID_Usuario = int.Parse(ID_Usuario);
            Cli.Telefone = Telefone;
            Cli.Nome = Nome;
            Cli.Tipo = Tipo;
            if (!String.IsNullOrEmpty(Indicacao))
                Cli.Indicacao = int.Parse(Indicacao);
            else
                Cli.Indicacao = null;

            DAL.Update();
            return 1;
        }

        public TB_CLIENTE GetBy(String ID)
        {
            return DAL.GetBy(int.Parse(ID));
        }
    }
}
