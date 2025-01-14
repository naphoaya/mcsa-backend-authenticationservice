
using MCSABackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCSABackend.Repositories
{
    public interface ICountryRepository
    {
        List<Countries> GetCountriesList();
    }
}
