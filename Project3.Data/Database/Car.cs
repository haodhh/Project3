namespace Project3.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Car
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string NumberCar { get; set; }

        public int? Type { get; set; }

        public int? Status { get; set; }
    }
}
