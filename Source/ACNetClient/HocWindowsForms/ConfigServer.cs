using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace ACNetClient
{
    public partial class ConfigServer : Form
    {
        private string ip;
        private int port;

        public ConfigServer()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            ip = ipserver.Text.Trim();
            port = int.Parse(portserver.Text.Trim());

            // luu cau hinh vao xml
            XmlTextWriter writer = new XmlTextWriter("../../config.xml", null);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Config");

            writer.WriteElementString("ip", ip);
            writer.WriteElementString("port", port.ToString());

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
            this.Close();
        }
    }
}
