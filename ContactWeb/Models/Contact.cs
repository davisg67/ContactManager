using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactWeb.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        
        [DisplayName("Group")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [DisplayName("Phone (primary)")]
        public string PhonePrimary { get; set; }

        [DisplayName("Phone (secondary)")]
        public string PhoneSecondary { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Birthday")]
        public DateTime? BirthDate { get; set; }

        [DisplayName("Address 1")]
        public string StreetAddress1 { get; set; }

        [DisplayName("Address 2")]
        public string StreetAddress2 { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("State")]
        public string State { get; set; }

        [DisplayName("Zip")]
        public string ZipCode { get; set; }
    }

    
}