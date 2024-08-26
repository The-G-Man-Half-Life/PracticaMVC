using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticaMVC.Models;

namespace PracticaMVC.DataBases;
public class ApplicationDbContext : DbContext
{
    public DbSet<DocumentType> DocumentTypes {get; set;}

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {

    }
}
