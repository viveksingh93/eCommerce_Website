using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using myWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeb.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int productId { get; set; }
        [ValidateNever]
        public Product product { get; set; }
        [ValidateNever]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public int count { get; set; }
        
    }
}
