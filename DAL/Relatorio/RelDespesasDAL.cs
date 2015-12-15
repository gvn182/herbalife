using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Relatorio
{
    public class RelDespesasDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();

        public List<TB_DESPESA> GetAllDespesas(int Usuario_ID, DateTime DataInicial, DateTime DataFinal)
        {
            return Ent.TB_DESPESA.Where(x => x.ID_Usuario == Usuario_ID && x.Data >= DataInicial && x.Data <= DataFinal).OrderBy(x => x.Data).ToList();
        }
    }
}
