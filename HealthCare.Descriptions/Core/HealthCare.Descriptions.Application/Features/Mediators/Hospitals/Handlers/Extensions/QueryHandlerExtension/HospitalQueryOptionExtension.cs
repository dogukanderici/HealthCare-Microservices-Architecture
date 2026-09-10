using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Handlers.Extensions.QueryHandlerExtension
{
    public static class HospitalQueryOptionExtension
    {
        public static DBQueryOptions<Hospital> AddCityWithDistrict(this DBQueryOptions<Hospital> query)
        {
            query.thenIncludes = new Dictionary<Expression<Func<Hospital, object>>, List<Expression<Func<object, object>>>>()
                {
                    {
                        x=>x.City,
                        new List<Expression<Func<object, object>>>{}
                    },
                    {
                        x=>x.District,
                        new List<Expression<Func<object, object>>>{}
                    }
                };

            return query;
        }
    }
}
