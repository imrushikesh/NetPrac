using System;
using System.Collections.Generic;

namespace ImportExport.Models
{
    public partial class TblImport
    {
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
