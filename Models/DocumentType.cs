using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaMVC.Models;

[Table("document_types")]

public class DocumentType
{
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    [Column("name")]
    [MinLength(1,ErrorMessage ="Must use at least 1 characters")]
    [MaxLength(50,ErrorMessage ="Must be at most 50 characters")]
    public required string Name { get; set; }

    [Column("abbreviation")]
    [MinLength(2,ErrorMessage ="Must use at least 2 characters")]
    [MaxLength(10,ErrorMessage ="Must be at most 10 characters")]
    public required string Abbreviation { get; set; }

    [Column("description")]
    [MaxLength(500,ErrorMessage ="Must use at most 500 characters")]
    public string? Description { get; set; }
}
