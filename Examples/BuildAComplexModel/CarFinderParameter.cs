namespace dapper_sandbox.Examples.BuildAComplexModel
{
    class CarFinderParameter
    {
        public int Year { get; set; }
        public Selection Make { get; set; }
        public Selection Model { get; set; }
        public Selection SubModel { get; set; }
        public Selection Engine { get; set; }
        public int Rating { get; set; }

        public override string ToString() => $"Year: {Year},\nMake: {Make},\nModel: {Model},\nSubModel: {SubModel},\nEngine: {Engine},\nRating: {Rating}\n";
    }
}