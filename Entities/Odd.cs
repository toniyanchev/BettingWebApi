namespace WebApi.Entities
{
    public class Odd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Bet Bet { get; set; }
        public decimal Value { get; set; }
        public decimal SpecialValue { get; set; }
    }
}