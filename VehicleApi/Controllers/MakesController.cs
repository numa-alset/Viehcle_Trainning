﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleApi.Controllers.Resources;
using VehicleApi.Core.Models;
using VehicleApi.Persistance;

namespace VehicleApi.Controllers
{
    public class MakesController:Controller
    {
        private readonly VehicleApiDbContext context;
        private readonly IMapper mapper;

       public  MakesController(VehicleApiDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/api/makes")]
        public async Task< IEnumerable<MakeResource>> GetMakes() 
        {
            var makes= await context.Makes.Include(m=>m.Models).ToListAsync();
            return mapper.Map<List<Make>,List<MakeResource>>(makes);
        }
    }
}
