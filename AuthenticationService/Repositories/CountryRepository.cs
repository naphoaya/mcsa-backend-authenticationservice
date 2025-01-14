
using MCSABackend.Configurations;
using MCSABackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCSABackend.Repositories
{
    public class CountryRepository:ICountryRepository
    {
        private MCSA_Context _context;
        public CountryRepository(MCSA_Context context)
        {
            _context = context;
        }
        public List<Countries> GetCountriesList()
        {
            return  _context.Country.ToList();
            
        }
    }
}
