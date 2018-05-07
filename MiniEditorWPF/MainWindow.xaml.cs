using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniEditorWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Inicializamos el TextBox en blanco.
            richTextBox3.Document.Blocks.Clear();

            //Marcamos el chackBox de la ventana 'Habilitar menú contextual' como activado.
            checkBox1.IsChecked = true;
        }

        /// <summary>
        /// Método que llamaremos al accionar el botón de examinar.
        /// Nos abrirá un openFileDialog.
        /// 
        /// El openFileDialog tendrá un filtro con las terminaciones de ficheros indicadas.
        /// Añadimos un booleano el cual dependiendo de si hemos seleccionado un archivo correctamente, nos lo añadirá
        /// a nuestro listBox1, en caso negativo nos mostrará un mensaje de error.
        /// 
        /// </summary>
        private void buttonExaminar_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf|All files (*.*)|*.*";
            openFileDialog1.FileName = "";

            //Abre openFileDialogue1 y comprueba si se ha cerrado.
            //En caso de que se cierre(if), hará lo que pedimos dentro del if (introducir el nombre del archivo en nuesrto listBox).

            Nullable<bool> result = openFileDialog1.ShowDialog();

            if (result == true)
            {
                try
                {
                    listBox1.Items.Add(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método el cual asignamos a nuestro comboBox.
        /// 
        /// El siguiente comboBox, dependiendo de nuestra selección hará una tarea u otra.
        /// 
        /// En caso de:
        ///     Seleccionar índice 1 (".txt"): Añade a listBox2 los elementos con terminación .txt de los que cargamos en el listBox1 anteriormente.
        ///     Seleccionar índice 2 (".rtf"): Añade a listBox2 los elementos con terminación .rtf de los que cargamos en el listBox1 anteriormente.
        ///     Seleccionar índice 3 (".*"): Añade a listBox2 los elementos con cualquier terminación de los que cargamos en el listBox1 anteriormente.
        /// 
        /// Antes de    cada acción hemos de hacer un clear() para dejar nuestro listBox2 limpio y no ir acumulando elementos.
        /// </summary>
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                //Limpiamos nuestro listBox2
                listBox2.Items.Clear();
                //Recorremos todos los items de nuestro listBox1
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    //En caso de terminar con la terminación deseada
                    if (listBox1.Items[i].ToString().EndsWith(".txt"))
                    {
                        //Lo añadimos a nuestro listBox2
                        listBox2.Items.Add(listBox1.Items[i]);
                    }
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                //Limpiamos nuestro listBox2
                listBox2.Items.Clear();
                //Recorremos todos los items de nuestro listBox1
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    //En caso de terminar con la terminación deseada
                    if (listBox1.Items[i].ToString().EndsWith(".rtf"))
                    {
                        //Lo añadimos a nuestro listBox2
                        listBox2.Items.Add(listBox1.Items[i]);
                    }
                }
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                //Limpiamos nuestro listBox2
                listBox2.Items.Clear();
                //Recorremos todos los items de nuestro listBox1
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    //Lo añadimos a nuestro listBox2
                    listBox2.Items.Add(listBox1.Items[i]);
                }
            }
        }

        /// <summary>
        /// Método que se accionará al hacer doble click sobre un item del listBox2.
        /// 
        /// Al hacer doble click en un item lo que haremos será leerlo y escribir el contenido del mismo en el richTextBox3.
        /// </summary>
        private void listBox2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //En caso de ser un archivo .rtf
            if (listBox2.SelectedItem.ToString().EndsWith(".rtf"))
            {
                //Primero de todo limpiamos el textBox3.
                richTextBox3.Document.Blocks.Clear();

                //Obtenemos el nombre del archivo del listBox2.
                String filename = listBox2.SelectedItem.ToString();

                //A partir del nombre del archivo, lo leeremos con un StreamReader.
                System.IO.StreamReader objReader = new System.IO.StreamReader(filename);

                //Una vez leído, introduciremos el texto en nuestro richTextBox.
                richTextBox3.AppendText(objReader.ReadToEnd());

                //Cerramos nuestro StreamReader
                objReader.Close();

            }
            //Aplicamos el mismo patrón para el caso de los fichero .txt
            else if (listBox2.SelectedItem.ToString().EndsWith(".txt"))
            {
                //Primero de todo limpiamos el textBox3.
                richTextBox3.Document.Blocks.Clear();

                //Obtenemos el nombre del archivo del listBox2.
                String filename = listBox2.SelectedItem.ToString();

                //A partir del nombre del archivo, lo leeremos con un StreamReader.
                System.IO.StreamReader objReader = new System.IO.StreamReader(filename);

                //Una vez leído, introduciremos el texto en nuestro richTextBox.
                richTextBox3.AppendText(objReader.ReadToEnd());

                //Cerramos nuestro StreamReader
                objReader.Close();

            }

        }

        //TODO
        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            //contextMenuStrip1.IsEnabled = true;
        }

        //TODO
        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            //contextMenuStrip1.IsEnabled = false;
        }

        // Método que limpia el contenido del richTextBox3.
        // Se accionará al dar click a la opción Nuevo de nuestro ToolBar.
        private void nuevoToolBarButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox3.Document.Blocks.Clear();
        }

        /// <summary>
        /// Método el cual llamamos a la hora de hacer click en la opcion guardar de nuestro ToolBar.
        /// 
        /// Escribirá el contenido de nuestro richTextBox3 en el fichero que seleccionamos principalmente en nuestro listBox2.
        /// 
        /// </summary>
        private void guardarToolBarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Obtenemos el nombre del fichero que tenemos abierto.
                String filename = listBox2.SelectedItem.ToString();

                //Creamos un TextWriter el cual escribirá el contenido del richTextBox en nuestro fichero.
                TextWriter txt = new StreamWriter(filename);

                //Obtenemos lo escrito en nuestro richTextBox
                string richText = new TextRange(richTextBox3.Document.ContentStart, richTextBox3.Document.ContentEnd).Text;

                //Escribimos el conetenido
                txt.Write(richText);

                //Cerramos el TextWriter
                txt.Close();

            }
            catch(Exception ex)
            {
                //Mensaje mostrado en caso de excepción, en caso de no haber seleccionado ningún archivo previamente.
                MessageBox.Show("Exception: No hay níngún fichero seleccionado");
            }
            
        }

        /// <summary>
        /// Método accionado al dar click a la opción cortar de nuestro ToolBar.
        /// 
        /// Accionará el comando cortar (el texto seleccionado) de nuestro richTextBox3.
        /// </summary>
        private void cortarToolBarButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox3.Cut();
        }

        /// <summary>
        /// Método accionado al dar click a la opción copiar de nuestro ToolBar.
        /// 
        /// Accionará el comando copiar (el texto seleccionado) de nuestro richTextBox3.
        /// </summary>
        private void copiarToolBarButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox3.Copy();

        }

        /// <summary>
        /// Método accionado al dar click a la opción pegar de nuestro ToolBar.
        /// 
        /// Accionará el comando pegar (el texto seleccionado) de nuestro richTextBox3.
        /// </summary>
        private void pegarToolBarButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox3.Paste();
        }


        // Método que llama a nuestro método close()
        // Se accionará al dar click a la opción Salir de nuestro ToolStrip.
        private void salirToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Método que nos mostrará nuestra ventana con el contenido de Acerca De.
        // Se accionará al dar click a la opción AcercaDe de nuestro ToolStrip.
        private void acercaDeToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Abrir una ventana que dice quién soy, versión y fecha.
            AcercaDe f = new AcercaDe();

            f.ShowDialog();
        }


        /// <summary>
        /// Método accionado al dar click a la opción nuevo de nuestro ToolStrip.
        /// 
        /// Limpiará el contenido de nuestro richTextBox3.
        /// </summary>
        private void nuevoToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            richTextBox3.Document.Blocks.Clear();
        }


        /// <summary>
        /// Método accionado al dar click a la opción guardar de nuestro ToolStrip.
        /// 
        /// Llamará a la función guardarToolBarButton_Click, definida para nuestro botón guardar del ToolBar.
        /// </summary>
        private void guardarToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            guardarToolBarButton_Click(sender, e);
        }


        /// <summary>
        /// Método accionado al dar click a la opción seleccionarTodo de nuestro ToolStrip.
        /// 
        /// Seleccionará todo el contenido de nuestro richTextBox3.
        /// </summary>
        private void seleccionarTodoToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            richTextBox3.SelectAll();
        }


        /// <summary>
        /// Método accionado al dar click a la opción cortar de nuestro ToolStrip.
        /// 
        /// Cortará el contenido (seleccionado) de nuestro richTextBox3.
        /// </summary>
        private void cortarToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            richTextBox3.Cut();
        }


        /// <summary>
        /// Método accionado al dar click a la opción copiar de nuestro ToolStrip.
        /// 
        /// Copiará el contenido (seleccionado) de nuestro richTextBox3.
        /// </summary>
        private void copiarToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            richTextBox3.Copy();
        }


        /// <summary>
        /// Método accionado al dar click a la opción pegar de nuestro ToolStrip.
        /// 
        /// Pegará el contenido (seleccionado) de nuestro richTextBox3.
        /// </summary>
        private void pegarToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            richTextBox3.Paste();
        }
    }
}
