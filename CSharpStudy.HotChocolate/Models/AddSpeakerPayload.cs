using CSharpStudy.HotChocolate.Data;

namespace CSharpStudy.HotChocolate.Models
{
    public class AddSpeakerPayload
    {
        public AddSpeakerPayload(Speaker speaker)
        {
            Speaker = speaker;
        }

        public Speaker Speaker { get; }
    }
}