﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HotelEntities : DbContext
    {
        public HotelEntities()
            : base("name=HotelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cennik> Cennik { get; set; }
        public virtual DbSet<Faktura> Faktura { get; set; }
        public virtual DbSet<KlasaPokoju> KlasaPokoju { get; set; }
        public virtual DbSet<Klient> Klient { get; set; }
        public virtual DbSet<Kraj> Kraj { get; set; }
        public virtual DbSet<Obecnosc> Obecnosc { get; set; }
        public virtual DbSet<Platnosc> Platnosc { get; set; }
        public virtual DbSet<Pokoj> Pokoj { get; set; }
        public virtual DbSet<Pracownik> Pracownik { get; set; }
        public virtual DbSet<Rezerwacja> Rezerwacja { get; set; }
        public virtual DbSet<SposobPlatnosci> SposobPlatnosci { get; set; }
        public virtual DbSet<Stanowisko> Stanowisko { get; set; }
        public virtual DbSet<StatusPlatnosci> StatusPlatnosci { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TypPokoju> TypPokoju { get; set; }
        public virtual DbSet<VAT> VAT { get; set; }
        public virtual DbSet<Znizka> Znizka { get; set; }
    }
}
