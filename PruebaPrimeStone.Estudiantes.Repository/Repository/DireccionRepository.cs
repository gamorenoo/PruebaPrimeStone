using System;
using AutoMapper;
using Common.DTOs;
using System.Linq;
using System.Text;
using Common.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using PruebaPrimeStone.Estudiantes.Repository.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Dynamic;
using Microsoft.Extensions.Configuration;

namespace PruebaPrimeStone.Estudiantes.Repository.Repository
{
    public class DireccionRepository
    {
        #region Propiedades
        /// <summary>
        /// Interfaz de configuración
        /// </summary>
        public static IConfiguration Configuration { get; private set; }
        /// <summary>
        /// Repositorio de estudiantes
        /// </summary>
        private readonly IGenericRepository<Direccion> _direeccionRepository;

        #endregion

        #region Build
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="context"></param>
        /// <param name="direeccionRepository"></param>
        public DireccionRepository(IGenericRepository<Direccion> direeccionRepository, IConfiguration configuration)
        {
            _direeccionRepository = direeccionRepository;
            Configuration = configuration;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Inserta las direcciones de un estudiante
        /// </summary>
        /// <param name="referencesData"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public async Task<bool> Insert(IEnumerable<Direccion> direcciones)
        {
            foreach (var direccion in direcciones)
            {
                using (SqlConnection sql = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("DireccionInsert", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar) { Value = direccion.StringDireccion });
                        cmd.Parameters.Add(new SqlParameter("@TipoDireccion", SqlDbType.Int) { Value = (int)direccion.TipoDireccion });
                        cmd.Parameters.Add(new SqlParameter("@EstudianteId", SqlDbType.Int) { Value = direccion.EstudianteId });
                    
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Actualiza las direcciones de un estudiante
        /// </summary>
        /// <param name="referencesData"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public async Task<bool> Update(IEnumerable<Direccion> direcciones)
        {
            foreach (var direccion in direcciones)
            {
                using (SqlConnection sql = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("DireccionDelete", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@EstudianteId", SqlDbType.Int) { Value = direccion.EstudianteId });

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Inserta las direcciones de un estudiante
        /// </summary>
        /// <param name="referencesData"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int EstudianteId)
        {
                using (SqlConnection sql = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("DireccionInsert", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EstudianteId", SqlDbType.Int) { Value = EstudianteId });

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

            return true;
        }

        /// <summary>
        /// Consulta las direcciones de un estudiante
        /// </summary>
        /// <param name="referencesData"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Direccion>> Get(int EstudianteId)
        {
            List<Direccion> direcciones = new List<Direccion>();
            dynamic responseDynamic = 0;
            using (SqlConnection sql = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("DireccionGet", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@EstudianteId", SqlDbType.Int) { Value = EstudianteId });

                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            direcciones.Add(MapToValue(reader));
                            //var dataRow = new ExpandoObject() as IDictionary<string, object>;
                            //for (var iFiled = 0; iFiled < reader.FieldCount; iFiled++)
                            //{
                            //    dataRow.Add(
                            //        reader.GetName(iFiled),
                            //        reader.IsDBNull(iFiled) ? null : reader[iFiled] // use null instead of {}
                            //    );
                            //}
                            //responseDynamic = (ExpandoObject)dataRow;
                        }
                    }
                }
            }
            // direcciones = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Direccion>>(Newtonsoft.Json.JsonConvert.SerializeObject(responseDynamic));
            return direcciones;
        }
        #endregion

        #region Private Method
        private Direccion MapToValue(SqlDataReader reader)
        {
            int td = (int)reader["TipoDireccion"];
            return new Direccion()
            {
                Id = (int)reader["Id"],
                EstudianteId = (int)reader["EstudianteId"],
                StringDireccion = reader["Direccion"].ToString(),
                TipoDireccion = (int)TipoDireccion.Domicilio == td ? TipoDireccion.Domicilio : (int)TipoDireccion.Laboral == td ? TipoDireccion.Laboral : TipoDireccion.Temporal,
                // EstaBorrado = (bool)reader["EstaBorrado"],
                FechaCreacion = (DateTime)reader["FechaCreacion"],
                // FechaActualizacion = (DateTime?)reader["FechaActualizacion"],
                // FechaBorrado = (DateTime?)reader["FechaBorrado"]

            };
        }
        #endregion
    }
}
