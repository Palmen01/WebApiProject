using Microsoft.EntityFrameworkCore;
using Configuration;
using Models;
using DbModels;
using DbContext;
using Seido.Utilities.SeedGenerator;

namespace DbRepos;

public class csAttractionRepo : IAttractionRepo
{
   public List<IAttraction> ReadAttractions(int _count, string _category, string _attractionName, string _description, string _country, string _city)
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var attractions = db.Attractions.Include(a => a.AddressDbM)
            .Include(a => a.CommentsDbM)
            .Where(a => a.Category.ToLower().Contains(_category))
            .Where(a => a.AttractionName.ToLower().Contains(_attractionName))
            .Where(a => a.Description.ToLower().Contains(_description))
            .Where(a => a.AddressDbM.Country.ToLower().Contains(_country))
            .Where(a => a.AddressDbM.City.ToLower().Contains(_city))
            .Take(_count).ToList<IAttraction>();

            return attractions;
        }
    }

    public IAttraction ReadAttraction(Guid _id) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var attraction = db.Attractions.Include(a => a.CommentsDbM).Include(a => a.AddressDbM).Where(a => a.AttractionId == _id).FirstOrDefault();
                                                                         
            return attraction;
        }
    }

    public List<IAttraction> ReadAttractionsByCity(string _city) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var attractions = db.Attractions.Include(a => a.AddressDbM).Where(a => a.AddressDbM.City == _city).ToList<IAttraction>();
            return attractions; 
        }
    }
    public List<IAttraction> ReadAttractionsWithoutComments() 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var attractions = db.Attractions.Include(a => a.CommentsDbM).Where(a => a.CommentsDbM.Count == 0).ToList<IAttraction>();
            return attractions; 
        }
    }
    
    public IAttraction AddAttraction() => throw new NotImplementedException();
    public IAttraction UpdateAttraction(Guid _id) => throw new NotImplementedException();
    public IAttraction RemoveAttraction(Guid _id) => throw new NotImplementedException();
}
