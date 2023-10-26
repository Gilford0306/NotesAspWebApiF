using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;


namespace DAL.EF
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }

    }
}
