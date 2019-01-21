namespace Project3.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RFIDCard
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string CodeCard { get; set; }

        public int IDStudent { get; set; }

        public int Status { get; set; }
    }
}