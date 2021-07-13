using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FirebirdSql.Data.FirebirdClient;

namespace APIChaves.Models
{
    public class ProdutosBD
    {
        #region obterConexao
        public FbConnection obterConexao()
        {
            string conexao = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            FbConnection db = new FbConnection(conexao);
            return db;
        }
        #endregion

        #region ObterProduto
        public List<ProdutosModel> ObterProduto(string NumReg, string Step)
        {
            FbConnection conn = obterConexao();
            conn.Open();
            List<ProdutosModel> chaveslist = new List<ProdutosModel>();
            FbDataReader reader = null;
            string Sql = "select FIRST " + NumReg + " SKIP " + Step + " p.pro_codigo AS Codigo, p.pro_resumo AS Descricao, ata.tbp_preco Preco, a.nome grupo, b.nome subgrupo, c.nome familia, " +
            "d.nome secao, (SELECT CONSULTA_ESTOQUE.disponivel FROM CONSULTA_ESTOQUE(P.PRO_CODIGO, 7, 0, 0, (select cast('Now' as date) from RDB$DATABASE))) AS disponivel, p.pro_Imagem as Imagem " +
            "from produtos p " +
            "left join tabelas_produtos ata on ata.tbp_pro_codigo = p.pro_codigo " +
            "left join produtos_nivel1 a on a.codigo = p.pro_nivel1 " +
            "left join produtos_nivel2 b on b.codigo = p.pro_nivel2 " +
            "left join produtos_nivel3 c on c.codigo = p.pro_nivel3 " +
            "left join produtos_nivel4 d on d.codigo = p.pro_nivel4 " +
            "where ata.tbp_tab_codigo = '1' " +
            "and p.pro_tipo in ('PA', 'PR') AND p.PRO_SITUACAO = 'A'";

            FbCommand cmd = new FbCommand(Sql, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (DBNull.Value.Equals(reader["Imagem"]))
                {
                    ProdutosModel chave = new ProdutosModel()
                    {
                        Codigo = reader["Codigo"].ToString(),
                        Descricao = reader["Descricao"].ToString(),
                        Preco = reader["Preco"].ToString(),
                        Grupo = reader["Grupo"].ToString(),
                        Subgrupo = reader["Subgrupo"].ToString(),
                        Familia = reader["Familia"].ToString(),
                        Secao = reader["Secao"].ToString(),
                        Disponivel = reader["Disponivel"].ToString(),
                        Image64 = null,
                    };
                    chaveslist.Add(chave);
                }
                else
                {
                    ProdutosModel chave = new ProdutosModel()
                    {
                        Codigo = reader["Codigo"].ToString(),
                        Descricao = reader["Descricao"].ToString(),
                        Preco = reader["Preco"].ToString(),
                        Grupo = reader["Grupo"].ToString(),
                        Subgrupo = reader["Subgrupo"].ToString(),
                        Familia = reader["Familia"].ToString(),
                        Secao = reader["Secao"].ToString(),
                        Disponivel = reader["Disponivel"].ToString(),
                        Image64 = Convert.ToBase64String((byte[])reader["Imagem"])
                    };
                    chaveslist.Add(chave);
                }
            }

            conn.Close();
            return chaveslist;
        }
        #endregion

    }
}