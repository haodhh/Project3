namespace Project3.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Historys")]
    public partial class History
    {
        public Guid historyID { get; set; }

        [Required]
        [StringLength(50)]
        public string carID { get; set; }

        [Required]
        [StringLength(50)]
        public string carName { get; set; }

        public int studentID { get; set; }

        [Required]
        [StringLength(50)]
        public string studentName { get; set; }

        public int month { get; set; }

        public int day { get; set; }

        public int year { get; set; }

        public DateTime exactTime { get; set; }

        public int state { get; set; }

        [Required]
        [StringLength(100)]
        public string coordinate { get; set; }

        [Required]
        [StringLength(100)]
        public string location { get; set; }

        public double speed { get; set; }

        public int scheduleID { get; set; }
    }
}
