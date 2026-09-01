using System.Collections;

namespace RESTbottle2.Models
{
    public class BottlesRepository
    {
        private List<Bottle> _bottles = new List<Bottle>();
        private int nextId = 1;

        public BottlesRepository(bool incluceTestData = false)
        {
            if (incluceTestData)
            {
                AddBottle(new Bottle() { Name = "Default Bottle", Volume = 1.0 });
                AddBottle(new Bottle() { Name = "Second Bottle", Volume = 2.0 });
                AddBottle(new Bottle() { Name = "Third Bottle", Volume = 3.0 });
                AddBottle(new Bottle() { Name = "Fourth Bottle", Volume = 4.0 });
            }
        }


        public IEnumerable<Bottle> GetBottles(string? nameStartsWith = null,
            double? minVolume = null,
            string? sortOrder = null)
        {
            IEnumerable<Bottle> result = _bottles.ToList();

            if (minVolume != null)
            {
                result = result.Where(b => b.Volume > minVolume);
            }
            if (nameStartsWith != null)
            {
                result = result.Where(b => b.Name != null && b.Name.StartsWith(nameStartsWith));
            }
            if (sortOrder != null)
            {
                switch (sortOrder.ToLower())
                {
                    case "name":
                    case "nameasc":
                        result = result.OrderBy(b => b.Name);
                        break;
                    case "namedesc":
                        result = result.OrderByDescending(b => b.Name);
                        break; 
                    case "volume":
                        result = result.OrderBy(b => b.Volume);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public Bottle AddBottle(Bottle b)
        {
            b.Id = nextId++;
            _bottles.Add(b);
            return b;
        }

        public Bottle? GetById(int id)
        {
            return _bottles.FirstOrDefault(b => b.Id == id);
        }

        public Bottle? DeleteById(int id)
        {
            Bottle? bottle = GetById(id);
            if (bottle != null)
            {
                _bottles.Remove(bottle);
            }
            return bottle;
        }

        public Bottle? Update(int id, Bottle data)
        {
            Bottle? bottle = GetById(id);
            if (bottle != null)
            {
                bottle.Volume = data.Volume;
                bottle.Name = data.Name;
            }
            return bottle;
        }

    }
}
