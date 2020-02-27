using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NetCoreAdoNet
{
    class BD
    { 
        private readonly string StrConexao;

        public BD(string strconexao)
        {
            this.StrConexao = strconexao;
        }

        public void RunCommand(byte[] dados)
        {
            using var conexao = new SqlConnection(StrConexao);
            using var comando = new SqlCommand("INSERT INTO TBL (dados) VALUES (@dados)", conexao);

            comando.Parameters.Add("@dados", SqlDbType.VarBinary, -1).Value = dados;
            conexao.Open();
            comando.ExecuteNonQuery();
        }

        public DataTable GetData()
        {
            var dt = new DataTable();

            using var conexao = new SqlConnection(this.StrConexao);
            using var comando = new SqlDataAdapter("SELECT * FROM TBL", conexao);

            conexao.Open();
            dt.Load(comando.SelectCommand.ExecuteReader());

            return dt;
        }
    }
}
