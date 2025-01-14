using MCSABackend.DTOs;
using MCSABackend.Models;
using MCSABackend.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCSABackend.Services
{
    public interface ITestService
    {
        List<Countries> GetCountriesList();
    }
}
