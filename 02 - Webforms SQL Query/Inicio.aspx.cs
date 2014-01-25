using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webforms_SQL_Query
{

    public partial class Inicio : System.Web.UI.Page
    {
        Inserta m = new Inserta();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Int32 Resultado = 0;

            Resultado = m.guarda(TxtNom.Text, TxtMon.Text);

            if (Resultado == 1)
            {
                LblMensage1.Text = ("Registro grabado");
            }
            else
            {
                LblMensage2.Text = ("Registro no grabado");
            }
        }

    }
}
