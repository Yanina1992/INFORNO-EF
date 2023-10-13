namespace INFORNO_EF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordini")]
    public partial class Ordini
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordini()
        {
            Dettagli = new HashSet<Dettagli>();
        }

        [Key]
        public int IdOrdine { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Indirizzo di spedizione")]
        public string IndirizzoSpedizione { get; set; }

        [StringLength(200)]
        [Display(Name = "Note")]
        public string Note { get; set; }

        public int FKUtente { get; set; }

        [Column(TypeName = "money")]
        public decimal ImportoTotale { get; set; }

        [Display(Name= "Hai concluso il tuo ordine?")]
        public bool? Concluso { get; set; }

        public bool? Evaso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dettagli> Dettagli { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
