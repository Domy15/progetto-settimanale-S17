using System.ComponentModel.DataAnnotations;
using progetto_settimanale_S17.Models;

namespace progetto_settimanale_S17.ViewModels
{
    public class AddVerbalViewModel
    {
        public string Cognome { get; set; }

        public string Nome { get; set; }

        public string Indirizzo { get; set; }

        public string Citta { get; set; }

        public string Cap { get; set; }

        public string CodiceFiscale { get; set; }

        public DateTime DataViolazione { get; set; }

        public string IndirizzoViolazione { get; set; } = null!;

        public string NominativoAgente { get; set; } = null!;

        public DateTime DataTrascrizioneVerbale { get; set; }

        public decimal Importo { get; set; }

        public int? DecurtamentoPunti { get; set; }

        public bool Contestabile { get; set; }

        public int IdViolazione { get; set; }

        public List<Violazione> violazioni { get; set; }
    }
}
