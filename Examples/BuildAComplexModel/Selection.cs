namespace dapper_sandbox.Examples.BuildAComplexModel
{
    class Selection
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"Id: {Id}, Description: {Description}";
    }
}