using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Relatorio
{
    public class FechamentoDiarioDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();

        public List<TB_VENDA_PRODUTO> GetAllProdutosVendidos(int Usuario_ID, DateTime DataInicial, DateTime DataFinal)
        {
            return Ent.TB_VENDA_PRODUTO.Where(x => x.TB_VENDA.ID_Usuario == Usuario_ID && x.TB_VENDA.Data >= DataInicial && x.TB_VENDA.Data <= DataFinal).ToList();
        }
        public List<TB_VENDA> GetAllVendas(int Usuario_ID, DateTime DataInicial, DateTime DataFinal)
        {
            return Ent.TB_VENDA.Where(x => x.ID_Usuario == Usuario_ID && x.Data >= DataInicial && x.Data <= DataFinal).OrderBy(x => x.Data_Insercao).ToList();
        }
    }
}
