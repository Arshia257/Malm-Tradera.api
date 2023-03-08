using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MalmöTradera.api.Model;
using Microsoft.EntityFrameworkCore;

namespace MalmöTradera.api.Data
{
    public class MalmöTraderaContext : DbContext
    {
        public MalmöTraderaContext(DbContextOptions options) : base(options) {}
        public DbSet<ItemsModel> Item { get; set; }

    }
}