using System;
using Academy.Lib.Context;
using Academy.Lib.Infrastructure;

namespace Academy.Lib.Models
{
    public abstract class Entity
    {
        public string Id { get; set; }

        public ValidationResult CurrentValidation { get; private set; }

    }
}
