using Common.Lib.Core;
using Common.Lib.Infrastructure;
using System.Collections.Generic;

namespace Project.Lib.Models
{
    public class Student : Entity
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
