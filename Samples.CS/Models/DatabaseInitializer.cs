using Samples.Associations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Samples.Models
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var categories = new List<Category> {
                new Category {Name="Beverages"},
                new Category {Name="Fruit"}
            };

            categories.ForEach(c => context.Categories.Add(c));

        }
    }
}