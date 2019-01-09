using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace dapper_sandbox.Examples.BuildAComplexModel
{
    public static class BuildAComplexModel
    {
        static BuildAComplexModel()
        {
            var selectionMap = new CustomPropertyTypeMap(typeof(Selection), (Type t, string c) =>
            {
                if (t != typeof(Selection))
                    return null;
                return t.GetProperty("Id");
            });
            SqlMapper.SetTypeMap(typeof(Selection), selectionMap);
        }

        public static void Run(IDbConnection conn)
        {
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


        private static IEnumerable<ResultT> Query<ResultT>(this IDbConnection conn, string sql) where ResultT : class
        {
            var selectionProps = typeof(ResultT).GetProperties().Where(p => p.PropertyType == typeof(Selection));

            var types = new Type[] { typeof(ResultT) }.Concat(selectionProps.Select(p => p.PropertyType)).ToArray();

            var splitOn = string.Join(", ", selectionProps.Select(p => p.Name));

            return conn.Query<ResultT>(sql, types, Map, splitOn: splitOn);


            ResultT Map(object[] obj)
            {
                var cfp = obj[0] as ResultT;
                var zippedWithValues = obj.Skip(1).Zip(selectionProps, (o, t) => new { PropInfo = t, Value = o });
                foreach (var kvp in zippedWithValues)
                {
                    if (kvp.Value is Selection s)
                    {
                        kvp.PropInfo.SetValue(cfp, s);
                    }
                }
                return cfp;
            };
        }
    }
}