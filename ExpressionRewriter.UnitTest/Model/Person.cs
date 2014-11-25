namespace ExpressionRewriting.UnitTest.Model
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
        public FamilyStatus FamilyStatus { get; set; }
    }

    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
    }

    public enum FamilyStatus
    {
        Single,
        Married,
        Divorced
    }
}