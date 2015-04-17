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

namespace TreeViewExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        private String lastSelectedPath = "";

        private void btn_loadDirectoryClick(object sender, EventArgs e)
        {
            String path = tb_path.Text;
            DirectoryInfo directoryInfo = new DirectoryInfo(@path);
            if (directoryInfo.Exists)
            {
                
                treeView1.Nodes.Clear();
                
                treeView1.AfterSelect += treeView1_AfterSelect;
                BuildTree(directoryInfo, treeView1.Nodes);

                // Open saved path
                if (lastSelectedPath.Length>0)
                {
                    var path_list = lastSelectedPath.Split('\\').ToList();
                    foreach (TreeNode node in treeView1.Nodes)
                        if (node.Text == path_list[0])
                            ExpandSavedPath(node, path_list);
                }
            }
            else
            {
                MessageBox.Show(String.Format("Path {0} not exist",@path));
            }
        }

        /// <summary>
        /// Open all nodes by saved path
        /// </summary>
        /// <param name="node"></param>
        /// <param name="path"></param>
        private void ExpandSavedPath(TreeNode node, List<string> path)
        {
            path.RemoveAt(0);
            node.Expand();

            if (path.Count == 0)
            {
                // Select last node
                node.TreeView.SelectedNode = node;
            }

            foreach (TreeNode mynode in node.Nodes)
            {
                if (path.Count == 0)
                    return;
                if (mynode.Text == path[0])
                    ExpandSavedPath(mynode, path);
            }
        }

        private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe)
        {
            TreeNode curNode = addInMe.Add(directoryInfo.Name);

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                curNode.Nodes.Add(file.FullName, file.Name);
            }
            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildTree(subdir, curNode.Nodes);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lastSelectedPath = e.Node.FullPath;

            if (e.Node.Name.EndsWith("txt") || e.Node.Name.EndsWith("bat"))
            {
                this.richTextBox1.Clear();
                StreamReader reader = new StreamReader(e.Node.Name);
                this.richTextBox1.Text = reader.ReadToEnd();
                reader.Close();
            }
        }
    }
}
