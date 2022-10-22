using BugTracker.Data;

namespace CarCollection.Data
    {
    public class Vehicle
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Engine { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        }
    }
