using System;
using System.Collections.Generic;

namespace Colleges.DBModels
{
    public partial class StudentCourse
    {
        public int Sid { get; set; }
        public int Cid { get; set; }
        public int Id { get; set; }

        public virtual Course CidNavigation { get; set; } = null!;
        public virtual Student SidNavigation { get; set; } = null!;
    }
}
