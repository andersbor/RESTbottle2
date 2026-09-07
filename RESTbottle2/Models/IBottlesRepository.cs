namespace RESTbottle2.Models
{
    public interface IBottlesRepository
    {
        Bottle AddBottle(Bottle b);
        Bottle? DeleteById(int id);
        IEnumerable<Bottle> GetBottles(string? nameStartsWith = null, double? minVolume = null, string? sortOrder = null);
        Bottle? GetById(int id);
        Bottle? Update(int id, Bottle data);
    }
}