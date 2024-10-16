using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DataAccess








{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Surname { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        [StringLength(60)]
        public string Adress { get; set; }
        [Required]
        [StringLength(250)]
        public string Job { get; set; }

        public List<Animal> Animals { get; } = new();
    }
}
