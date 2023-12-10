using System;
using System.Collections.Generic;

namespace Colleges.DBModels
{
    public partial class StudentUniversity
    {
        public int Id { get; set; }
        public int Sid { get; set; }
        public int Uid { get; set; }

        public virtual Student SidNavigation { get; set; } = null!;
        public virtual University UidNavigation { get; set; } = null!;
    }
}
