namespace projectMVC_game.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class gra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public gra()
        {
            this.wypozyczenies = new HashSet<wypozyczenie>();
        }
    
        public int id_gra { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string nazwa { get; set; }
        [Range(0, 21, ErrorMessage = "Maksymalne ograniczenie 21 rok")]
        public int ograniczenie_wiekowe { get; set; }
        [Required(ErrorMessage = "Enter gatunek")]
        public string gatunek { get; set; }
        [Required(ErrorMessage = "Enter platforma")]
        public string platforma { get; set; }
        [Required(ErrorMessage = "Enter cena")]
        [Range(0, 999.99, ErrorMessage = "Za duza cyfra,max 999.99")]
        public double cena { get; set; }
        [Required(ErrorMessage = "Enter release year")]
        [Range(0, 2018, ErrorMessage = "Nie jest jeszcze takiego roku")]
        public int release_year { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wypozyczenie> wypozyczenies { get; set; }
    }
}
