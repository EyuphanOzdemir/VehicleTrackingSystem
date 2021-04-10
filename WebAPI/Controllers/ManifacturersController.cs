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
using CommonLibrary;
using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    public class ManifacturersController : CustomControllerBase
    {
        private readonly IManifacturerRepository _repository;

        public ManifacturersController(IManifacturerRepository repository, IMapper mapper) : base(mapper)
        {
            _repository = repository;
        }

        // GET: api/Manifacturers
        //For paginated grids.
        [HttpGet]
        public async Task<ActionResult> GetEntities([FromQuery] int page=0, int pageSize=0)
        {
            return await GetEntities<Manifacturer, ManifacturerDTO>(_repository,"Id", pageSize>0, page, pageSize);
        }
        // GET: api/Manifacturers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntity(int id)
        {
            return await GetEntity<Manifacturer, ManifacturerDTO>(_repository, id);
        }

        // PUT: api/Manifacturers/5
        //Updates the manifacturer
        [HttpPut]
        public async Task<IActionResult> PutEntity(ManifacturerDTO  dtoEntity)
        {
            return await PutEntityWithNameCheck(_repository, dtoEntity);
        }

        // POST: api/Manifacturers\
        //Adds a manifacturer
        //Duplicates names are not allowed.
        [HttpPost]
        public async Task<IActionResult> PostEntity(ManifacturerDTO dtoEntity)
        {
            return await PostEntityWithNameCheck(_repository, dtoEntity);
        }

        // DELETE: api/Manifacturers/5
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            //The manifacturers that are related to vehicles cannot be deleted!
            if (_repository.CheckRelatedVehicles(id))
            {
                return BadRequest("This manifacturer cannot be deleted as there are some vehicles related to this manifacturer");
            }
            return await DeleteEntity(_repository, id);
        }
    }
}
