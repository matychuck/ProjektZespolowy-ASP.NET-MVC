﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Resources;

namespace SystemRezerwacjiKortow.Models
{
    public class Hire
    {
        public int HireID { get; set; }

        [Display(Name = "DateStart", ResourceType = typeof(Texts))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "DateEnd", ResourceType = typeof(Texts))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTo { get; set; }

        [Display(Name = "GearID", ResourceType = typeof(Texts))]
        public int GearID { get; set; }

        [Display(Name = "GearAmount", ResourceType = typeof(Texts))]
        public int GearAmount { get; set; }

        [Display(Name = "CourtID", ResourceType = typeof(Texts))]
        public int CourtID { get; set; }

        [Display(Name = "DuePayment", ResourceType = typeof(Texts))]
        [DataType(DataType.Currency)]
        public decimal Payment { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Texts))]
        public int UserID { get; set; }

        [Display(Name = "DatePayment", ResourceType = typeof(Texts))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DatePayment{ get; set; }
    }
}