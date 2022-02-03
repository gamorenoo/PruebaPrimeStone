using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTOs;
using Common.Models;

namespace Common.Automapper
{
    public class GlobalMapperProfile: Profile
    {
        /// <summary>
        /// Inicia una nueva instancia del perfil
        /// </summary>
        public GlobalMapperProfile() : base()
        {
            CreateMap<EstudianteDTO, Estudiante>()
                .ForMember(d => d.Direcciones, o => o.Ignore());
            CreateMap<Estudiante, EstudianteDTO>();

            CreateMap<DireccionDTO, Direccion>();
            CreateMap<Direccion, DireccionDTO>();


            CreateMap<CursoDTO, Curso>()
                .ForMember(d => d.EstudiantesCursos, o => o.Ignore())
                .ForMember(d => d.FechaCreacion, o => o.Ignore());
            CreateMap<Curso, CursoDTO>();

            //    .ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role.Name));
            //CreateMap<Role, RoleDTO>();
        }
    }
}
