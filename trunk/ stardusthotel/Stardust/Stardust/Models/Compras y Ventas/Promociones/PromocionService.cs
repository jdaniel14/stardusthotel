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

        public List<PromocionBean> ListarPromocion(int id, int hotel)
        {
            return promocionDAO.ListarPromocion(id, hotel);
        }

        public PromocionBean GetPromocion(int id)
        {
            return promocionDAO.GetPromocion(id);
        }

        public void ActualizarPromocion(PromocionBean promocion)
        {
            promocionDAO.ActualizarPromocion(promocion);
        }

        public void EliminarPromocion(int id)
        {
            promocionDAO.EliminarPromocion(id);
        }

        public List<Tipo> GetTipo(int i)
        {
            return promocionDAO.getTipo(i);
        }

        public List<Hoteles> GetHoteles(int i)
        {
            return promocionDAO.getHoteles(i);
        }
    }
}