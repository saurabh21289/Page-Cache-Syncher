using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Chat_APP
{
    public partial class Form1 : Form
    {
        Socket sck;
        EndPoint epLocal, epRemote;
        ConcurrentDictionary<string, int> myDictionary = new ConcurrentDictionary<string, int>();
        ConcurrentDictionary<string, int> exDictionary = new ConcurrentDictionary<string, int>();
        ConcurrentDictionary<string,int> cacheDictionary = new ConcurrentDictionary<string,int>();
        bool synced = false;

        public Form1()
        {
            InitializeComponent();
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            textLocalIP.Text = GetLocalIP();
            textFriendIP.Text = GetLocalIP();
        }

        private string GetLocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }

        private void MessageCallBack(IAsyncResult aResult)
        {
            try
            {
                int size = sck.EndReceiveFrom(aResult,ref epRemote);
                if(size>0)
                {
                    byte[] receivedData = new byte[1500];
                    receivedData = (byte[])aResult.AsyncState;
                    ASCIIEncoding eEncoding = new ASCIIEncoding();
                    string receivedMessage = eEncoding.GetString(receivedData);
                    listDataEX.Items.Add(receivedMessage.TrimEnd());
                    string[] data = receivedMessage.Split('\t');
                    string data1= data[0];
                    string data2 = data[1].TrimEnd('\0');
                    exDictionary.TryAdd(data1, Int32.Parse(data2));
                }
                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        //send button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] msg = new byte[15000];
                int countr = 0;
                foreach(KeyValuePair<string, int> kvp in myDictionary)
                {
                    countr++;
                    msg = enc.GetBytes(kvp.Key + "\t" + kvp.Value);
                    sck.Send(msg);
                    button2.Text = "Data Sent";
                    button2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //start connection button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                epLocal = new IPEndPoint(IPAddress.Parse(textLocalIP.Text), Convert.ToInt32(textLocalPort.Text));
                sck.Bind(epLocal);
                epRemote = new IPEndPoint(IPAddress.Parse(textFriendIP.Text),Convert.ToInt32(textFriendPort.Text));
                sck.Connect(epRemote);
                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
                button1.Enabled = false;
                button1.Text = "Connected";
                button2.Enabled = true;
                textMessage.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //load local data button
        private void button3_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\Users\\DAV\\Desktop\\Spring 2016\\ec504\\Project\\PageCache\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textMessage.Text = openFileDialog1.FileName;
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        string path = openFileDialog1.FileName;
                        string[] myData = File.ReadAllLines(path);
                        foreach (string line in myData)
                        {
                            string[] fields = line.Split('\t');
                            string theWord = fields[0];
                            string theInt = fields[1];
                            int theNumber = Convert.ToInt32(theInt);
                            myDictionary.TryAdd(theWord, theNumber);
                           
                        }   
                        foreach (KeyValuePair<string, int> kvp in myDictionary)
                        {
                            listMessage.Items.Add(kvp.Key + "\t" + kvp.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        //initital sync button
        private void button4_Click(object sender, EventArgs e)
        {
            button4.Text = "Sync";
            listUnique.Items.Add("Unique search terms in imported data: ");
            int count1 = 0;
            int count2 = 0;
            foreach (KeyValuePair<string, int> line in exDictionary)
            {
                string theTerm = line.Key;
                int theValue;
                if (myDictionary.TryGetValue(theTerm, out theValue))
                {
                    myDictionary[theTerm] = theValue + line.Value;
                    count1++;
                }
                else
                {
                    myDictionary.TryAdd(theTerm, line.Value);
                    count2++;
                    listUnique.Items.Add(theTerm);
                    listMessage.Items.Add(theTerm + "\t" + line.Value);
                }
            }
            listUnique.Items.Add("Number of identical search terms: " + count1);
            listUnique.Items.Add("Number of unique search terms in imported data: " + count2);

            if(!synced)
            {
                foreach (KeyValuePair<string, int> kvp in exDictionary)
                {
                    File.AppendAllText("newSyncedData.txt", (kvp.Key + "\t" + kvp.Value + Environment.NewLine));

                }
                synced = true;
            }
            exDictionary.Clear();
        }


        //search button
        private void searchButton_Click(object sender, EventArgs e)
        {
            string theTerm = textSearch.Text;
            int theValue;
            if (myDictionary.TryGetValue(theTerm,out theValue))
            {
                theValue = myDictionary[theTerm]++;
                listUnique.Items.Add("Search term: " + theTerm + " found in current data, searched " + theValue + " times");
            }
            else
            {
                myDictionary.TryAdd(theTerm, 1);
                cacheDictionary.TryAdd(theTerm, 1);
                listUnique.Items.Add("Not found, will be added.");
                listMessage.Items.Add(theTerm);
                listMessage.Items.Add(myDictionary.Count());
            }

            if(timeToSync())
            {
                syncCache();
            }
        }

        //function: when its time to sync cache
        private bool timeToSync()
        {
            //sync data on every three new search terms
            if(cacheDictionary.Count == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void syncCache()
        {
             try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] msg = new byte[1500];
                foreach(KeyValuePair<string, int> kvp in cacheDictionary)
                {
                    msg = enc.GetBytes(kvp.Key + "\t" + kvp.Value);
                    
                    sck.Send(msg);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
             cacheDictionary.Clear();
        }
        

        //Function: This is for the suggestions, still have to build a suffix tree for it.
       private void myAutoComplete()
        {
            AutoCompleteStringCollection suggest = new AutoCompleteStringCollection();
            List<string> suggester = new List<string>(3);
            foreach (KeyValuePair<string, int> k in myDictionary.OrderBy(k => k.Value))
            {
                suggest.Add(k.Key);
            }
            textSearch.AutoCompleteCustomSource = suggest;
        }

        //Checkbox to turn on suggestions
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool has = false;
            if(checkBox1.Checked)
            {
                if(!has)
                {
                    myAutoComplete();
                }
                textSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
                has = true;
            }
            else
            {
                textSearch.AutoCompleteMode = AutoCompleteMode.None;
            }
        }

       //change the behaviour of the suggestions so it only shows top 4 searched terms
        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                //TODO: Still have to build a suffix trie for this.
            }
        }

        //Clear search bar button
        private void clearButton_Click(object sender, EventArgs e)
        {
            textSearch.Clear();
        }

        //Clear console button
        private void clearConsole_Click(object sender, EventArgs e)
        {
            listUnique.Items.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

      
    }

  
}
