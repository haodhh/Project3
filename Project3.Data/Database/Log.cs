namespace Project3.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [Column(TypeName = "text")]
        public string Notes { get; set; }
    }
}
