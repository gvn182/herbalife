using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Relatorio
{
    public class VendaItemClienteDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();

        public List<TB_VENDA_PRODUTO> GetAllProdutosVendidos(int Usuario_ID, DateTime DataInicial, DateTime DataFinal)
        {
            return Ent.TB_VENDA_PRODUTO.Where(x => x.TB_VENDA.ID_Usuario == Usuario_ID && x.TB_VENDA.Data >= DataInicial && x.TB_VENDA.Data <= DataFinal).ToList();
        }
        public List<TB_VENDA_PRODUTO> GetAllProdutosVendidos(int Usuario_ID, int Cliente_ID, DateTime DataInicial, DateTime DataFinal)
        {
            return Ent.TB_VENDA_PRODUTO.Where(x => x.TB_VENDA.ID_Usuario == Usuario_ID && x.TB_VENDA.Data >= DataInicial && x.TB_VENDA.Data <= DataFinal && x.TB_VENDA.ID_Cliente == Cliente_ID).ToList();
        }
        public List<TB_VENDA_PRODUTO> GetAllProdutosVendidos(int Usuario_ID, string Produto_ID, DateTime DataInicial, DateTime DataFinal)
        {
            int ProdutoID = int.Parse(Produto_ID);
            return Ent.TB_VENDA_PRODUTO.Where(x => x.TB_VENDA.ID_Usuario == Usuario_ID && x.TB_VENDA.Data >= DataInicial && x.TB_VENDA.Data <= DataFinal && x.ID_Produto == ProdutoID).ToList();
        }
        public List<TB_VENDA_PRODUTO> GetAllProdutosVendidos(int Usuario_ID, int Produto_ID, int ClienteID, DateTime DataInicial, DateTime DataFinal)
        {
            return Ent.TB_VENDA_PRODUTO.Where(x => x.TB_VENDA.ID_Usuario == Usuario_ID && x.TB_VENDA.Data >= DataInicial && x.TB_VENDA.Data <= DataFinal && x.TB_VENDA.ID_Cliente == ClienteID && x.ID_Produto == Produto_ID).ToList();
        }
        //public List<TB_VENDA> GetAllVendas(int Usuario_ID, DateTime DataInicial, DateTime DataFinal)
        //{
        //    return Ent.TB_VENDA.Where(x => x.ID_Usuario == Usuario_ID && x.Data >= DataInicial && x.Data <= DataFinal).OrderBy(x => x.Data_Insercao).ToList();
        //}
    }
}
