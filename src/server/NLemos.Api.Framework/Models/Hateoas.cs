using System.Collections.Generic;

namespace NLemos.Api.Framework.Models
{
    public class Hateoas<T>
    {
        public Hateoas(T value, Dictionary<string, string> links)
        {
            Value = value;
            Links = links;
        }

        public T Value { get; set; }

        public Dictionary<string, string> Links { get; set; }
    }
}
