
using System;
using System.Linq;
using Common.Context;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PruebaPrimeStone.Estudiantes.Repository.Repository.IRepositories;

namespace PruebaPrimeStone.Estudiantes.Repository.Repository
{
    /// <summary>
    /// Administracion generica del Repositorio
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly AplicacionDBContext _AppDBcontext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ApiDBcontext"></param>
        public GenericRepository(AplicacionDBContext ApiDBcontext)
        {
            _AppDBcontext = ApiDBcontext;
        }
        /// <summary>
        /// Agrega un registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> Add(TEntity entity)
        {
            await _AppDBcontext.AddAsync(entity);
            await _AppDBcontext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        ///  Agrega un rango de registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> AddRange(List<TEntity> entity)
        {
            await _AppDBcontext.AddRangeAsync(entity);
            await _AppDBcontext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Borra registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Delete(TEntity entity)
        {
            var addedEntry = _AppDBcontext.Remove(entity);
            return await _AppDBcontext.SaveChangesAsync();
        }

        /// <summary>
        /// Ontiene un registro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _AppDBcontext.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        /// <summary>
        /// Ontiene na lista de registros
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return await (filter == null ? _AppDBcontext.Set<TEntity>().ToListAsync() : _AppDBcontext.Set<TEntity>().Where(filter).ToListAsync());
        }

        /// <summary>
        /// Actualiza registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> Update(TEntity entity)
        {
            var addedEntry = _AppDBcontext.Update(entity);
            await _AppDBcontext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Actualiza un rango de registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> UpdateRange(List<TEntity> entity)
        {
            _AppDBcontext.UpdateRange(entity);
            await _AppDBcontext.SaveChangesAsync();
            return entity;
        }
    }
}
