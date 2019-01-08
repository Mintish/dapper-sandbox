using System;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

public static class BuildAComplexModel {
    public static void Run(IDbConnection conn) {
            const string sql = @"select 2011 as Year, 
                                        75 as Rating,
                                        53 as Make,
                                        31 as Model,
                                        2 as SubModel,
                                        1 as Engine;";

            var carParmText = conn.Query<CarFinderParameter>(sql)
                                  .FirstOrDefault()?
                                  .ToString();

            Console.WriteLine("=== BuildAComplexModel ===");
            Console.WriteLine(carParmText);
    }
}