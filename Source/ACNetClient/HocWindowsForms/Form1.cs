using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace ACNetClient
{



    public partial class Form1 : Form
    {
        #region Attributes
        int No = 0; // so thu tu bai hat dc chon share
        private const int BUFFER_SIZE = 100;

        TcpClient client = new TcpClient();
        private int ClientPort;
        Stream stream = null;
        StreamReader reader = null;
        StreamWriter writer = null;

        private int _downloadConnectionCount = 0;
        private const int MAX_CONNECTION = 20;
        ///   /////
        Thread thP2PSetup;

        bool ListenClientX = false;

        // send songinfo

        string pathfile = "";
        string title = "";
        string type;
        //load songinfo
        string loadsonginfo;
        string loadID;
        string loadtitle;
        string loadartist;
        string loadauthor;
        string loadtype;
        string loadIP;
        string loadclientPort;
        #endregion

        /// <summary>
        /// / Xac dinh tieu chi search
        /// </summary>
        /// 
        // khoi tao nguoi dung chua chon tieu chi search

        string typesearch = "";
        string keysearch = "";




        public Form1()
        {
            InitializeComponent();
            open.Enabled = false;
            sharenhac.Enabled = false;
            refreshlistSong.Enabled = false;
            download.Enabled = false;
            comboBox1.SelectedItem = "Title";
            ClientPort = RandomNumber();

            //FileInfo info = new FileInfo("../../clientXML.xml");
            //if (info.Exists)
            //{
            //    info.Delete();
            //}
        }

        //Ham random port
        private static int RandomNumber()
        {
            Random random = new Random();
            return random.Next(0, 1000);
        }


        public void LoadListSong()
        {
            NetworkStream str = client.GetStream();
            StreamReader readerStr = new StreamReader(str);
            StreamWriter writerStr = new StreamWriter(str);
            writerStr.AutoFlush = true;

            ListViewItem item = new ListViewItem();
            int nlist = 0;
            loadsonginfo = null;
            while (!string.IsNullOrEmpty(loadsonginfo = readerStr.ReadLine()))
            {
                if (loadsonginfo == "|")
                    break;
                nlist++;
                loadsongInfoHandle(loadsonginfo);
                loadsonginfo = null;
                item = new ListViewItem(nlist.ToString());

                item.SubItems.Add(loadtitle);
                item.SubItems.Add(loadartist);
                item.SubItems.Add(loadauthor);
                item.SubItems.Add(loadtype);
                item.SubItems.Add(loadID);
                item.SubItems.Add(loadIP);
                item.SubItems.Add(loadclientPort);

                listallsong.Items.Add(item);
            }
        }

        public void loadsongInfoHandle(string info)
        {
            if (string.IsNullOrEmpty(info))
                return;
            int[] index = new int[7];
            int temp = 0;
            int Len = info.Length;
            for (int n = 0; n < Len; n++)
            {
                if (info[n] == '|')
                {
                    index[temp] = n;
                    temp++;
                }
            }

            loadID = info.Substring(0, index[0]);
            loadtitle = info.Substring(index[0] + 1, index[1] - index[0] - 1);
            loadartist = info.Substring(index[1] + 1, index[2] - index[1] - 1);
            loadauthor = info.Substring(index[2] + 1, index[3] - index[2] - 1);
            loadtype = info.Substring(index[3] + 1, index[4] - index[3] - 1);
            loadIP = info.Substring(index[4] + 1, index[5] - index[4] - 1);
            loadclientPort = info.Substring(index[5] + 1, index[6] - index[5] - 1);
        }

        // Connect toi server roi lam gi thi lam
        public void connect_Click(object sender, EventArgs e)
        {
            string ip = txtIp.Text;
            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("Please enter IP server");
                return;
            }
            client.Connect(IPAddress.Parse(txtIp.Text), 9999);
            if (!client.Connected)
            {
                MessageBox.Show("Connection failed");
                return;
            }

            statusclient.Items.Add("Da connect toi server.....");

            // initializing streams
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            // Sau khi connect toi server thi load lien danh sach bai hat toan mang
            connect.Enabled = false;
            open.Enabled = true;
            sharenhac.Enabled = true;
            refreshlistSong.Enabled = true;
            download.Enabled = true;
            //1. Gui port  len cho server            
            writer.WriteLine(ClientPort);

            this.listallsong.Columns.Add("No", 30, HorizontalAlignment.Left);
            this.listallsong.Columns.Add("Tile", 200, HorizontalAlignment.Left);
            this.listallsong.Columns.Add("Artist", 100, HorizontalAlignment.Left);
            this.listallsong.Columns.Add("Author", 100, HorizontalAlignment.Left);
            this.listallsong.Columns.Add("Type", 50, HorizontalAlignment.Left);
            this.listallsong.Columns.Add("ID", 30, HorizontalAlignment.Left);
            this.listallsong.Columns.Add("IP", 60, HorizontalAlignment.Left);
            this.listallsong.Columns.Add("ClientPort", 60, HorizontalAlignment.Left);
            this.listallsong.CheckBoxes = true;

            this.listallsong.View = View.Details;
            LoadListSong();
        }

        private void open_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Mp3 Files|*.Mp3|Wma Files|*.wma";
            //openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.listView1.Columns.Add("No", 30, HorizontalAlignment.Center);
                this.listView1.Columns.Add("Path", 200, HorizontalAlignment.Left);
                this.listView1.Columns.Add("Name", 200, HorizontalAlignment.Left);
                listView1.FullRowSelect = true;
                listView1.View = View.Details;
                ListViewItem item1;
                // Chi cho share moi lan 1 bai thoi
                string name = openFileDialog1.FileName;
                if (name != null)
                    open.Enabled = false;

                //                 FileInfo info = new FileInfo(name);
                //                 type = info.Extension;
                //                 title = info.;
                //                 pathfile = info.FullName;

                int n = name.Length;
                //////// type
                type = "" + name[n - 3] + name[n - 2] + name[n - 1];
                int i = n - 1;
                while (name[i] != '\\')
                {
                    title = "" + name[i] + title;
                    i--;
                }

                title = title.Substring(0, title.Length - 4);
                //  pathfile = name.Substring(0, i + 1);
                pathfile = name;




                ///Show ra listview
                No++;
                item1 = new ListViewItem(No.ToString());
                item1.SubItems.Add(pathfile);
                item1.SubItems.Add(title);
                listView1.Items.Add(item1);

                ////////////////////
                //Ghi vo XML de luu local
                if (!File.Exists("../../clientXML.xml"))
                {
                    XmlTextWriter textWritter = new XmlTextWriter("../../clientXML.xml", null);
                    textWritter.WriteStartDocument();
                    textWritter.WriteStartElement("LISTSONG");
                    textWritter.WriteEndElement();

                    textWritter.Close();
                }

                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load("../../clientXML.xml");

                XmlElement subRoot = xmlDoc.CreateElement("Song");
                // ID
                XmlElement appendedElementID = xmlDoc.CreateElement("ID");
                XmlText xmlTextID = xmlDoc.CreateTextNode(No.ToString());
                appendedElementID.AppendChild(xmlTextID);
                subRoot.AppendChild(appendedElementID);
                xmlDoc.DocumentElement.AppendChild(subRoot);
                //Path
                XmlElement appendedElementPath = xmlDoc.CreateElement("Path");
                XmlText xmlTextPath = xmlDoc.CreateTextNode(pathfile);
                appendedElementPath.AppendChild(xmlTextPath);
                subRoot.AppendChild(appendedElementPath);
                xmlDoc.DocumentElement.AppendChild(subRoot);

                xmlDoc.Save("../../clientXML.xml");
            }
        }



        private void sharenhac_Click(object sender, EventArgs e)
        {
            // Neu chua chon duong dan toi bai hat thi thong bao keu chon
            if (string.IsNullOrEmpty(pathfile))
                MessageBox.Show("Please choose the path file!");
            // Gui thong tin bai hat di           
            else
            {
                writer.WriteLine("S"); // thong bao voi server la mun share
                // Neu khong nhap ten ca si va nhac si coi nhu la "Unknow"
                if (artist_textbox.Text == "") artist_textbox.Text = "Unknown";
                if (author_textbox.Text == "") author_textbox.Text = "Unknown";

                string songinfo = No.ToString() + '|' + title + '|' + artist_textbox.Text + '|' + author_textbox.Text + '|' + type + '|';


                try
                {
                    writer.WriteLine(songinfo);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

                statusclient.Items.Add("Shared " + title);
                /// Sao khi gui bai hat mun share di thi mo tieu trinh tao P2Pserver cho ket noi
                // Tao môt thread cho nay đê nhận kết noi tu client khác
                if (ListenClientX == false) // neu chua mo socket dung cho
                {
                    ListenClientX = true; // Set no ve true-> click vo nut share la khong mo them tieu trinh nua
                    // Kick off a new thread
                    thP2PSetup = new Thread(P2PServer);
                    thP2PSetup.Start();

                }

                // Set nhung gia tri nay ve rong de phuc vu lan share sau
                title = "";
                type = "";
                pathfile = "";
                artist_textbox.Text = "";
                author_textbox.Text = "";
                open.Enabled = true;
            }
        }


        private void refreshlistSong_Click_1(object sender, EventArgs e)
        {
            listallsong.Items.Clear();
            writer.WriteLine("R");

            stream.Flush();
            LoadListSong();
        }

        /// <summary>
        /// ////////// thiet lap P2PServer
        /// </summary>
        private void P2PServer()
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");

            TcpListener listener = new TcpListener(address, ClientPort);
            listener.Start();

            // loop for accept download connections
            while (_downloadConnectionCount < MAX_CONNECTION || MAX_CONNECTION == 0)
            {
                Socket socketX = listener.AcceptSocket();
                _downloadConnectionCount++;

                // With a connection, initialize a thread to do DoWork method
                Thread t = new Thread((obj) =>
                {
                    Sendfile((Socket)obj);
                }
                );
                t.Start(socketX);
            }
        }
        /// <summary>
        /// Tim trong xml show ra path
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>

        private string LookupPathinXML(string ID)
        {
            string pathResult = ""; // Giu path tim duoc
            string fileName = "../../clientXML.xml";
            XPathDocument doc = new XPathDocument(fileName);
            XPathNavigator nav = doc.CreateNavigator();

            // Compile a standard XPath expression
            XPathExpression expr;
            string key = "/LISTSONG/Song/Path[../ID = " + ID + ']';
            expr = nav.Compile(key);
            XPathNodeIterator iterator = nav.Select(expr);

            // Iterate on the node set
            while (iterator.MoveNext())
            {
                XPathNavigator nav2 = iterator.Current.Clone();
                pathResult = nav2.Value;
            }
            //  statusclient.Items.Add(pathResult);
            return pathResult;
        }


        /// <summary>
        /// /// Gui file di
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="PathofSharedFile"></param>
        private void Sendfile(Socket socket)
        {
            NetworkStream streamSendFile = new NetworkStream(socket);
            StreamReader readerSendFile = new StreamReader(streamSendFile);
            StreamWriter writerSendFile = new StreamWriter(streamSendFile);
            writerSendFile.AutoFlush = true;

            string IDofSharedSong = readerSendFile.ReadLine();
            string PathofSharedFile = LookupPathinXML(IDofSharedSong);
            PathofSharedFile = PathofSharedFile.Replace('\\', '/');

            FileInfo info = new FileInfo(PathofSharedFile);
            long len = info.Length;

            writerSendFile.WriteLine(len.ToString());
            //Mo stream doc file mp3
            Stream Readmp3 = File.OpenRead(PathofSharedFile);

            const int buffersize = 1024;
            byte[] buffer = new Byte[buffersize];
            int bytesRead;
            int offset = 0;
            int seek = 0;
            int total = 0;
            while ((bytesRead = Readmp3.Read(buffer, offset, buffersize)) > 0)
            {
                if (SocketConnected(socket) == false)
                {
                    break;
                }
                total += bytesRead;
                int n = socket.Send(buffer, bytesRead, SocketFlags.None);
                string res = readerSendFile.ReadLine();
                if (res.Equals("pause"))
                {
                    string wait = readerSendFile.ReadLine();
                    if (wait.Equals("resume"))
                    {
                        wait = readerSendFile.ReadLine(); // nhan vi tri byte can goi tiep
                        seek = int.Parse(wait);
                        Readmp3.Seek(seek, SeekOrigin.Begin);
                        continue;
                    }
                    else if (wait.Equals("stop"))
                    {
                        break;
                    }
                }
            }

            Readmp3.Close();
            streamSendFile.Close();
            readerSendFile.Close();
            writerSendFile.Close();
        }

        private void download_Click(object sender, EventArgs e)
        {
            ////1.Lay duoc IP + ID cua bai hat tu tren list
            //string getIP = "";
            //string getID = "";
            //string getClientPort = "";
            //int count = 0;
            //foreach (ListViewItem itemchecked in listallsong.CheckedItems)
            //{
            //    count++;
            //}

            //if (count != 1)
            //{
            //    MessageBox.Show("Choose just a song!");
            //}
            //else
            //{
            //    foreach (ListViewItem itemchecked in listallsong.CheckedItems)
            //    {
            //        getIP = itemchecked.SubItems[6].Text;
            //        // statusclient.Items.Add(getIP);
            //        getID = itemchecked.SubItems[5].Text;
            //        //statusclient.Items.Add(getID);
            //        getClientPort = itemchecked.SubItems[7].Text;

            //    }
            //}
            //// this.IsMdiContainer = true;
            //DownloadForm FDownload = new DownloadForm();
            //FDownload.getIP(getIP);
            //FDownload.getID(getID);
            //FDownload.getClientPort(getClientPort);
            ////  FDownload.MdiParent = this;
            //FDownload.Show();

            //1.Lay duoc IP + ID cua bai hat tu tren list
            string getIP = "";
            string getID = "";
            string getClientPort = "";
            int count = 0;

            foreach (ListViewItem itemchecked in listallsong.CheckedItems)
            {
                getIP = itemchecked.SubItems[6].Text;
                // statusclient.Items.Add(getIP);
                getID = itemchecked.SubItems[5].Text;
                //statusclient.Items.Add(getID);
                getClientPort = itemchecked.SubItems[7].Text;

                // this.IsMdiContainer = true;
                DownloadForm FDownload = new DownloadForm();
                FDownload.getIP(getIP);
                FDownload.getID(getID);
                FDownload.getClientPort(getClientPort);
                //  FDownload.MdiParent = this;
                FDownload.Show();
            }
        }

        /// khi tat form thi tat tieu trinh
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thP2PSetup != null)
                thP2PSetup.Abort();
            if (stream != null)
                stream.Close();
            if (reader != null)
                reader.Close();
            if (writer != null)
                writer.Close();
        }

        private void search_Click(object sender, EventArgs e)
        {
            keysearch = textsearch.Text;
            if (keysearch == "")
            {
                MessageBox.Show("Go vao tu khoa muon tim!");
            }
            else
            {
                writer.WriteLine("search");
                if (typesearch == "Title")
                {
                    writer.WriteLine("T" + keysearch);
                }
                else
                {
                    writer.WriteLine("A" + keysearch);
                }
            }

            listallsong.Items.Clear();
            LoadListSong();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            typesearch = comboBox1.SelectedItem.ToString();
        }


        /// <summary>
        ///  Xu li trayicon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigServer config = new ConfigServer();
            config.ShowDialog();
        }


        // detect socket disconnected
        private bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 & part2)
                return false;
            else
                return true;
        }

    }
}