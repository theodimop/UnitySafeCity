using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using scMessage;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


public class loginScript : MonoBehaviour
{
    private int
        sPort = 3000, // server port
        pfrPort = 2999; // policy file request port

    private Socket
        cSock; // client socket

    public string
        ipAddress = "127.0.0.1", // server ip address
        username = "",          //Variable from TextField
        password = "" ;         //Variable from passwordfield
    public bool
        connectedToServer = false;

    private List<message>
        incMessages = new List<message>();

    private static loginScript instance;
    public static loginScript Instance
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
    void Start()
    {
        connect();
    }
 
    //this function will be on a UI button
    public void logInAccount()
    {
        //Get username from Input field
        GameObject inputFieldObject = GameObject.Find("InputFieldUsernameLogin");
        InputField inputField = inputFieldObject.GetComponent<InputField>();
        string username = inputField.text;

        //Get Password
        inputFieldObject = GameObject.Find("InputFieldPasswordLogin");
        inputField = inputFieldObject.GetComponent<InputField>();
        string password = inputField.text;

        if (checkLoginInputFields(username,password))
        {
            scObject data = new scObject("loginInfo");
            data.addString("username", username);
            string nPass = calculateMD5Hash(password);
            data.addString("password", nPass);
            message mes = new message("login");
            mes.addSCObject(data);
            SendServerMessage(mes);
        }
        else
        {
            //Error message
            GameObject errorTextOb = GameObject.Find("TextLoginError");
            Text errorText = errorTextOb.GetComponent<Text>();
            errorText.text = "*Please type your username and password.";
        }

       
    }
    //Check form with the user credentials
    private bool checkLoginInputFields(string n,string p)
    {
        if (n.Equals("") || p.Equals(""))
            return false;
        else
            return true;
    }

    //this function will be on a UI button
    public void registerAccount()
    {
        string[] registryCredentials = new string[7];
        /*
         0 firstname
         1 lastname
         2 date of birth
         3 email
         4 username
         5 password1
         6 password2*/

        
        //get firstname
        GameObject inputFieldObject = GameObject.Find("InputFieldFirstName");
        InputField inputField = inputFieldObject.GetComponent<InputField>();
        registryCredentials[0] = inputField.text;

        inputFieldObject = GameObject.Find("InputFieldLastName");
        inputField = inputFieldObject.GetComponent<InputField>();
        registryCredentials[1] = inputField.text;

        inputFieldObject = GameObject.Find("InputFieldDateOfBirth");
        inputField = inputFieldObject.GetComponent<InputField>();
        registryCredentials[2] = inputField.text;

        inputFieldObject = GameObject.Find("InputFieldEmail");
        inputField = inputFieldObject.GetComponent<InputField>();
        registryCredentials[3] = inputField.text;

        inputFieldObject = GameObject.Find("InputFieldUsername");
        inputField = inputFieldObject.GetComponent<InputField>();
        registryCredentials[4] = inputField.text;

        inputFieldObject = GameObject.Find("InputFieldPassword");
        inputField = inputFieldObject.GetComponent<InputField>();
        registryCredentials[5] = inputField.text;

        inputFieldObject = GameObject.Find("InputFieldRpassword");
        inputField = inputFieldObject.GetComponent<InputField>();
        registryCredentials[6] = inputField.text;

        GameObject errorTextOb = GameObject.Find("TextRegisterError");
        Text errorText = errorTextOb.GetComponent<Text>();

        GameObject test = GameObject.Find("InputFieldFirstName");
        InputField firstName = test.GetComponent<InputField>();
        firstName.ActivateInputField();

        switch (checkRegisterForm(registryCredentials))
        {
                /*
                 Error Codes
                 0. send message to server(all ok)
                 1. not all forms filled
                 2. passwords not match
                 3. not correct email
                 4. not correct date of birth
                 5. weak password
                 */
            case 0: 
                scObject data = new scObject("registerInfo");
                data.addString("firstname", registryCredentials[0]);
                data.addString("lastname", registryCredentials[1]);
                data.addString("dateofbirth", registryCredentials[2]);
                data.addString("email", registryCredentials[3]);
                data.addString("username", registryCredentials[4]);
                string nPass = calculateMD5Hash(registryCredentials[5]);
                data.addString("password", nPass);
                message mes = new message("register");
                mes.addSCObject(data);
                SendServerMessage(mes);
                errorText.text = "";
                break;
            case 1:
                errorText.text = "*Please fill all the fields.";
                
                break;
            case 2:
                errorText.text = "*Passwords don't match.";
                break;
            case 3:
                errorText.text = "*Not valid email address.";
                break;
            case 4:
                errorText.text = "*Not valid birth of date (dd/mm/yyyy).";
                break;
            case 5:
                errorText.text = "*Passwords need to be at least 8 characters.";
                break;

        }
    }

    private int checkRegisterForm(string[] registryCredentials)
    {
        //check empty fields
        foreach (string s in registryCredentials)
            if (String.IsNullOrEmpty(s))
                return 1;

        //check password match
        if (registryCredentials[5] != registryCredentials[6])
            return 2;

        //check if email
        if (!IsValidEmail(registryCredentials[3]))
            return 3;

        //check DateOfBirth 
        //If its number and the proper number
        try
        {
            int d, m, y;
            string[] date = registryCredentials[2].Split('/');
            bool isNumeric = int.TryParse(date[0], out d);
            isNumeric = int.TryParse(date[1], out m);
            isNumeric = int.TryParse(date[2], out y);

            if (!(isNumeric && d > 0 && d < 31 && m > 0 && m < 13 && y > 1930 && y < 2010))
                return 4;
        }
        catch
        {
            return 4;
        }

        //check password length
        if (registryCredentials[5].Length < 8)
            return 5;
        //TODO: check email,password le
        return 0;
    }
    bool IsValidEmail(string strIn)
    {
        // Return true if strIn is in valid e-mail format.
        return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }
    

    private string calculateMD5Hash(string password)
    {
        MD5 md = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(password);
        byte[] hash = md.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

    void connect()
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
            clientConnection gsCon = new clientConnection(cSock);
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

    private void OnApplicationQuit()
    {
        try { cSock.Close(); }
        catch { }
    }

    public void addServerMessageToQue(message msg)
    {
        incMessages.Add(msg);
    }

    void Update()
    {
        if (incMessages.Count > 0)
        {
            doMessages();
        }
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
            case "loginResponse":
                Debug.Log("The login response returned :"+(mess.getSCObject(0).getBool("response") ? "correct" : "not correct"));
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
}