using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Doctor
    {
        public Guid Id { get; set; }
        public string Initials { get; set; } = null!;
        public int CabinetId { get; set; }
        public int SpecialityId { get; set; }
        public int RegionId { get; set; }
    }
}
