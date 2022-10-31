using BugTracker.Data;

namespace CarCollection.Repository.IRepository
    {
    public interface ICommentRepository : IGenericRepository<Comment>
        {
        Task<IEnumerable<Comment>> GetAllCommentsWithVehicleId(int? id);
        //Task<Comment> GetCommentsFromVehicle(int? id);
        }
    }
