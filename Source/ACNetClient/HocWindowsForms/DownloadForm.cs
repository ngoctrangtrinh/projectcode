using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ACNetClient
{
    public partial class DownloadForm : Form
    {
        Int64 startByte = 0;
        TcpClient clientX = new TcpClient();

        Stream stream = null;
        string IPServerX;
        int PortServerX;
        string IDDownloadedSong;
        private Stream writemp3 = null;
        private StreamWriter writer = null;
        private StreamReader reader = null;
        private Thread thrDownload;
        private delegate void UpdateProgessCallback(Int64 BytesRead, Int64 TotalBytes);
        Int64 filesize;
        Int64 byteReceive = 0;
        string pathsavefile = "";
        bool isPauseClicked = false;
        private bool downloadComplete = false;


        // The delegate which we will call from the thread to update the form
        public DownloadForm()
        {
            InitializeComponent();
            pause.Enabled = false;
            resume.Enabled = false;
        }

        public void getIP(string IP)
        {
            IPServerX = IP;
        }

        public void getID(string ID)
        {
            IDDownloadedSong = ID;
        }

        public void getClientPort(string clientPort)
        {
            PortServerX = Convert.ToInt32(clientPort);
        }
        private void download_Click(object sender, EventArgs e)
        {
            // disable resume button and itself
            resume.Enabled = false;
            download.Enabled = false;

            if (pathsavefile == "")
            {
                download.Enabled = true;
                MessageBox.Show("Fill the pathfile to save file!");
                return;
            }
            else
            {
                if (thrDownload != null && thrDownload.ThreadState == ThreadState.Running)
                    MessageBox.Show("Your download is already running...");
                else
                {
                    //Tao ket noi truoc
                    listView1.Items.Add(IPServerX);
                    listView1.Items.Add(IDDownloadedSong);
                    IPAddress addressX = IPAddress.Parse(IPServerX);
                    clientX.Connect(addressX, PortServerX);
                    stream = clientX.GetStream();

                    writer = new StreamWriter(stream);
                    reader = new StreamReader(stream);
                    writer.AutoFlush = true;
                    writer.WriteLine(IDDownloadedSong);
                    string strfilesize = reader.ReadLine();
                    // Lay duoc size

                    filesize = Int64.Parse(strfilesize);


                    pause.Enabled = true;
                    label1.Text = "Download is starting...";
                    thrDownload = new Thread(DownloadFile);
                    thrDownload.Start();
                }

            }
        }


        private void DownloadFile()
        {
            try
            {
                stream.Flush();

                if (startByte == 0)
                {
                    writemp3 = new FileStream(pathsavefile, FileMode.Create, FileAccess.Write, FileShare.None);
                }
                else
                {
                    writemp3 = new FileStream(pathsavefile, FileMode.Append, FileAccess.Write, FileShare.None);
                }



                const int buffersize = 1024;
                byte[] buffer = new Byte[buffersize];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffersize)) > 0)
                {

                    writemp3.Write(buffer, 0, bytesRead);
                    if (isPauseClicked == false)
                    {
                        writer.WriteLine("continue");
                    }
                    stream.Flush();

                    byteReceive = byteReceive + bytesRead;

                    this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { byteReceive, filesize });
                }

                // update download status
                downloadComplete = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                stream.Close();
                writemp3.Close();
            }
        }


        private void UpdateProgress(Int64 BytesRead, Int64 TotalBytes)
        {
            // Calculate the download progress in percentages
            int PercentProgress = Convert.ToInt32((BytesRead * 100) / TotalBytes);
            //    Make progress on the progress bar
            progressBar1.Value = PercentProgress;
            //    Display the current progress on the form
            label1.Text = "Downloaded " + BytesRead + " out of " + TotalBytes + " (" + PercentProgress + "%)";
        }


        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thrDownload != null)
            {
                thrDownload.Abort();
            }
            // close all streams
            if (stream != null)
            {
                stream.Close();
                reader.Close();
                writer.Close();
            }
            progressBar1.Value = 0;
        }



        private void stop_Click(object sender, EventArgs e)
        {
            if (!downloadComplete)
            {
                writer.WriteLine("stop");
                thrDownload.Abort();
                // Close the web response and the streams
                stream.Close();
                writemp3.Close();
                // Set the progress bar back to 0 and the label
                progressBar1.Value = 0;
                textBox1.Text = "Download Stopped";
                // Disable the Pause/Resume button because the download has ended
                this.Close();
            }
        }

        private void pause_Click(object sender, EventArgs e)
        {
            isPauseClicked = true;
            resume.Enabled = true;
            pause.Enabled = false;

            writer.WriteLine("pause");
            label1.Text = "Paused download...";

        }

        private void resume_Click(object sender, EventArgs e)
        {
            isPauseClicked = false;
            writer.WriteLine("resume");
            writer.WriteLine(byteReceive);
            label1.Text = "Resume download...";
            pause.Enabled = true;
        }

        private void browse_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "*.mp3";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pathsavefile = saveFileDialog1.FileName;
                textBox1.Text = saveFileDialog1.FileName;
            }
        }
    }
}