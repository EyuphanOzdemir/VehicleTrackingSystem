using AutoMapper;
using CommonLibrary;
using CoreEntity;
using CoreEntity.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPI.Controllers
{
    /// <summary>
    ///  This is the custom base controller class. It saves lives! 
    ///  All the controllers inherit from this and thanks to this they contain as less code as possible!
    ///  Note that in addition to common methods, there are commonly used action results.
    /// </summary>
    public class CustomControllerBase:ControllerBase
    {

        protected readonly IMapper _mapper;
        public CustomControllerBase(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ActionResult> GetEntities<EntityClass, DTOClass>(IGenericRepository<EntityClass> _repository, string sortColumn = "Id", bool paginated=false, int page = 0, int pageSize = 0) where EntityClass:BaseEntity
        {
            if (paginated)
            {
                var rowCount = _repository.Count();
                var allEntities = await _repository.GetAllAsyncPaginated(page, pageSize, sortColumn);
                var rows = (_mapper.Map<List<DTOClass>>(allEntities.ToList()));
                return Ok(new { rowCount, rows });
            }
            else
            {
                var allEntities = await _repository.GetAllAsync(sortColumn);
                return Ok(_mapper.Map<List<DTOClass>>(allEntities.ToList()));
            }
        }


        public async Task<ActionResult> GetViewEntities<EntityClass, ViewClass, DTOClass>(IGenericRepository<EntityClass> _repository, string sortColumn = "Id", bool paginated = false, int page=0, int pageSize=0) where EntityClass : BaseEntity where ViewClass : BaseEntity
        {
            if (paginated)
            {
                var rowCount = _repository.CountForView<ViewClass>();
                var allEntities = await _repository.GetViewRecordsPaginated<ViewClass>(page, pageSize, sortColumn);
                var rows = (_mapper.Map<List<DTOClass>>(allEntities.ToList()));
                return Ok(new { rowCount, rows });
            }
            else
            {
                var allEntities = await _repository.GetAllAsync(sortColumn);
                return Ok(_mapper.Map<List<DTOClass>>(allEntities.ToList()));
            }
        }

        public async Task<IActionResult> GetEntity<EntityClass, DTOClass>(IGenericRepository<EntityClass> _repository, int id) where EntityClass: BaseEntity
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return NoEntityResult();
            }

            return Ok(_mapper.Map<DTOClass>(entity));
        }

        public async Task<EntityClass> GetOnlyEntity<EntityClass, DTOClass>(IGenericRepository<EntityClass> _repository, int id) where EntityClass : BaseEntity
        {
            var entity= await _repository.GetByIdAsync(id);
            //.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<IActionResult> GetViewEntity<EntityClass, ViewClass, DTOClass>(IGenericRepository<EntityClass> _repository, int id) where EntityClass : BaseEntity where ViewClass : BaseEntity
        {
            var entity = await _repository.GetViewRecordById<ViewClass>(id);

            if (entity == null)
            {
                return NoEntityResult();
            }

            return Ok(_mapper.Map<DTOClass>(entity));
        }

        public async Task<IActionResult> PutEntityWithNameCheck<EntityClass, DTOClass>(IGenericRepositoryForNamedEntities<EntityClass> _repository, DTOClass dtoEntity) where EntityClass:BaseNamedEntity
        {
            var entity = _mapper.Map<EntityClass>(dtoEntity);
            var alreadyExists = _repository.CheckIfNameExists(entity.Name, entity.Id);
            if (alreadyExists)
            {
                return DuplicateNameResult<EntityClass>();
            }

            return await PutEntity(_repository, entity);
        }

        public async Task<IActionResult> PutEntity<EntityClass, DTOClass>(IGenericRepository<EntityClass> _repository, DTOClass dtoEntity) where EntityClass : BaseEntity
        {
            var entity = _mapper.Map<EntityClass>(dtoEntity);
            if (!_repository.Exists(entity.Id))
            {
                return NoEntityResult();
            }

            var result = await _repository.UpdateAsync(entity);
            return result ? Ok() : ExceptionActionResult();
        }

        public async Task<IActionResult> PostEntityWithNameCheck<EntityClass, DTOClass>(IGenericRepositoryForNamedEntities<EntityClass> _repository, DTOClass dtoEntity) where EntityClass : BaseNamedEntity
        {
            var entity = _mapper.Map<EntityClass>(dtoEntity);
            var alreadyExists = _repository.CheckIfNameExists(entity.Name);
            if (alreadyExists)
            {
                return DuplicateNameResult<EntityClass>();
            }

            return await PostEntity(_repository,entity);
        }

        public async Task<IActionResult> PostEntity<EntityClass, DTOClass>(IGenericRepository<EntityClass> _repository, DTOClass dtoEntity) where EntityClass : BaseEntity
        {
            var entity = _mapper.Map<EntityClass>(dtoEntity);
            var insertedEntity = await _repository.AddAsync(entity);
            if (insertedEntity != null)
                return Ok(_mapper.Map<DTOClass>(insertedEntity));
            else
                return ExceptionActionResult();
        }

        public async Task<IActionResult> DeleteEntity<EntityClass>(IGenericRepository<EntityClass> _repository, int id) where EntityClass : BaseEntity
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NoEntityResult();
            }
            
            var result = await _repository.RemoveAsync(entity);

            return result ? NoContent() : ExceptionActionResult();
        }

        private IActionResult ExceptionActionResult()=> StatusCode(StatusCodes.Status500InternalServerError, CommonConstants.ExceptionNotice);

        private IActionResult DuplicateNameResult<EntityClass>()=> DuplicatePropertyResult<EntityClass>("name");

        public IActionResult DuplicatePropertyResult<EntityClass>(string propertyName) => BadRequest($"This {typeof(EntityClass).Name} with this {propertyName} already exists");

        public IActionResult PropertyNeededResult<EntityClass>(string propertyName) => BadRequest($"{typeof(EntityClass).Name} needs {propertyName}");

        public IActionResult NoEntityResult() => NotFound("There is no such an entity with the indicated Id");

        public IActionResult MaxWeightExceededResult() => BadRequest($"Maximum weight ({CommonConstants.MAX_WEIGHT}) exceeded!");
    }
}
