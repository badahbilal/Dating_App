using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        // The PasswordHash property stores the hashed representation of the user's password.
        // Hashing is a one-way function that converts the password into a fixed-size byte array,
        // making it difficult to retrieve the original password from the stored hash.
        public byte[] PasswordHash { get; set; }
        // The PasswordSalt property stores a unique random value used during the password hashing process.
        // Salt adds additional complexity to the hashing algorithm, making it more resistant to precomputed attacks.
        public byte[] PasswordSalt { get; set; }

    }
}