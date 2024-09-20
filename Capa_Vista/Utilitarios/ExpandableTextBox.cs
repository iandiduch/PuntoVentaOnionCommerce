using Bunifu.Framework.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Onion_Commerce.Utilitarios
{
    public partial class ExpandableTextBox : UserControl
    {
        
        private string fullText;

        public ExpandableTextBox()
        {
           InitializeComponent();
        }

        private void Label_Click(object sender, EventArgs e)
        {
            // Abre una ventana con el texto completo
            TextoCompleto popupForm = new TextoCompleto();

            popupForm.textBox.Text = fullText;

            popupForm.ShowDialog();
        }

        public override string Text
        {
            get { return fullText; }
            set
            {
                fullText = value;
                label2.Text = value;

                // Ajustar el tamaño del label
                AdjustSize();
            }
        }

        private void AdjustSize()
        {
            // Ajustar el tamaño del label al contenido
            var textSize = TextRenderer.MeasureText(label2.Text, label2.Font, label2.MaximumSize, TextFormatFlags.WordBreak);

            if (textSize.Height > this.Height)
            {
                // Limitar el tamaño del control y mostrar "..." al final del texto
                label2.Text = fullText.Substring(0, fullText.Length / 2) + "...";
                label1.Visible = true;
            }

            label2.Size = textSize;
        }
    }
}
