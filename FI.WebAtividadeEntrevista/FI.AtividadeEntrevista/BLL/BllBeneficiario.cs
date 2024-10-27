using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Util;
using System;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BllBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long Incluir(Beneficiario beneficiario, out string msg)
        {
            if (!CPFUtil.Validar(beneficiario.CPF))
            {
                msg = "CPF inválido!";
                return 0;
            }

            if (!VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente))
            {
                DaoBeneficiario cli = new DaoBeneficiario();
                var id = cli.Incluir(beneficiario);
                msg = (id > 0) ? "Beneficiario cadastrado com sucesso." : "Não foi possível cadastrar o beneficiario! Verifique com o Gestor.";
                return id;
            }

            msg = "Já existe um beneficiario com esse CPF!";
            return 0;
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public bool Alterar(Beneficiario beneficiario, out string msg)
        {
            if (!CPFUtil.Validar(beneficiario.CPF))
            {
                msg = "CPF inválido!";
                return false;
            }

            if (!VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente, beneficiario.Id))
            {
                DaoBeneficiario cli = new DaoBeneficiario();
                cli.Alterar(beneficiario);
                msg = "Beneficiario alterado com sucesso.";
                return true;
            }

            msg = "Já existe um beneficiario com esse CPF!";
            return false;
        }

        /// <summary>
        /// Consulta o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public Beneficiario Consultar(long id)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Consulta beneficiarios por cliente
        /// </summary>
        /// <param name="idCliente">idCliente do beneficiario</param>
        /// <returns></returns>
        public List<Beneficiario> ConsultarPorCliente(long idCliente)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            return cli.ConsultarPorCliente(idCliente);
        }

        /// <summary>
        /// Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            cli.Excluir(id);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        private bool VerificarExistencia(string cpf, Int64 idCliente, Int64? ID = null)
        {
            return new DaoBeneficiario().VerificarExistencia(cpf, idCliente, ID);
        }
    }
}