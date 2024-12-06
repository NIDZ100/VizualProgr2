using Lab2.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab4.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Owner1Controller : ControllerBase
    {
        private readonly OwnerDbContext _db;
        public DbSet<Owner> Owners { get; set; }



        // Konstruktorā inicializējam DbContext
        public Owner1Controller()
        {
            _db = new OwnerDbContext();
        }

        // 6.2. Datu saraksta iegūšana
        [HttpGet]
        public Owner[] GetOwnerss()
        {
            return _db.Owners.ToArray();
        }

        // 6.3. Viena ieraksta iegūšana pēc ID
        [HttpGet("{id}")]
        public Owner GetOwner(int id)
        {
            return _db.Owners.FirstOrDefault(s => s.Id == id);
        }

        // 6.4. Jauna ieraksta pievienošana
        [HttpPost]
        public void Post([FromBody] Owner owner)
        {
            _db.Owners.Add(owner);
            _db.SaveChanges();
        }

        // 6.5. Ieraksta dzēšana
        [HttpDelete("{id}")]
        public void DeleteOwner(int id)
        {
            var data = _db.Owners.FirstOrDefault(s => s.Id == id);

            if (data != null)
            {
                _db.Owners.Remove(data);
                _db.SaveChanges();
            }
        }

        // 6.6. Ieraksta labošana (update)
        [HttpPut("{id}")]
        public void UpdateOwner([FromBody] Owner owner, int id)
        {
            var existingOwner = _db.Owners.FirstOrDefault(s => s.Id == id);

            if (existingOwner != null)
            {
                existingOwner.Job = owner.Job;
                existingOwner.Surname = owner.Surname;
                _db.SaveChanges();
            }
        }
    }
}
