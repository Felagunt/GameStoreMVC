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

        [Required(ErrorMessage = "Enter your adress")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Select town")]
        public string City { get; set; }

        [Required(ErrorMessage = "Choose your country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
