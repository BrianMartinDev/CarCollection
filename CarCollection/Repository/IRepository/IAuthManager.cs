using CarCollection.Data;
using CarCollection.ViewModels.AppUser;
using Microsoft.AspNetCore.Identity;

namespace CarCollection.Repository.IRepository
    {
    public interface IAuthManager
        {
        /// <summary>
        /// get list of all AppUsers
        /// </summary>
        /// <returns></returns>
        Task<List<AppUser>> GetAppUserList();
        /// <summary>
        /// get employee details by AppUser id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<AppUser> GetAppUserDetailsById(string? userEmail);
        Task<bool> Login(AppUserViewModel appUserViewModel);
        Task<bool> Logout(AppUserViewModel appUserViewModel);
        Task<IEnumerable<IdentityError>> Register(AppUserViewModel appUserViewModel);

        }
    }
