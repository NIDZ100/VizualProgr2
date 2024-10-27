using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DataAccess
{
    public class Passport
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Stamps { get; set; }
        [Required]
        public string Vaccine { get; set; }
        [Required]
        [StringLength(60)]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(250)]
        public string Notes { get; set; }
        public int AnimalId { get; set; }
        public Animal  Animal { get; set; } 
    }

}
