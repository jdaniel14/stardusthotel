using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class VariablesFacade
    {
        VariablesService variablesServ = new VariablesService();

        public VariablesBean getVariables() {
            return variablesServ.getVariables();
        }

        public void actualizarVariables( VariablesBean variables ) {
            variablesServ.actualizarVariables( variables );
        }

        //public void valorDefault() {
        //    variablesServ.valorDefault();
        //}
    }
}