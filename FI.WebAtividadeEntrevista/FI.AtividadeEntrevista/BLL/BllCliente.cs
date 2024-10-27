using FI.AtividadeEntrevista.DAL.Clientes;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace FI.AtividadeEntrevista.BLL
{
    public class BllCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(Cliente cliente, out string msg)
        {
            if (!CPFUtil.Validar(cliente.CPF))
            {
                msg = "CPF inválido!";
                return 0;
            }

            if (!VerificarExistencia(cliente.CPF))
            {
                DaoCliente cli = new DaoCliente();
                var id = cli.Incluir(cliente);
                msg = (id > 0) ? "Cliente cadastrado com sucesso." : "Não foi possível cadastrar o cliente! Verifique com o Gestor.";
                return id;
            }

            msg = "Já existe um cliente com esse CPF!";
            return 0;
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public bool Alterar(Cliente cliente, out string msg)
        {
            if (!CPFUtil.Validar(cliente.CPF))
            {
                msg = "CPF inválido!";
                return false;
            }

            if (!VerificarExistencia(cliente.CPF, cliente.Id))
            {
                DaoCliente cli = new DaoCliente();
                cli.Alterar(cliente);
                msg = "Cliente alterado com sucesso.";
                return true;
            }

            msg = "Já existe um cliente com esse CPF!";
            return false;
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public Cliente Consultar(long id)
        {
            DaoCliente cli = new DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public bool Excluir(long id, out string msg)
        {
            DaoCliente cli = new DaoCliente();
            long qtB = cli.ContarBenef(id);
            if (qtB == 0)
            {
                cli.Excluir(id);
                msg = "Cliente excluído com sucesso.";
                return true;
            }
            else
            {
                msg = "O cliente não pode ser excluído porque possui " + qtB.ToString() + " beneficiário" + (qtB == 1 ? "" : "s") + "!";
                return false;
            }
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Cliente> Listar()
        {
            DaoCliente cli = new DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DaoCliente cli = new DaoCliente();
            return cli.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        private bool VerificarExistencia(string cpf, Int64? id = null)
        {
            return new DaoCliente().VerificarExistencia(cpf, id);
        }
    }
}