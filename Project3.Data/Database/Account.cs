namespace Project3.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        [Key]
        [StringLength(20)]
        public string User { get; set; }

        [Required]
        public string Pass { get; set; }

        public int Level { get; set; }

        public int Status { get; set; }
    }
}
