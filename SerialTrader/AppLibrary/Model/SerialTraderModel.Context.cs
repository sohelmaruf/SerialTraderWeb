﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppLibrary.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class serialtraderEntities : DbContext
    {
        public serialtraderEntities()
            : base("name=serialtraderEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<taccount> taccounts { get; set; }
        public DbSet<texchanx> texchanges { get; set; }
        public DbSet<tkey> tkeys { get; set; }
        public DbSet<tmarket> tmarkets { get; set; }
        public DbSet<tmarkettick> tmarketticks { get; set; }
        public DbSet<tmastertrade> tmastertrades { get; set; }
        public DbSet<ttrade> ttrades { get; set; }
        public DbSet<ttradestosync> ttradestosyncs { get; set; }
        public DbSet<tworkerassignment> tworkerassignments { get; set; }
        public DbSet<tworker> tworkers { get; set; }
        public DbSet<tprice> tprices { get; set; }
    }
}