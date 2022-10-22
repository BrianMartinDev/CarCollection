using CarCollection.Data;

namespace BugTracker.Data
    {
    public class Comment
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        }
    }
