using FI.AtividadeEntrevista.DAL.Padrao;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL.Beneficiarios
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiario
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal long Incluir(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("CPF", beneficiario.CPF),
                new SqlParameter("NOME", beneficiario.Nome),
                new SqlParameter("IDCLIENTE", beneficiario.IdCliente)
            };

            DataSet ds = base.Consultar("FI_SP_IncBenef", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Consultar um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal Beneficiario Consultar(long id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("Id", id)
            };

            DataSet ds = base.Consultar("FI_SP_ConsBenef", parametros);
            List<Beneficiario> cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        /// <summary>
        /// Consultar beneficiarios por cliente
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal List<Beneficiario> ConsultarPorCliente(long idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("IDCLIENTE", idCliente)
            };

            DataSet ds = base.Consultar("FI_SP_ConsBenefPorCliente", parametros);
            List<Beneficiario> cli = Converter(ds);

            return cli.ToList();
        }

        internal bool VerificarExistencia(string cpf, Int64 idCliente, Int64? id = null)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("CPF", cpf),
                new SqlParameter("IDCLIENTE", idCliente)
            };

            if (id.HasValue && id.Value > 0)
                parametros.Add(new SqlParameter("ID", id));

            DataSet ds = base.Consultar("FI_SP_VerificaBenef", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// Alterar beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal void Alterar(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("CPF", beneficiario.CPF),
                new SqlParameter("Nome", beneficiario.Nome),
                new SqlParameter("ID", beneficiario.Id)
            };

            base.Executar("FI_SP_AltBenef", parametros);
        }

        /// <summary>
        /// Excluir Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal void Excluir(long id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("Id", id)
            };

            base.Executar("FI_SP_DelBenef", parametros);
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> lista = new List<Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Beneficiario cli = new Beneficiario
                    {
                        Id = row.Field<long>("Id"),
                        CPF = CPFUtil.Format(row.Field<string>("CPF")),
                        Nome = row.Field<string>("Nome")
                    };
                    lista.Add(cli);
                }
            }

            return lista;
        }
    }
}