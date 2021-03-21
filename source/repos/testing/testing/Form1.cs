using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

using Microsoft.Msagl;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace tubesstima
{
    public partial class Form1 : Form
    {
        string[] input;
        string method;
        Graph g = new Graph(3);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //browse button
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text|*.txt|All|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //search button
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //upload button
            File.Copy(textBox1.Text, Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(textBox1.Text)), true);
            input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(textBox1.Text)));
            textBox1.Text = input[0];

            g.NewEdge("A", "B");
            g.NewEdge("B", "C");
            g.NewEdge("A", "C");
            List<string> test = g.SearchPath("A", "C", "BFS");
            //create a form 
            //System.Windows.Forms.Form form = Form.ActiveForm;
            //create a viewer object 
            //Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "C");
            for (int i = 0; i < test.Count; i++)
            {
                graph.FindNode(test[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            }

            Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
            renderer.CalculateLayout();
            //bind the graph to the viewer 
            //associate the viewer with the form 
            //form.FormBorderStyle = 
            /*
            using (Graphics gfx = form.CreateGraphics())
            {
                    Bitmap bmp = new Bitmap(form.Width-700, form.Height-50, gfx);
                Bitmap bmp1;
                    form.DrawToBitmap(bmp, new Rectangle(10, 10, form.Width-700, form.Height-50));
                bmp.Save("graph.png");
                pictureBox1.Image = bmp;
            }
            */
            //Microsoft.Msagl.Drawing.SvgGraphWriter.Write(graph, @"C:\Users\SAFIQ FARAY\source\repos\testing\testing\bin\Debug\graph.svg");
            int width = 150;
            Bitmap bitmap = new Bitmap(width, (int)(graph.Height * (width / graph.Width)), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);
            bitmap.Save("graph.png");
            pictureBox1.Image = bitmap;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //select account
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //explore friends with
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //bfs
            method = "BFS";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //dfs
            method = "DFS";
        }
    }
}
