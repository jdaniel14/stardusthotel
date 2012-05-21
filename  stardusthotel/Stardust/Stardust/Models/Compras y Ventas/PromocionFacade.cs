using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class PromocionFacade
    {
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
    }
}