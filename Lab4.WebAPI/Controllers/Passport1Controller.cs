using Lab2.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab4.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Passport1Controller : ControllerBase
    {
        private readonly OwnerDbContext _db;
        public DbSet<Passport> Passports { get; set; }



        // Konstruktorā inicializējam DbContext
        public Passport1Controller()
        {
            _db = new OwnerDbContext();
        }

        // 6.2. Datu saraksta iegūšana
        [HttpGet]
        public Passport[] GetPassport()
        {
            return _db.Passport.ToArray();
        }

        // 6.3. Viena ieraksta iegūšana pēc ID
        [HttpGet("{id}")]
        public Passport GetPassport(int id)
        {
            return _db.Passport.FirstOrDefault(s => s.Id == id);
        }

        // 6.4. Jauna ieraksta pievienošana
        [HttpPost]
        public void Post([FromBody] Passport passport)
        {
            _db.Passport.Add(passport);
            _db.SaveChanges();
        }

        // 6.5. Ieraksta dzēšana
        [HttpDelete("{id}")]
        public void DeletePassportr(int id)
        {
            var data = _db.Passport.FirstOrDefault(s => s.Id == id);

            if (data != null)
            {
                _db.Passport.Remove(data);
                _db.SaveChanges();
            }
        }

        // 6.6. Ieraksta labošana (update)
        [HttpPut("{id}")]
        public void UpdatePassport([FromBody] Passport passport, int id)
        {
            var existingPassport = _db.Passport.FirstOrDefault(s => s.Id == id);

            if (existingPassport != null)
            {
                existingPassport.Stamps = passport.Stamps;
                existingPassport.Vaccine = passport.Vaccine;
                _db.SaveChanges();
            }
        }
    }
}
