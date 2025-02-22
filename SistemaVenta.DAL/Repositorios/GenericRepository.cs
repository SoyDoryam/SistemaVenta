using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.Repositorios;
using SistemaVenta.DAL.DBContex;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SistemaVenta.DAL.Repositorios.Contrato;

namespace SistemaVenta.DAL.Repositorios
{
    public class GenericRepository<TModel>: IGenericRepository<TModel> where TModel : class
    {
        private readonly DbventaContext _dbcontex;

        public GenericRepository(DbventaContext dbcontex)
        {
            _dbcontex = dbcontex;
        }

        public async Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro)
        {
            try
            {
                TModel modelo = await _dbcontex.Set<TModel>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch
            {
                throw;
            }
        }


        public async Task<TModel> Crear(TModel modelo)
        {
            try
            {
                _dbcontex.Set<TModel>().Add(modelo);
                await _dbcontex.SaveChangesAsync();
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(TModel modelo)
        {
            try
            {
                _dbcontex.Set<TModel>().Update(modelo);
                await _dbcontex.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(TModel modelo)
        {
            try
            {
                _dbcontex.Set<TModel>().Remove(modelo);
                await _dbcontex.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModel> queryModelo = filtro == null ? _dbcontex.Set<TModel>() : _dbcontex.Set<TModel>().Where(filtro);
                return queryModelo;
            }
            catch
            {
                throw;
            }
        }
    }
}
