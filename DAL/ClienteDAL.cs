using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DAL
{
    public class ClienteDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();


        public int Add(TB_CLIENTE Object)
        {
            Ent.AddToTB_CLIENTE(Object);
            Ent.SaveChanges();
            return Object.ID;
        }
        public TB_CLIENTE GetBy(int ID)
        {
            return Ent.TB_CLIENTE.FirstOrDefault(x => x.ID == ID);
        }
        public void Delete(TB_CLIENTE Cliente)
        {
            Ent.DeleteObject(Cliente);
            Ent.SaveChanges();
        }
        public void Update()
        {
            Ent.SaveChanges();
        }
        public List<TB_CLIENTE> GetAll(int ID_Usuario, String Column, bool Desc)
        {

            var p = Expression.Parameter(typeof(TB_CLIENTE));

            Func<TB_CLIENTE, object> sortBy = Expression.Lambda<Func<TB_CLIENTE, dynamic>>(Expression.TypeAs(Expression.Property(p, Column), typeof(object)), p).Compile();

            if (Desc)
                return Ent.TB_CLIENTE.Where(x => x.ID_Usuario == ID_Usuario).OrderBy(sortBy).ToList();
            else
                return Ent.TB_CLIENTE.Where(x => x.ID_Usuario == ID_Usuario).OrderByDescending(sortBy).ToList();
        }
        public int GetLastCode(int ID_Usuario)
        {
            return Ent.TB_CLIENTE.FirstOrDefault(x => x.ID_Usuario == ID_Usuario) != null ? Ent.TB_CLIENTE.Where(x => x.ID_Usuario == ID_Usuario).Max(x => x.Codigo) : 1000;
        }
    }
}
