using AutoMapper;
using AutoMapper.Features;
using VehicleApi.Controllers.Resources;
using VehicleApi.Core.Models;

namespace VehicleApi.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {

            // Domain to API Resource
            CreateMap<Photo,PhotoResource>();
            CreateMap(typeof(QueryResult<>),typeof(QueryResultResource<>));
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v=>v.FeatureVehicles.Select(vf=>vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.FeatureVehicles, opt => opt.MapFrom(v => v.FeatureVehicles.Select(vf=>new KeyValuePairResource { Id=v.Features.First(f=>f.Id==vf.FeatureId).Id,Name= v.Features.First(f => f.Id == vf.FeatureId).Name })))
                .ForMember(vr=>vr.Make,opt=>opt.MapFrom(v=>v.Model.Make))
                ;

            // API Resource to Domain
            CreateMap<VehicleQueryResource, VehicleQuery>();
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v=>v.Id,opt=>opt.Ignore())
                .ForMember(v=>v.FeatureVehicles,opt=> opt.Ignore())
                .ForMember(v=>v.Features,opt=>opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    // no LINQ
                    /*
                    // Remove unselected Features
                    var removedFeatures = new List<FeatureVehicle>();
                    foreach (var f in v.FeatureVehicles)
                        if (!vr.Features.Contains(f.FeatureId))
                            removedFeatures.Add(f);
                    foreach (var f in removedFeatures)
                        v.FeatureVehicles.Remove(f);
                    // Add new features
                    foreach (var id in vr.Features)
                        if (!v.FeatureVehicles.Any(f => f.FeatureId == id))
                            v.FeatureVehicles.Add(new FeatureVehicle { FeatureId = id });
                    */
                    //using LINQ
                    // Remove unselected Features
                    var removedFeatures =v.FeatureVehicles.Where(f=>!vr.Features.Contains(f.FeatureId)).ToList();
                    foreach (var f in removedFeatures)
                        v.FeatureVehicles.Remove(f);
                    // Add new features
                    var addedFeatures = vr.Features.Where(id => !v.FeatureVehicles.Any(f => f.FeatureId == id)).Select(id=>new FeatureVehicle { FeatureId=id}).ToList();
                    foreach (var f in addedFeatures)
                        v.FeatureVehicles.Add(f);
                })
                ;
        }
    }
}
