using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VehicleApi.Controllers.Resources;
using VehicleApi.Core;
using VehicleApi.Core.Models;

namespace VehicleApi.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController:Controller
    {
        private readonly IPhotoRepository photoRepository;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment host;
        private readonly IMapper mapper;
        private readonly IOptionsSnapshot<PhotoSettings> options;
        
        private readonly PhotoSettings photoSettings;

        public PhotosController(IPhotoRepository photoRepository,IVehicleRepository repository,IUnitOfWork unitOfWork,IWebHostEnvironment host,IMapper mapper,IOptionsSnapshot<PhotoSettings>options)
        {
            this.photoRepository = photoRepository;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.host = host;
            this.mapper = mapper;
            this.photoSettings = options.Value;
        }
        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {
            var photos = await photoRepository.GetPhotos(vehicleId);
            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId,IFormFile file)
        {
            var vehicle =await repository.GetVehicle(vehicleId,icludeRelated:false);
            if(vehicle == null) { return NotFound(); }

            if (file == null) return BadRequest("Null File");
            if (file.Length == 0) return BadRequest("Empty File");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Maximum size file exeeded");
            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("invalid file type");

           var uploadesFolderPath=Path.Combine(Directory.GetCurrentDirectory(), "photos");
            if (!Directory.Exists(uploadesFolderPath)) { 
            Directory.CreateDirectory(uploadesFolderPath);
            }
            var fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName) ;
            var filePath = Path.Combine(uploadesFolderPath,fileName) ;
            Console.WriteLine(filePath);

            using(var stream =new FileStream(filePath,FileMode.Create))
            {
              await  file.CopyToAsync(stream);
            }
           var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
           await unitOfWork.CompleteAsync();
            return Ok(mapper.Map<Photo,PhotoResource>(photo));

        }
    }
}
