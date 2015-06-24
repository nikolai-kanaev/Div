using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pub.Models
{
    public class PublicProfile
    {
        [Key]
        public int Id { get; set; }
        
        public string UserId { get; set; }

        [Display(Name="Display name")]
        public string DisplayName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
    }
}