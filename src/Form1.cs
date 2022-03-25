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

namespace TubesStima2
{
    public partial class Form1 : Form
    {
        Graph g;
        string[] files;
        string method = "BFS";
        string filesearch = "";
        string root = "";
        Boolean alloccurence = false;
        Boolean found = false;

        Microsoft.Msagl.Drawing.Graph graph;
        public Form1()
        {
            InitializeComponent();
        }

        private void printGraph()
        {
            Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
            //graph.Attr.  .ArrowheadAtTarget = ArrowStyle.None;
            renderer.CalculateLayout();

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            Bitmap bitmap = new Bitmap(width, (int)(graph.Height* (width / graph.Width)), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);
            bitmap.Save("graph.png");
            pictureBox1.Image = bitmap;
        }

        private void graphConstruct(List<string> path)
        {
            graph = new Microsoft.Msagl.Drawing.Graph("graph");
            foreach (var n in g.nodes)
            {
                foreach (var m in g.pointingTo[g.findIdxInNodes(n)])
                {
                    if (n!="" || m != "" || n!=null || m!=null)
                    {
                        var edge= graph.AddEdge(n, m);
                        
                        edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        graph.FindNode(n).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
                        graph.FindNode(m).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
                    }

                }
                
            }

            foreach (var p in path)
            {
                System.Diagnostics.Debug.WriteLine(p);
                graph.FindNode(p).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //browse button
            //get file directory, file yang di load "BUKAN" di dalam folder debug
            FolderBrowserDialog somefolder = new FolderBrowserDialog();
            
            if (somefolder.ShowDialog() == DialogResult.OK)
            {
                this.files = Directory.GetFileSystemEntries(somefolder.SelectedPath);
                label3.Text = somefolder.SelectedPath;
                this.root = label3.Text;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.method = "BFS";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.method = "DFS";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.alloccurence = !this.alloccurence;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!root.Equals(""))
            {
                this.filesearch = textBox1.Text;
                string daunorigin = this.root;
                string[] daunoriginpointingto = this.files;
                //Generate all the tree first
                g = new Graph(daunorigin, daunoriginpointingto);
                create_graph(daunorigin, daunoriginpointingto);
                List<String> path = g.SearchPath(daunorigin, this.filesearch, this.method);
                if (path.Count > 0)
                {
                    linkLabel1.Text = path[path.Count - 1];

                }
                graphConstruct(path);
                printGraph();
            }
            
        }
        private Boolean checkifitsfolder(string dir)
        {
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(dir);

            //detect whether its a directory or file
            if (attr.HasFlag(FileAttributes.Directory))
                return true;
            return false;
        }
        private void create_graph(string from, string[] pointingto)
        {
            List<String> path = g.SearchPath(from, this.filesearch, this.method);
            //kalau tidak nemu, maka add nodes buat idk
            if (pointingto.Length > 0 && path.Count == 0)
            {
                
                foreach (var dop in pointingto)
                {
                    if (checkifitsfolder(dop))
                    {
                        try
                        {
                            g.AddBatch(dop, Directory.GetFileSystemEntries(dop));
                            create_graph(dop, Directory.GetFileSystemEntries(dop));
                        }
                        catch
                        {
                        }
                    }
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start((string)e.Link.LinkData);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}