using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCollection.Data.Configurations
    {
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
        {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
            {
            builder.HasData(
                new Vehicle
                    {
                    Id = 1,
                    Name = "F-150",
                    Year = 2005,
                    Description = "usce quis sem vel ante scelerisque eleifend nec vel ante. Fusce vel dictum tellus.",
                    Address = "Orlando, FL",
                    Engine = "V8"
                    },
                new Vehicle
                    {
                    Id = 2,
                    Name = "F-150",
                    Year = 2005,
                    Description = "usce quis sem vel ante scelerisque eleifend nec vel ante. Fusce vel dictum tellus.",
                    Address = "Orlando, FL",
                    Engine = "V8"
                    },
                new Vehicle
                    {
                    Id = 3,
                    Name = "F-150",
                    Year = 2005,
                    Description = "usce quis sem vel ante scelerisque eleifend nec vel ante. Fusce vel dictum tellus.",
                    Address = "Orlando, FL",
                    Engine = "V8"
                    }
                );
            }
        }
    }
