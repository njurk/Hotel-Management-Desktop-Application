//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVVMFirma.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Platnosc
    {
        public int IdPlatnosci { get; set; }
        public int IdRezerwacji { get; set; }
        public int IdSposobuPlatnosci { get; set; }
        public int IdStatusuPlatnosci { get; set; }
        public System.DateTime DataPlatnosci { get; set; }
        public decimal Kwota { get; set; }
    
        public virtual Rezerwacja Rezerwacja { get; set; }
        public virtual SposobPlatnosci SposobPlatnosci { get; set; }
        public virtual StatusPlatnosci StatusPlatnosci { get; set; }
    }
}