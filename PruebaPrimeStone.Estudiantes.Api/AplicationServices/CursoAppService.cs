using Common.DTOs;
using PruebaPrimeStone.Estudiantes.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaPrimeStone.Estudiantes.Api.AplicationServices
{
    public class CursoAppService
    {
        #region Propiedades
        /// <summary>
        /// Servicio de repositorio para persistencia de cursos
        /// </summary>
        private readonly CursoRepository _cursoRepository;
        #endregion

        #region Build
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="cursoRepository"></param>
        public CursoAppService(CursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Realiza la creacion o actualizacion de un curso
        /// </summary>
        public async Task<GenericResult> Save(CursoDTO curso)
        {
            GenericResult result = new GenericResult();
            try
            {
                result.Error = false;
                result.Result = await _cursoRepository.Save(curso);
                return result;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                return result;
                // TODO: Implemnetar log
            }
        }

        /// <summary>
        /// Obtiene una lista de cursos
        /// </summary>
        public async Task<GenericResult> Get()
        {
            GenericResult result = new GenericResult();
            try
            {
                result.Error = false;
                result.Result = await _cursoRepository.Get();
                return result;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                return result;
                // TODO: Implemnetar log
            }
        }

        /// <summary>
        /// Obtiene un curso por su id
        /// </summary>
        public async Task<GenericResult> Get(int Id)
        {
            GenericResult result = new GenericResult();
            try
            {
                result.Error = false;
                result.Result = await _cursoRepository.Get(Id);
                return result;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                return result;
                // TODO: Implemnetar log
            }
        }

        /// <summary>
        /// Elimina un curso
        /// </summary>
        public async Task<GenericResult> Delete(int Id)
        {
            GenericResult result = new GenericResult();
            try
            {
                result.Error = false;
                result.Result = await _cursoRepository.Delete(Id);
                return result;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                return result;
                // TODO: Implemnetar log
            }
        }
        #endregion
    }
}
