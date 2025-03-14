using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace progetto_settimanale_S17.Models;

[PrimaryKey("IdAnagrafica", "IdVerbale", "IdViolazione")]
[Table("Verbale")]
public partial class Verbale
{
    [Key]
    public int IdVerbale { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataViolazione { get; set; }

    [StringLength(30)]
    public string IndirizzoViolazione { get; set; } = null!;

    [StringLength(30)]
    public string NominativoAgente { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DataTrascrizioneVerbale { get; set; }

    [Column(TypeName = "money")]
    public decimal Importo { get; set; }

    public int? DecurtamentoPunti { get; set; }

    public bool Contestabile { get; set; }

    [Key]
    public int IdViolazione { get; set; }

    [Key]
    public Guid IdAnagrafica { get; set; }

    [ForeignKey("IdAnagrafica")]
    [InverseProperty("Verbales")]
    public virtual Anagrafica IdAnagraficaNavigation { get; set; } = null!;

    [ForeignKey("IdViolazione")]
    [InverseProperty("Verbales")]
    public virtual Violazione IdViolazioneNavigation { get; set; } = null!;
}
