using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class VendaDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();
        public TB_VENDA ADD(TB_VENDA NewItem)
        {
            Ent.AddToTB_VENDA(NewItem);
            Ent.SaveChanges();
            return NewItem;
        }
        public TB_VENDA_PRODUTO ADD(TB_VENDA_PRODUTO NewItem)
        {
            Ent.AddToTB_VENDA_PRODUTO(NewItem);
            Ent.SaveChanges();
            return NewItem;
        }
        public List<TB_VENDA> GetVendasBy(int ID_Usuario, int ID_Cliente)
        {
            return Ent.TB_VENDA.Where(x => x.ID_Usuario == ID_Usuario && x.ID_Cliente == ID_Cliente).ToList();
        }
        public void RemoveAllItensVenda(int ID_Venda)
        {
            Ent.TB_VENDA_PRODUTO.Where(x => x.ID_Venda == ID_Venda).ToList().ForEach(Ent.TB_VENDA_PRODUTO.DeleteObject);
            Ent.SaveChanges();
        }

        public void RemoverVenda(int ID_Venda)
        {
            TB_VENDA Venda = Ent.TB_VENDA.FirstOrDefault(x => x.ID == ID_Venda);
            Ent.TB_VENDA.DeleteObject(Venda);
            Ent.SaveChanges();
        }

        public bool CheckCaixa(int ID_Usuario, DateTime Date)
        {
            return Ent.TB_ABERTURA_CAIXA.FirstOrDefault(x => x.Data == Date && x.ID_Usuario == ID_Usuario) != null ? true : false;
        }

        public void AbreCaixa(int ID_User, DateTime DTAbertura, decimal DecValue)
        {
            TB_ABERTURA_CAIXA NewItem = new TB_ABERTURA_CAIXA();
            NewItem.ID_Usuario = ID_User;
            NewItem.Data = DTAbertura;
            NewItem.Valor = DecValue;
            Ent.TB_ABERTURA_CAIXA.AddObject(NewItem);
            Ent.SaveChanges();
        }

        public List<TB_ABERTURA_CAIXA> GetAberturas(int ID_Usuario, DateTime dateTime, DateTime dateTime_2)
        {
            return Ent.TB_ABERTURA_CAIXA.Where(x => x.Data >= dateTime && x.Data <= dateTime_2 && x.ID_Usuario == ID_Usuario).ToList();
        }
    }
}