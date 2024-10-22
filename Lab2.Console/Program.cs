using Lab2.DataAccess;
using Microsoft.EntityFrameworkCore;
namespace Lab2.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new OwnerDbContext(); 

            db.Database.EnsureCreated();


            var results = db.Owners.Where(s => s.Surname == "Egle");
            var data = db.Owners  
    .OrderBy(s => s.Height)  
    .FirstOrDefault(s => s.Surname == "Berzs");

            foreach (var owner in results)
            {
                System.Console.WriteLine(owner.Job);
            }
            if (data != null)
            {
                System.Console.WriteLine($" Augums: {data.Height}");
            }
            else
            {
                System.Console.WriteLine("Netika atrasts");
            }
            var ow = new Owner
            {
                Surname = "Zvirbulis",
                Height = 188,
                Adress = "Ikskile",
                Job = "Talbraucejs",
                Animals = new List<Animal>
                {
                    new Animal
                    {
                    Name = "Draudzins",
                    Weight = 7,
                    Age = 10,
                    Description = "Suns"

                    }
                }
            };

            db.Owners.Add(ow);
            var jhon = db.Owners.Include(s =>s.Animals).FirstOrDefault(s => s.Job == "Pilots");

            jhon.Surname = "Klava"; //Jau tika mainits
            //jhon.Animals.RemoveAt(0); // Izvelamies indeksu lai dzest
            db.SaveChanges();

        }
    }
}
