using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class EstoqueDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();
        public void ADD(TB_ESTOQUE Item)
        {
            Ent.AddToTB_ESTOQUE(Item);
            Ent.SaveChanges();
        }

        public List<TB_ESTOQUE> GetEstoqueBy(int ID_Produto, DateTime DataInicial, DateTime DataFinal)
        {
            return Ent.TB_ESTOQUE.Where(x => x.ID_Produto == ID_Produto && x.Data >= DataInicial && x.Data <= DataFinal).ToList();
        }
        public List<TB_ESTOQUE> GetEstoqueAdicaoBy(int ID_Produto, DateTime DataInicial, DateTime DataFinal)
        {
            return Ent.TB_ESTOQUE.Where(x => x.ID_Produto == ID_Produto && x.Data >= DataInicial && x.Data <= DataFinal && x.Qtd > 0).ToList();
        }
        public List<TB_ESTOQUE> GetEstoqueExclusaoBy(int ID_Produto, DateTime DataInicial, DateTime DataFinal)
        {
            return Ent.TB_ESTOQUE.Where(x => x.ID_Produto == ID_Produto && x.Data >= DataInicial && x.Data <= DataFinal && x.Qtd < 0).ToList();
        }


        public List<TB_ESTOQUE> GetEstoqueBy(string ID_Usuario, DateTime DataInicial, DateTime DataFinal)
        {
            int IDUser = int.Parse(ID_Usuario);
            return Ent.TB_ESTOQUE.Where(x => x.TB_PRODUTO.ID_EMPRESA == IDUser && x.Data >= DataInicial && x.Data <= DataFinal).ToList();
        }
        public List<TB_ESTOQUE> GetEstoqueAdicaoBy(string ID_Usuario, DateTime DataInicial, DateTime DataFinal)
        {
            int IDUser = int.Parse(ID_Usuario);
            return Ent.TB_ESTOQUE.Where(x => x.TB_PRODUTO.ID_EMPRESA == IDUser && x.Data >= DataInicial && x.Data <= DataFinal && x.Qtd > 0).ToList();
        }
        public List<TB_ESTOQUE> GetEstoqueExclusaoBy(string ID_Usuario, DateTime DataInicial, DateTime DataFinal)
        {
            int IDUser = int.Parse(ID_Usuario);
            return Ent.TB_ESTOQUE.Where(x => x.TB_PRODUTO.ID_EMPRESA == IDUser && x.Data >= DataInicial && x.Data <= DataFinal && x.Qtd < 0).ToList();
        }





    }
}
