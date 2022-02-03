using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PruebaPrimeStone.Estudiantes.Api.AplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaPrimeStone.Estudiantes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        #region Fields
        /// <summary>
        /// Servicio de aplicacion para administracion estudiantes
        /// </summary>
        private readonly EstudianteAppService _estudianteAppService;

        public IConfiguration Configuration { get; }
        #endregion

        #region Build
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="estudianteAppService"></param>
        public EstudianteController(EstudianteAppService estudianteAppService, IConfiguration configuration)
        {
            _estudianteAppService = estudianteAppService;
            Configuration = configuration;
        }
        #endregion

        #region public Metodos
        /// <summary>
        /// Crea o actualiza un estudiante
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> save(EstudianteDTO estudiante)
        {
            var result = await _estudianteAppService.Save(estudiante);
            if (!result.Error)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest("Error al crear el Estudiante :" + (string)result.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de estudiantes
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _estudianteAppService.Get();
            if (!result.Error)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest("Error al consultar los Estudiantes :" + (string)result.Message);
            }
        }

        /// <summary>
        /// Obtiene un estudiante por su id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {
            var result = await _estudianteAppService.Get(Id);
            if (!result.Error)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest("Error al consultar el Estudiantes con Id: " + Id.ToString() + " " + (string)result.Message);
            }
        }

        /// <summary>
        /// Obtiene un estudiante por su id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{Id}/cursos")]
        public async Task<ActionResult> Cursos(int Id)
        {
            var result = await _estudianteAppService.Cursos(Id);
            if (!result.Error)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest("Error al consultar el Estudiantes con Id: " + Id.ToString() + " " + (string)result.Message);
            }
        }

        /// <summary>
        /// Matricula un estudiante en los cursos
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("matricular")]
        public async Task<ActionResult> Matricular(EstudianteCursoDTO estudianteCursoDTO)
        {
            var result = await _estudianteAppService.Matricular(estudianteCursoDTO);
            if (!result.Error)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest("Error al eliminar el Estudiantes :" + (string)result.Message);
            }
        }

        /// <summary>
        /// Elimina un estudiantes
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var result = await _estudianteAppService.Delete(Id);
            if (!result.Error)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest("Error al eliminar el Estudiantes :" + (string)result.Message);
            }
        }
        #endregion
    }
}
