﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSTS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DemirbasTakipEntities : DbContext
    {
        public DemirbasTakipEntities()
            : base("name=DemirbasTakipEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblDemirba> tblDemirbas { get; set; }
        public virtual DbSet<tblDepartman> tblDepartmen { get; set; }
        public virtual DbSet<tblFakulte> tblFakultes { get; set; }
        public virtual DbSet<tblLog> tblLogs { get; set; }
        public virtual DbSet<tblOda> tblOdas { get; set; }
        public virtual DbSet<tblOdaDemirba> tblOdaDemirbas { get; set; }
        public virtual DbSet<tblPersonel> tblPersonels { get; set; }
        public virtual DbSet<tblTur> tblTurs { get; set; }
        public virtual DbSet<tblYetki> tblYetkis { get; set; }
    }
}
