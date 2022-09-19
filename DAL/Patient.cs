using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Patient
    {
        public Guid Id { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public bool Sex { get; set; }
        public int RegionId { get; set; }
    }
}
