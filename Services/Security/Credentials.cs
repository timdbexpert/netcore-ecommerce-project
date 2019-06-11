using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.Security
{
    public class Credentials
    {
        public UserEntity GetCredentials(String login, String password)
        {
            UserEntity user;
            using (RepositoryUser rep = new RepositoryUser())
            {
                Func<UserEntity, bool> predicate = (p => p.Login == login && p.Password == password);

                user = rep.Get(predicate).FirstOrDefault<UserEntity>();
                if (user != null)
                {
                    user.Password = String.Empty;
                    return user;
                }

                return null;
            }
        }
    }
}