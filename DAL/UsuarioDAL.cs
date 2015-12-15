using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class UsuarioDAL
    {
        hbmaxEntities Ent = new hbmaxEntities();


        public int Add(TB_USUARIO Object)
        {
            Ent.AddToTB_USUARIO(Object);
            Ent.SaveChanges();
            return Object.ID;
        }
        public TB_USUARIO GetBy(int ID)
        {
            return Ent.TB_USUARIO.FirstOrDefault(x => x.ID == ID);
        }

        public void Update()
        {
            Ent.SaveChanges();
        }
        public int AddDetalhe(TB_DETALHE_USUARIO Object)
        {
            Ent.TB_DETALHE_USUARIO.AddObject(Object);
            return Object.ID;
        }
        public TB_DETALHE_USUARIO GetDetalheByID_Usuario(int ID)
        {
            return Ent.TB_DETALHE_USUARIO.FirstOrDefault(x => x.ID_USUARIO == ID);
        }
        public TB_DETALHE_USUARIO GetDetalheBy(int ID)
        {
            return Ent.TB_DETALHE_USUARIO.FirstOrDefault(x => x.ID == ID);
        }
        public TB_USUARIO GetBy(string Usuario)
        {
            return Ent.TB_USUARIO.FirstOrDefault(x => x.Login.ToUpper().Equals(Usuario.ToUpper()));
        }
        public TB_USUARIO GetBy(string Usuario, string Senha)
        {
            return Ent.TB_USUARIO.FirstOrDefault(x => x.Login.Equals(Usuario) && x.Senha.Equals(Senha));
        }


        public TB_USUARIO GetBy(int ID_Usuario, string Senha)
        {
            return Ent.TB_USUARIO.FirstOrDefault(x => x.ID == ID_Usuario && Senha == x.Senha);
        }

        public TB_USUARIO GetByEmail(string Email)
        {
            return Ent.TB_USUARIO.FirstOrDefault(x => x.Email == Email);
        }

        public void AddAcesso(TB_ACESSO tB_ACESSO)
        {
            Ent.TB_ACESSO.AddObject(tB_ACESSO);
            Ent.SaveChanges();
        }
    }
}
