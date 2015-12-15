using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DAL
{

   public class ProdutoDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();


        public TB_PRODUTO Add(TB_PRODUTO Object)
        {
            Ent.AddToTB_PRODUTO(Object);
            Ent.SaveChanges();
            return Object;
        }
        public void AddList(List<TB_PRODUTO> Prod)
        {
            foreach (TB_PRODUTO item in Prod)
            {
                Ent.AddToTB_PRODUTO(item);
            }
            Ent.SaveChanges();

        }
        public TB_PRODUTO GetBy(int ID)
        {
            return Ent.TB_PRODUTO.FirstOrDefault(x => x.ID == ID);
        }
        public TB_PRODUTO GetByCodHerbalife(int COD, int ID_Cliente)
        {
            return Ent.TB_PRODUTO.FirstOrDefault(x => x.COD_HERBALIFE == COD && x.ID_EMPRESA == ID_Cliente);
        }

        public List<TB_PRODUTO> GetAll(int ID_Empresa, String Column, bool Desc)
        {

            var p = Expression.Parameter(typeof(TB_PRODUTO));

            Func<TB_PRODUTO, object> sortBy = Expression.Lambda<Func<TB_PRODUTO, dynamic>>(Expression.TypeAs(Expression.Property(p, Column), typeof(object)), p).Compile();

            if (Desc)
                return Ent.TB_PRODUTO.Where(x => x.ID_EMPRESA == ID_Empresa).OrderBy(sortBy).ToList();
            else
                return Ent.TB_PRODUTO.Where(x => x.ID_EMPRESA == ID_Empresa).OrderByDescending(sortBy).ToList();
        }
        public List<TB_PRODUTO> GetAllAfetaEstoqueGrid(int ID_Empresa, String Column, bool Desc)
        {

            var p = Expression.Parameter(typeof(TB_PRODUTO));

            Func<TB_PRODUTO, object> sortBy = Expression.Lambda<Func<TB_PRODUTO, dynamic>>(Expression.TypeAs(Expression.Property(p, Column), typeof(object)), p).Compile();

            if (Desc)
                return Ent.TB_PRODUTO.Where(x => x.ID_EMPRESA == ID_Empresa && x.AFETA_ESTOQUE).OrderBy(sortBy).ToList();
            else
                return Ent.TB_PRODUTO.Where(x => x.ID_EMPRESA == ID_Empresa && x.AFETA_ESTOQUE).OrderByDescending(sortBy).ToList();
        }
        public void Update()
        {
            Ent.SaveChanges();
        }
        public void Delete(TB_PRODUTO Prod)
        {
            Ent.TB_PRODUTO.DeleteObject(Prod);
            Ent.SaveChanges();
        }
        public List<TB_UNIDADE> GetAllUnidades()
        {
            return Ent.TB_UNIDADE.Select(x => x).ToList<TB_UNIDADE>();
        }
        public List<TB_PRODUTO_DEFAULT> GetProdutosDefault()
        {
            return Ent.TB_PRODUTO_DEFAULT.Select(x => x).ToList<TB_PRODUTO_DEFAULT>();
        }


        public List<TB_PRODUTO> GetAll(int UsuarioID)
        {
            return Ent.TB_PRODUTO.Where(x => x.ID_EMPRESA == UsuarioID).ToList();
        }
        public List<TB_PRODUTO> GetAllAfetaEstoque(int UsuarioID)
        {
            return Ent.TB_PRODUTO.Where(x => x.ID_EMPRESA == UsuarioID && x.AFETA_ESTOQUE).ToList();
        }

        public List<TB_VENDA_PRODUTO> GetProdutosVendaBy(int ID_Venda)
        {
            return Ent.TB_VENDA_PRODUTO.Where(x => x.ID_Venda == ID_Venda).ToList();
        }

        
    }
}
