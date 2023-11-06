using Data.Entities;
using Data.Managers;

namespace API.Services
{
    public class OperacionesService
    {   
        private readonly OperacionesManager operacionesManager;
        public OperacionesService() 
        {
            operacionesManager = new OperacionesManager();
        }

        public async Task<bool> GuardarOperacion(Operaciones operacion)
        {
            return await operacionesManager.GuardarOperacion(operacion);
        }
    }
}
