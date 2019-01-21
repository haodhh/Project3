namespace Project3.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Schedule
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public int? Hour { get; set; }

        public int? Day { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public int IDCar { get; set; }

        public int IDStudent { get; set; }

        public int? IDTeacher { get; set; }

        public int? Status { get; set; }
    }
}
