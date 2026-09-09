using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Cities.Results
{
    public class GetCitiesByFilterQueryResult : IListResult
    {
        public Guid Id { get; set; }
        public int Plate { get; set; }
        public string CityName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
