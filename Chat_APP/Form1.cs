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
using Gma.DataStructures.StringSearch;
using NUnit.Framework;
using System.IO.Compression;

namespace Chat_APP
{
    public partial class Form1 : Form
    {
        Socket sck, sck2;
        EndPoint epLocal, epRemote;
        EndPoint epLocal2, epRemote2; // 2 more ports per DC
        ConcurrentDictionary<string, int> myDictionary = new ConcurrentDictionary<string, int>();
        ConcurrentDictionary<string, int> exDictionary = new ConcurrentDictionary<string, int>();
        ConcurrentDictionary<string,int> cacheDictionary = new ConcurrentDictionary<string,int>();
        PatriciaTrie<string> myTrie = new PatriciaTrie<string>();
        int firstWords = 0;
        int sharedWords = 0;
        int syncedWords = 0;
        int newWords = 0;
        
        
        public Form1()
        {
            InitializeComponent();
            button1.Focus();
            //sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//using stream
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            sck2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); //another socket per DC
            sck2.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            textLocalIP.Text = GetLocalIP();
            textFriendIP.Text = GetLocalIP();
            textFriend2IP.Text = GetLocalIP();


            timer1.Enabled = true;
            timer1.Start();
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
                //int size2 = sck2.EndReceiveFrom(aResult, ref epRemote2); //receiving msgs from DC 3
                if(size>0)
                {
                    byte[] receivedData = new byte[1500]; //1500
                    receivedData = (byte[])aResult.AsyncState;


                    //byte[] decompressedBytes = new byte[receivedData.Length];
                    //using (MemoryStream fileToDecompress = new MemoryStream())
                    //{
                    //    using (GZipStream decompressionStream = new GZipStream(fileToDecompress, CompressionMode.Decompress))
                    //    {
                    //        decompressionStream.Read(decompressedBytes, 0, receivedData.Length);
                    //    }
                    //}


                    ASCIIEncoding eEncoding = new ASCIIEncoding();
                    //string receivedMessage = eEncoding.GetString(receivedData);
                    string receivedMessage = Unzip(receivedData);
                    
                    listDataEX.Items.Add(receivedMessage.TrimEnd());
                    string[] data = receivedMessage.Split('\t');          //comment out if using tcp socket
                    string data1 = data[0];                                //comment out if using tcp socket
                    string data2 = data[1].TrimEnd('\0');                   //comment out if using tcp socket
                    exDictionary.TryAdd(data1, Int32.Parse(data2));         //comment out if using tcp socket
                    //if using tcp socket try: exDictionary.TryAdd(data 1,TryParse(data2,out int something))
                }

               
    

                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
                //sck2.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote2, new AsyncCallback(MessageCallBack), buffer);
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        //Implementing 2nd Message Callback function for DC 3

