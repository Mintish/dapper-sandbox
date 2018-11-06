using System;
using MySql.Data;
using Dapper;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace dapper_sandbox
{
    class CarFinderParameter {
        public int Year { get; set; }
        public Selection Make { get; set; }
        public Selection Model { get; set; }
        public Selection SubModel { get; set; }
        public Selection Engine { get; set; }
        public int Rating { get; set; }

        public override string ToString() => $"Year: {Year},\nMake: {Make},\nModel: {Model},\nSubModel: {SubModel},\nEngine: {Engine},\nRating: {Rating}\n";
    }

    class Selection {
        public int Id { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"Id: {Id}, Description: {Description}";
    }

    public static class Extensions {

        static Extensions() {
            var selectionMap = new CustomPropertyTypeMap(typeof(Selection), (Type t, string c) => {
                if (t != typeof(Selection))
                    return null;
                return t.GetProperty("Id");
            });
            SqlMapper.SetTypeMap(typeof(Selection), selectionMap);
        }

        public static IEnumerable<ResultT> Query<ResultT>(this IDbConnection conn, string sql) where ResultT : class {
                var selectionProps = typeof(ResultT).GetProperties().Where(p => p.PropertyType == typeof(Selection));

                var types = new Type[] { typeof(ResultT) }.Concat(selectionProps.Select(p => p.PropertyType)).ToArray();

                var splitOn = string.Join(", ", selectionProps.Select(p => p.Name));

                return conn.Query<ResultT>(sql, types, Map, splitOn: splitOn);


                ResultT Map(object[] obj) {
                    var cfp = obj[0] as ResultT;
                    var zippedWithValues = obj.Skip(1).Zip(selectionProps, (o, t) => new { PropInfo = t, Value = o });
                    foreach (var kvp in zippedWithValues) {
                        if (kvp.Value is Selection s) {
                            kvp.PropInfo.SetValue(cfp, s);
                        }
                    }
                    return cfp;
                };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            const string connectionString = "Persist Security Info=False;database=[DB name];server=localhost;user id=[username]];Password=[password]";

            const string sql = @"select 2011 as Year, 
                                        75 as Rating,
                                        53 as Make,
                                        31 as Model,
                                        2 as SubModel,
                                        1 as Engine;";

            MySqlConnection connection = null;
            try {
                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                connection.Open();

                var carParmText = connection.Query<CarFinderParameter>(sql).FirstOrDefault()?.ToString();

                Console.WriteLine(carParmText);
            } catch (Exception ex) {
                connection?.Dispose();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
