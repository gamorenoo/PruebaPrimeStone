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
    public class EstudianteRepository
    {
        #region Propiedades
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Repositorio de estudiantes
        /// </summary>
        private readonly IGenericRepository<Estudiante> _estudianteRepository;

        /// <summary>
        /// Repositorio de cursos
        /// </summary>
        private readonly IGenericRepository<Curso> _cursoRepository;

        /// <summary>
        /// Repositorio de direcciones
        /// </summary>
        private readonly IGenericRepository<Direccion> _direeccionRepository;

        /// <summary>
        /// Repositorio de estudiantescursos
        /// </summary>
        private readonly IGenericRepository<EstudianteCurso> _estudianteCursoRepository;

        #endregion

        #region Build
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="userRepository"></param>
        public EstudianteRepository(IMapper mapper, IGenericRepository<Estudiante> estudianteRepository, 
                                    IGenericRepository<Direccion> direeccionRepository, IGenericRepository<Curso> cursoRepository,
                                    IGenericRepository<EstudianteCurso> estudianteCursoRepository)
        {
            _mapper = mapper;
            _estudianteRepository = estudianteRepository;
            _direeccionRepository = direeccionRepository;
            _cursoRepository = cursoRepository;
            _estudianteCursoRepository = estudianteCursoRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Realiza la creacion o actualizacion de un estudiante
        /// </summary>
        public async Task<EstudianteDTO> Save(EstudianteDTO estudianteDTO)
        {
            Estudiante estudiante = _mapper.Map<Estudiante>(estudianteDTO);
            // estudiante.Direcciones = _mapper.Map<IEnumerable<Direccion>>(estudianteDTO.Direcciones);
            
            if (estudiante.Id == 0)
            {
                estudiante.FechaCreacion = DateTime.Now;
                estudiante = await _estudianteRepository.Add(estudiante);


                return _mapper.Map<EstudianteDTO>(estudiante);
            }
            else
            {
                estudiante.FechaActualizacion = DateTime.Now;
                return _mapper.Map<EstudianteDTO>(await _estudianteRepository.Update(estudiante));
            }
        }

        /// <summary>
        /// Consulta la lista de estudiantes
        /// </summary>
        public async Task<IEnumerable<EstudianteDTO>> Get()
        {
            var estudiantes = await _estudianteRepository.GetList();
            foreach (var estudiante in estudiantes)
            {
                estudiante.Direcciones = await _direeccionRepository.GetList(d => d.EstudianteId == estudiante.Id);
            }
            return _mapper.Map<IEnumerable<EstudianteDTO>>(estudiantes);
        }

        /// <summary>
        /// Consulta un estudiante por su id
        /// </summary>
        public async Task<EstudianteDTO> Get(int Id)
        {
            var estudiante = await _estudianteRepository.GetList(e => e.Id == Id);
            
            return _mapper.Map<EstudianteDTO>(estudiante.FirstOrDefault());
        }


        /// <summary>
        /// Elimina un estudiante
        /// </summary>
        public async Task<bool> Delete(int Id)
        {
            var estudiante = await _estudianteRepository.Get(e => e.Id == Id);
            
            if(estudiante == null) throw new Exception("No se encuentra el estudiante con Id: " + Id);

            await _estudianteRepository.Delete(estudiante);
            return true;
        }

        /// <summary>
        /// Realiza la creacion o actualizacion de un estudiante
        /// </summary>
        public async Task<bool> Matricular(EstudianteCursoDTO estudianteCursoDTO)
        {
            Estudiante estudiante = await _estudianteRepository.Get(e => e.Id.Equals(estudianteCursoDTO.EstudianteId));
            if (estudiante == null) throw new Exception("No se encuentra el estudiante con Id: " + estudianteCursoDTO.EstudianteId);
            foreach (var cursoId in estudianteCursoDTO.CursoId)
            {
                Curso curso = await _cursoRepository.Get(c => c.Id.Equals(cursoId));

                if (curso == null) throw new Exception("No se encuentra el curso con Id: " + cursoId);

                await _estudianteCursoRepository.Add(new EstudianteCurso()
                {
                    CursoId = curso.Id,
                    EstudianteId = estudiante.Id,
                    FechaCreacion = DateTime.Now,
                });

            }
            

            return true;
        }

        #endregion
    }
}
