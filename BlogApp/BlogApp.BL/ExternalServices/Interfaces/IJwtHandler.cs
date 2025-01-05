using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BL.ExternalServices.Interfaces
{
    public interface IJwtHandler
    {
        string CreateJwtToken(User user,int hours);
    }
}
