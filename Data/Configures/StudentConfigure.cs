using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Data.Configures
{
    public class StudentConfigure: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> S)
        {
            S.HasKey(x => x.Id);
            S.HasMany(S => S.StuCrsResults);
            S.HasOne(S => S.Department);
            S.Property(x => x.Id)
                .IsRequired();
            S.Property(x => x.Name)
                .HasMaxLength(50);

            S.Property(x => x.Age)
                .IsRequired();
                
        }
    }
}
