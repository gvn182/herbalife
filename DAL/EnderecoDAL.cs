using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class EnderecoDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();
        public TB_ENDERECO GetEnderecoBy(int CEP)
        {
            return Ent.TB_ENDERECO.FirstOrDefault(x => x.CEP == CEP);
        }
    }
}
