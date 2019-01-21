namespace Project3.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CurrentState")]
    public partial class CurrentState
    {
        [Key]
        [StringLength(50)]
        public string carID { get; set; }

        public int state { get; set; }

        [Required]
        [StringLength(100)]
        public string coordinate { get; set; }

        [StringLength(100)]
        public string location { get; set; }

        public int studentID { get; set; }

        public double speed { get; set; }

        public DateTime currentTime { get; set; }

        [StringLength(50)]
        public string studentName { get; set; }
    }
}
