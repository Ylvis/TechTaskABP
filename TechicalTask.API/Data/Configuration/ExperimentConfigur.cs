using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechicalTask.API.Classes;

namespace TechicalTask.API.Data.Configuration
{
    public class ExperimentConfigur : IEntityTypeConfiguration<Experiment>
    {
        public void Configure(EntityTypeBuilder<Experiment> builder)
        {
            builder.HasData(new Experiment[]
            {
                new Experiment {Id = 1, Name = "button_color"},
                new Experiment {Id = 2, Name = "price"}
            });
        }
    }
}
