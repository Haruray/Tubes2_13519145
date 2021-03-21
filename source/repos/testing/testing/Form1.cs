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

namespace tubesstima
{
    public partial class Form1 : Form
    {
        string[] input;
        string method;
        Graph g = new Graph(5);
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //search button
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //upload button
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
