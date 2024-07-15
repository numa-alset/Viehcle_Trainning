using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleApi.Controllers.Resources;
using VehicleApi.Core.Models;
using VehicleApi.Persistance;

namespace VehicleApi.Controllers
{
    public class FeatureController : ControllerBase
    {
        private readonly VehicleApiDbContext context;
        private readonly IMapper mapper;

        public FeatureController(VehicleApiDbContext context,IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("/api/featurs")]
        public  IEnumerable<KeyValuePairResource> GetFeatures()
        {
            var features =  context.Features.ToList();
            return mapper.Map<List<Feature>,List<KeyValuePairResource>>(features);
        }

    }
}
