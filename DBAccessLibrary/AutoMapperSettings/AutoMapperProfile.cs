namespace DBAccessLibrary.AutoMapperSetting
{
    using DBAccessLibrary.DBEntities;
    using DBAccessLibrary.DTOs;
    using AutoMapper;
    /// <summary>
    ///  The constructor of this class defines some conversion relations between DTO (data transfer object) classes and 
    ///  corresponding entity classes. without automap, the code will be very verbose and ugly.
    ///  And without DTOs, Domain oriented design would be impossible.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Vehicle, VehicleDTO>();
            CreateMap<VehicleDTO, Vehicle>();
            CreateMap<VehicleView, VehicleViewRecordDTO>();
            CreateMap<VehicleViewRecordDTO, Vehicle>();
            CreateMap<CategoryView, CategoryViewRecordDTO>();
            CreateMap<CategoryViewRecordDTO, Category>();
            CreateMap<Manifacturer, ManifacturerDTO>();
            CreateMap<ManifacturerDTO, Manifacturer>();
        }
    }
}
