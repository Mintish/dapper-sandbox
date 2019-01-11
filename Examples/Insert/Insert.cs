using System;
using System.Data;
using Dapper;

namespace dapper_sandbox.Examples.Insert
{
    public static class Insert
    {
        public static void Run(IDbConnection conn)
        {
            var tx = conn.BeginTransaction();
            CreateTables(conn, tx);


            const string insertSql = @"
                INSERT INTO messages (text) 
                VALUES (@text);";

            conn.Execute(insertSql, new[] {
                new { text = "Hello world! Here's a message!" },
                new { text = "This is only a short sequence of messages..." },
                new { text = "In fact, here is the last one!" }
            });


            PrintTheResults(conn, tx);
            tx.Rollback();
        }

        private static void CreateTables(IDbConnection conn, IDbTransaction tx)
        {
            conn.Execute(@"
                CREATE TEMPORARY TABLE messages (
                    id INT NOT NULL AUTO_INCREMENT,
                    text VARCHAR(250) NULL,
                    PRIMARY KEY (id)
                );", transaction: tx);
        }

        private static void PrintTheResults(IDbConnection conn, IDbTransaction tx)
        {
            const string selectSql = @"
                SELECT id, text
                FROM messages;";

            var messages = conn.Query<Message>(selectSql, transaction: tx);

            Console.WriteLine("== Insert ==");
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}