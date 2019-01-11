### Insert/Update/Delete ###

All use `conn.Execute(...)`. AFAIK, there's no way of doing complex mapping here. All models must be "flat" objects of simple properties.
You can pass in a single item or a list of items and Dapper will automagically figure out what to do!