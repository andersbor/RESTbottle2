using System.Globalization;

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

        public IEnumerable<Bottle> GetBottles(
            string? nameStartsWith = null, 
            double? minVolume = null, 
            string? sortOrder = null)
        {
            
            IQueryable<Bottle> query = _context.Bottles;
            if (nameStartsWith != null)
            {
                query = query.Where(b => b.Name != null && b.Name.StartsWith(nameStartsWith));
            }
            if (minVolume != null)
            {
                query = query.Where(b => b.Volume >= minVolume);
            }
            if (sortOrder != null)
            {
               switch (sortOrder.ToLower())
                {
                    case "name":
                        query = query.OrderBy(b => b.Name);
                        break;
                    case "volume":
                        query = query.OrderBy(b => b.Volume);
                        break;
                    default:
                        // Invalid sort order, do nothing or throw an exception
                        break;
                }
            }

            return query;
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
