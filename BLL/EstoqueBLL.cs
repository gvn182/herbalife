using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class EstoqueBLL
    {
        EstoqueDAL DAL = new EstoqueDAL();
        public void ADD(int ID_Produto, int QTD, String Motivo)
        {
            DAL.ADD(new TB_ESTOQUE { ID_Produto = ID_Produto, Qtd = QTD, Motivo = Motivo, Data = DateTime.Now });
        }
        public List<TB_ESTOQUE> GetAllBy(String ID_Produto, String Data, String filtro)
        {
            String[] Datas = Data.Split(new string[] { " - " }, StringSplitOptions.None);

            DateTime DataInicial = DateTime.ParseExact(Datas[0] + " 00:00:00", "dd/MM/yyyy HH:mm:ss", null);
            DateTime DataFinal = DateTime.ParseExact(Datas[1] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", null);
            switch (filtro)
            {
                case "-1": return DAL.GetEstoqueBy(int.Parse(ID_Produto), DataInicial, DataFinal);
                case "0": return DAL.GetEstoqueAdicaoBy(int.Parse(ID_Produto), DataInicial, DataFinal);
                case "1": return DAL.GetEstoqueExclusaoBy(int.Parse(ID_Produto), DataInicial, DataFinal);
                default: return null;
            }
        }
        public List<TB_ESTOQUE> GetAllBy(int ID_Usuario,String Data, String filtro)
        {
            String[] Datas = Data.Split(new string[] { " - " }, StringSplitOptions.None);

            DateTime DataInicial = DateTime.ParseExact(Datas[0] + " 00:00:00", "dd/MM/yyyy HH:mm:ss", null);
            DateTime DataFinal = DateTime.ParseExact(Datas[1] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", null);
            switch (filtro)
            {
                case "-1": return DAL.GetEstoqueBy(ID_Usuario.ToString(), DataInicial, DataFinal);
                case "0": return DAL.GetEstoqueAdicaoBy(ID_Usuario.ToString(), DataInicial, DataFinal);
                case "1": return DAL.GetEstoqueExclusaoBy(ID_Usuario.ToString(), DataInicial, DataFinal);
                default: return null;
            }
        }
    }
}
