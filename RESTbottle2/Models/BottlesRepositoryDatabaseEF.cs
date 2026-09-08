namespace RESTbottle2.Models
{
    public class BottlesRepositoryDatabaseEF : IBottlesRepository
    {
        private BottlesDbContext _context;

        public BottlesRepositoryDatabaseEF(BottlesDbContext context)
        {
            _context = context;
        }

        public Bottle AddBottle(Bottle b)
        {
            var addedBottle = _context.Bottles.Add(b);
            _context.SaveChanges();
            return addedBottle.Entity;
        }

        public Bottle? DeleteById(int id)
        {
            var bottle = _context.Bottles.Find(id);
            if (bottle == null) return null;
            _context.Bottles.Remove(bottle);
            _context.SaveChanges();
            return bottle;
        }

        public IEnumerable<Bottle> GetBottles(string? nameStartsWith = null, double? minVolume = null, string? sortOrder = null)
        {
            // TODO : Implement filtering and sorting based on the parameters
            return _context.Bottles;
        }

        public Bottle? GetById(int id)
        {
            return _context.Bottles.FirstOrDefault(b => b.Id == id);
        }

        public Bottle? Update(int id, Bottle data)
        {
           var bottle = _context.Bottles.Find(id);
            if (bottle == null) return null;
            bottle.Name = data.Name;
            bottle.Volume = data.Volume;
            _context.SaveChanges();
            return bottle;
        }
    }
}
