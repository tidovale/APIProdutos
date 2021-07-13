using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIChaves.Models;

namespace APIChaves.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Produtos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetProduto(string NumReg, string Step)
        {
            ProdutosBD bd = new ProdutosBD();
            List<ProdutosModel> produtolist = new List<ProdutosModel>();

            if (String.IsNullOrEmpty(NumReg))
            {
                return null;
            }
            else if (String.IsNullOrEmpty(Step))
            {
                return null;
            }
            else
            {
                produtolist = bd.ObterProduto(NumReg.Trim(), Step.Trim());

                //return Json(produtolist, JsonRequestBehavior.AllowGet);

                JsonResult jsonResult = Json(produtolist, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
    }
}