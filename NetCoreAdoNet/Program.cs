using System;
using System.Data;
using System.Text;

namespace NetCoreAdoNet
{
    class Program
    {

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
    }
}
