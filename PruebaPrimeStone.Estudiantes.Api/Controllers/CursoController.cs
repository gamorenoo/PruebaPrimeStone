using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PruebaPrimeStone.Estudiantes.Api.AplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PruebaPrimeStone.Estudiantes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        #region Fields
        /// <summary>
        /// Servicio de aplicacion para administracion estudiantes
        /// </summary>
        private readonly CursoAppService _cursoAppService;

        public IConfiguration Configuration { get; }
        #endregion

        #region Build
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cursoAppService"></param>
        public CursoController(CursoAppService cursoAppService, IConfiguration configuration)
        {
            _cursoAppService = cursoAppService;
            Configuration = configuration;
        }
        #endregion

        #region public Metodos
        /// <summary>
        /// Crea o actualiza un curso
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> save(CursoDTO curso)
        {
            var result = await _cursoAppService.Save(curso);
            if (!result.Error)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest("Error al crear el Curso : " + (string)result.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de cursos
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _cursoAppService.Get();
            if (!result.Error)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest("Error al consultar los Cursos :" + (string)result.Message);
            }
        }

        /// <summary>
        /// Obtiene un curso por su id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {
            var result = await _cursoAppService.Get(Id);
            if (!result.Error)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest("Error al consultar el Curso con Id: " + Id.ToString() + " " + (string)result.Message);
            }
        }

        /// <summary>
        /// Elimina un Curso
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var result = await _cursoAppService.Delete(Id);
            if (!result.Error)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest("Error al eliminar el Curso : " + (string)result.Message);
            }
        }

        #endregion
    }
}
