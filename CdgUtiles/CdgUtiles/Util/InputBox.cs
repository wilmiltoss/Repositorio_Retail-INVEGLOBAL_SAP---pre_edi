using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CdgUtiles.Util
{
    public class InputBox
    {
        /// <summary>
        /// Despliega un cuadro de dialogo para ingreso de valor
        /// </summary>
        /// <param name="cTituloParam">Titulo del cuadro</param>
        /// <param name="cPrompParam">Mensaje de solicitud</param>
        /// <param name="cValorParam">Valor devuelto</param>
        /// <param name="bOculto">Si el valor ingresado debe ocultarse o no</param>
        /// <returns>Resultado el Dialogo</returns>
        public static DialogResult Show(string cTituloParam, string cPrompParam, ref string cValorParam, bool bOculto)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = cTituloParam;
            label.Text = cPrompParam;
            textBox.Text = cValorParam;
            if (bOculto != null && bOculto)
                textBox.PasswordChar = '*';

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(System.Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            cValorParam = textBox.Text;

            return dialogResult;
        }

    }
}
