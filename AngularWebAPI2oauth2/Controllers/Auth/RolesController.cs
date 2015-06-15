using System.Threading.Tasks;
using System.Web.Http;
using AngularWebAPI2oauth2.DAL;
using AngularWebAPI2oauth2.Models.Auth.BindingModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularWebAPI2oauth2.Controllers.Auth
{
    namespace AspNetIdentity.WebApi.Controllers
    {
        /// <summary>
        /// 
        /// </summary>
        [Authorize(Roles = "Owner, Admin")]
        [RoutePrefix("api/roles")]
        public class RolesController : ApiController
        {

            private readonly AuthRepository _repository;

            /// <summary>
            /// 
            /// </summary>
            public RolesController(AuthRepository repository)
            {
                _repository = repository;
            }

            /// <summary>
            /// Returns all roles registred in the system.
            /// </summary>
            /// <returns></returns>
            public IHttpActionResult GetAllRoles()
            {
                var roles = _repository.GetAllRoles();

                return Ok(roles);
            }

            /// <summary>
            /// Returns all roles assigned to a specific user.
            /// </summary>
            /// <returns></returns>
            public async Task<IHttpActionResult> GetRolesForUser(string userName)
            {
                return Ok(await _repository.GetRolesByUserName(userName));
            }

            /// <summary>
            /// Creates a new role.
            /// </summary>
            /// <param name="model"></param>
            /// <returns></returns>
            [Route("Create")]
            public async Task<IHttpActionResult> Create(CreateRoleBindingModel model)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var role = new IdentityRole { Name = model.Name };

                var result = await _repository.CreateRole(role);

                return !result.Succeeded ? GetErrorResult(result) : CreatedAtRoute("DefaultApi", new { id = role.Id }, role);
            }

            /// <summary>
            /// Deletes a role in the system.
            /// </summary>
            /// <param name="roleName"></param>
            /// <returns></returns>
            [Route("Delete")]
            public async Task<IHttpActionResult> Delete(string roleName)
            {
                var result = await _repository.DeleteRole(roleName);

                return !result.Succeeded ? GetErrorResult(result) : Ok();
            }

            /// <summary>
            /// Assigns roles to a specific user.
            /// </summary>
            /// <param name="model"></param>
            /// <returns></returns>
            [Route("ManageUserInRoles")]
            public async Task<IHttpActionResult> PutUserInRoles(UserInRolesModel model)
            {
                foreach (var role in model.EnrolledRoles)
                {
                    var result = await _repository.AddUserToRole(model.UserName, role);
                    if (!result.Succeeded)
                        ModelState.AddModelError("", string.Format("User: {0} could not be added to role: {1}", model.UserName, role));
                }

                foreach (var role in model.RemovedRoles)
                {
                    var result = await _repository.RemoveUserFromRole(model.UserName, role);
                    if (!result.Succeeded)
                        ModelState.AddModelError("", string.Format("User: {0} could not be removed from role: {1}", model.UserName, role));
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok();
            }

            private IHttpActionResult GetErrorResult(IdentityResult result)
            {
                if (result == null)
                    return InternalServerError();

                if (result.Succeeded)
                    return null;
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                    return BadRequest();

                return BadRequest(ModelState);
            }
        }
    }
}