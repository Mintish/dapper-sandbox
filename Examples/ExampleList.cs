using System;
using System.Collections.Generic;
using System.Data;

public static class ExampleList {
    public static IEnumerable<Action<IDbConnection>> GetList() =>
        new Action<IDbConnection>[] {
            Simple.Run,
            BuildAComplexModel.Run
        };
}