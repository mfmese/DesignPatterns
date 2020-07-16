using System;
using System.Collections;
using System.Collections.Generic;

namespace Composite
{
    /// <summary>
    /// Amaç: Hiyerarşik nesnelerin oluşturulması için kullanılır. Örneğin bir kurumdaki roller ve o rollerin hiyerarşik yapısının (Organizasyon Şeması) kurgulanması için kullanılabilir.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
  

            Employee manager = new Employee { Name = "Mehmet Fethi" };
            Employee ahmet = new Employee { Name = "Ahmet" };

            Employee mehmet = new Employee { Name = "Mehmet" };
            Employee mahmut = new Employee { Name = "Mahmut" };
            Employee slm = new Employee { Name = "Süleyman" };

            Contructor yucel = new Contructor { Name = "Yücel" };

            manager.AddSubordinates(ahmet);
            ahmet.AddSubordinates(mehmet);
            ahmet.AddSubordinates(mahmut);
            manager.AddSubordinates(slm);
            ahmet.AddSubordinates(yucel);

            Console.WriteLine(manager.Name);
            foreach (Employee mngr in manager)
            {
                Console.WriteLine("  " + mngr.Name);

                foreach (IPerson employee in mngr)
                {
                    Console.WriteLine("     " + employee.Name);
                }
            }



            Console.Read();
        }
    }


    interface IPerson
    {
         string Name { get; set; }
    }

    class Contructor : IPerson
    {
        public string Name { get; set; }
    }

    class Employee : IPerson, IEnumerable<IPerson>
    {
        List<IPerson> subordinates = new List<IPerson>();

        public void AddSubordinates(IPerson person)
        {
            subordinates.Add(person);
        }

        public void RemoveSubordinates(IPerson person)
        {
            subordinates.Remove(person);
        }

        public IPerson GetSubordinate(int index)
        {
            return subordinates[index];
        }

        public string Name { get; set; }

        public IEnumerator<IPerson> GetEnumerator()
        {
            foreach (IPerson subordinate in subordinates)
            {
                yield return subordinate;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
