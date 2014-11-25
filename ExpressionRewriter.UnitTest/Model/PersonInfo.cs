namespace ExpressionRewriting.UnitTest.Model
{
    public class PersonInfo
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public FamilyStatus Status { get; set; }
        public Location Location { get; set; }
        public string Country { get; set; }
    }


    public class Location
    {
        public string Town { get; set; }
    }
}