using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_Dinamica
{
    public partial class FormImagenGran : Form
    {
        public FormImagenGran(string rutaImagen)
        {
            InitializeComponent();

            this.Text = Path.GetFileName(rutaImagen);
            this.Size = new Size(600, 600);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = Image.FromFile(rutaImagen);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            this.Controls.Add(pictureBox);
        }

    }
}
