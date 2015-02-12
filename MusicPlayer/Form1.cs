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

namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //程序加载的时候 给当前窗体更换一个好看的皮肤
            skinEngine1.SkinFile = @"C:\Users\Su Liang\Desktop\皮肤\skin\DeepGreen.ssk";
            //程序加载的时候 给当前的PictureBox赋值一张默认的图片
            pictureBox1.Image = Image.FromFile(@"C:\Users\Su Liang\Desktop\Images\1.jpg");
            //设置图片在PictureBox中的布局
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            //取消播放器的自动播放功能
            MusicPlayer.settings.autoStart = false;
            //给当前播放器赋值一个默认的文件路径
           // MusicPlayer.URL = @"C:\Users\Su Liang\Desktop\music\模特.mp3";
        }

        int i = 0;
        List<string> listSongs = new List<string>(); //存储音乐文件全路径
        private void btnChange_Click(object sender, EventArgs e)
        {
            // 把所有的皮肤文件全部读取进来
            // 获取皮肤文件夹中的所有皮肤文件的全路径 存储到stylePath数组中
            string[] stylePath = Directory.GetFiles(@"C:\Users\Su Liang\Desktop\皮肤\skin");
            //点击更换皮肤 就是在数组中拿一个全路径 赋值给SkinFile
            i++;
            if (i == stylePath.Length)
            {
                i = 0;
            }
            skinEngine1.SkinFile = stylePath[i];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //每隔指定的秒数来更换图片

            //读取图片文件
            string[] imgsPath = Directory.GetFiles(@"C:\Users\Su Liang\Desktop\Images");
            i++;
            if (i == imgsPath.Length)
            {
                i = 0;
            }
            pictureBox1.Image = Image.FromFile(imgsPath[i]);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MusicPlayer_Enter(object sender, EventArgs e)
        {

        }

        private void btnPlayOrPause_Click(object sender, EventArgs e)
        {
            //播放和暂停

                if (btnPlayOrPause.Text == "播放")
                {
                    MusicPlayer.Ctlcontrols.play();
                    btnPlayOrPause.Text = "暂停";
                }
                else if (btnPlayOrPause.Text == "暂停")
                {
                    MusicPlayer.Ctlcontrols.pause();
                    btnPlayOrPause.Text = "播放";
                }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //点击停止
            MusicPlayer.Ctlcontrols.stop();
            lblLrc.Text = "   当前无歌曲播放   ";
        }

        //选择音乐
        private void ChooseMusic_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //设置对话框属性
            //设置打开对话框标题
            ofd.Title = "请选择要播放的音乐文件";
            //设置对话框可以多选
            ofd.Multiselect = true;
            //设置打开文件类型
            ofd.Filter = "音乐文件|*.mp3|所有文件|*.*";
            //设置打开文件的初始路径
            ofd.InitialDirectory = @"C:\Users\Su Liang\Desktop";
            //展示对话框
            ofd.ShowDialog();

            //获得对话框选中的文件的全路径
            string[] filePath = ofd.FileNames;
            //根据全路径截取文件名加载到ListBox列表中
            //需要将数组中的全路径储存起来

            for (int i = 0; i < filePath.Length; i++)
            {
                //将全路径存储到集合
                listSongs.Add(filePath[i]);
                //将文件名截取放到listBox列表中
                listBox1.Items.Add(Path.GetFileName(filePath[i]));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //双击播放
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return ;
            
            MusicPlayer.URL = listSongs[listBox1.SelectedIndex];
            MusicPlayer.Ctlcontrols.play();
            //更改播放.暂停按钮
            if (btnPlayOrPause.Text == "播放")
            {
                btnPlayOrPause.Text = "暂停";
            }
            //加载歌词
            LoadLrc();
        }

        //上一曲
        private void BeforeSong_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            //将之前选中项的索引清空，确保之后只有一个歌曲被选中
            listBox1.SelectedIndices.Clear();
            if (index == -1) //列表为空
                return;

            index--;
            if (index < 0)
            {
                index = listBox1.Items.Count - 1; // 更新当前列表索引
            }
            listBox1.SelectedIndex = index;
            MusicPlayer.URL = listSongs[index];
            MusicPlayer.Ctlcontrols.play();
        }

        //下一曲
        private void NextSong_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            listBox1.SelectedIndices.Clear();
            if (index == -1)  //列表为空
                return;

            index++;
            if (index >= listBox1.Items.Count)
            {
                index = 0;
            }
            listBox1.SelectedIndex = index; //更新当前列表索引
            MusicPlayer.URL = listSongs[index];
            MusicPlayer.Ctlcontrols.play();
        }

        //多选删除
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = listBox1.SelectedItems.Count;//要清除歌曲的数量

            for (int i = 0; i < count; i++)
            {
                listSongs.RemoveAt(listBox1.SelectedIndex);//先删集合
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);//再删集合
            }
        }

        private void Playinfo_Click(object sender, EventArgs e)
        {

        }
        void showInfo()
        {
            
        }

        //自动播放及歌词显示
        private void timer2_Tick(object sender, EventArgs e)
        {
            //当前正在播放
            if (MusicPlayer.playState ==
                WMPLib.WMPPlayState.wmppsPlaying)
            {
                
                Playinfo.Text = "\r\n" + MusicPlayer.currentMedia.name
                    + "\r\n" + MusicPlayer.Ctlcontrols.currentPositionString
                    + "\r\n" + MusicPlayer.currentMedia.durationString;
               
                //根据时间差进行下一曲
                if (MusicPlayer.currentMedia.duration -
                   MusicPlayer.Ctlcontrols.currentPosition <= 1
                     )
                {
                    //自动播放下一曲
                    int index = listBox1.SelectedIndex;

                    listBox1.SelectedIndices.Clear();
                    if (index == -1)  //列表为空
                        return;

                    index++;
                    if (index >= listBox1.Items.Count)
                    {
                        index = 0;
                    }
                    listBox1.SelectedIndex = index; //更新当前列表索引
                    MusicPlayer.URL = listSongs[index];
                    MusicPlayer.Ctlcontrols.play();
                }
            }
        }

        //根据播放状态来播放下一曲
        private void MusicPlayer_PlaylistChange(object sender, 
            AxWMPLib._WMPOCXEvents_PlaylistChangeEvent e)
        {
            /*
            if (MusicPlayer.playState ==
                WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                //自动播放下一曲
                int index = listBox1.SelectedIndex;

                listBox1.SelectedIndices.Clear();
                if (index == -1)  //列表为空
                    return;

                index++;
                if (index >= listBox1.Items.Count)
                {
                    index = 0;
                }
                listBox1.SelectedIndex = index; //更新当前列表索引
                MusicPlayer.URL = listSongs[index];
                
            }

            if (MusicPlayer.playState ==
                WMPLib.WMPPlayState.wmppsReady)
            {
                try
                {
                    MusicPlayer.Ctlcontrols.play();
                }
                catch { }
                
            }
            */
        }

        //显示歌词
        void  LoadLrc()
        {
            string songPath = listSongs[listBox1.SelectedIndex];
            string songPath1 = songPath + ".lrc";  //.mp3.lrc文件
            songPath = songPath.Remove(songPath.LastIndexOf(".mp3"));
            songPath += ".lrc";
            string songPath2 = songPath;
            if (File.Exists(songPath2)) //.lrc文件
            {
                //如果存在，通过路径读取
                string[] lrcText = File.ReadAllLines
                    (songPath2, Encoding.Default);
                
                FormatLrc(lrcText);
            }
            else
            {
                if (File.Exists(songPath1)) //.mp3.lrc文件
                {
                    //如果存在，通过路径读取
                    string[] lrcText = File.ReadAllLines
                        (songPath1, Encoding.Default);

                    FormatLrc(lrcText);
                }
                else
                {
                    //不存在
                    lblLrc.Text = "    歌词未找到   ";
                }
                
            }
        }
        
        //储存时间
        List<double> listTime = new List<double>();
        //储存歌词
        List<string> listLrc = new List<string>();

        void FormatLrc(string[] lrcText)
        {
            for (int i = 0; i < lrcText.Length; i++)
            {
                //[00:15.57]歌词歌词歌词
                string[] lrcTmp = lrcText[i].Split(new char[] { '[', ']' },
                    StringSplitOptions.RemoveEmptyEntries);
                if (lrcTmp.Length<2)
                {
                    continue;
                }
                listLrc.Add(lrcTmp[1]);

                string[] lrcNewTmp = lrcTmp[0].Split(new char[] { ':' },
                    StringSplitOptions.RemoveEmptyEntries);

                double time = double.Parse(lrcNewTmp[0]) * 60 + 
                    double.Parse(lrcNewTmp[1]);

                listTime.Add(time);
            }
        }

        //逐句显示歌词
        private void timer3_Tick(object sender, EventArgs e)
        {
            //当前播放时间
            double currentTime =
                MusicPlayer.Ctlcontrols.currentPosition;

            for (int i = 0; i < listTime.Count - 1; i++)
            {
                if (currentTime >= listTime[i] && currentTime < listTime[i+1])
                {
                    lblLrc.Text = listLrc[i];
                }
            }
        }

        private void lblLrc_Click(object sender, EventArgs e)
        {

        }

    }
}
