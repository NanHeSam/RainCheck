﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RainCheck
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RainCheckServerEntities : DbContext
    {
        public RainCheckServerEntities()
            : base("name=RainCheckServerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<car> cars { get; set; }
        public virtual DbSet<city> cities { get; set; }
        public virtual DbSet<coverage_level> coverage_level { get; set; }
        public virtual DbSet<credential> credentials { get; set; }
        public virtual DbSet<customer_tbl> customer_tbl { get; set; }
        public virtual DbSet<login> logins { get; set; }
        public virtual DbSet<policy_tbl> policy_tbl { get; set; }
        public virtual DbSet<quote> quotes { get; set; }
        public virtual DbSet<state> states { get; set; }
        public virtual DbSet<user_tbl> user_tbl { get; set; }
    }
}