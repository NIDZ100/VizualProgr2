using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DataAccess
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
        public Owner Owner { get; set; }
        public Passport Passport { get; set; }
    }
}
