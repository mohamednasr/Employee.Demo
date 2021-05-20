using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace XTestBuilders.EntitesBuilders
{
    public class RoleBuilders
    {
        public JobRole BuildDefultRole()
        {
            return new JobRole()
            {
                Id = 1,
                name = "Role 1"
            };
        }

        public List<JobRole> BuildTestRoles()
        {
            List<JobRole> roles = new List<JobRole>()
            {
                new JobRole(){name = "role 1"},
                new JobRole(){name = "role 2"}
            };

            return roles;
        }
    }
}
