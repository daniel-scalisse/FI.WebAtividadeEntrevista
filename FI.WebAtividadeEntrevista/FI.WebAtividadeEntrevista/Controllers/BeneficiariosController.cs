using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Util;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiariosController : Controller
    {
        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BllBeneficiario bo = new BllBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                string cpf = CPFUtil.RemoveMask(model.CPF);
                model.Id = bo.Incluir(new Beneficiario()
                {
                    CPF = cpf,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                }, out string msg);

                return Json(new { Ok = model.Id > 0, Msg = msg, CPF = CPFUtil.Format(cpf), Id = model.Id });
            }
        }

        [HttpPost]
        public JsonResult ConsultarPorId(long id)
        {
            BllBeneficiario bo = new BllBeneficiario();
            Beneficiario beneficiario = bo.Consultar(id);
            BeneficiarioModel model = null;

            if (beneficiario != null)
            {
                model = new BeneficiarioModel()
                {
                    Id = beneficiario.Id,
                    CPF = beneficiario.CPF,
                    Nome = beneficiario.Nome
                };
            }

            return Json(model);
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            BllBeneficiario bo = new BllBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                string cpf = CPFUtil.RemoveMask(model.CPF);
                bool gravou = bo.Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    CPF = cpf,
                    Nome = model.Nome
                }, out string msg);

                return Json(new { Ok = gravou, Msg = msg, CPF = CPFUtil.Format(cpf) });
            }
        }

        [HttpPost]
        public JsonResult Listar(long idCliente)
        {
            try
            {
                List<Beneficiario> list = new BllBeneficiario().ConsultarPorCliente(idCliente);
                return Json(new { Records = list });
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Excluir(long id)
        {
            BllBeneficiario bo = new BllBeneficiario();
            bo.Excluir(id);

            return Json(new { Ok = true, Msg = "Beneficiário excluído com sucesso." });
        }
    }
}