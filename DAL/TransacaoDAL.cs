using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class TransacaoDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();
        public void AddNew(DateTime DataHora, String Codigo, String Sender, int Status)
        {
            Ent.TB_TRANSACOES.AddObject(new TB_TRANSACOES { Codigo = Codigo, DataHora = DataHora, Sender = Sender, Status = Status });
            Ent.SaveChanges();
        }
    }
}
