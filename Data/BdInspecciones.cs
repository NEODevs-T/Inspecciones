using Inspecciones.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Inspecciones.Data{
    public interface IDataImaqPre
    {
        Task<List<ImaqPre>> ObtenerLasPreguntasPorMquina(int idMaquina);
    }

    public class DataImaqPre : IDataImaqPre
    {
        private readonly DbNeoContext _cotext;

        public DataImaqPre(DbNeoContext context)
        {
            this._cotext = context;
        }

        public async Task<List<ImaqPre>> ObtenerLasPreguntasPorMquina(int idMaquina)
        {
            return await this._cotext.ImaqPres.Where(mp => mp.IdMaquina == idMaquina).Include(mp => mp.IdPreguntaNavigation).ThenInclude(p => p.IdTipPreNavigation).ToListAsync();
        }
    }

    public interface IDataInspeccion
    {
        Task<bool> InsertarInspeccion(Inspeccion inspeccion,List<InspecDatum> listData);
    }
    public class DataInspeccion : IDataInspeccion
    {
        private readonly DbNeoContext _cotext;

        public DataInspeccion(DbNeoContext context)
        {
            this._cotext = context;
        }
        public async Task<bool> InsertarInspeccion(Inspeccion inspeccion,List<InspecDatum> listData){
            bool estado;
            this._cotext.Inspeccions.Add(inspeccion);
            estado = await _cotext.SaveChangesAsync() > 0;
            if(estado == true){
                estado = await this.InsertarData(inspeccion,listData);
            }
            return estado;
        }

        private async Task<bool> InsertarData(Inspeccion inspeccion,List<InspecDatum> listData){
            foreach (var item in listData)
            {
                item.IdInspecNavigation = inspeccion;
                this._cotext.InspecData.Add(item);
            }
            return await this._cotext.SaveChangesAsync() > 0;
        }
    }

}