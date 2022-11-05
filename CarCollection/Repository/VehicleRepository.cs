using CarCollection.Data;
using CarCollection.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CarCollection.Repository
    {
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
        {
        private readonly ApplicationDbContext _applicationDbContext;

        public VehicleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
            {
            _applicationDbContext = applicationDbContext;
            }

        public async Task<Vehicle> GetVehicleWithComments(int? id)
            {
            return await _applicationDbContext.Vehicles.Include(o => o.Comments).Where(c => c.Id == id).FirstAsync(o => o.Id == id);
            }
        }
    }
