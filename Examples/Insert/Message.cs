namespace dapper_sandbox.Examples.Insert
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public override string ToString() =>
            $"Id: {Id}\nText: {Text}\n";

    }
}