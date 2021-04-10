using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBAccessLibrary.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using DBAccessLibrary.DBEntities;
    using DBAccessLibrary.Repositories;
    using AutoMapper;
    using DBAccessLibrary.DTOs;
    using global::WebAPI.Controllers;
    using CommonLibrary;
    using Microsoft.AspNetCore.Cors;

    namespace WebAPI.Controllers
    {
        [Route("api/[controller]")]
        [EnableCors("AllowAllOrigins")]
        [ApiController]
        public class VehiclesController : CustomControllerBase
        {
            private readonly IVehicleRepository _repository;

            public VehiclesController(IVehicleRepository repository, IMapper mapper):base(mapper)
            {
                _repository = repository;
            }

            // GET: api/Vehicles
            //for paginated grids
            [HttpGet]
            public async Task<ActionResult> GetEntities([FromQuery] int page=0, int pageSize=0)
            {
                return await GetViewEntities<Vehicle, VehicleView, VehicleViewRecordDTO>(_repository, "Id", true, page, pageSize);
            }

            // GET: api/Vehicles/5
            [HttpGet("{id}")]
            public async Task<IActionResult> GetEntity(int id)
            {
                return await GetViewEntity<Vehicle, VehicleView, VehicleViewRecordDTO>(_repository, id);
            }

            // PUT: api/Vehicles/5
            //Updates a vehicle

            [HttpPut]
            public async Task<IActionResult> PutEntitiy(VehicleDTO dtoEntity)
            {
                //Max Weight (1000000) should not be exceeded
                if (dtoEntity.Weight > CommonConstants.MAX_WEIGHT)
                {
                    return MaxWeightExceededResult();
                }
                return await PutEntity(_repository, dtoEntity);
            }

            // POST: api/Vehicles
            //Adds a vehicle
            [HttpPost]
            public async Task<IActionResult> PostEntity(VehicleDTO dtoEntity)
            {
                //Max Weight (1000000) should not be exceeded
                if (dtoEntity.Weight > CommonConstants.MAX_WEIGHT)
                {
                    return MaxWeightExceededResult();
                }
                return await PostEntity(_repository, dtoEntity);
            }

            // DELETE: api/Vehicles/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteEntity(int id)
            {
                return await DeleteEntity(_repository, id);
            }
        }
    }

}
