using ArchCorpUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public class User: EntityBase, IUserAuth, IUser
    {
        public User() { Surname = string.Empty; UserName = string.Empty; PassWord = string.Empty; }
        public User(string userName, string passWord, string? authGuid, string? name, string surname, int level)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"'{nameof(userName)}' cannot be null or whitespace.", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(passWord))
            {
                throw new ArgumentException($"'{nameof(passWord)}' cannot be null or whitespace.", nameof(passWord));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new ArgumentException($"'{nameof(surname)}' cannot be null or whitespace.", nameof(surname));
            }
            
            if (level < 0 && level > 11)
            {
                throw new ArgumentException($"'{nameof(level)}' level must be between 0 and 10.", nameof(level));
            }

            UserName = userName;
            PassWord = passWord;
            Guid_ = authGuid;
            Name = name;
            Surname = surname;
            Level = level;
        }

        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Surname { get; set; }
        public int Level { get; set; } = 1;
    }
}
