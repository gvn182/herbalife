using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace BLL
{
    public class ProdutoBLL
    {
        ProdutoDAL DAL = new ProdutoDAL();
        EstoqueBLL BllEstoque = new EstoqueBLL();
        public class ProdutoREP
        {

            public string ID_Produto { get; set; }
            public string COD_Herbalife { get; set; }
            public string Descricao { get; set; }
            public string Preco { get; set; }
            public string PV { get; set; }
            public string UNID { get; set; }
            public string Qtd_Estoque { get; set; }
            public string Estoque_Minimo { get; set; }
            public string Gasto_Estimado { get; set; }
            public string Afeta_Estoque { get; set; }
        }
        public class ProdutoEstoqueREP
        {
            public string ID_Produto { get; set; }
            public string COD_Herbalife { get; set; }
            public string Descricao { get; set; }
            public string Preco { get; set; }
            public string Estoque_Minimo { get; set; }
            public string Qtd_Estoque { get; set; }
        }
        public class UnidadeREP
        {

            public int ID { get; set; }
            public string Descricao { get; set; }
        }
        public void AddList(List<TB_PRODUTO> Prod)
        {
            DAL.AddList(Prod);
        }
        public ProdutoREP Add(String ID_Empresa, String COD_Herbalife, String Descricao, String UNID, String PV, String Preco, String Gasto_Estimado, String Estoque_Minimo, String Afeta_Estoque)
        {
            if (DAL.GetByCodHerbalife(int.Parse(COD_Herbalife), int.Parse(ID_Empresa)) != null)
                return null;

            TB_PRODUTO PROD = DAL.Add(new TB_PRODUTO
            {
               AFETA_ESTOQUE = Afeta_Estoque == "Sim" ? true : false ,
                COD_HERBALIFE = int.Parse(COD_Herbalife),
                DESCRICAO = Descricao,
                ID_EMPRESA = int.Parse(ID_Empresa),
                PRECO = decimal.Parse(Preco),
                PV = double.Parse(PV),
                UNID = int.Parse(UNID),
                GASTO_ESTIMADO = decimal.Parse(Gasto_Estimado),
                ESTOQUE_MINIMO = int.Parse(Estoque_Minimo)
            });
            String Afeta_Est = PROD.AFETA_ESTOQUE ? "Sim" : "Não";
            return new ProdutoREP { COD_Herbalife = PROD.COD_HERBALIFE.ToString(), Descricao = PROD.DESCRICAO, Preco = PROD.PRECO.ToString(), PV = PROD.PV.ToString(), UNID = PROD.UNID.ToString(), ID_Produto = PROD.ID.ToString(), Estoque_Minimo = PROD.ESTOQUE_MINIMO.ToString(), Gasto_Estimado = PROD.GASTO_ESTIMADO.ToString(), Afeta_Estoque = Afeta_Est };
        }
        public int Update(String ID_Produto, String ID_Empresa,  String Descricao, String UNID, String PV, String Preco, String Gasto_Estimado, String Estoque_Minimo, String Afeta_Estoque)
        {

            TB_PRODUTO PROD = DAL.GetBy(int.Parse(ID_Produto));
            PROD.AFETA_ESTOQUE = Afeta_Estoque == "Sim" ? true : false;
            PROD.DESCRICAO = Descricao;
            PROD.PRECO = decimal.Parse(Preco);
            PROD.PV = double.Parse(PV);
            PROD.UNID = int.Parse(UNID);
            PROD.ESTOQUE_MINIMO = int.Parse(Estoque_Minimo);
            PROD.GASTO_ESTIMADO = decimal.Parse(Gasto_Estimado);
            DAL.Update();
            return 1;
        }
        public int Delete(String ID_Produto)
        {
            //TODO: check if is using before delete
            //return -1
            TB_PRODUTO Prod = DAL.GetBy(int.Parse(ID_Produto));
            DAL.Delete(Prod);
            return 1;

        }
        public object GetAll(string ID_Empresa, string filter, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            String Sort = jtSorting.Split(' ')[0];
            bool Desc = jtSorting.Split(' ')[1] == "ASC" ? false : true;

            List<ProdutoREP> Lista = DAL.GetAll(int.Parse(ID_Empresa), Sort, Desc).Select(x => new ProdutoREP
            {
                Descricao = x.DESCRICAO,
                ID_Produto = x.ID.ToString(),
                Preco = x.PRECO.ToString("N2"),
                PV = string.Format("{0:n2}", x.PV),
                UNID = x.UNID.ToString(),
                Estoque_Minimo = x.ESTOQUE_MINIMO.ToString(),
                Gasto_Estimado = string.Format("{0:n2}", x.GASTO_ESTIMADO),
                COD_Herbalife = x.COD_HERBALIFE.ToString(),
                Afeta_Estoque = x.AFETA_ESTOQUE ? "Sim" : "Não"
                
            }).ToList<ProdutoREP>();

            if (filter != String.Empty)
            {
                filter = filter.ToUpper();
                Lista = Lista.Where(x => x.Descricao.ToUpper().Contains(filter) || x.COD_Herbalife.ToUpper().Contains(filter)).ToList();
                return new { Result = "OK", Records = Lista, TotalRecordCount = Lista.Count };
            }
            int ListCount = Lista.Count;
            Lista = Lista.Skip(jtStartIndex).Take(jtPageSize).ToList();
            return new { Result = "OK", Records = Lista, TotalRecordCount = ListCount };
        }
        public List<TB_PRODUTO> GetAll(string ID_Empresa)
        {
            return DAL.GetAll(int.Parse(ID_Empresa));
        }
        public List<TB_PRODUTO> GetAllAfetaEstoque(string ID_Empresa)
        {
            return DAL.GetAllAfetaEstoque(int.Parse(ID_Empresa));
        }
        public List<UnidadeREP> GetAllUnidades()
        {
            return DAL.GetAllUnidades().Select(x => new UnidadeREP
            {
                ID = x.ID,
                Descricao = x.DESCRICAO.ToString()

            }).ToList<UnidadeREP>();
        }
        public List<TB_PRODUTO_DEFAULT> GetAllProdutoDefault()
        {
            return DAL.GetProdutosDefault();
        }
        public List<ProdutoEstoqueREP> GetAllEstoque(string ID_Empresa, string filter)
        {
            List<ProdutoEstoqueREP> Lista = DAL.GetAllAfetaEstoqueGrid(int.Parse(ID_Empresa), "COD_Herbalife", false).Select(x => new ProdutoEstoqueREP
            {
                Descricao = x.DESCRICAO,
                ID_Produto = x.ID.ToString(),
                Preco = x.PRECO.ToString("N2"),
                Estoque_Minimo = x.ESTOQUE_MINIMO.ToString(),
                Qtd_Estoque = x.QTD_ESTOQUE.ToString(),
                COD_Herbalife = x.COD_HERBALIFE.ToString()
            }).ToList<ProdutoEstoqueREP>();

            if (filter != String.Empty)
            {
                filter = filter.ToUpper();
                return Lista.Where(x => x.Descricao.ToUpper().Contains(filter) || x.COD_Herbalife.ToUpper().Contains(filter)).ToList();
            }
            return Lista;
        }
        public TB_PRODUTO GetProdutoBy(int ID)
        {
            return DAL.GetBy(ID);
        }
        public void AdicionaEstoque(int ID_Produto, int QTD, string Motivo)
        {
            TB_PRODUTO Prod = GetProdutoBy(ID_Produto);
            Prod.QTD_ESTOQUE += QTD;
            DAL.Update();
            BllEstoque.ADD(ID_Produto, QTD, Motivo);
        }
        public void RemoveEstoque(int ID_Produto, int QTD, string Motivo)
        {
            TB_PRODUTO Prod = GetProdutoBy(ID_Produto);
            Prod.QTD_ESTOQUE -= QTD;
            DAL.Update();
            BllEstoque.ADD(ID_Produto, -QTD, Motivo);
        }

        internal List<TB_VENDA_PRODUTO> GetProdutosVendaBy(int ID_Venda)
        {
            return DAL.GetProdutosVendaBy(ID_Venda);
        }

        
    }
}