        private void MessageCallBack2(IAsyncResult aResult)
        {
            try
            {
                int size = sck2.EndReceiveFrom(aResult, ref epRemote2);
                //int size2 = sck2.EndReceiveFrom(aResult, ref epRemote2); //receiving msgs from DC 3
                if (size > 0)
                {


                    byte[] receivedData = new byte[1500]; //1500
                    receivedData = (byte[])aResult.AsyncState;

                    //byte[] decompressedBytes = new byte[receivedData.Length];
                    //using (MemoryStream fileToDecompress = new MemoryStream())
                    //{
                    //    using (GZipStream decompressionStream = new GZipStream(fileToDecompress, CompressionMode.Decompress))
                    //    {
                    //        decompressionStream.Read(decompressedBytes, 0, receivedData.Length);
                    //    }
                    //}

                    ASCIIEncoding eEncoding = new ASCIIEncoding();
                    //string receivedMessage = eEncoding.GetString(receivedData);
                    string receivedMessage = Unzip(receivedData);

                    listDataEX.Items.Add(receivedMessage.TrimEnd());
                    string[] data = receivedMessage.Split('\t');          //comment out if using tcp socket
                    string data1 = data[0];                                //comment out if using tcp socket
                    string data2 = data[1].TrimEnd('\0');                   //comment out if using tcp socket
                    exDictionary.TryAdd(data1, Int32.Parse(data2));         //comment out if using tcp socket
                    //if using tcp socket try: exDictionary.TryAdd(data 1,TryParse(data2,out int something))
                }

                //if (size2 > 0)
                //{
                //    byte[] receivedData = new byte[1500];
                //    receivedData = (byte[])aResult.AsyncState;
                //    ASCIIEncoding eEncoding = new ASCIIEncoding();
                //    string receivedMessage = eEncoding.GetString(receivedData);
                //    listDataEX.Items.Add(receivedMessage.TrimEnd());
                //    string[] data = receivedMessage.Split('\t');          //comment out if using tcp socket
                //    string data1 = data[0];                                //comment out if using tcp socket
                //    string data2 = data[1].TrimEnd('\0');                   //comment out if using tcp socket
                //    exDictionary.TryAdd(data1, Int32.Parse(data2));         //comment out if using tcp socket
                //    //if using tcp socket try: exDictionary.TryAdd(data 1,TryParse(data2,out int something))
                //}


                byte[] buffer = new byte[1500];
                //sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
                sck2.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote2, new AsyncCallback(MessageCallBack2), buffer);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        //append byte array function
        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        //COMPRESS STRING:
        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];
            int cnt;
            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }
                return mso.ToArray();
            }
        }
        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }
                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }




        //send button
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button2.Text = "Data Sent";
            //Compression variables
            UnicodeEncoding uniEncode = new UnicodeEncoding();
            byte[] bytesToCompress;
            int sendDataSize = 0;
            
            try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] msg = new byte[0];
                byte[] whole = new byte[0];
                

                int countr = 0;
                foreach (KeyValuePair<string, int> kvp in myDictionary)
                {
                    countr++;

                    msg = Zip(kvp.Key + "\t" + kvp.Value);
                    sendDataSize = sendDataSize + msg.Length;
                    sck.Send(msg);
                    sck2.Send(msg); //Sending data to DC 3

                    //whole = Combine(whole,msg);
                }
           
                    button2.Text = "Data Sent";
                    button2.Enabled = false;
                    //dataSize.Text = (sendDataSize).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //start connection button
        //DC 1 => 1 2 3 4
        //DC 2 => 1 2 5 6
        //DC 3 => 3 4 5 6
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {   
                // port configuration 
                //epLocal = new IPEndPoint(IPAddress.Parse(textLocalIP.Text), Convert.ToInt32(textLocalPort.Text));
                
                //For DC 1
                //epLocal = new IPEndPoint(IPAddress.Parse(textLocalIP.Text), 1);
                //epRemote = new IPEndPoint(IPAddress.Parse(textFriendIP.Text), 2);
                //epLocal2 = new IPEndPoint(IPAddress.Parse(textLocalIP.Text), 3);
                //epRemote2 = new IPEndPoint(IPAddress.Parse(textFriendIP.Text), 4);

                //For DC 2
                //epLocal = new IPEndPoint(IPAddress.Parse(textLocalIP.Text), 2);
                //epRemote = new IPEndPoint(IPAddress.Parse(textFriendIP.Text), 1);
                //epLocal2 = new IPEndPoint(IPAddress.Parse(textLocalIP.Text), 5);
                //epRemote2 = new IPEndPoint(IPAddress.Parse(textFriendIP.Text), 6);

                ////For DC 3
                epLocal = new IPEndPoint(IPAddress.Parse(textLocalIP.Text), 4);
                epRemote = new IPEndPoint(IPAddress.Parse(textFriendIP.Text), 3);
                epLocal2 = new IPEndPoint(IPAddress.Parse(textLocalIP.Text), 6);
                epRemote2 = new IPEndPoint(IPAddress.Parse(textFriendIP.Text), 5);

                sck.Bind(epLocal);
                sck2.Bind(epLocal2); //Bind port for DC 3
                //epRemote = new IPEndPoint(IPAddress.Parse(textFriendIP.Text),Convert.ToInt32(textFriendPort.Text));
                
                
                sck.Connect(epRemote);
                sck2.Connect(epRemote2); //Connect to DC 3

                byte[] buffer = new byte[8192];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
                sck2.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote2, new AsyncCallback(MessageCallBack2), buffer);
                button1.Enabled = false;
                button1.Text = "Connected";
                button2.Enabled = true;
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
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        long position = myStream.Position;
                        button3.Text = "Loading Data";
                        button3.Enabled = false;
                        string path = openFileDialog1.FileName;
                        string[] myData = File.ReadAllLines(path);
                        foreach (string line in myData)
                        {
                            string[] fields = line.Split('\t');
                            string theWord = fields[0];
                            string theInt = fields[1];
                            int theNumber = Convert.ToInt32(theInt);
                            myDictionary.TryAdd(theWord, theNumber);
                            firstWords++;

                            UpdateProgress(position, firstWords);
                           
                        }   
                        foreach (KeyValuePair<string, int> kvp in myDictionary)
                        {
                            listMessage.Items.Add(kvp.Key + "\t" + kvp.Value);
                            //storeData(kvp.Key, kvp.Value);
                            
                        }
                        treeBuilder();
                       
                    }
                    button3.Text = "Data Loaded";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        //progress bar
        private void UpdateProgress(long position, int firstWords)
        {
            progressBar1.Maximum = firstWords;
            progressBar1.Increment(1);
            //if(progressBar1.Value == progressBar1.Maximum)
            //    button3.Text = "Data Loaded.";
        }

        //sync button
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
                    syncedWords++;
                    myTrie.Add(theTerm, theTerm);

                }
            }
            listUnique.Items.Add("Number of identical search terms: " + count1);
            listUnique.Items.Add("Number of unique search terms in imported data: " + count2);

            sharedWords = sharedWords + exDictionary.Count;

            exDictionary.Clear();
            listDataEX.Items.Clear();
            listUnique.TopIndex = listUnique.Items.Count - 1;//always scroll to buttom of listBox
            button4.Enabled = false;

            listMessage.Items.Clear();
            foreach(KeyValuePair<string,int> kvp in myDictionary)
            {
                listMessage.Items.Add(kvp.Key + "\t" + kvp.Value);
            }
            

            //treeBuilder();
        }



        //search button
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (textSearch.Text.Length > 0)
            {
                string theTerm = textSearch.Text;
                int theValue;

                if (myDictionary.TryGetValue(theTerm, out theValue))
                {
                    theValue = myDictionary[theTerm]++;
                    listUnique.Items.Add("Search term: " + theTerm + " found in current data, searched by " + theValue);
                }
                else
                {
                    myDictionary.TryAdd(theTerm, 1);
                    cacheDictionary.TryAdd(theTerm, 1);
                    myTrie.Add(theTerm, theTerm);
                    listUnique.Items.Add("Not found, will be added.");
                    listMessage.Items.Add(theTerm + "\t" + 1);
                    newWords++;
                }

                if (timeToSync())
                {
                    syncCache();
                }
                listUnique.TopIndex = listUnique.Items.Count - 1;//always scroll to buttom of listBox
            }
        }

        //function: sync timer, syncs every 3 new search queries
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

        //automatic sync function, sends new search queries to other data centers
        private void syncCache()
        {
            if (button1.Text == "Connected")
            {
                try
                {
                    //System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                    byte[] msg = new byte[0];
                    foreach (KeyValuePair<string, int> kvp in cacheDictionary)
                    {
                        msg = Zip(kvp.Key + "\t" + kvp.Value);
                        sck.Send(msg);
                        sck2.Send(msg);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                cacheDictionary.Clear();
            }
        }
        

       //changing text in the search bar updates the suggestions
        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            textSearch.Focus();
            AutoCompleteStringCollection suggest = new AutoCompleteStringCollection();
            suggest.Clear();
            textSearch.AutoCompleteCustomSource = suggest;
            if ((checkBox1.Checked)&&(textSearch.Text.Length)>=1)
            {
                 
                IEnumerable<string> result = myTrie.Retrieve(textSearch.Text);
                List<string> myKeys = result.ToList();
                if(myKeys.Count !=0)
                {
                    List<int> myValues = new List<int>();
                    string maxKey;
                    foreach (string i in myKeys)
                    {
                        myValues.Add(myDictionary[i]);
                    }
                    for(int i = 0; (i<4)&&(i < myValues.Count);i++)
                    {
                        maxKey = (myKeys[myValues.IndexOf(myValues.Max())]);
                        myValues[myValues.IndexOf(myValues.Max())] = 0;
                        suggest.Add(maxKey);
                    }
                    
                    textSearch.AutoCompleteCustomSource = suggest;
                }

            }

        }

       

        //build suffix tree function
        public void treeBuilder()
        {
            foreach(KeyValuePair<string, int> kvp in myDictionary)
            {
                myTrie.Add(kvp.Key, kvp.Key);
            }

        }


        //add new data to our data text file
        public void storeData(string key,int value)
        {
            File.AppendAllText("newSyncedData.txt", (key + "\t" + value + Environment.NewLine));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if(exDictionary.Count !=0)
            {
                button4.Enabled = true;
                button4.FlatStyle = FlatStyle.Standard;
                Random rand = new Random();
                int A = rand.Next(0, 255);
                int R = rand.Next(0, 255);
                int G = rand.Next(0, 255);
                int B = rand.Next(0, 255);
                button4.BackColor = Color.FromArgb(A, R, G, B);
            }
            else
            {
                button4.FlatStyle = FlatStyle.System;
            }  
            
            
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            listUnique.Items.Clear();
            listUnique.Items.Add("Summary:");
            listUnique.Items.Add("Initial number of search terms stored: " + firstWords);
            listUnique.Items.Add("Total number of new search terms: " + newWords);
            listUnique.Items.Add("Total number of search terms received from other datacenters: "  + sharedWords);
            listUnique.Items.Add("Total number of unique search terms synced from other datacenters: " + syncedWords);
            

        }

        private void textSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                searchButton_Click(this, new EventArgs());
            }
        }

        private void storeButton_Click(object sender, EventArgs e)
        {
            foreach(KeyValuePair<string, int> kvp in myDictionary)
            {
                storeData(kvp.Key, kvp.Value);
            }
            //sck.Close();
            //sck2.Close();
            //this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


       

       

       
      
    }

  
}
