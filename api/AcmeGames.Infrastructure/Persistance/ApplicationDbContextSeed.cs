using AcmeGames.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AcmeGames.Infrastructure.Persistance
{
    public static class ApplicationDbContextSeed
    {
        public static void SeedSampleData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasData(ReadItemsFromFile<Game>("games.json"));
            modelBuilder.Entity<GameKey>().HasData(ReadItemsFromFile<GameKey>("keys.json"));
            modelBuilder.Entity<User>().HasData(ReadItemsFromFile<User>("users.json"));
            modelBuilder.Entity<Ownership>().HasData(ReadItemsFromFile<Ownership>("ownership.json"));
        }
        private static IEnumerable<TEntity> ReadItemsFromFile<TEntity>(string filename)
            where TEntity : class
        {
            string executableLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(
                File.ReadAllText(Path.Combine(executableLocation, @$"Persistance\SeedFiles\{filename}")));
        }
    }
}
