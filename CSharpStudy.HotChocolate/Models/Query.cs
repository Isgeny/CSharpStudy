using System.Linq;
using CSharpStudy.HotChocolate.Data;
using HotChocolate;

namespace CSharpStudy.HotChocolate.Models
{
    public class Query
    {
        public IQueryable<Speaker> GetSpeakers([Service] ApplicationDbContext context) => context.Speakers;
    }
}