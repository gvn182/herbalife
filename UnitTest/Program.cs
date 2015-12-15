using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;
using DAL;


namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ProdutoDAL ProdDAL = new ProdutoDAL();
            UsuarioDAL DAL = new UsuarioDAL();
            String ID_Usuario = "33";
            List<TB_PRODUTO_DEFAULT> ProdutoDefault = ProdDAL.GetProdutosDefault();

            List<TB_PRODUTO> Produtos = ProdutoDefault.Select(x => new TB_PRODUTO
            {
                COD_HERBALIFE = x.COD_HERBALIFE,
                DESCRICAO = x.DESCRICAO,
                ID_EMPRESA = int.Parse(ID_Usuario),
                PRECO = x.PRECO,
                PV = x.PV,
                ESTOQUE_MINIMO = x.COD_HERBALIFE == 9999 || x.COD_HERBALIFE == 8888 ? 0 : 3,
                QTD_ESTOQUE = 0,
                GASTO_ESTIMADO = GetPrecoEstimado("Supervisor", x.PRECO),
                UNID = x.UNID,
                AFETA_ESTOQUE = x.AFETA_ESTOQUE
            }).ToList<TB_PRODUTO>();

            ProdDAL.AddList(Produtos);
        }
        public static decimal GetPrecoEstimado(String Nivel, decimal Preco)
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
    }
}
