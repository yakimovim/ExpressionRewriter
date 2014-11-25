using System.Collections.Generic;

namespace ExpressionRewriting.UnitTest.Model
{
    public class PersonRepository
    {
        public IEnumerable<Person> GetPersons()
        {
            yield return new Person
            {
                PersonId = 1,
                Name = "John",
                Age = 31,
                FamilyStatus = FamilyStatus.Single,
                Address = new Address
                {
                    Country = "USA",
                    City = "New York"
                }
            };

            yield return new Person
            {
                PersonId = 2,
                Name = "Mary",
                Age = 29,
                FamilyStatus = FamilyStatus.Single,
                Address = new Address
                {
                    Country = "USA",
                    City = "Los Angeles"
                }
            };

            yield return new Person
            {
                PersonId = 3,
                Name = "David",
                Age = 53,
                FamilyStatus = FamilyStatus.Divorced,
                Address = new Address
                {
                    Country = "USA",
                    City = "San Francisco"
                }
            };

            yield return new Person
            {
                PersonId = 4,
                Name = "Ann",
                Age = 42,
                FamilyStatus = FamilyStatus.Married,
                Address = new Address
                {
                    Country = "UK",
                    City = "London"
                }
            };

            yield return new Person
            {
                PersonId = 5,
                Name = "Peter",
                Age = 46,
                FamilyStatus = FamilyStatus.Married,
                Address = new Address
                {
                    Country = "UK",
                    City = "London"
                }
            };

            yield return new Person
            {
                PersonId = 6,
                Name = "Rachel",
                Age = 36,
                FamilyStatus = FamilyStatus.Divorced,
                Address = new Address
                {
                    Country = "Germany",
                    City = "Berlin"
                }
            };

            yield return new Person
            {
                PersonId = 7,
                Name = "Svetlana",
                Age = 36,
                FamilyStatus = FamilyStatus.Single,
                Address = new Address
                {
                    Country = "Russia",
                    City = "Moscow"
                }
            };

            yield return new Person
            {
                PersonId = 8,
                Name = "Yen",
                Age = 36,
                FamilyStatus = FamilyStatus.Married,
                Address = new Address
                {
                    Country = "Japan",
                    City = "Tokyo"
                }
            };
        }
    }
}