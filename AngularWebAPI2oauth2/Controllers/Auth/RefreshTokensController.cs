using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AngularWebAPI2oauth2.DAL;

namespace AngularWebAPI2oauth2.Controllers.Auth
{
    /// <summary>
    /// This resource represents all refreshtokens issued by the server.
    /// It gives the ability to control user sessions
    /// </summary>
    [Authorize]
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {

        private readonly AuthRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        public RefreshTokensController(AuthRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Returns all refresh tokens
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [Authorize(Roles = "Admin, Owner")]
        public IHttpActionResult GetRefreshTokens()
        {
            return Ok(_repository.GetAllRefreshTokens());
        }

        /// <summary>
        /// Returns all user refresh tokens
        /// </summary>
        /// <returns></returns>
        [Route("User")]
        public IHttpActionResult GetUserTokens()
        {
            return Ok(_repository.GetAllRefreshTokens().Where(r => r.Subject == User.Identity.Name));
        }

        /// <summary>
        /// Removing a refreshtoken invalidates a users session. 
        /// When accesstoken gets invalid the user will need to reauthenticate
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            bool result;
            if (User.IsInRole("Admin") || User.IsInRole("Owner")) //TODO: Send in all roles in one call ie. User.IsInRole("Admin, Owner");
            {
                result = await _repository.RemoveRefreshToken(tokenId);
            }
            else
            {
                var token =  _repository.GetAllRefreshTokens().FirstOrDefault(x => x.Equals(tokenId));
                if (token == null)
                    return BadRequest("No Token found with that Token Id");

                result = await _repository.RemoveRefreshToken(token);
            }
            
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }

        /// <remarks />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}