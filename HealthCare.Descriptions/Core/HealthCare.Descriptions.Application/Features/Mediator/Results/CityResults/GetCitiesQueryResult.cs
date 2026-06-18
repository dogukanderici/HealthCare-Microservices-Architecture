using HealthCare.Descriptions.Application.Features.Mediator.Results.GenericResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Results.CityResults
{
    public class GetCitiesQueryResult : GenericAuditResult
    {
        public Guid Id { get; set; }
        public int Plate { get; set; }
        public string CityName { get; set; }
        public bool IsAvailable { get; set; }
    }
}