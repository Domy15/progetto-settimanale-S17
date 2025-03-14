using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace progetto_settimanale_S17.Models;

[Table("Violazione")]
public partial class Violazione
{
    [Key]
    public int IdViolazione { get; set; }

    public string Descrizione { get; set; } = null!;

    [InverseProperty("IdViolazioneNavigation")]
    public virtual ICollection<Verbale> Verbales { get; set; } = new List<Verbale>();
}
