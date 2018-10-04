using Courses.Domain.Interfaces;
using Courses.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Courses.Infra.Data.Repositories
{
    /// <summary>
    /// Class With basic EF actions, Insert, UPDATE, DELETE
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        #region Variables 
        protected readonly ProjectCourseContext _context;
        #endregion Variables 

        #region Constructor
        /// <summary>
        /// Repository Context Constructor
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(ProjectCourseContext context)
        {
            _context = context;
        }
        #endregion Constructor
        /// <summary>
        /// Add database context
        /// </summary>
        /// <param name="obj">Object of T type</param>
        public void Add(TEntity obj)
        {
            try
            {
                _context.Set<TEntity>().Add(obj);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                //TODO : IMPLENT LOG
                throw e;
            }
        }
        /// <summary>
        /// Get By ID database context
        /// </summary>
        /// <param name="id">identifier</param>
        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        /// <summary>
        /// Get all database context
        /// </summary>
        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return _context.Set<TEntity>().ToList();
            }
            catch (Exception e)
            {
                //TODO : IMPLENT LOG
                throw e;
            }
        }
        /// <summary>
        /// Update database context row
        /// </summary>
        /// <param name="obj">Object of T type</param>
        public void Update(TEntity obj)
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                //TODO : IMPLENT LOG
                throw e;
            }
        }
        /// <summary>
        /// Delete database context row
        /// </summary>
        /// <param name="obj">Object of T Type</param>
        public void Remove(TEntity obj)
        {
            try
            {
                _context.Set<TEntity>().Remove(obj);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                //TODO : IMPLENT LOG
                throw e;
            }
        }

        public IList<TEntity> ExecProcedure(string procName)
        {
            IList<TEntity> ret;
            try
            {
                ret = _context.Set<TEntity>().FromSql(procName).ToList();
            }
            catch (Exception e)
            {
                //TODO : IMPLENT LOG
                throw e;
            }
            return ret;
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
