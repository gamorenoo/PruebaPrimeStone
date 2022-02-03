using AutoMapper;
using Common.DTOs;
using Common.Models;
using PruebaPrimeStone.Estudiantes.Repository.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaPrimeStone.Estudiantes.Repository.Repository
{
    public class CursoRepository
    {
        #region Propiedades
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Repositorio de estudiantes
        /// </summary>
        private readonly IGenericRepository<Curso> _cursoRepository;


        /// <summary>
        /// Repositorio de estudiantes
        /// </summary>
        private readonly IGenericRepository<EstudianteCurso> _estudianteCursoRepository;

        #endregion

        #region Build
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="cursoRepository"></param>
        /// <param name="estudianteCursoRepository"></param>
        public CursoRepository(IMapper mapper, IGenericRepository<Curso> cursoRepository, IGenericRepository<EstudianteCurso> estudianteCursoRepository)
        {
            _mapper = mapper;
            _cursoRepository = cursoRepository;
            _estudianteCursoRepository = estudianteCursoRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Realiza la creacion o actualizacion de un estudiante
        /// </summary>
        public async Task<CursoDTO> Save(CursoDTO cursoDTO)
        {
            if (cursoDTO.Id == 0)
            {
                Curso curso = _mapper.Map<Curso>(cursoDTO);
                curso.FechaCreacion = DateTime.Now;
                return _mapper.Map<CursoDTO>(await _cursoRepository.Add(curso));
            }
            else
            {
                //var c = await _cursoRepository.Get(c => c.Id == cursoDTO.Id);
                //if (c == null)
                //    throw new Exception("No se encuentra curso asociado al Id: " + cursoDTO.Id);
                Curso curso = _mapper.Map<Curso>(cursoDTO);
                // curso.FechaCreacion = c.FechaCreacion;
                // curso.EstaBorrado = c.EstaBorrado;
                // curso.FechaCreacion = c.FechaCreacion;
                curso.FechaActualizacion = DateTime.Now;
                curso = await _cursoRepository.Update(curso);
                return _mapper.Map<CursoDTO>(curso);
            }
        }

        /// <summary>
        /// Consulta la lista de cursos
        /// </summary>
        public async Task<IEnumerable<CursoDTO>> Get()
        {
            var cursos = await _cursoRepository.GetList();
            return _mapper.Map<IEnumerable<CursoDTO>>(cursos);
        }

        /// <summary>
        /// Consulta un curso por su id
        /// </summary>
        public async Task<CursoDTO> Get(int Id)
        {
            var curso = await _cursoRepository.GetList(e => e.Id == Id);
            return _mapper.Map<CursoDTO>(curso.FirstOrDefault());
        }

        /// <summary>
        /// Consulta los cursos de un estudiante
        /// </summary>
        public async Task<IEnumerable<CursoDTO>> GetPorEstudiantes(int EstudianteId)
        {
            var cursos = await _estudianteCursoRepository.GetList(e => e.EstudianteId == EstudianteId);
            var listaCursos = new List<Curso>();

            foreach (var curso in cursos)
            {
                listaCursos.Add(_cursoRepository.Get(c => c.Id == curso.CursoId).Result);
            }

            return _mapper.Map<List<CursoDTO>>(listaCursos);
        }

        /// <summary>
        /// Elimina un curso
        /// </summary>
        public async Task<bool> Delete(int Id)
        {
            var estudiante = await _cursoRepository.Get(e => e.Id == Id);

            if (estudiante == null)
                throw new Exception("No se ecunetra el estudiante con Id: " + Id);

            await _cursoRepository.Delete(estudiante);
            return true;
        }

        #endregion
    }
}
