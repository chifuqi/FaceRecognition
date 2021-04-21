using FaceRecognition.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecognition
{
    public partial class FaceFrom : Form
    {
        private PictureCutter _pictureCutter;
        private RecongniseProxy _recongnise;

        //测试
        //C:\Users\chifuqi\Desktop\4. 民政局\测试数据-民政局
        //C:\Users\chifuqi\Desktop\已识别
        public FaceFrom()
        {

            InitializeComponent();
            _pictureCutter = new PictureCutter();
            _recongnise = new RecongniseProxy(this);
            Thread.CurrentThread.Name = "FACEFORM_THREAD";

            Console.WriteLine("Main-ThreadName: " + Thread.CurrentThread.Name);
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }


        private void btn_selected_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog_selected.ShowDialog() == DialogResult.OK)
            {
                txtBox_selected.Text = folderBrowserDialog_selected.SelectedPath;
            }
        }

        private void btn_stored_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog_stored.ShowDialog() == DialogResult.OK)
            {
                txtBox_stored.Text = folderBrowserDialog_stored.SelectedPath;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(txtBox_selected.Text))
            {
                MessageBox.Show("请选择检索文件夹");
                return;
            }

            if (String.IsNullOrEmpty(txtBox_stored.Text))
            {
                MessageBox.Show("请选择存储文件夹");
                return;
            }
            btn_search.Enabled = false;

          
            
            Thread thread = new Thread(new ThreadStart(threadRun));
            thread.Start();

            //Thread.CurrentThread.Join();
            thread.Join();
            txtBox_log.AppendText("-----------------搜索结束-----------------");
            btn_search.Enabled = true;
        }

        public void threadRun() {
            Context context = new Context(txtBox_selected.Text,
                txtBox_stored.Text,
                _pictureCutter,
                _recongnise,
                this);

             context.execute();
        }

        public void appendText(String txt)
        {
             txtBox_log.AppendText(txt);
        }
    }
}
