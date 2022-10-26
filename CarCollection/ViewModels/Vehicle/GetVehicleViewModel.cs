using CarCollection.ViewModels.Comment;

namespace CarCollection.ViewModels.Vehicle
    {
    public class GetVehicleViewModel : BaseVehicleViewModel
        {
        public int Id { get; set; }
        public ICollection<CommentViewModel>? Comments { get; set; }

        }
    }
