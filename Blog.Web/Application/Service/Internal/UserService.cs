using Microsoft.AspNet.Identity;
using Blog.Web.Application.Service.Entity;
using Blog.Web.Application.Storage;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Blog.Web.Application.Service.Internal
{
    public class UserService : IUserService, IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>, IUserLockoutStore<User, string>, IUserTwoFactorStore<User, string>
    {
        private readonly IConfigService _configService;
        private readonly IRepository _repository;

        public UserService(IConfigService configService, IRepository repository)
        {
            _configService = configService;
            _repository = repository;

            var user = new User();
            var identityUser = HttpContext.Current.GetOwinContext().Authentication.User;
            if (identityUser != null)
            {
                var identity = identityUser.Identity;
                var formsIdentity = identity as FormsIdentity;
                var friendlyName = formsIdentity != null ? formsIdentity.Ticket.UserData : identity.Name;
                if (string.IsNullOrEmpty(friendlyName)) { friendlyName = identity.Name; }

                var isAdmin =
                    identity.IsAuthenticated
                    && _configService.Current.Admins != null
                    && _configService.Current.Admins.Contains(identity.Name, StringComparer.InvariantCultureIgnoreCase);

                user = new User()
                  {
                      FriendlyName = friendlyName,
                      IsAuthenticated = identity.IsAuthenticated,
                      IsAdmin = isAdmin
                  };

            }
            Current = user;
        }

        public User Current { get; private set; }

        public System.Threading.Tasks.Task CreateAsync(User user)
        {
            if ((object)user == null)
            {
                throw new ArgumentNullException("user");
            }
            _repository.Save<User>(user);


            return Task.FromResult(0);
        }

        public System.Threading.Tasks.Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<User> FindByIdAsync(string userId)
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                // no cartesian product, batch call. Don't know if it's really needed: should we eager load or let lazy loading do its stuff?
                var query = _repository.All<User>().FirstOrDefault(x => x.Id == userId);
                return query;
            });
        }

        public System.Threading.Tasks.Task<User> FindByNameAsync(string userName)
        {
            return System.Threading.Tasks.Task.Run(() =>
                 {
                     // no cartesian product, batch call. Don't know if it's really needed: should we eager load or let lazy loading do its stuff?
                     var query = _repository.All<User>().FirstOrDefault(x => x.UserName == userName);
                     return query;
                 });

        }

        public System.Threading.Tasks.Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<string> GetPasswordHashAsync(User user)
        {
            if ((object)user == null)
            {
                throw new ArgumentNullException("user");
            }
            else
            {
                return Task.FromResult<string>(user.PasswordHash);
            }
        }

        public System.Threading.Tasks.Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult<bool>(user.PasswordHash != null);
        }

        public System.Threading.Tasks.Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if ((object)user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult<int>(0);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                // no cartesian product, batch call. Don't know if it's really needed: should we eager load or let lazy loading do its stuff?
                var query = _repository.All<User>().FirstOrDefault(x => x.UserName == email);
                return query;
            });

        }

        public Task<string> GetEmailAsync(User user)
        {
            return Task.FromResult<string>(user.UserName);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            //  throw new NotImplementedException();
            return Task.FromResult<bool>(true);
        }

        public Task SetEmailAsync(User user, string email)
        {
            // throw new NotImplementedException();
            return Task.FromResult<int>(0);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            user.LockoutEnabled = enabled;
            _repository.Save(user);

            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDateUtc = lockoutEnd.UtcDateTime;
            _repository.Save(user);

            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            user.TwoFactorEnabled = enabled;
            _repository.Save(user);

            return Task.FromResult(0);
        }
    }



}