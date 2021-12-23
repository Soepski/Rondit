using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.ViewModels
{
    public class PostCreateViewModel
    {
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostDescription { get; set; }
    }
}
