using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubesStima2
{
	public class Graph
	{
		private int totalnode; //jumlah node
		public List<string> nodes; //list nama node
		public List<List<string>> pointingTo; //list of list of string, yang tiap index nya adalah node-node yg ditunjuk oleh node bersangkutan
										   //Misal
										   //nodes = ["A","B"]
										   //pointingTo = [ ["B","C"], ["A"] ]
										   //Berarti, node A menunjuk ke node B dan C, dst
										   //Maaf kalau keliru istilah. idgaf about istilah


		public Graph(string from, string[] pointing)
        {
			int size = pointing.Length;
			nodes = new List<string>();
			pointingTo = new List<List<string>>();
			totalnode = size+1;

			foreach (var p in pointing)
            {
				this.NewEdge(from, p);
            }
		}

		public void AddBatch(string from, string[] pointing)
        {
			foreach (var p in pointing)
			{
				this.NewEdge(from, p);
			}
		}

		

		public int findIdxInNodes(string value)
		{
			//Mencari index dari this->nodes
			//input adalah string node yg mau dicari
			//output adalah index value dari list nodes
			int i = 0;
			foreach (var item in nodes)
			{
				if (value.Equals(item))
				{
					break;
				}
				i++;
			}
			return i;
		}

		public void NewEdge(string thisNode, string toNode)
		{
			//Menambah edge baru
			if (!nodes.Contains(thisNode))
			{
				nodes.Add(thisNode);
				pointingTo.Add(new List<string>());
				totalnode++;
			}
			if (!nodes.Contains(toNode))
			{
				nodes.Add(toNode);
				pointingTo.Add(new List<string>());
				totalnode++;
			}
			nodes.Sort();
			if (!pointingTo[findIdxInNodes(thisNode)].Contains(toNode)){
				pointingTo[findIdxInNodes(thisNode)].Add(toNode);
				pointingTo[findIdxInNodes(thisNode)].Sort(); //Diurutkan berdasarkan abjad
			}

			
		}

		public List<string> SearchPath(string from, string target, string with)
		{
			//Mencari jalur terpendek dari node from ke target dengan BFS
			//Cara kerjanya adalah mencatat predecessor dari iterasi tiap node yang dicari oleh BFS, kemudian kita mencari path nya secara mundur,
			//yaitu mulai target hingga from
			string targetpath = "";
			string[] pred = new string[totalnode]; //predecessor
			List<string> path = new List<string>(); //seperti namanya, list ini adalah jalur dari from ke target
			string currentNode = target; //node yang sedang diproses
			if (with == "BFS")
			{
				if (BFS(from, target, ref pred, ref targetpath))
				{ //jika target ditemukan oleh algoritma BFS, maka proses pencarian path dimulai
					currentNode = targetpath;
					path.Add(currentNode);
					while (pred[findIdxInNodes(currentNode)] != "#None#")
					{ //Jika node bukan merupakan node from, maka proses akan diteruskan (karena node from predecessornya adalah "#None#"
						path.Add(pred[findIdxInNodes(currentNode)]);
						currentNode = pred[findIdxInNodes(currentNode)]; //berjalan mundur
					}
				}
			}

			else if (with == "DFS")
			{ //menggunakan DfS

				Boolean[] visit = new Boolean[totalnode];
				Boolean found = false;
				for (int i = 0; i < totalnode; i++)
				{
					pred[i] = "#None#";
				}
				if (DFS(from, target, visit, pred, ref found, ref targetpath))
				{
					currentNode = targetpath;
					path.Add(currentNode);
					while (pred[findIdxInNodes(currentNode)] != "#None#")
					{ //Jika node bukan merupakan node from, maka proses akan diteruskan (karena node from predecessornya adalah "#None#"
						
						path.Add(pred[findIdxInNodes(currentNode)]);
						currentNode = pred[findIdxInNodes(currentNode)]; //berjalan mundur
					}
					
				}
			}

			path.Reverse(); //dibalik agar kemudahan printing result
			return path;

		}

		public Boolean BFS(string from, string target, ref string[] pred, ref string targetpath)
		{
			string first; //untuk traversal saja, ignore this
			bool[] visit = new bool[totalnode]; //menandai apakah node-node telah dikunjungi atau tidak
			List<string> list;
			for (int i = 0; i < totalnode; i++)
			{
				visit[i] = false; //default value : false
				pred[i] = "#None#";
			}

			// antrian node untuk dikunjungi
			List<string> queue = new List<string>();

			// Node pertama
			visit[findIdxInNodes(from)] = true;
			queue.Add(from);

			while (queue.Count != 0)
			{
				// Dequeue, masukkan ke path
				first = queue.First();
				queue.RemoveAt(0);

				// Melist pontingTo agar diproses ; in short mencari tetangga dari node tsb.
				list = pointingTo[findIdxInNodes(first)];

				foreach (var val in list)
				{

					if (!visit[findIdxInNodes(val)])
					{
						visit[findIdxInNodes(val)] = true;
						pred[findIdxInNodes(val)] = first;
						queue.Add(val);


						if (Path.GetFileName(val).Equals(target))
						{ //Jika ketemu, maka akan return true
							targetpath = val;
							return true;
						}
					}
				}
			}

			return false;
		}

		public Boolean DFS(string from, string target, Boolean[] visit, string[] pred, ref Boolean found, ref string targetpath)
		{
			//Penerapan algoritma BFS
			//Cara kerja (dalam konteks pencarian path) hampir sama dengan BFS.
			visit[findIdxInNodes(from)] = true; //ditandai bahwa node from sudah dikunjungi
			foreach (var val in pointingTo[findIdxInNodes(from)]) //traversal adjascent node
			{
				if (!visit[findIdxInNodes(val)]) //jika belum dikunjungi, maka diproses
				{
					pred[findIdxInNodes(val)] = from; //dicatat predecessornya
					DFS(val, target, visit, pred, ref found, ref targetpath); //rekursif
					if (Path.GetFileName(val).Equals(target))
					{
						targetpath = val;
						found = true; //jika ketemu, maka mengganti variable found menjadi true
					}
				}
			}
			return found;
		}
	}
}
