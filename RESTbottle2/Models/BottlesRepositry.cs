namespace RESTbottle2.Models
{
    public class BottlesRepositry
    {
        private List<Bottle> _bottles = new List<Bottle>();
        private int nextId = 1;

        public List<Bottle> Get() {
            return _bottles.ToList();  
        }

        public Bottle AddBottle(Bottle b) { 
            b.Id = nextId++;
            _bottles.Add(b);
            return b;
        }

        public Bottle? GetById(int id)
        {
            return _bottles.FirstOrDefault(b => b.Id == id);
        }

        public Bottle? DeleteById(int id) {
            Bottle? bottle = GetById(id);
            if (bottle != null) {
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
