using CarCollection.Data;

namespace CarCollection.Repository.IRepository
    {
    public interface IVehicleRepository : IGenericRepository<Vehicle>
        {
        Task<Vehicle> GetVehicleWithComments(int? id);
        }
    }
