using AutoMapper;
using MCSABackend.DTOs;
using MCSABackend.Models;
using MCSABackend.Repositories;
using MCSABackend.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSABackend.Services
{
    public class TestService : ITestService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public TestService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public  List<Countries>  GetCountriesList()
        {
            var result = _countryRepository.GetCountriesList();
            return result;
        }
    }
}
