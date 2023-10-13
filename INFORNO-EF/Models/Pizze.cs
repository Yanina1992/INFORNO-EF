namespace INFORNO_EF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pizze")]
    public partial class Pizze
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pizze()
        {
            Dettagli = new HashSet<Dettagli>();
        }

        [Key]
        public int IdPizza { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Nome pizza")]
        public string Nome { get; set; }

        [Display(Name = "Immagine")]
        [StringLength(50)]
        public string Foto { get; set; }

        [NotMapped]
        public string FileFoto { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Prezzo")]
        public string Prezzo { get; set; }

        [StringLength(50)]
        [Display(Name = "Tempo di consegna")]
        public string TempoConsegna { get; set; }

        [StringLength(200)]
        [Display(Name = "Ingredienti")]
        public string Ingredienti { get; set; }

        [Display(Name = "Quantità")]
        public int? Quantita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dettagli> Dettagli { get; set; }
    }
}
