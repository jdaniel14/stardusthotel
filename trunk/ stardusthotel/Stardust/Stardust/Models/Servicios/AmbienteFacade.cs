﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class AmbienteFacade
    {
        AmbienteService AmbienteService = new AmbienteService();
        public List<AmbienteBean> ListarAmbiente(String Nombre, String estado, float precio_menor, float precio_mayor)
        {
            return AmbienteService.ListarAmbiente(Nombre, estado, precio_menor, precio_mayor);
        }

        public String RegistrarAmbiente(AmbienteBean ambiente)
        {
            return AmbienteService.RegistrarAmbiente(ambiente);
        }

        public String ActualizarAmbiente(AmbienteBean ambiente)
        {
            return AmbienteService.ActualizarAmbiente(ambiente);
        }

        public AmbienteBean GetAmbiente(int id)
        {
            return AmbienteService.GetAmbiente(id);
        }

        public String EliminarAmbiente(int id)
        {
            return AmbienteService.EliminarAmbiente(id);
        }
    }
}