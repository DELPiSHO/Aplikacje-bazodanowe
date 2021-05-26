namespace projectMVC_game.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class adres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public adres()
        {
            this.klients = new HashSet<klient>();
            this.pracowniks = new HashSet<pracownik>();
        }
    
        public int id_adres { get; set; }
        [Required(ErrorMessage = "Wpisz panstwo")]
        public string panstwo { get; set; }
        [Required(ErrorMessage = "Wpisz miasto")]
        public string miasto { get; set; }
        [Required(ErrorMessage = "Wpisz ulice")]
        public string ulica { get; set; }
        [Required(ErrorMessage = "Wpisz nr_domu")]
        public string nr_domu { get; set; }
        [Required(ErrorMessage = "Wpisz nr_pokoju")]
        public int nr_pokoju { get; set; }
        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<klient> klients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pracownik> pracowniks { get; set; }
    }
}
