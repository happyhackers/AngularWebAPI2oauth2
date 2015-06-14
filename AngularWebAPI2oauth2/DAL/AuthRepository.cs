using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularWebAPI2oauth2.Models.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularWebAPI2oauth2.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthRepository : IDisposable
    {
        private readonly AuthContext _ctx;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// 
        /// </summary>
        public AuthRepository()
        {

            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_ctx));
        }

        #region Account handling
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        /// <summary>
        /// Changes password from oldpassword to newpassword
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
        }

        /// <summary>
        /// Removes application user from database
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DeleteUser(string userName)
        {
            var appUser = await _userManager.FindByNameAsync(userName);

            if (appUser == null)
                return new IdentityResult("Application user not found");

            return await _userManager.DeleteAsync(appUser);
        }

        /// <summary>
        /// Deletes old password and adds a new one
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ReplacePassword(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return new IdentityResult("Application user not found");

            _userManager.RemovePassword(user.Id);
            return await _userManager.AddPasswordAsync(user.Id, password);
        }

        /// <summary>
        /// Update username
        /// </summary>
        /// <param name="oldUserName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IdentityResult> UpdateUserName(string oldUserName, string userName)
        {
            var user = await _userManager.FindByNameAsync(oldUserName);
            if (user == null)
                return new IdentityResult("Application user not found");

            user.UserName = userName;
            return await _userManager.UpdateAsync(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);

            return user;
        }
        #endregion

        #region Client and refreshtoken handling
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _ctx.RefreshTokens.FirstOrDefault(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

            if (existingToken != null)
            {
                await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);
            if (refreshToken == null)
                return false;
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }
        #endregion

        #region RoleHandling
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetRolesByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return await _userManager.GetRolesAsync(user.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateRole(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
                return new IdentityResult("Role: {0} does not exists", roleName);

            return await _roleManager.DeleteAsync(role);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> AddUserToRole(string userName, string roleName)
        {
            var appUser = await _userManager.FindByNameAsync(userName);

            if (appUser == null)
                return new IdentityResult(string.Format("User: {0} does not exists", userName));

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
                return new IdentityResult(string.Format("Role: {0} does not exists", roleName));

            if (await _userManager.IsInRoleAsync(appUser.Id, role.Name))
                return IdentityResult.Success;

            return await _userManager.AddToRoleAsync(appUser.Id, role.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IdentityResult> RemoveUserFromRole(string userName, string roleName)
        {
            var appUser = await _userManager.FindByNameAsync(userName);

            if (appUser == null)
                return new IdentityResult("User: {0} does not exists", userName);

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
                return new IdentityResult("Role: {0} does not exists", roleName);

            if (!await _userManager.IsInRoleAsync(appUser.Id, role.Name))
                return IdentityResult.Success;

            return await _userManager.RemoveFromRoleAsync(appUser.Id, role.Name);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public ClaimsIdentity CreateClaimsIdentity(IdentityUser user, string authenticationType)
        {
            return _userManager.CreateIdentity(user, authenticationType);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}