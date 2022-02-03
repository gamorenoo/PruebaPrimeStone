using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using static PruebaPrimeStone.Estudiantes.Api.Utilidades.JwtAuth;

namespace PruebaPrimeStone.Estudiantes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {

        #region Metodos
        public IConfiguration Configuration { get; }
        #endregion

        #region Builders
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userAppService"></param>
        public SessionController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Valiad usuario y clave
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public async Task<ActionResult> login()
        {
            var Token = Authenticate("Test", "Test", Configuration.GetValue<string>("TokenKey"));
            return Ok(Token);
        }
        #endregion
    }
}
