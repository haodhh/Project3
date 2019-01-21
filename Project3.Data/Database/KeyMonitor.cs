namespace Project3.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KeyMonitor")]
    public partial class KeyMonitor
    {
        public int ID { get; set; }

        [Required]
        public string Key { get; set; }
    }
}
