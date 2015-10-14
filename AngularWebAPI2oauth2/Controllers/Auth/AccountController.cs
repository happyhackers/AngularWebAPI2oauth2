using System.Threading.Tasks;
using System.Web.Http;
using AngularWebAPI2oauth2.DAL;
using AngularWebAPI2oauth2.Models.Auth;
using AngularWebAPI2oauth2.Models.Auth.BindingModels;
using Microsoft.AspNet.Identity;

namespace AngularWebAPI2oauth2.Controllers.Auth
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly AuthRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        public AccountController()
        {
            _repository = new AuthRepository();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!await _repository.IsUsernameAvailable(userModel.UserName))
            {
                ModelState.AddModelError("userModel.Username", "Username is already in use.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _repository.RegisterUser(userModel);

            var errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok("Registration Complete");
        }

        /// <summary>
        /// Changes user password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> PutPassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _repository.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("model.Password", "Incorrect Password");
                return BadRequest(ModelState);
            }

            return Ok("Password successfully changed");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
