﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Words
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WordsEntities : DbContext
    {
        public WordsEntities()
            : base("name=WordsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cetsix> cetsix { get; set; }
        public virtual DbSet<Comment_t> Comment_t { get; set; }
        public virtual DbSet<Record_t> Record_t { get; set; }
        public virtual DbSet<User_t> User_t { get; set; }
    }
}
