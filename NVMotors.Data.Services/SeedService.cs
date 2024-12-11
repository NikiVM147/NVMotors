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
        public void SeedData<T>(string json, DbSet<T> table) where T : class
        {
            if (!table.Any()) 
            {
                var path = Path.Combine(AppContext.BaseDirectory, json);
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("JSON file not found.", path);
                }

                var jsonData = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<List<T>>(jsonData);

                if (data != null)
                {
                    table.AddRange(data);
                    context.SaveChanges();
                }
            }
        }

    }
}
