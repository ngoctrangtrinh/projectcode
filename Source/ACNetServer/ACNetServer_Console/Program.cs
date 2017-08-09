//// Trinh Thi Ngoc Trang 0912597

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.Xml.XPath;

namespace Socket_Server
{
    class Server
    {
        #region Attibutes

        static string ID;
        static string IP;
        static string title;
        static string artist;
        static string author;
        static string type;
        const int MAX_CONNECTION = 100;
        const int PORT_NUMBER = 9999;
        static int connectionCount = 0; // Initialize the number of connection
        static TcpListener listerner;

        static int[] index = new int[5];
        #endregion

        static void Main(string[] args)
        {
            FileInfo info = new FileInfo("../../serverXML.xml");
            if (info.Exists)
            {
                info.Delete();
            }
            IPAddress address = IPAddress.Parse("127.0.0.1");
            listerner = new TcpListener(address, PORT_NUMBER);

            Console.WriteLine("Server waiting for connection...");
            listerner.Start();

            // loop for accept connections
            while (connectionCount < MAX_CONNECTION || MAX_CONNECTION == 0)
            {
                Socket socket = listerner.AcceptSocket();
                connectionCount++;

                // With a connection, initialize a thread to do DoWork method
                Thread t = new Thread((obj) =>
                {
                    DoWork((Socket)obj);
                }
                );
                t.Start(socket);


            }
        }

