using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NerdDinner.Models
{
    public partial class Dinner
    {
        [HiddenInput(DisplayValue = false)]
        public int DinnerID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title may not be longer than 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Event Date is required")]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(256, ErrorMessage = "Description may not be longer than 256 characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(20, ErrorMessage = "Hosted By name may not be longer than 20 characters")]
        [Display(Name = "Host's Name")]
        public string HostedBy { get; set; }

        [Phone]
        [Required(ErrorMessage = "Phone number is required")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(50, ErrorMessage = "Address may not be longer than 50 characters")]
        [Display(Name = "Address, City, State, ZIP")]
        public string Address { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public bool IsHostedBy(string userName)
        {
            return HostedBy.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual  ICollection<RSVP> RSVPs { get; set; } 
    }

    public class RuleViolation
    {
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }

        public RuleViolation(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }
}