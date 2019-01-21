namespace Project3.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? Age { get; set; }

        public int? Gender { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public int? Status { get; set; }

        [Required]
        [StringLength(20)]
        public string User { get; set; }

        public string Model { get; set; }

        public string Avata { get; set; }
    }
}
