using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        // The 'SeedUsers' method is used to populate the database with user data if no users exist.

        public static async Task SeedUsers(DataContext context)
        {
            // Check if there are any users in the database; if there are, exit without seeding.
            if (await context.Users.AnyAsync()) return;

            // Read user data from a JSON file (presumably containing initial user data).
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            // Configure JSON deserialization options.
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Deserialize the JSON data into a list of 'AppUser' objects.
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

            // Iterate through the list of users.
            foreach (var user in users)
            {
                // Create an instance of HMACSHA512 to generate password hash and salt.
                using var hmac = new HMACSHA512();

                // Ensure that the username is in lowercase for consistency.
                user.UserName = user.UserName.ToLower();

                // Compute the password hash using a default password ("Pa$$w0rd").
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));

                // Store the HMAC key as the password salt.
                user.PasswordSalt = hmac.Key;

                // Add the user to the database context for insertion.
                context.Users.Add(user);
            }

            // Save the changes to persist the seeded user data to the database.
            await context.SaveChangesAsync();
        }

    }
}