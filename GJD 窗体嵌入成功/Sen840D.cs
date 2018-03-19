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

namespace GJD
{
    interface I840D
    {
        void Sen840Dmotion(object a);
        void CharaStrucG(object a);
        void EdgeStrucG(object a);
        void EdgeStruc840DMachine(object a);
    }
    public partial class Sen840D : Form, I840D
    {
        public static bool sen840DHasMotion = false;
        public static bool sen840DHasEdgemachine = false;
        public static bool charaStrucHasG = false;
        public static bool edgeStrucHasG = false;
        public void Sen840Dmotion(object a)
        {
            System.Threading.Thread.Sleep(1000);
            sen840DHasMotion = true;
        }
        public void CharaStrucG(object a)
        {
            System.Threading.Thread.Sleep(1000);
            charaStrucHasG = true;
        }
        public void EdgeStrucG(object a)
        {
            System.Threading.Thread.Sleep(1000);
            edgeStrucHasG = true; 
        }
        public void EdgeStruc840DMachine(object a)
        {
            System.Threading.Thread.Sleep(1000);
            sen840DHasEdgemachine = true; 
        }
        public Sen840D()
        {
            InitializeComponent();
            SetListView();
        }

        string currentPath = null;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentPath = System.IO.Path.GetFullPath(dialog.FileName);
                //移除ListView所有项   
                this.listView1.Items.Clear();
                //数据更新 UI暂时挂起直到EndUpdate绘制控件,可以有效避免闪烁并大大提高加载速度  
                this.listView1.BeginUpdate();

                int count = 0;
                foreach (string filename in dialog.FileNames)
                {
                    count = count + 1;
                    //Path.GetExtension方法:返回指定的路径字符串的扩展名  
                    string ExtensionName = Path.GetExtension(filename);
                    //Path.GetFileName方法:返回指定路径字符串的文件名和扩展名。  
                    string FileName = Path.GetFileName(filename);
                    FileInfo fileInfo = new FileInfo(filename);


                    ListViewItem listItem = new ListViewItem(); 
                    listItem.Text = "["+count.ToString()+"]" + fileInfo.Name;    //显示文件名  
                    //listItem.ForeColor = Color.Blue;                            //设置行颜色  

                    //length/1024转换为KB字节数整数值 Ceiling返回最小整数值 Divide除法  
                    long length = fileInfo.Length;                                //获取当前文件大小  
                    listItem.SubItems.Add(Math.Ceiling(decimal.Divide(length, 1024)) + " KB");

                    ////获取文件最后访问时间  
                    //listItem.SubItems.Add(fileInfo.LastWriteTime.ToString());  

                    //获取文件扩展名时可用Substring除去点 否则显示".txt文件"  
                    listItem.SubItems.Add(ExtensionName);
                    //加载数据至listView1  
                    this.listView1.Items.Add(listItem);  
                }           
                this.listView1.EndUpdate(); 
            }

        }

        private void SetListView()
        {
            //行和列是否显示网格线  
            this.listView1.GridLines = false;
            //显示方式(注意View是Details详细显示)  
            this.listView1.View = View.Details;
            //是否可编辑  
            this.listView1.LabelEdit = true;
            //没有足够的空间显示时,是否添加滚动条  
            this.listView1.Scrollable = true;
            //对表头进行设置  
            this.listView1.HeaderStyle = ColumnHeaderStyle.Clickable;
            //是否可以选择行  
            this.listView1.FullRowSelect = true;

            //设置listView列标题头 宽度为9/13 2/13 2/13   
            //其中设置标题头自动适应宽度,-1根据内容设置宽度,-2根据标题设置宽度  
            this.listView1.Columns.Add("名称", 9 * listView1.Width / 13);
            this.listView1.Columns.Add("大小", 2 * listView1.Width / 13);
            this.listView1.Columns.Add("类型", 2 * listView1.Width / 13);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(currentPath !=null)
            {
                Directory.SetCurrentDirectory(Directory.GetParent(currentPath).FullName);
                string parentPath = Directory.GetCurrentDirectory();
               
                ListView.SelectedListViewItemCollection currentFile = listView1.SelectedItems;
                string fileName = currentFile[0].SubItems[0].Text;
                int indexPath = fileName.IndexOf(']');
                fileName = fileName.Substring(indexPath + 1);              
                string fullpath = parentPath + "\\" + fileName + "." ;

                textBox1.Text = File.ReadAllText(fullpath);     
                        
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }  

    }

}
