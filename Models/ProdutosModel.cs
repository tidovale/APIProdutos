using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIChaves.Models
{
    public class ProdutosModel
    {
        public String Codigo { get; set; }
        public String Descricao { get; set; }
        public String Preco { get; set; }
        public String Grupo { get; set; }
        public String Subgrupo { get; set; }
        public String Familia { get; set; }
        public String Secao { get; set; }
        public String Disponivel { get; set; }
        //public byte[] Imagem { get; set; }
        public string Image64 { get; set; }
    }
}