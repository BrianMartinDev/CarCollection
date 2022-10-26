using Microsoft.Build.Framework;

namespace CarCollection.ViewModels.Comment
    {
    public class BaseCommentViewModel
        {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        }
    }
