using System;
using Xunit;
using RESTbottle2.Models;

namespace RESTbottle2.Tests
{
    public class BottlesRepositoryTests
    {
        [Fact]
        public void AddBottle_AssignsIdAndAddsToRepository()
        {
            // Arrange
            var repo = new BottlesRepositry();
            var bottle = new Bottle { Name = "Test", Volume = 1.5 };

            // Act
            var added = repo.AddBottle(bottle);

            // Assert
            Assert.Equal(1, added.Id);
            List<Bottle> all = repo.Get();
            Assert.Single(all);
            Assert.Equal("Test", all[0].Name);
            Assert.Equal(1.5, all[0].Volume);
        }

        [Fact]
        public void Get_ReturnedListIsReferenceToInternalList_DemonstratesLeakage()
        {
            // This test documents current behavior: Get() returns the internal list instance.
            // Arrange
            var repo = new BottlesRepositry();
            repo.AddBottle(new Bottle { Name = "A", Volume = 0.5 });

            // Act
            List<Bottle> list = repo.Get();
            // Mutate the returned list
            list.Clear();

            // Assert - since Get() currently leaks the internal list, the repository is now empty
            Assert.Empty(repo.Get());
        }
    }
}
