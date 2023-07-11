using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeb.Models.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a Category name.")]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime createdDateTime { get; set; } = DateTime.Now;
    }
}
