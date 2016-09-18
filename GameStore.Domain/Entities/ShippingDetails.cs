using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Write your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your address")]
        [Display(Name="First address")]
        public string Line1 { get; set; }
        [Display(Name="Second address")]
        public string Line2 { get; set; }
        [Display(Name="Third address")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Select town")]
        [Display(Name = "Town")]
        public string City { get; set; }

        [Required(ErrorMessage = "Choose your country")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
