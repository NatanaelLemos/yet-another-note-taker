using System.Collections.Generic;

namespace YetAnotherNoteTaker.Common.Dtos
{
    public class HateoasDto<T>
    {
        public T Value { get; set; }

        public Dictionary<string, string> Links { get; set; }
    }
}
