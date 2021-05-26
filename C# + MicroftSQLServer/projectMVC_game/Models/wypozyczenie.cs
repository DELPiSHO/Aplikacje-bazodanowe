namespace projectMVC_game.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class wypozyczenie
    {
        public int id_wypozyczenie { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true )]
        public System.DateTime data_wypozyczenia { get; set; }
        public int id_gra { get; set; }
        [Display(Name = "Imie pracownika")]
        public int id_pracownik { get; set; }
        [Display(Name = "Imie klienta")]
        public int id_klient { get; set; }
    
        public virtual gra gra { get; set; }
        public virtual klient klient { get; set; }
        public virtual pracownik pracownik { get; set; }
    }
}
