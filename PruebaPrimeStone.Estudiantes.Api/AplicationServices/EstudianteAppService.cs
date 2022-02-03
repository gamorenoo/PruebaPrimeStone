using AutoMapper;
using Common.Context;
using Common.DTOs;
using Common.Models;
using PruebaPrimeStone.Estudiantes.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaPrimeStone.Estudiantes.Api.AplicationServices
{
    public class EstudianteAppService
    {
        #region Propiedades
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Servicio de repositorio para persistencia de estudiantes
        /// </summary>
        private readonly EstudianteRepository _estudianteRepository;
        /// <summary>
        /// Servicio de repositorio para persistencia de direcciones
        /// </summary>
        private readonly DireccionRepository _direccionRepository;
        /// <summary>
        /// Servicio de repositorio para persistencia de cuross
        /// </summary>
        private readonly CursoRepository _cursoRepository;
        #endregion

        #region Build
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="estudianteRepository"></param>
        /// <param name="direccionRepository"></param>
        /// <param name="cursoRepository"></param>
        public EstudianteAppService(IMapper mapper, EstudianteRepository estudianteRepository, DireccionRepository direccionRepository, CursoRepository cursoRepository)
        {
            _mapper = mapper;
            _cursoRepository = cursoRepository;
            _estudianteRepository = estudianteRepository;
            _direccionRepository = direccionRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Realiza la creacion o actualizacion de un estudiante
        /// </summary>
        public async Task<GenericResult> Save(EstudianteDTO estudiante)
        {
            GenericResult result = new GenericResult();
            try
            {

                List<Direccion> direcciones = _mapper.Map<IEnumerable<Direccion>>(estudiante.Direcciones).ToList();
                estudiante = await _estudianteRepository.Save(estudiante);

                // Insertando direcciones
                direcciones.ForEach(d => d.EstudianteId = estudiante.Id);
                await _direccionRepository.Insert(direcciones);

                // Cnsultado direcciones
                estudiante.Direcciones = _mapper.Map<IEnumerable<DireccionDTO>>(await _direccionRepository.Get(estudiante.Id));

                result.Error = false;
                result.Result = estudiante;

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
        /// Obtiene una lista de estudiantes
        /// </summary>
        public async Task<GenericResult> Get()
        {
            GenericResult result = new GenericResult();
            try
            {
                var res = await _estudianteRepository.Get();
                foreach (var item in res.ToList())
                {
                    item.Direcciones = _mapper.Map<IEnumerable<DireccionDTO>>(await _direccionRepository.Get(item.Id));
                }
                result.Error = false;
                result.Result = res;
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
        /// Obtiene un estudiante por su id
        /// </summary>
        public async Task<GenericResult> Get(int Id)
        {
            GenericResult result = new GenericResult();
            try
            {
                var estudiante = await _estudianteRepository.Get(Id);
                estudiante.Direcciones = _mapper.Map<IEnumerable<DireccionDTO>>(await _direccionRepository.Get(estudiante.Id));
                result.Error = false;
                result.Result = estudiante;
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
        /// Obtiene los curos de estudiante
        /// </summary>
        public async Task<GenericResult> Cursos(int Id)
        {
            GenericResult result = new GenericResult();
            try
            {
                var estudiante = await _estudianteRepository.Get(Id);
                estudiante.Cursos = _mapper.Map<IEnumerable<CursoDTO>>(await _cursoRepository.GetPorEstudiantes(estudiante.Id));
                result.Error = false;
                result.Result = estudiante;
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
        /// Elimina estudiantes
        /// </summary>
        public async Task<GenericResult> Delete(int Id)
        {
            GenericResult result = new GenericResult();
            try
            {
                result.Error = false;
                result.Result = await _estudianteRepository.Delete(Id);
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
        /// Realiza la creacion o actualizacion de un estudiante
        /// </summary>
        public async Task<GenericResult> Matricular(EstudianteCursoDTO estudianteCursoDTO)
        {
            GenericResult result = new GenericResult();
            try
            {
                result.Error = false;
                result.Result = await _estudianteRepository.Matricular(estudianteCursoDTO);

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
