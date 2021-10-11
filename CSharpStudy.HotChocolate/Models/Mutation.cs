using System.Threading.Tasks;
using CSharpStudy.HotChocolate.Data;
using HotChocolate;

namespace CSharpStudy.HotChocolate.Models
{
    public class Mutation
    {
        public async Task<AddSpeakerPayload> AddSpeakerAsync(AddSpeakerInput input, 
            [Service] ApplicationDbContext context)
        {
            var speaker = new Speaker
            {
                Name = input.Name,
                Bio = input.Bio,
                WebSite = input.WebSite
            };

            context.Speakers.Add(speaker);
            await context.SaveChangesAsync();

            return new AddSpeakerPayload(speaker);
        }
    }
}