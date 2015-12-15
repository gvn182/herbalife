using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Relatorio
{
    public class FechamentoDiarioDetalheDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();

        public List<TB_VENDA> GetAllProdutosVendidos(int Usuario_ID, DateTime DataInicial, DateTime DataFinal)
        {
            return Ent.TB_VENDA.Where(x => x.ID_Usuario == Usuario_ID && x.Data >= DataInicial && x.Data <= DataFinal).OrderBy(x=> x.Data_Insercao).ToList();
        }
    }
}
