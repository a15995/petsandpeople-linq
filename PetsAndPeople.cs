
namespace Xamarin.Course.LINQ
{
    public enum AnimalType { Bird, Fish }

    public class AnimalKind
    {
        public string Name { get; }
        public AnimalType Type { get; }
        public int Eyes { get; }
        public int Legs { get; }

        public AnimalKind(string name, AnimalType type)
            : this(name, type, 2, 2) { }

        public AnimalKind(
            string name, AnimalType type,
            int eyes, int legs)
        {
            this.Name = name;
            this.Type = type;
            this.Eyes = eyes;
            this.Legs = legs;
        }
    }

    public class Pet
    {
        public string Name { get; }
        public AnimalKind Kind { get; }

        public Pet(string name, AnimalKind kind)
        {
            this.Name = name;
            this.Kind = kind;
        }
    }

    public class Person
    {
        public string Name { get; }
        public int BirthYear { get; }
        public Pet Pet { get; }

        public Person(string name, int birthYear)
            : this(name, birthYear, null) { }

        public Person(string name, int birthYear, Pet pet)
        {
            this.Name = name;
            this.BirthYear = birthYear;
            this.Pet = pet;
        }
    }
}
