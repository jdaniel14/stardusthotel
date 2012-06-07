using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class EventoService
    {

        EventoDAO eventoDAO = new EventoDAO();

        public List<EventoBean> ListarEvento(String nombre, String fechaini, String fechafin)
        {
            return eventoDAO.ListarEvento(nombre,fechaini, fechafin);
        }

        public String RegistrarEvento(EventoBean evento)
        {
            return eventoDAO.insertarEvento(evento);
        }

        public String ActualizarEvento(EventoBean evento)
        {
            return eventoDAO.ActualizarEvento(evento);
        }

        public String EliminarEvento(int idEvento)
        {
            return eventoDAO.DeleteEvento(idEvento);
        }

        public EventoBean GetEvento(int idEvento)
        {
            return eventoDAO.SeleccionarEvento(idEvento);
        }

        public string GetNombre(int id)
        {
            return eventoDAO.GetNombre(id);
        }


    }
}
