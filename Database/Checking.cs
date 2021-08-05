namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Checking")]
    public partial class Checking
    {
        [Column("checkingId")]
        public int CheckingId { get; set; }

        [Column("yenValue", TypeName = "money")]
        public decimal? YenValue { get; set; }

        [Column("checkDate")]
        public DateTime? CheckDate { get; set; }
    }
}
