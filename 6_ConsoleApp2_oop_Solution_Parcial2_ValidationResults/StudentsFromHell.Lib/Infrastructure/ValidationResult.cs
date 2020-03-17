using System.Collections.Generic;

namespace Academy.Lib.Infrastructure
{
    public class ValidationResult<T>
    {
        public T ValidatedResult { get; set; }

        public bool IsSuccess { get; set; }

        public List<string> Messages { get; set; } = new List<string>();

    }

}
