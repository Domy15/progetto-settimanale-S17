using Microsoft.EntityFrameworkCore;
using progetto_settimanale_S17.Data;
using progetto_settimanale_S17.Models;
using progetto_settimanale_S17.ViewModels;

namespace progetto_settimanale_S17.Services
{
    public class QueryService
    {
        private readonly ApplicationDbContext _context;

        public QueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TotalVerbalViewModel> GetTotalVerbalByPerson()
        {
            try
            {
                var totalVerbals = await _context.Verbales.Include(v => v.IdAnagraficaNavigation).GroupBy(v => v.IdAnagrafica).Select(g => new QueryTotalVerbal
                {
                    Name = g.FirstOrDefault().IdAnagraficaNavigation.Nome,
                    Surname = g.FirstOrDefault().IdAnagraficaNavigation.Cognome,
                    TotVerbali = g.Select(v => v.IdVerbale).Distinct().Count()
                }).ToListAsync();

                return new TotalVerbalViewModel() { queryTotalVerbals = totalVerbals };
            }
            catch
            {
                return new TotalVerbalViewModel();
            }
        }

        public async Task<TotalPointsViewModel> GetTotalPointsByPerson()
        {
            try
            {
                var totalPoints = await _context.Verbales.Include(v => v.IdAnagraficaNavigation).GroupBy(v => v.IdAnagrafica).Select(g => new QueryTotalPoints
                {
                    Name = g.FirstOrDefault().IdAnagraficaNavigation.Nome,
                    Surname = g.FirstOrDefault().IdAnagraficaNavigation.Cognome,
                    TotPunti = g.GroupBy(v => v.IdVerbale).Select(g => g.FirstOrDefault().DecurtamentoPunti).Sum()
                }).ToListAsync();

                return new TotalPointsViewModel() { TotalPoints = totalPoints };
            }
            catch
            {
                return new TotalPointsViewModel();
            }
        }
    }
}
