using Microsoft.EntityFrameworkCore;
using RESTbottle2.Models;

namespace repotest
{
    public class RepositoryUnitTest
    {
        private bool useDatabase = true;
        private IBottlesRepository repo;

        // TODO delete test

        public RepositoryUnitTest()
        {
            // You can set useDatabase to true if you want to test with a database instead of the in-memory list.
            if (useDatabase) {
                var optionsBuilder = new DbContextOptionsBuilder<BottlesDbContext>();
                // https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets
                optionsBuilder.UseSqlServer(Secrets.ConnectionStringSimply);
                // connection string structure
                //   "Data Source=mssql7.unoeuro.com;Initial Catalog=FROM simply.com;Persist Security Info=True;User ID=FROM simply.com;Password=DB PASSWORD FROM simply.com;TrustServerCertificate=True"
                BottlesDbContext _dbContext = new(optionsBuilder.Options);
                // clean database table: remove all rows
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Bottles");
                repo = new BottlesRepositoryDatabaseEF(_dbContext);
            }
            else
            {
                // Use the in-memory list repository for testing
                repo = new BottlesRepositoryList();
            }
        }

        // [Fact]
        public void TestConstructorwithTestData()
        {
            // arrange
            // act
            IEnumerable<Bottle> bottles = repo.GetBottles();
            // assert
            Assert.NotNull(bottles);
            Assert.Equal(4, bottles.Count());
        }

        [Fact]
        public void TestConstructorWithoutTestData()
        {
           // act
            var bottles = repo.GetBottles();
            // assert
            Assert.NotNull(bottles);
            Assert.Empty(bottles);
        }

        //[Fact]
        public void TestConstructorWithDefaultValue()
        {
           // act
            var bottles = repo.GetBottles(minVolume: 2.0, nameStartsWith: "And");
            // assert
            Assert.NotNull(bottles);
            Assert.Empty(bottles);
        }

        [Fact]
        public void TestAdd()
        {
            // arrange
            Bottle b = new Bottle { Volume = 1.5, Name = "Test Bottle" };
            
            // act
            Bottle addedBottle = repo.AddBottle(b);

            // assert
            // TODO assert på Id
            Assert.NotNull(addedBottle);
           // Assert.Equal(1, addedBottle.Id);
      
            Assert.Equal("Test Bottle", addedBottle.Name);
            Assert.Equal(1.5, addedBottle.Volume);

        }
    }
}
