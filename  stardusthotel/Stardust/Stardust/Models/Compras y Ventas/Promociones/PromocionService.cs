using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class PromocionService
    {
        PromocionDAO promocionDAO = new PromocionDAO();

        public void RegistrarPromocion(PromocionBean promocion)
        {
            promocionDAO.RegistrarPromocion(promocion);
        }
    }
}