using System;
using System.Data;
using System.Linq;
using Dapper;
namespace dapper_sandbox.Examples.Simple
{
    public static class Simple
    {
        public static void Run(IDbConnection conn)
        {
            const string sql = @"select 'John Doe' Name,
                                        '1234 S 84th St.' Line1,
                                        null Line2,
                                        'Omaha' City, 
                                        'NE' State,
                                        '68122' Zip
                                union all
                                select 'Smitty Smith' Name,
                                        '6220 Ct Fox Ln.' Line1,
                                        'Box 6' Line2,
                                        'Altoona' City, 
                                        'PA' State,
                                        '15006' Zip;";

            var addresses = conn.Query<Address>(sql, new { });

            Console.WriteLine("=== Simple.cs ===");
            Console.WriteLine(string.Join("\r\n\r\n", addresses));
        }
    }
}