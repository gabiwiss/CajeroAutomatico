using Data.Entities;
using Data.Managers;
using Data.Dto;

namespace API.Services
{
    public class OperacionesService
    {   
        private readonly OperacionesManager operacionesManager;
        public OperacionesService() 
        {
            operacionesManager = new OperacionesManager();
        }

        public async Task<bool> GuardarOperacion(OperacionesDto operacionDto)
        {
            Operaciones operacion = operacionDto;
            return await operacionesManager.GuardarOperacion(operacion);
        }
    }
}
