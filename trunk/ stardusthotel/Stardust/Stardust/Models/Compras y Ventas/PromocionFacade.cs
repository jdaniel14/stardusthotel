using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 
 Registro de promociones(registrar, buscar, eliminar)   1 iteraccion
 Registrar Pago de Evento                               2 iteraccion
 Registrar pago de ambiente                             2 iteraccion
 Reporte de eventos                                     3 iteraccion

 */
namespace Stardust.Models
{
    public class PromocionFacade
    {

        /*-----Registrar Promociones-1-------*/

        PromocionService promocionService = new PromocionService();

        public void RegistrarPromocion(PromocionBean promocion)
        {
            promocionService.RegistrarPromocion(promocion);
        }

        public List<PromocionBean> ListarPromocion(int id,int hotel)
        {
            return promocionService.ListarPromocion(id,hotel);
        }

        public PromocionBean GetPromocion(int id)
        {
            return promocionService.GetPromocion(id);
        }

        public void ActualizarPromocion(PromocionBean promocion)
        {
            promocionService.ActualizarPromocion(promocion);
        }

        public void EliminarPromocion(int id)
        {
            promocionService.EliminarPromocion(id);
        }

        public List<Tipo> GetTipo(int i)
        {
            return promocionService.GetTipo(i);
        }

        public List<Hoteles> GetHoteles(int i)
        {
            return promocionService.GetHoteles(i);
        }

        /*-----Registrar Pago de Evento-2-------*/


        /*-----Registrar Pago de Ambiente-2-------*/


        /*-----Reporte de Eventos-3-------*/

    }
}