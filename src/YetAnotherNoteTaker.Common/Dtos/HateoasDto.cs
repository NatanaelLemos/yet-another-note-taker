using System.Collections.Generic;

namespace YetAnotherNoteTaker.Common.Dtos
{
    public class HateoasDto<T>
    {
        public HateoasDto()
            : this(default, new Dictionary<string, string>())
        {
        }

        public HateoasDto(T value, Dictionary<string, string> links)
        {
            Value = value;
            Links = links;
        }

        public T Value { get; set; }

        public Dictionary<string, string> Links { get; set; }
    }
}
