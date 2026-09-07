namespace RESTbottle2.Models
{
    public class BottlesRepositoryDatabaseEF : IBottlesRepository
    {
        public Bottle AddBottle(Bottle b)
        {
            throw new NotImplementedException();
        }

        public Bottle? DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bottle> GetBottles(string? nameStartsWith = null, double? minVolume = null, string? sortOrder = null)
        {
            throw new NotImplementedException();
        }

        public Bottle? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Bottle? Update(int id, Bottle data)
        {
            throw new NotImplementedException();
        }
    }
}
