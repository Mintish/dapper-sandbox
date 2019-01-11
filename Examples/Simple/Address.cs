namespace dapper_sandbox.Examples.Simple
{
    public class Address
    {
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public override string ToString() =>
            string.Concat($"{Name}\n",
                string.IsNullOrWhiteSpace(Line1) ? string.Empty : $"{Line1}\n",
                string.IsNullOrWhiteSpace(Line2) ? string.Empty : $"{Line2}\n",
                $"{City}, {State} {Zip}");
    }
}