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

        [Display(Name = "Pizza")]
        public int FKPizza { get; set; }

        [Display(Name = "Quantità")]
        public int? Quantita { get; set; }

        public int FKOrdine { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Pizze Pizze { get; set; }
    }
}
