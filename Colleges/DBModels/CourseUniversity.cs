using System;
using System.Collections.Generic;

namespace Colleges.DBModels
{
    public partial class CourseUniversity
    {
        public int Cid { get; set; }
        public int Uid { get; set; }
        public int Id { get; set; }

        public virtual Course CidNavigation { get; set; } = null!;
        public virtual University UidNavigation { get; set; } = null!;
    }
}
