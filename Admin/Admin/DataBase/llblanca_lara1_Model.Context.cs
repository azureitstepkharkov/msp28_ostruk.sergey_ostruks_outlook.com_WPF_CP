﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Admin.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class llblanca_lara1Entities : DbContext
    {
        public llblanca_lara1Entities()
            : base("name=llblanca_lara1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<contact_types> contact_types { get; set; }
        public virtual DbSet<contacts> contacts { get; set; }
        public virtual DbSet<migrations> migrations { get; set; }
        public virtual DbSet<permission_role> permission_role { get; set; }
        public virtual DbSet<permissions> permissions { get; set; }
        public virtual DbSet<project_technology> project_technology { get; set; }
        public virtual DbSet<projects> projects { get; set; }
        public virtual DbSet<role_user> role_user { get; set; }
        public virtual DbSet<roles> roles { get; set; }
        public virtual DbSet<task_comment> task_comment { get; set; }
        public virtual DbSet<task_file> task_file { get; set; }
        public virtual DbSet<task_technology> task_technology { get; set; }
        public virtual DbSet<task_user> task_user { get; set; }
        public virtual DbSet<tasks> tasks { get; set; }
        public virtual DbSet<technologies> technologies { get; set; }
        public virtual DbSet<technology_user> technology_user { get; set; }
        public virtual DbSet<user_comment> user_comment { get; set; }
        public virtual DbSet<user_contact> user_contact { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<password_resets> password_resets { get; set; }
    }
}
