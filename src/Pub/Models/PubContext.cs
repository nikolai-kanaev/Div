using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pub.Models
{
    public class PubContext : DbContext
    {
        public PubContext() : base ("name=PubContext")
        {
        }

        public DbSet<PublicProfile> PublicProfiles { get; set; }
    }
}