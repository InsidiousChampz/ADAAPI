﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADAAPI
{
    public partial class tbl_A
    {
        [Key]
        public int Id { get; set; }
        [StringLength(5)]
        public string empId { get; set; }
        [StringLength(50)]
        public string empFirstname { get; set; }
        [StringLength(50)]
        public string empLastname { get; set; }
        [StringLength(10)]
        public string empPhoneNumber { get; set; }
        public int empStatus { get; set; }
        [Column(TypeName = "date")]
        public DateTime createdDate { get; set; }
        public bool isActive { get; set; }
    }
}