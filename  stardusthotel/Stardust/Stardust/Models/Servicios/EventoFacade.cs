using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class EventoFacade
    {
        EventoService eventoService = new EventoService();


        public List<EventoBean> ListarEvento(String nombre, String fechaini, String fechafin)
        {
            return eventoService.ListarEvento(nombre, fechaini, fechafin);
        }

        public String RegistrarEvento(EventoBean evento)
        {
            return eventoService.RegistrarEvento(evento);
        }

        public String ActualizarEvento(EventoBean evento)
        {
            return eventoService.ActualizarEvento(evento);
        }

        public String EliminarEvento(int idEvento)
        {
            return eventoService.EliminarEvento(idEvento);
        }

        public EventoBean GetEvento(int idEvento)
        {
            return eventoService.GetEvento(idEvento);
        }

        public string GetNombre(int id)
        {
            return eventoService.GetNombre(id);
        }
    }
}