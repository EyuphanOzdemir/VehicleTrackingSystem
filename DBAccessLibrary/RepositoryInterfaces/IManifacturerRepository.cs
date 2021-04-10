using CoreEntity.RepositoryInterfaces;
using DBAccessLibrary.DBEntities;


namespace DBAccessLibrary.Repositories
{    /// <summary>
     ///  Interface for ManifacturerRepository.
     /// </summary>
    public interface IManifacturerRepository: IGenericRepositoryForNamedEntities<Manifacturer>
    {
        bool CheckRelatedVehicles(int manifacturerId); 
    }
}
