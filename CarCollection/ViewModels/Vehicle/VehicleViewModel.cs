using CarCollection.ViewModels.Comment;

namespace CarCollection.ViewModels.Vehicle
    {
    public class VehicleViewModel : BaseVehicleViewModel
        {
        public int Id { get; set; }
        public virtual IEnumerable<CommentViewModel>? Comments { get; set; }
        }
    }
