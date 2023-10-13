namespace INFORNO_EF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dettagli")]
    public partial class Dettagli
    {
        [Key]
        public int IdDettaglio { get; set; }

        public int FKPizza { get; set; }

        public int? Quantita { get; set; }

        public int FKOrdine { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Pizze Pizze { get; set; }
    }
}
