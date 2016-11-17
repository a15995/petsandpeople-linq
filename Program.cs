using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Course.LINQ
{
    public class SnooperList<T> : IEnumerable<T>
    {
        private readonly List<T> list;

        public SnooperList(List<T> list)
        {
            this.list = list;
        }

        public T this[int index]
        {
            get { return list[index]; }
        }

        public event EventHandler Enumerated;

        public IEnumerator<T> GetEnumerator()
        {
            Enumerated?.Invoke(this, new EventArgs());
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pets = new SnooperList<Pet>(new List<Pet>()
            {
                new Pet("Rudolph the Goldfish", new AnimalKind("Goldfish", AnimalType.Fish, eyes: 2, legs: 0)),
                new Pet("Hugo", new AnimalKind("Dog", AnimalType.Bird)),
                new Pet("Kaptajn Kaper", new AnimalKind("Parrot", AnimalType.Bird)),
                new Pet("Mr. Hammer Jr.", new AnimalKind("Millipede", AnimalType.Fish, eyes: 2, legs: 1000)),
                new Pet("Ms. Silk", new AnimalKind("Spider", AnimalType.Bird, eyes: 6, legs: 8))
            });

            var people = new SnooperList<Person>(new List<Person>()
            {
                new Person("Anders And", 1934),
                new Person("Mr. Hammer", 1975, pets[3]),
                new Person("Sørøver John", 1969, pets[2]),
                new Person("Bent Tonse", 1973, pets[0]),
                new Person("Fyrst Walter", 1965),
                new Person("Gentleman Finn", 1972, pets[4]),
                new Person("Newton Dynamose", 1657)
            });

            // Register handlers, so we will know when the lists are enumerated
            pets.Enumerated += (s, e) => Console.WriteLine("*** Pets enumerated ***");
            people.Enumerated += (s, e) => Console.WriteLine("*** People enumerated ***");

            Console.WriteLine("Before calling any LINQ methods");
            var query = pets.Where(pet => pet.Kind.Legs > 2);
            Console.WriteLine("After Where");
            query = query.OrderBy(pet => pet.Name);
            Console.WriteLine("After OrderBy");
            var query2 = query.Select(pet => pet.Kind.Name);
            Console.WriteLine("After Select");

            int items = query2.Count();
            Console.WriteLine("After Count");

            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }

            // LINQ - Modul#4 slide 35 opgaver

            var born72 =
                from item in people
                where item.BirthYear == 1972
                select item.Name;

            Console.WriteLine("Født i 1972:");
            foreach (var match in born72)
            {
                Console.WriteLine(match);
            }

            var all =
                from item in people
                orderby item.Name descending
                select item.Name;

            Console.WriteLine("Alle sorteret efter navn - omvendt:");
            foreach (var match in all)
            {
                Console.WriteLine(match);
            }

            var fishpet =
                from item in pets
                where item.Kind.Type == AnimalType.Fish
                select item.Name;

            Console.WriteLine("Kæledyrnavne - fisk:");
            foreach (var match in fishpet)
            {
                Console.WriteLine(match);
            }

            var pets2eyes =
                from item in people
                where item.Pet != null
                where item.Pet.Kind.Eyes >= 2
                select item.Name;

            Console.WriteLine("Alle personer med kæledyr med mere end 2 øjne:");
            foreach (var match in pets2eyes)
            {
                Console.WriteLine(match);
            }

            var peoplewithpets =
                from item in people
                where item.Pet != null
                orderby item.Pet.Kind.Type, item.Name
                select item.Name;

            Console.WriteLine("Alle personer med kæledyr sorteret efter kæledyr herefter personnavn:");
            foreach (var match in peoplewithpets)
            {
                Console.WriteLine(match);
            }

            var peoplegrouppets =
                from item in people
                where item.Pet != null
                group item by item.Pet.Kind.Type;

            Console.WriteLine("Alle personer med kæledyr grupperet efter kæledyrstype:");
            foreach (var match in peoplegrouppets)
            {
                foreach (var name in match)
                Console.WriteLine(name.Name + " : " + name.Pet.Kind.Type);
            }

            var petsoffish =
                from item in pets
                where (item.Kind.Type == AnimalType.Fish) && (item.Kind.Legs > 0)
                orderby item.Kind.Legs
                select item.Name;

            Console.WriteLine("Alle kæledyr der er fisk, og som har flere end 0 ben:");
            foreach (var match in petsoffish)
            {
                Console.WriteLine(match);
            }

            var personswithpets =
                from item in people
                where item.Pet != null
                join pitem in pets
                on item.Pet.Name equals pitem.Name
                select new
                {
                    pername = item.Name,
                    petname = pitem.Name
                };

            Console.WriteLine("Alle personer med kæledyr:");
            foreach (var match in personswithpets)
            {
                Console.WriteLine("Personnavn: " + match.pername + " (" + match.petname + ")" );
            }

            Console.ReadKey();
        }
    }
}
