using Lab2.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab4.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Animal1Controller : ControllerBase
    {
        private readonly OwnerDbContext _db;
        public DbSet<Animal> Animals { get; set; }

        // Konstruktorā inicializējam DbContext
        public Animal1Controller()
        {
            _db = new OwnerDbContext();
        }

        // 6.2. Datu saraksta iegūšana
        [HttpGet]
        public Animal[] GetAnimals()
        {
            return _db.Animals.ToArray();
        }

        // 6.3. Viena ieraksta iegūšana pēc ID
        [HttpGet("{id}")]
        public Animal GetAnimal(int id)
        {
            return _db.Animals.FirstOrDefault(s => s.Id == id);
        }

        // 6.4. Jauna ieraksta pievienošana
        [HttpPost]
        public void Post([FromBody] Animal animal)
        {
            _db.Animals.Add(animal);
            _db.SaveChanges();
        }

        // 6.5. Ieraksta dzēšana
        [HttpDelete("{id}")]
        public void DeleteAnimal(int id)
        {
            var data = _db.Animals.FirstOrDefault(s => s.Id == id);

            if (data != null)
            {
                _db.Animals.Remove(data);
                _db.SaveChanges();
            }
        }

        // 6.6. Ieraksta labošana (update)
        [HttpPut("{id}")]
        public void UpdateAnimal([FromBody] Animal animal, int id)
        {
            var existingAnimal = _db.Animals.FirstOrDefault(s => s.Id == id);

            if (existingAnimal != null)
            {
                existingAnimal.Description = animal.Description;
                existingAnimal.Name = animal.Name;
                _db.SaveChanges();
            }
        }

        // ievadam dzivnieka svaru lai redzet ipasnieku uzvardus kam dziviekIEm ir tads svars
        [HttpGet("Uzdevums uz 10,(ievadam 10 testam) owners-by-animal-age/{age}")]
        public string[] GetOwnersByAnimalAge(int age)
        {
            var owners = (from animal in _db.Animals
                          join owner in _db.Owners on animal.OwnerId equals owner.Id
                          where animal.Age == age
                          select new { animal.Weight, owner.Surname })
                         .Distinct()
                         .ToList();

            var result = (from o in owners
                          where owners.Any(x => x.Weight == o.Weight)
                          select o.Surname).Distinct().ToArray();

            return result;
        }
    }
}