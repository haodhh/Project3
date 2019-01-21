namespace Project3.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MonitorOnline
    {
        public int ID { get; set; }

        public int? IDSchedule { get; set; }

        public int? IDCar { get; set; }

        public int? IDStudent { get; set; }

        public double? LocationX { get; set; }

        public double? LocationY { get; set; }

        public int? Speed { get; set; }

        public int? Status { get; set; }

        public int? Second { get; set; }

        public int? Minute { get; set; }

        public int? Hour { get; set; }

        public int? Day { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }
    }
}
