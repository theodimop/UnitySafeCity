﻿using OpenDataScreen;
using scMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


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

    public Text textCounty, textDay, textMonth, textYear, textAccidentType,textToDay,textToMonth,TextToYear,contentText;

 

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
        
    }


   
	// Use this for initialization
	void Start () {

        Debug.Log("|" +String.Format("{0,-8}{1,-8}","theo_","theo____")+"|");
        Debug.Log("|"+ String.Format("{0,-8}{1,-8}", "theotheo","theotheo") + "|");

        //  contentText.text += "\n|" + String.Format("{0,-50}{1,-15}", "13ο χλμ. Επ.Οδ. Ανδρίτσαινας-Θολου, Νέα Φιγαλεια", "theo____") + "|";
        //  contentText.text += "\n|" + String.Format("{0,-42}{1,-15}", "θοδωρης", "theo____") + "|";
        //  contentText.text += "\n|" + String.Format("{0,-50}{1,-15}", "Λ.Γεωργίου Παπανδρέου 583 Χαλκίδα", "theotheo") + "|";

        contentText.text = fixRow("LOCATION","TIME","TYPE","CAUSE","VEHICLE","VICTIM","AGE");
       connect();
	}
	
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
        string userPrefs = textCounty.text + "," + textDay.text + "," + textMonth.text + "," + textYear.text + "," + textAccidentType.text;
        
        
        Debug.Log(userPrefs);

        scObject data = new scObject("userprefs");
        data.addString("county", textCounty.text);
        data.addString("day", textDay.text);
        data.addString("month", textMonth.text);
        data.addString("year", textYear.text);
        data.addString("accidentType", textAccidentType.text);
        data.addString("toDay",textToDay.text);
        data.addString("toMonth", textToMonth.text);
        data.addString("toDay", TextToYear.text);

        message openDatamsg = new message("opendata");
        openDatamsg.addSCObject(data);

        //object for construct correct message
        HandlePlayerQuery pq = new HandlePlayerQuery();
        //Send the correct message

        if(pq.messageOpenDataReady(openDatamsg)!=null)
             SendServerMessage(pq.messageOpenDataReady(openDatamsg));   
        else
        {
            Debug.Log("THERE ARE EMPTY FIELDS...");
        }
    }

    //---------------------

    public  void messageToScrollviewData(message mes)
    {
        for (int i = 0; i < mes.getSCObjectCount(); i++)
        {
            string location = mes.getSCObject(i).getString("location");
            string time = mes.getSCObject(i).getString("time");
            string type = mes.getSCObject(i).getString("type");
            string cause = mes.getSCObject(i).getString("cause");
            string vehicle = mes.getSCObject(i).getString("vehicle");
            string victim = mes.getSCObject(i).getString("victim");
            string age = mes.getSCObject(i).getString("age");

            Debug.Log(location + "|" + time + "|" + type + "|" + cause + "|" + vehicle + "|" + victim + "|" + age);

            contentText.text += fixRow(location, time, type, cause, vehicle, victim, age);

        }
    }
    //location
    public  string fixRow(string s0, string s1, string s2, string s3, string s4, string s5, string s6)
    {
        string location, time, type, cause, vehicle, victim, age;

        location = PadLeftAndRight(s0, 50);
        time = PadLeftAndRight(s1, 15);
        type = PadLeftAndRight(s2, 22);
        cause = PadLeftAndRight(s3, 42);
        vehicle = PadLeftAndRight(s4, 28);
        victim = PadLeftAndRight(s5, 10);
        age = PadLeftAndRight(s6, 7);

        Debug.Log(location + "|" + time + "|" + type + "|" + cause + "|" + vehicle + "|" + victim + "|" + age);
        string dataRow = location + " " + time + " " + type + " " + cause + " " + vehicle + " " + victim + " " + age+"\n";

        return dataRow;
        //      12ο χλμ. Επ.Οδ.Φιλιατών - Μαυρονεριου    09:00 - 13:00   Ανατροπή Αίτια αναφερόμενα στην οδό και τον καιρό    Ι.Χ.Ε.Οδηγός  55 +        

    }
    //Pad left and right of string
    public  string PadLeftAndRight(string source, int length)
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
                messageToScrollviewData(mess);                
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
