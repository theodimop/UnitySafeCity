using OpenDataScreen;
using scMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
//80 row of results in drop down string!

public class DropDownMenuHandle : MonoBehaviour {

    private int
sPort = 3000, // server port
pfrPort = 2999; // policy file request port

    private Socket
        cSock; // client socket

    public string
        ipAddress = "127.0.0.1";// server ip address

        public bool
            connectedToServer = false;

    private List<message>
        incMessages = new List<message>();

    private static DropDownMenuHandle instance;
    public static bool isLethalRequest;

    public Text textCounty, textMonth, textYear, textAccidentType,textInfoCounty,textInfoDate,textInfoTACC, contentText;

    public Button nextPage;
    public message opedDataMes=null;
    public int objectCounter;

    public static DropDownMenuHandle Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        textInfoCounty.text = "THIS IS BIGGER ???";

        
    }

	// Use this for initialization
	void Start () {

      
      
        /****************************/
        message mes = new message("openDataResponse");
                    scObject data = new scObject("data");
                    data.addString("location", "Αγ. Παρασκεύη,Λ. Μεσογείων και Καποδιστρίου");
                    data.addString("time", "13:00 - 17:00");
                    data.addString("cause", "Αίτια αναφερόμενα στην οδό και τον καιρό");
                    data.addString("type", "Άλλο ή άγνωστο είδος");
                    data.addString("vehicle", "Φορτηγό κάτω των 3,5 τόνων");
                    data.addString("victim", "Επιβάτης");
                    data.addString("age", "55 +");
                    mes.addSCObject(data);

                    scObject data1 = new scObject("data");
                    data1.addString("location", "13ο χλμ. Επ.Οδ. Ανδρίτσαινας-Θολου, Νέα Φιγαλεια");
                    data1.addString("time", "13:00 - 17:00");
                    data1.addString("cause", "Αίτια αναφερόμενα στην οδό και τον καιρό");
                    data1.addString("type", "Άλλο ή άγνωστο είδος");
                    data1.addString("vehicle", "I.X.E   ");
                    data1.addString("victim", "Οδηγός");
                    data1.addString("age", "55 +");
                    mes.addSCObject(data1);

        
   /*    for(int i=0;i<40;i++)
            messageToScrollviewData(mes);*/

        connect();
	}
    public int iterator;

    //temp
 /*   void showMoreData(message mes)
    {
        
        for (int i = 0; i < 30; i++)
        {
            messageToScrollviewData(mes);
        }
    }*/


	// Update is called once per frame
	void Update () {
        if (incMessages.Count > 0)
        {
            doMessages();
        }
    }

    //Listener for button Show me Data
    public void buttonListenerShowMeData()
    {
        string userPrefs = textCounty.text + "," + textMonth.text + "," + textYear.text + "," + textAccidentType.text;
        
        
        Debug.Log(userPrefs);

        scObject data = new scObject("userprefs");
        data.addString("county", textCounty.text);
        data.addString("month", textMonth.text);
        data.addString("year", textYear.text);
        data.addString("accidentType", textAccidentType.text);
   

        message openDatamsg = new message("opendata");
        openDatamsg.addSCObject(data);
        //TODO check dates if correct
        //object for construct correct message
        HandlePlayerQuery pq = new HandlePlayerQuery();
        //Send the correct message

        message svrMessage = new message("ServerOpenDataRequest");
        
        svrMessage = pq.messageOpenDataReady(openDatamsg);

        if (svrMessage != null)
        {
            UpdateInfoPanel(textCounty.text,svrMessage.getSCObject(0).getString("dateMY"), textAccidentType.text);
            SendServerMessage(svrMessage);
        }
        else
        {
            Debug.Log("THERE ARE EMPTY FIELDS...");
        }
    }

    //Listener for button next page of Results
    public void nextPageButton()
    {
        
        
        string location = "", type = "", cause = "", vehicle = "", victim = "", age = "", time = "", date = "";
        int size = 0;

        contentText.text = PadLeftAndRight("ΗΜΕΡΟΜΗΝΙΑ", 14) + "\t" + PadLeftAndRight("ΤΟΠΟΘΕΣΙΑ", 56) + "\t" + PadLeftAndRight("ΏΡΑ  ", 15) + "\t" + PadLeftAndRight("ΕΙΔΟΣ", 22) + "\t" + PadLeftAndRight("ΑΙΤΙΑ", 42) + "\t" + PadLeftAndRight("ΌΧΗΜΑ", 28) + "\t" + PadLeftAndRight("ΙΔΙΟΤΗΤΑ", 10) + "\t" + PadLeftAndRight("ΗΛΙΚΙΑ", 7) + "\n\n";

        if (opedDataMes.getSCObjectCount() > objectCounter + 70)
            size = objectCounter + 70;

        else
        {
            size = opedDataMes.getSCObjectCount();
            nextPage.interactable = false;
        }

        for (int i = objectCounter; i <size ; i++)
        {
           
            date = opedDataMes.getSCObject(i).getString("date");
            location = opedDataMes.getSCObject(i).getString("location");
            time = opedDataMes.getSCObject(i).getString("time");
            type = opedDataMes.getSCObject(i).getString("type");
            cause = opedDataMes.getSCObject(i).getString("cause");
            vehicle = opedDataMes.getSCObject(i).getString("vehicle");
            victim = opedDataMes.getSCObject(i).getString("victim");
            age = opedDataMes.getSCObject(i).getString("age");

            ++objectCounter;
            string iterS = objectCounter.ToString();
            contentText.text += iterS.PadRight(2) + " " + date.PadRight(14) + location.PadRight(55) + "\t" + time.PadRight(15) + "\t" + type.PadRight(22) + "\t" + cause.PadRight(42) + "\t" + vehicle.PadRight(28) + "\t" + victim.PadRight(10) + "\t" + age.PadRight(7) + "\n";
        }


    }


    public void UpdateInfoPanel(string county,string dateMY,string acType)
    {
        textInfoCounty.text = "COUNTY".PadRight(15) + ": " + county;
        if(!isLethalRequest)
             textInfoDate.text = "MONTH-YEAR".PadRight(15) + ": " + dateMY;
        textInfoTACC.text = "ACCIDENTS".PadRight(15) + " : " + acType;
    }

    //---------------------

    public  void messageToScrollviewData()
    {
        //For the next page
        objectCounter = 0;
        //enable next page button
        nextPage.interactable = true;
        if (isLethalRequest)
        {
            string location = "", type = "", cause = "", vehicle = "", victim = "", age = "", time = "",date="";
            int objs = opedDataMes.getSCObjectCount();

            contentText.text =PadLeftAndRight("ΗΜΕΡΟΜΗΝΙΑ",14)+"\t"+PadLeftAndRight("ΤΟΠΟΘΕΣΙΑ", 56) + "\t" + PadLeftAndRight("ΏΡΑ  ", 15) + "\t" + PadLeftAndRight("ΕΙΔΟΣ", 22) + "\t" + PadLeftAndRight("ΑΙΤΙΑ", 42) + "\t" + PadLeftAndRight("ΌΧΗΜΑ", 28) + "\t" + PadLeftAndRight("ΙΔΙΟΤΗΤΑ", 10) + "\t" + PadLeftAndRight("ΗΛΙΚΙΑ", 7) + "\n\n";
           
            for (int i = 0; i < 70; i++)
            {
                if (i == objs)
                {
                    nextPage.interactable = false;
                    break;
                }
                    
                date = opedDataMes.getSCObject(i).getString("date");
                location = opedDataMes.getSCObject(i).getString("location");
                time = opedDataMes.getSCObject(i).getString("time");
                type = opedDataMes.getSCObject(i).getString("type");
                cause = opedDataMes.getSCObject(i).getString("cause");
                vehicle = opedDataMes.getSCObject(i).getString("vehicle");
                victim = opedDataMes.getSCObject(i).getString("victim");
                age = opedDataMes.getSCObject(i).getString("age");

                objectCounter++;
                string iterS = objectCounter.ToString();
               contentText.text += iterS.PadRight(2)+" " +date.PadRight(14)+ location.PadRight(55) + "\t" + time.PadRight(15) + "\t" + type.PadRight(22) + "\t" + cause.PadRight(42) + "\t" + vehicle.PadRight(28) + "\t" + victim.PadRight(10) + "\t" + age.PadRight(7) + "\n";
            }
        }
        else
        {
           
            int objs = opedDataMes.getSCObjectCount(),sumOfAcc=0;
            string date = "";
            int numOfAcc = 0;
            //No need for next page here
            nextPage.interactable = false;

            contentText.text = PadLeftAndRight("ΗΜΕΡΟΜΗΝΙΑ", 14) + "\t" + PadLeftAndRight("Πλήθος Ατυχημάτων", 20)+"\n\n";

            for (int i = 0; i < objs; i++)
            {
                date = opedDataMes.getSCObject(i).getString("date");
                numOfAcc = opedDataMes.getSCObject(i).getInt("numOfAcc");

                sumOfAcc += numOfAcc ;

                ++objectCounter;
                string iterS = objectCounter.ToString();
                contentText.text += iterS.PadRight(2) + " " + date.PadRight(14) + PadLeftAndRight(numOfAcc.ToString(), 14) + "\n";
            }
            contentText.text += "\n  ΣΥΝΟΛΟ :"+sumOfAcc;
        }
    }
    public string PadLeftAndRight(string source, int length)
    {
        int spaces = length - source.Length;
        int padLeft = spaces / 2 + source.Length;
        return source.PadLeft(padLeft).PadRight(length);

    }

    //-----------------
    //Below methods are responible for server connection

    private void connect()
    {
        try
        {
            // get policy if we are on the web or in editor
            if ((Application.platform == RuntimePlatform.WindowsWebPlayer) || (Application.platform == RuntimePlatform.WindowsEditor))
            {
                Security.PrefetchSocketPolicy(ipAddress, pfrPort);
            }

            cSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            cSock.Connect(new IPEndPoint(IPAddress.Parse(ipAddress), sPort));
            clientConnection gsCon = new clientConnection(cSock, "opendata");
        }
        catch
        {
            Debug.Log("Unable to connect to server.");
        }
    }

    public void onConnect()
    {
        connectedToServer = true;
    }

    private void doMessages()
    {
        // do messages
        List<message> completedMessages = new List<message>();
        for (int i = 0; i < incMessages.Count; i++)
        {
            try
            {
                handleData(incMessages[i]);
                completedMessages.Add(incMessages[i]);
            }
            catch { }
        }

        // delete completed messages
        for (int i = 0; i < completedMessages.Count; i++)
        {
            try
            {
                incMessages.Remove(completedMessages[i]);
            }
            catch { }
        }
    }

    private void handleData(message mess)
    {

        switch (mess.messageText)
        {
            case "openDataResponse":
                Debug.Log("The open response returned :" + (mess.getSCObject(0).getString("location")));
                opedDataMes = mess;
                messageToScrollviewData();            
                break;
            default:
                Debug.Log("The server sent a message: " + mess.messageText);
                break;
        }


    }

    public void SendServerMessage(message mes)
    {
        if (connectedToServer)
        {
            try
            {
                // convert message into a byte array, wrap the message with framing
                byte[] messageObject = conversionTools.convertObjectToBytes(mes);
                byte[] readyMessage = conversionTools.wrapMessage(messageObject);

                // send completed message
                cSock.Send(readyMessage);
            }
            catch
            {


                Debug.Log("There was an error sending server message " + mes.messageText);
            }
        }
    }

    public void addServerMessageToQue(message msg)
    {
        incMessages.Add(msg);
    }

    private void OnApplicationQuit()
    {
        try { cSock.Close(); }
        catch { }
    }
}
