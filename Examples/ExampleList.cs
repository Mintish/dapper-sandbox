using System;
using System.Collections.Generic;
using System.Data;

namespace dapper_sandbox.Examples
{
    public static class ExampleList
    {
        public static IEnumerable<Action<IDbConnection>> GetList() =>
            new Action<IDbConnection>[] {
                Simple.Simple.Run,
                BuildAComplexModel.BuildAComplexModel.Run
            };
    }
}