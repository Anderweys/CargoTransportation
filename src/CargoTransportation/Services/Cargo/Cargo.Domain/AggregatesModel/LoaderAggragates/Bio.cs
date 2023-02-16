using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.AggregatesModel.LoaderAggragates;


public class Bio : ValueObject
{
    public string LastName { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public short Age { get; private set; }

    public Bio()
    {
    }

    public Bio(string lastName, string firstName, string middleName, short age)
    {
        LastName=lastName;
        FirstName=firstName;
        MiddleName=middleName;
        Age=age;
    }
}
