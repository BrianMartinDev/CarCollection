using AutoMapper;
using CarCollection.Data;
using CarCollection.Repository.IRepository;
using CarCollection.ViewModels.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarCollection.Repository
    {
    public class AuthManager : IAuthManager
        {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<AppUser> userManager)
            {
            _mapper = mapper;
            _userManager = userManager;
            }

        public async Task<AppUser> GetAppUserDetailsById(string? userEmail)
            {
            AppUser empList;
            try
                {
                empList = await _userManager.Users.FirstAsync(o => o.Email == userEmail);
                }
            catch (Exception)
                {
                throw;
                }

            return empList;
            }

        public async Task<List<AppUser>> GetAppUserList()
            {
            List<AppUser> empList;
            try
                {
                empList = await _userManager.Users.ToListAsync();
                }
            catch (Exception)
                {
                throw;
                }
            return empList;
            }

        public Task<bool> Login(AppUserViewModel appUserViewModel)
            {

            var appUser = _mapper.Map<AppUser>(appUserViewModel);
            throw new NotImplementedException();
            }

        public Task<bool> Logout(AppUserViewModel appUserViewModel)
            {
            var appUser = _mapper.Map<AppUser>(appUserViewModel);
            throw new NotImplementedException();
            }

        public async Task<IEnumerable<IdentityError>> Register(AppUserViewModel appUserViewModel)
            {
            var appUser = _mapper.Map<AppUser>(appUserViewModel);
            appUser.Email = appUserViewModel.Email;
            var result = await _userManager.CreateAsync(appUser, appUserViewModel.Password);

            if (result.Succeeded)
                {
                await _userManager.AddToRoleAsync(appUser, "User");
                }
            return result.Errors;
            }

        }
    }
