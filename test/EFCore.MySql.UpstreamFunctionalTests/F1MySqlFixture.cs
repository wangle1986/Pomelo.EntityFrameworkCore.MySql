using EFCore.MySql.UpstreamFunctionalTests.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestModels.ConcurrencyModel;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace EFCore.MySql.UpstreamFunctionalTests
{
    public class F1MySqlFixture : F1RelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory => MySqlTestStoreFactory.Instance;

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            modelBuilder.Entity<Chassis>().Property<byte[]>("Version").IsRowVersion();
            modelBuilder.Entity<Driver>().Property<byte[]>("Version").IsRowVersion();

            modelBuilder.Entity<Team>().Property<byte[]>("Version")
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            modelBuilder.Entity<TitleSponsor>()
                .OwnsOne(s => s.Details)
                .Property(d => d.Space).HasColumnType("decimal(18,2)");
        }
    }
}
