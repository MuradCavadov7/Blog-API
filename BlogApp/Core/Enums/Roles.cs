using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Enums
{
    public enum Roles
    {
        Publisher = 1,
        Viewer = 2,
        Editor = 4,
        Banner = 8,
        Moderator = 16
    }
}
