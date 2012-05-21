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
    }
}