using System;
//using System. .Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace InveStockMBL.CLASES
{

    public static class Inputbox
    {

        // Conserva esta cabecera
        //  Emperorxdevil 2007

        static Form f;
        static Label l;
        static TextBox t; // Elementos necesarios
        static Button b1;
        static Button b2;
        static string Valor;

        /// <summary>
        /// Objeto Estático que muestra un pequeño diálogo para introducir datos
        /// </summary>
        /// <param name="title">Título del diálogo</param>
        /// <param name="prompt">Texto de información</param>
        /// <param name="posicion">Posición de inicio</param>
        /// <returns>Devuelve la escrito en la caja de texto como string</returns>
        public static string Show(string title, string prompt)
        {

            f = new Form();
            f.Text = title;
            //f.ShowIcon = false;
            f.Icon = null;
            f.KeyPreview = true;
            //f.ShowInTaskbar = false;
            f.MaximizeBox = false;
            f.MinimizeBox = false;
            f.FormBorderStyle = FormBorderStyle.FixedDialog;
            f.Width = 200;
            f.Height = 120;
            //f.StartPosition = posicion;
            f.KeyPress += new KeyPressEventHandler(f_KeyPress);

            l = new Label();
            //l.AutoSize = true;
            l.Text = prompt;


            t = new TextBox();
            t.Width = 182;
            t.Top = 40;

            //valor por defecto
            Valor = string.Empty;

            b1 = new Button();
            b1.Text = "Aceptar";
            b1.Click += new EventHandler(b1_Click);


            b2 = new Button();
            b2.Text = "Cancelar";
            b2.Click += new EventHandler(b2_Click);

            f.Controls.Add(l);
            f.Controls.Add(t);
            f.Controls.Add(b1);
            //f.Controls.Add(b2);

            l.Top = 10;
            t.Left = 5;
            t.Top = 30;

            b1.Left = 5;
            b1.Top = 60;

            b2.Left = 112;
            b2.Top = 60;

            f.ShowDialog();
            return (Valor);
        }

        static void f_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (Convert.ToChar(e.KeyChar))
            {

                case ('\r'):
                    Acepta();
                    break; ;

                case (''):
                    Cancela();
                    break; ;
            }
        }
        static void b2_Click(object sender, EventArgs e)
        {
            Cancela();
        }
        static void b1_Click(object sender, EventArgs e)
        {
            Acepta();
        }
        private static string Val
        {

            get { return (Valor); }
            set { Valor = value; }
        }
        private static void Acepta()
        {
            Val = t.Text;
            f.Dispose();
        }
        private static void Cancela()
        {
            Val = null;
            f.Dispose();
        }

        private static void closing(object sender, EventArgs e){
            if (t.Text.Equals(string.Empty))
            {
                f.Dispose();
            }
        }

    }

}
