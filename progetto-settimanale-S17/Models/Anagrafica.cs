using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace progetto_settimanale_S17.Models;

[Table("Anagrafica")]
public partial class Anagrafica
{
    [Key]
    public Guid IdAnagrafica { get; set; }

    [StringLength(20)]
    public string Cognome { get; set; } = null!;

    [StringLength(20)]
    public string Nome { get; set; } = null!;

    [StringLength(30)]
    public string Indirizzo { get; set; } = null!;

    [StringLength(20)]
    public string Citta { get; set; } = null!;

    [Column("CAP")]
    [StringLength(5)]
    public string Cap { get; set; } = null!;

    [StringLength(16)]
    public string CodiceFiscale { get; set; } = null!;

    [InverseProperty("IdAnagraficaNavigation")]
    public virtual ICollection<Verbale> Verbales { get; set; } = new List<Verbale>();
}
