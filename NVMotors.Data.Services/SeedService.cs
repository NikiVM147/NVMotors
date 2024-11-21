using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NVMotors.Sevices.Data
{
    public class SeedService
    {
        private readonly NVMotorsDbContext context;

        public SeedService(NVMotorsDbContext _context)
        {
            context = _context;
        }
        public void SeedCategories(string json)
        {
            if (!context.MotorCategories.Any()) 
            {
                var path = Path.Combine(AppContext.BaseDirectory, json);
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("JSON file not found.", path);
                }

                var data = File.ReadAllText(path);
                var categories = JsonSerializer.Deserialize<List<MotorCategory>>(data);

                if (categories != null)
                {
                    context.MotorCategories.AddRange(categories);
                    context.SaveChanges();
                }
            }
        }

    }
}
