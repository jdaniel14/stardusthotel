using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class VariablesService
    {
        VariablesDAO variablesDAO = new VariablesDAO();

        public VariablesBean getVariables() {
            return variablesDAO.getVariables();
        }

        public void actualizarVariables( VariablesBean variables ) {
            variablesDAO.actualizarVariables( variables );
        }

        public void valorDefault() {
            variablesDAO.valorDefault();
        }
    }
}