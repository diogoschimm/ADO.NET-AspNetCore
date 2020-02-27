# ADO.NET-AspNetCore
Projeto com exemplo de utilização de ADO.NET System.Data.SqlClient com ASP.NET Core 3.1

## Script de Banco de Dados

```sql
    CREATE TABLE [dbo].[TBL] (
      [id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
      [dados] [varbinary](max) NULL 
    )  
    GO
```

## Classe de Acesso a Dados

```c#
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
```

## Método para Testar o Acesso ao Banco de Dados

```c#
    static void Main(string[] args)
    {
        var strConexao = "Data Source=EST-BT13\\SQL2017;Initial Catalog=BDEscola;Integrated Security=SSPI;";
        var dirFiles = "C:/TESTE";

        var exec = new BD(strConexao);
        foreach (var file in new System.IO.DirectoryInfo(dirFiles).GetFiles())
        {
            var bytes = System.IO.File.ReadAllBytes(file.FullName);
            exec.RunCommand(bytes);
        }

        foreach (DataRow dr in exec.GetData().Rows)
            Console.WriteLine(Encoding.Default.GetString((byte[])dr["dados"]));
    }
```
