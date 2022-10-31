using BugTracker.Data;
using CarCollection.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CarCollection.Repository
    {
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
        {
        private readonly ApplicationDbContext _applicationDbContext;

        public CommentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
            {
            _applicationDbContext = applicationDbContext;
            }

        public async Task<IEnumerable<Comment>> GetAllCommentsWithVehicleId(int? vehicleId)
            {
            return await _applicationDbContext.Comment.Where(o => o.VehicleId == vehicleId).ToListAsync();
            }

        //public async Task<Comment> GetCommentsFromVehicle(int? id)
        //    {
        //    return await _applicationDbContext.Comment.Where(o => o.VehicleId == id).ToListAsync();
        //    }
        }
    }
