using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DAL
{

    public class DespesaDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();


        public TB_DESPESA Add(TB_DESPESA Object)
        {
            Ent.AddToTB_DESPESA(Object);
            Ent.SaveChanges();
            return Object;
        }

        public TB_DESPESA GetBy(int ID)
        {
            return Ent.TB_DESPESA.FirstOrDefault(x => x.ID == ID);
        }

        public List<TB_DESPESA> GetAll(int ID_Empresa, String Column, bool Desc)
        {

            var p = Expression.Parameter(typeof(TB_DESPESA));

            Func<TB_DESPESA, object> sortBy = Expression.Lambda<Func<TB_DESPESA, dynamic>>(Expression.TypeAs(Expression.Property(p, Column), typeof(object)), p).Compile();

            if (Desc)
                return Ent.TB_DESPESA.Where(x => x.ID_Usuario == ID_Empresa).OrderBy(sortBy).ToList();
            else
                return Ent.TB_DESPESA.Where(x => x.ID_Usuario == ID_Empresa).OrderByDescending(sortBy).ToList();
        }
        public void Update()
        {
            Ent.SaveChanges();
        }
        public void Delete(TB_DESPESA Prod)
        {
            Ent.TB_DESPESA.DeleteObject(Prod);
            Ent.SaveChanges();
        }
        public List<TB_DESPESA> GetAll(int UsuarioID)
        {
            return Ent.TB_DESPESA.Where(x => x.ID_Usuario == UsuarioID).ToList();
        }
    }
}
