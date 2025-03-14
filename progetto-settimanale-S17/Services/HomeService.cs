using Microsoft.EntityFrameworkCore;
using progetto_settimanale_S17.Data;
using progetto_settimanale_S17.Models;
using progetto_settimanale_S17.ViewModels;

namespace progetto_settimanale_S17.Services
{
    public class HomeService
    {
        private readonly ApplicationDbContext _context;

        public HomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Violazione>> GetViolations()
        {
            try
            {
                return await _context.Violaziones.ToListAsync();
            }
            catch
            {
                return new List<Violazione>();
            }
        }

        public async Task<List<Verbale>> GetVerbals()
        {
            try
            {
                return await _context.Verbales.OrderBy(v => v.IdVerbale).Include(v => v.IdAnagraficaNavigation).Include(v => v.IdViolazioneNavigation).ToListAsync();
            }
            catch
            {
                return new List<Verbale>();
            }
        }

        public async Task<bool> CreateVerbalAsync(AddVerbalViewModel addVerbalViewModel)
        {
            var person = await _context.Anagraficas.Where(a => a.CodiceFiscale == addVerbalViewModel.CodiceFiscale).FirstOrDefaultAsync();

            if (person == null) 
            {
                person = new Anagrafica()
                {
                    IdAnagrafica = Guid.NewGuid(),
                    Cognome = addVerbalViewModel.Cognome,
                    Nome = addVerbalViewModel.Nome,
                    Indirizzo = addVerbalViewModel.Indirizzo,
                    Citta = addVerbalViewModel.Citta,
                    Cap = addVerbalViewModel.Cap,
                    CodiceFiscale = addVerbalViewModel.CodiceFiscale,
                };

                _context.Anagraficas.Add(person);
            }

            var maxIdVerbal = await _context.Verbales.OrderByDescending(v => v.IdVerbale).Select(v => v.IdVerbale).FirstOrDefaultAsync();

            var verbal = new Verbale()
            {
                IdVerbale = maxIdVerbal + 1,
                DataViolazione = addVerbalViewModel.DataViolazione,
                IndirizzoViolazione = addVerbalViewModel.IndirizzoViolazione,
                NominativoAgente = addVerbalViewModel.NominativoAgente,
                DataTrascrizioneVerbale = addVerbalViewModel.DataTrascrizioneVerbale,
                Importo = addVerbalViewModel.Importo,
                DecurtamentoPunti = addVerbalViewModel.DecurtamentoPunti,
                Contestabile = addVerbalViewModel.Contestabile,
                IdViolazione = addVerbalViewModel.IdViolazione,
                IdAnagrafica = person.IdAnagrafica
            };

            _context.Verbales.Add(verbal);

            return await SaveAsync();
        }

        public async Task<List<Verbale>> GetContestableVerbals()
        {
            try
            {
                return await _context.Verbales.OrderBy(v => v.IdVerbale).Where(v => v.Contestabile == true).Include(v => v.IdAnagraficaNavigation).Include(v => v.IdViolazioneNavigation).ToListAsync();
            }
            catch
            {
                return new List<Verbale>();
            }
        }

        public async Task<List<Verbale>> GetVerbalsWithHighImport()
        {
            try
            {
                return await _context.Verbales.OrderBy(v => v.IdVerbale).Where(v => v.Importo >= 400).Include(v => v.IdAnagraficaNavigation).Include(v => v.IdViolazioneNavigation).ToListAsync();
            }
            catch
            {
                return new List<Verbale>();
            }
        }

        public async Task<List<Verbale>> GetVerbalsWithHighPoints()
        {
            try
            {
                return await _context.Verbales.OrderBy(v => v.IdVerbale).Where(v => v.DecurtamentoPunti >= 10).Include(v => v.IdAnagraficaNavigation).Include(v => v.IdViolazioneNavigation).ToListAsync();
            }
            catch
            {
                return new List<Verbale>();
            }
        }
    }
}
