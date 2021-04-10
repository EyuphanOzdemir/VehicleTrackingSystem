namespace DBAccessLibrary.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using DBAccessLibrary.DBEntities;
    using DBAccessLibrary.Repositories;
    using AutoMapper;
    using global::DBAccessLibrary.DTOs;
    using CommonLibrary;
    using Microsoft.AspNetCore.Hosting;
    using CommonLibrary.Services.FileService;
    using global::WebAPI.Controllers;
    using Microsoft.AspNetCore.Cors;

    namespace WebAPI.Controllers
    {
        [Route("api/[controller]")]
        [EnableCors("AllowAllOrigins")]
        [ApiController]
        public class CategoriesController : CustomControllerBase
        {
            const string CATEGORY_ICON_DIRECTORY = "images\\categoryIcons";
            private readonly ICategoryRepository _repository;
            private readonly IFileService _fileService;

            public CategoriesController(ICategoryRepository repository, IMapper mapper, IFileService fileService) : base(mapper)
            {
                _repository = repository;
                _fileService = fileService;
            }

            // GET: api/Categories/?page=0&pageSize=10
            //Category grid is paginated!
            [HttpGet]
            public async Task<ActionResult> GetEntities([FromQuery] int page=0, int pageSize=0)
            {
                return await GetViewEntities<Category, CategoryView, CategoryViewRecordDTO>(_repository, "MinWeight", true, page, pageSize);
            }

            // GET: api/Categories/5
            [HttpGet("{id}")]
            public async Task<IActionResult> GetEntity(int id)
            {
                return await GetViewEntity<Category, CategoryView, CategoryViewRecordDTO>(_repository, id);
            }

            // POST: api/Categories
            //Adds a category
            [HttpPost]
            public async Task<IActionResult> PostEntity([FromForm] CategoryViewRecordWithIconDTO dtoEntity)
            {
                //Max weight control
                if (dtoEntity.MinWeight> CommonConstants.MAX_WEIGHT)
                {
                    return MaxWeightExceededResult();
                }
                //icon is required
                if (dtoEntity.Icon == null)
                {
                    return IconNeededResult();
                }

                //Duplicate min weights are not allowed
                if (_repository.CheckIfMinWeightExists(dtoEntity.MinWeight))
                {
                    return DuplicateMinWeightResult();
                }
                var uploadResult = IconUpload(dtoEntity.Icon, dtoEntity.Name);
                if (!uploadResult.Item1) //if the result is false
                    return BadRequest(uploadResult.Item2); //then item2 (returned message) is the error message
                //if item1(result) is true, then the item2(message) is the file name
                dtoEntity.IconFileName = uploadResult.Item2; //set the filename of the entity
                //Duplicate names are not allowed
                return await PostEntityWithNameCheck(_repository, dtoEntity);
            }

            // PUT: api/Categories/5
            //Updates a category
            //Very similar logic to PostEntity
            [HttpPut]
            public async Task<IActionResult> PutEntitiy([FromForm] CategoryViewRecordWithIconDTO dtoEntity)
            {
                if (dtoEntity.MinWeight > CommonConstants.MAX_WEIGHT)
                {
                    return MaxWeightExceededResult();
                }
                if (_repository.CheckIfMinWeightExists(dtoEntity.MinWeight, dtoEntity.Id))
                {
                    return DuplicateMinWeightResult();
                }
                //if there is an icon, save the image and delete the old one!
                var oldIconFileName = dtoEntity.IconFileName;
                if (dtoEntity.Icon != null)
                {
                    var uploadResult = IconUpload(dtoEntity.Icon, dtoEntity.Name);
                    if (!uploadResult.Item1)
                        return BadRequest(uploadResult.Item2);
                    dtoEntity.IconFileName = uploadResult.Item2;
                }
                var putResult = await PutEntityWithNameCheck(_repository, dtoEntity);
                //if post is successful, delete the old file
                if (putResult is OkResult && !oldIconFileName.ToLower().Equals(dtoEntity.IconFileName.ToLower()))
                    _fileService.Delete(CATEGORY_ICON_DIRECTORY, oldIconFileName);
                return putResult;
            }

            // DELETE: api/Categories/5
            //Deletes the category.
            //Note that the root category with 0 min weight cannot be deleted!
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteEntity(int id)
            {
                var entity =await GetOnlyEntity<Category, CategoryViewRecordDTO>(_repository, id);
                if (entity == null)
                    return NoEntityResult();
                if (entity.MinWeight == 0)
                    return BadRequest("The category with 0 minimum weight cannot be deleted");
                var result = await DeleteEntity(_repository, id);
                if (result is NoContentResult)
                    _fileService.Delete(CATEGORY_ICON_DIRECTORY, entity.IconFileName);
                return result;
            }

            //A common method for adding and updating entity
            //The image max size is 1 MB
            //IsValidImage checks the image format as well.
            private (bool, string) IconUpload(IFormFile icon, string fileName)
            {
                string checkImageResult = icon.IsValidImage(1024 * 1024); //1 MB max.
                if (!string.IsNullOrEmpty(checkImageResult))
                {
                    return (false,checkImageResult);
                }
                return _fileService.Upload(icon, CATEGORY_ICON_DIRECTORY, fileName);
            }

            //Some common action results. Note that they use actions from CustomControllerBase.
            private IActionResult DuplicateMinWeightResult() => DuplicatePropertyResult<Category>("minimum weight");
            private IActionResult IconNeededResult() => PropertyNeededResult<Category>("icon");

        }
    }

}
