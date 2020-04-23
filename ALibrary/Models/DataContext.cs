﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection") {}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Slide> Slider { get; set; }
        public DbSet<Banner> Advertising { get; set; }
    }
}