        // detect socket disconnected
        private static bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 & part2)
                return false;
            else
                return true;
        }

        private static void DoWork(Socket socket)
        {
            Console.WriteLine("Connection received from : {0}", socket.RemoteEndPoint);
            string clientPort = null;
            try
            {
                var stream = new NetworkStream(socket);
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);
                writer.AutoFlush = true;

                //1. NHan port cua client
                clientPort = reader.ReadLine();


                #region send songs list to client
                //1.Tao xmlserver load ve danh sach rong cho client dau tien connect vao
                if (!File.Exists("../../serverXML.xml"))
                {

                    XmlTextWriter textWritter = new XmlTextWriter("../../serverXML.xml", null);
                    textWritter.WriteStartDocument();
                    textWritter.WriteStartElement("LISTSONG_SERVER");
                    textWritter.WriteEndElement();

                    textWritter.Close();
                }

                // 2. Load ds bai hat ve cho client
                LoadlistSongtoClient(writer);
                #endregion

                //3. chay vong lap nhan yeu cau cua client--moi yeu cau thi goi toi mot ham
                while (true)
                {
                    if (SocketConnected(socket) == false)
                    {
                        break;
                    }

                    string clientRequest = reader.ReadLine();
                    Console.WriteLine(clientRequest);
                    switch (clientRequest)
                    {
                        case "R":
                            LoadlistSongtoClient(writer);
                            break;
                        case "S":
                            getSonginfo(socket, clientPort, reader);
                            break;
                        case "search":
                            SelectSong(reader, writer);
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            RemoveSongNode(clientPort);
            Console.WriteLine("Client disconnected ");
            connectionCount--;

            socket.Close();
        }

        public static void SelectSong(StreamReader reader, StreamWriter writer)
        {
            string fullkeySearch = reader.ReadLine();
            Console.WriteLine(fullkeySearch);
            string keySearch = fullkeySearch.Substring(1, fullkeySearch.Length - 1);
            Console.WriteLine(keySearch);
            string fileName = "../../serverXML.xml";

            XPathDocument doc = new XPathDocument(fileName);
            XPathNavigator nav = doc.CreateNavigator();

            // Compile a standard XPath expression
            string key = "";
            XPathExpression expr;
            if (fullkeySearch[0] == 'T')
                key = "/LISTSONG_SERVER/Song/ID[../Title =\"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/Title[../Title =\"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/Artist[../Title = \"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/Author[../Title = \"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/Type[../Title = \"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/IP[../Title = \"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/ClientPort[../Title = \"" + keySearch + "\"]";

            if (fullkeySearch[0] == 'A')
                key = "/LISTSONG_SERVER/Song/ID[../Artist =\"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/Title[../Artist =\"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/Artist[../Artist = \"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/Author[../Artist = \"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/Type[../Artist = \"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/IP[../Artist = \"" + keySearch + "\"]" + '|' + "/LISTSONG_SERVER/Song/ClientPort[../Title = \"" + keySearch + "\"]";

            expr = nav.Compile(key);
            XPathNodeIterator iterator = nav.Select(expr);

            StringBuilder rs = new StringBuilder();

            // Iterate on the node set
            int count = 0;
            string result2client = "";
            while (iterator.MoveNext())
            {
                XPathNavigator nav2 = iterator.Current.Clone();
                result2client = result2client + nav2.Value.ToString() + '|';
                count++;
                if (count == 7)
                {
                    writer.WriteLine(result2client);
                    count = 0;
                    result2client = "";
                }
            }
            writer.WriteLine('|');
        }

        ///3.Nhan thông tin share bai hat tư client -- lưu vào xml 
        public static void getSonginfo(Socket socket, string clientPort, StreamReader reader)
        {
            string str = reader.ReadLine();
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Nhan chuoi rong!!");
                return;
            }
            Console.WriteLine(str);

            //////1. cat chuoi truoc roi tính
            int temp = 0;
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j] == '|')
                {
                    index[temp] = j;
                    temp++;
                }
            }

            ID = str.Substring(0, index[0]);
            title = str.Substring(index[0] + 1, index[1] - index[0] - 1);
            artist = str.Substring(index[1] + 1, index[2] - index[1] - 1);
            author = str.Substring(index[2] + 1, index[3] - index[2] - 1);
            type = str.Substring(index[3] + 1, index[4] - index[3] - 1);

            IPEndPoint remoteIpEndPoint = socket.RemoteEndPoint as IPEndPoint;
            IP = remoteIpEndPoint.Address.ToString();


            ///// 4.luu vo XML ///////
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load("../../serverXML.xml");

            XmlElement subRoot = xmlDoc.CreateElement("Song");
            // ID
            XmlElement appendedElementID = xmlDoc.CreateElement("ID");
            XmlText xmlTextID = xmlDoc.CreateTextNode(ID);
            appendedElementID.AppendChild(xmlTextID);
            subRoot.AppendChild(appendedElementID);
            xmlDoc.DocumentElement.AppendChild(subRoot);
            //title
            XmlElement appendedElementPath = xmlDoc.CreateElement("Title");
            XmlText xmlTextPath = xmlDoc.CreateTextNode(title.Trim());
            appendedElementPath.AppendChild(xmlTextPath);
            subRoot.AppendChild(appendedElementPath);
            xmlDoc.DocumentElement.AppendChild(subRoot);
            // artist
            XmlElement appendedElementArtist = xmlDoc.CreateElement("Artist");
            XmlText xmlTextArtist = xmlDoc.CreateTextNode(artist.Trim());
            appendedElementArtist.AppendChild(xmlTextArtist);
            subRoot.AppendChild(appendedElementArtist);
            xmlDoc.DocumentElement.AppendChild(subRoot);
            //author
            XmlElement appendedElementAuthor = xmlDoc.CreateElement("Author");
            XmlText xmlTextAuthor = xmlDoc.CreateTextNode(author.Trim());
            appendedElementAuthor.AppendChild(xmlTextAuthor);
            subRoot.AppendChild(appendedElementAuthor);
            xmlDoc.DocumentElement.AppendChild(subRoot);
            //type
            XmlElement appendedElementType = xmlDoc.CreateElement("Type");
            XmlText xmlTextType = xmlDoc.CreateTextNode(type);
            appendedElementType.AppendChild(xmlTextType);
            subRoot.AppendChild(appendedElementType);
            xmlDoc.DocumentElement.AppendChild(subRoot);

            // IP
            XmlElement appendedElementIP = xmlDoc.CreateElement("IP");
            XmlText xmlTexIP = xmlDoc.CreateTextNode(IP);
            appendedElementIP.AppendChild(xmlTexIP);
            subRoot.AppendChild(appendedElementIP);
            xmlDoc.DocumentElement.AppendChild(subRoot);

            //    clientID
            XmlElement appendedElementclientID = xmlDoc.CreateElement("ClientPort");
            XmlText xmlTexclientID = xmlDoc.CreateTextNode(clientPort);
            appendedElementclientID.AppendChild(xmlTexclientID);
            subRoot.AppendChild(appendedElementclientID);
            xmlDoc.DocumentElement.AppendChild(subRoot);


            xmlDoc.Save("../../serverXML.xml");
        }


        public static void LoadlistSongtoClient(StreamWriter writer)
        {
            int i = 0;
            string s = null;


            XmlTextReader Xmlreader = new XmlTextReader("../../serverXML.xml");
            while (Xmlreader.Read())
            {
                if (Xmlreader.NodeType == XmlNodeType.Text)
                {
                    i++;
                    s = s + Xmlreader.Value.Trim() + "|";
                    if (i == 7)
                    {
                        writer.WriteLine(s);
                        Console.WriteLine(s);

                        s = null;
                        i = 0;
                    }
                }
            }
            Xmlreader.Close();
            // using for stop transfering
            writer.WriteLine('|');
        }



        private static void RemoveSongNode(string clientPort)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("../../serverXML.xml");


            XmlNode nodeDel;
            nodeDel = xmldoc.SelectSingleNode("/LISTSONG_SERVER/Song[ClientPort= \"" + clientPort + "\"]");

            while (nodeDel != null)
            {
                nodeDel.ParentNode.RemoveChild(nodeDel);
                nodeDel = xmldoc.SelectSingleNode("/LISTSONG_SERVER/Song[ClientPort= \"" + clientPort + "\"]");
            }

            xmldoc.Save("../../serverXML.xml");
        }
    }
}