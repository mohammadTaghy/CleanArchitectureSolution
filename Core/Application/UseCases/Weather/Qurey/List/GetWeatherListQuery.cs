using Application.Common;
using Application.Decorators;
using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Weather
{
    
    public class GetWeatherListQuery : IQuery<List<WeatherDto>>
    {
        public string[] WeatherTypes { get; set; }

        public GetWeatherListQuery(string[] types)
        {
            WeatherTypes = types;
        }


        
        public sealed class GetWeatherListQueryHandler : IQueryHandler<GetWeatherListQuery, List<WeatherDto>>
        {
            public List<WeatherDto> Handle(GetWeatherListQuery query)
            {
                List<WeatherDto> weatherList = new();
               foreach(var type in query.WeatherTypes)
                {
                    weatherList.Add(new WeatherDto { Name = type });
                }

               return weatherList;
            }
        }
    }
}
