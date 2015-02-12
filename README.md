# MusicPlayer
MusicPlayer  C# winform

**********************
*   音乐播放器开发   *
**********************

开发：
 1.导入windows media Player组件

 2.解决播放器丑陋问题，使用PictureBox每隔指定的时间来显示一张不同的图片
     --->Timer组件
     --->Click事件中，每隔指定的秒数来更换图片

 3.解决播放器自动播放问题
   播放器会自动播放你赋值给他的音乐播放文件的路径
   --->在程序加载的时候，取消音乐播放器的自动播放功能
   
 4.实现播放器的播放或者暂停状态
   --->通过控制按钮的文本来控制当前播放器是否播放
   
 5.实现播放器的停止功能
 
 6.导入歌曲列表，并且实现双击播放
   --->向窗体中拖入一个ListBox列表控件，显示歌曲名称
   --->选择音乐
     --->弹出打开对话框
     --->在打开对话框中选择音乐
     --->将选中的音乐文件的名称加载到列表中
     
7.双击播放
  --->给当前的listBox列表控件注册一个单击事件
  
8.下一曲和上一曲

9.多选删除
  --->将列表设置为多选
  --->导入右键菜单，跟listBox列表进行关联
  --->需要删除两个地方
      listSongs这个集合
      listBox这个列表
      先删谁？怎么删？
          --->根据索引来删 先删集合，后删列表
          
10.自动播放下一曲
   两种做法：
   		1.根据时间差来进行下一曲
        ---> musicPlayer.Ctlcontrols.currentPosition : 当前音乐播放时间 double类型 62.75
        ---> musicPlayer.Ctlcontrols.currentPositionString :当前播放时间 string类型 01:02
        ---> musicPlayer.currentMedia.duration ：当前歌曲总时间 double类型 258.3222121
        ---> musicPlayer.currentMedia.durationString :当前歌曲总时间 string类型 04:19
       2.根据播放器的状态来进行下一曲
        ---> Ready Playing Pause Ended Stop
            当播放器的状态到达Ended的时候进行下一曲
            在Ready之后，我们开始play()
            
11.显示歌词
   --->判断是否存在歌词文件
   
BUG与改进:
  BUG:
      --->打开软件时，无音乐的URL,点击播放按钮无效
      --->在listBox上双击播放ok,select后点“播放”按钮，歌词显示有问题
      --->More...
      
  改进：
      --->歌词实现逐字变化显示，目前只是逐句显示
      --->歌曲与相关图片一对多映射
      
  深度改进：   
      --->网络播放
      --->推荐算法
