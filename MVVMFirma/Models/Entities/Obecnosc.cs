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
    
    public partial class Obecnosc
    {
        public int IdObecnosci { get; set; }
        public int IdPracownika { get; set; }
        public System.DateTime Data { get; set; }
        public bool CzyObecny { get; set; }
        public bool CzyUsprawiedliwiony { get; set; }
        public Nullable<System.TimeSpan> GodzinaRozpoczecia { get; set; }
        public Nullable<System.TimeSpan> GodzinaZakonczenia { get; set; }
        public string Uwagi { get; set; }
    
        public virtual Pracownik Pracownik { get; set; }
    }
}
