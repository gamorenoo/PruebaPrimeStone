﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace PruebaPrimeStone.Estudiantes.Repository.Repository.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class, new()
    {
        /// <summary>
        /// Ontiene un registro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<T> Get(Expression<Func<T, bool>> filter = null);
        /// <summary>
        /// Ontiene na lista de registros
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<T>> GetList(Expression<Func<T, bool>> filter = null);
        /// <summary>
        /// Agrega un registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Add(T entity);
        /// <summary>
        /// Actualiza registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Update(T entity);
        /// <summary>
        /// Borra registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Delete(T entity);
        /// <summary>
        /// Agrega un rango de registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<List<T>> AddRange(List<T> entity);
        /// <summary>
        /// Actualiza un rango de registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<List<T>> UpdateRange(List<T> entity);
    }
}
