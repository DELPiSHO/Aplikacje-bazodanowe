namespace projectMVC_game.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class pracownik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pracownik()
        {
            this.wypozyczenies = new HashSet<wypozyczenie>();
        }
    
        public int id_pracownik { get; set; }
        [Required(ErrorMessage = "Wpisz imie")]
        public string imie { get; set; }
        [Required(ErrorMessage = "Wpisz nazwisko")]
        public string nazwisko { get; set; }
        [Required(ErrorMessage = "Wpisz pesel")]
        [StringLength(11, MinimumLength = 11)]
        public string pesel { get; set; }
        [Required(ErrorMessage = "Wpisz email")]
        public string email { get; set; }
        public int id_adres { get; set; }
    
        public virtual adres adres { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wypozyczenie> wypozyczenies { get; set; }
    }
}
