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
    public partial class Form1 : Form
    {
        private Button btnAddControls;
        private FlowLayoutPanel flowLayoutPanel;
        private Button btnSeleccionarCarpeta;
        private Button btnLimpiarImagenes;
        private List<Button> dynamicButtons = new List<Button>();
        private List<TextBox> dynamicTextBoxes = new List<TextBox>();

        private List<string> imagenes = new List<string>();

        private int imagenIndex = 0;
        private string carpetaSeleccionada = "";
        private int controlCounter = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Configuración de la ventana
            this.Text = "Interfaz grafica dinamica";
            this.WindowState = FormWindowState.Maximized;//Al ejecutar la ventana form1 se hace grande
            //this.Size = new Size(1000, 500);

            // Botón para agregar controles dinámicamente
            btnAddControls = new Button();
            btnAddControls.Text = "Agregar Imagenes";
            btnAddControls.Size = new Size(150, 45);
            btnAddControls.Location = new Point(200, 20);
            btnAddControls.Click += new EventHandler(AddControls);
            this.Controls.Add(btnAddControls);

            btnSeleccionarCarpeta = new Button();
            btnSeleccionarCarpeta.Text = "Seleccionar carpeta de imagenes";
            btnSeleccionarCarpeta.Size = new Size(150, 45);
            btnSeleccionarCarpeta.Location = new Point(20, 20);
            btnSeleccionarCarpeta.Click += BtnSeleccionarCarpeta_Click;
            this.Controls.Add(btnSeleccionarCarpeta);

            btnLimpiarImagenes = new Button();
            btnLimpiarImagenes.Text = "Limpiar imagenes";
            btnLimpiarImagenes.Size = new Size(150,45);
            btnLimpiarImagenes.Location = new Point(380, 20);
            btnLimpiarImagenes.Click += BtnEliminarImagenes_Click;
            this.Controls.Add(btnLimpiarImagenes);
;
            flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Bottom;
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.Size = new Size(760, 400);
            flowLayoutPanel.Location = new Point(20, 80);
            this.Controls.Add(flowLayoutPanel);
        }

        private void AddControls(object sender, EventArgs e)
        {
            if (imagenes.Count == 0)
            {
                MessageBox.Show("Seleccione una carpeta primero con imagenes .jpg por favor", "No selecciono carpeta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (imagenIndex >= imagenes.Count)//Si ya ni hay mas imagenes, se vuelven a agregar
            {
                imagenIndex = 0;
            }

            string rutaImagen = imagenes[imagenIndex];//Obtener la imagen actual
            imagenIndex++;//Agregar la siguiente imagen

            //Crear un PictureBox para mostrar la imagen
            PictureBox pictureBox = new PictureBox();
            
            
            pictureBox.Image = Image.FromFile(rutaImagen);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Anchor = AnchorStyles.Top;
            pictureBox.Size = new Size(200, 200);
            pictureBox.Location = new Point(50, 50);
            pictureBox.Margin = new Padding(5);
            pictureBox.Tag = rutaImagen;
            pictureBox.Click += PictureBox_Click;//Para la imagen grande

            flowLayoutPanel.Controls.Add(pictureBox);//Se agrega imagen al Flowlayoupanel

            // Crear un nuevo botón
            //Button newButton = new Button();
            // newButton.Text = "Botón " + controlCounter;
            // newButton.Size = new Size(100, 30);
            // newButton.Location = new Point(20, 60 + dynamicButtons.Count * 40);
            // newButton.Click += DynamicButtonClick;

            // Crear una nueva caja de texto
            //TextBox newTextBox = new TextBox();
            //newTextBox.Size = new Size(150, 30);
            //newTextBox.Location = new Point(140, 60 + dynamicTextBoxes.Count * 40);

            //dynamicTextBoxes.Add(newTextBox);
            // dynamicButtons.Add(newButton);

            //this.Controls.Add(newTextBox);
            // this.Controls.Add(newButton);

            // controlCounter++;
        }
        private void DynamicButtonClick(object sender, EventArgs e)
        {
            if (imagenes.Count == 0)
            {
                MessageBox.Show("No hay imágenes disponibles. Asegúrate de seleccionar una carpeta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (imagenIndex >= imagenes.Count)//Si ya ni hay mas imagenes, se vuelven a agregar
            {
                imagenIndex = 0;
            }

            string rutaImagen = imagenes[imagenIndex];
            imagenIndex++;//Agrega la siguiente imagen de la carpeta seleccionada

            //Agrega la imagen al FlowLayoutPanel
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile(rutaImagen);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Width = 100;
            pictureBox.Height = 100;
            pictureBox.Margin = new Padding(5);
            pictureBox.Tag = rutaImagen;
            pictureBox.Click += PictureBox_Click;

            flowLayoutPanel.Controls.Add(pictureBox);

        }

        private void BtnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    carpetaSeleccionada = dialog.SelectedPath;
                    imagenes = Directory.GetFiles(carpetaSeleccionada, "*.jpg").ToList();

                    if (imagenes.Count == 0)
                    {
                        MessageBox.Show("No se encontraron imagenes .jpg en la carpeta", "No imagenes .jpg", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        imagenIndex = 0;//Volver a poner las imagenes de la carpeta seleccionada
                        MessageBox.Show($"Se encontraron {imagenes.Count} imagenes en la carpeta.", "Imagenes encontradas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void BtnEliminarImagenes_Click(object sender, EventArgs e)
        {
            flowLayoutPanel.Controls.Clear();//Elimina todas las imagenes del flowlayoutpanel
            imagenIndex = 0;//Reinicia el contador de imágenes
            MessageBox.Show("Se eliminaron las imagenes", "Imagenes borradas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PictureBox_Click(object sender, EventArgs e)
            {
                if (sender is PictureBox pictureBox)//Para saber la ruta de la carpeta
                {
                    string rutaImagen = pictureBox.Tag.ToString();
                    new FormImagenGran(rutaImagen).ShowDialog();
                }
            }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}
