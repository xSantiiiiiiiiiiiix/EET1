using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;


namespace Presupuesto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Leer el contenido del archivo de precios
            string[] lineas = File.ReadAllLines("precios.txt");
            Dictionary<string, double> precios = new Dictionary<string, double>();

            foreach (string linea in lineas)
            {
                string[] partes = linea.Split(':');
                if (partes.Length == 2)
                {
                    string producto = partes[0].Trim();
                    if (!precios.ContainsKey(producto))
                    {
                        double precio;
                        if (double.TryParse(partes[1], out precio))
                        {
                            precios.Add(producto, precio);
                        }
                    }
                }
            }

            // Calcular el presupuesto
            string[] productosSeleccionados = textBoxProductos.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            double presupuestoTotal = 0;

            foreach (string productoSeleccionado in productosSeleccionados)
            {
                string producto = productoSeleccionado.Trim();
                if (precios.ContainsKey(producto))
                {
                    presupuestoTotal += precios[producto];
                }
            }

            // Mostrar el presupuesto total
            label1.Text = "Presupuesto Total: $" + presupuestoTotal.ToString("0.00");
        }
    }
}
