using RESTbottle2.Models;

namespace repotest
{
    public class UnitTest1
    {
        [Fact]
        public void TestConstructorwithTestData()
        {
            // arrange
            BottlesRepository repo = new BottlesRepository(incluceTestData: true);
            // act
            IEnumerable<Bottle> bottles = repo.GetBottles();
            // assert
            Assert.NotNull(bottles);
            Assert.Equal(4, bottles.Count());
        }

        [Fact]
        public void TestConstructorWithoutTestData()
        {
            // arrange
            BottlesRepository repo = new BottlesRepository(incluceTestData: false);
            // act
            var bottles = repo.GetBottles();
            // assert
            Assert.NotNull(bottles);
            Assert.Empty(bottles);
        }

        [Fact]
        public void TestConstructorWithDefaultValue()
        {
            // arrange
            BottlesRepository repo = new BottlesRepository();
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
            BottlesRepository repo = new BottlesRepository();
            Bottle b = new Bottle { Volume = 1.5, Name = "Test Bottle" };
            
            // act
            Bottle addedBottle = repo.AddBottle(b);

            // assert
            Assert.NotNull(addedBottle);
            Assert.Equal(1, addedBottle.Id);
      
            Assert.Equal("Test Bottle", addedBottle.Name);
            Assert.Equal(1.5, addedBottle.Volume);

        }
    }
}
