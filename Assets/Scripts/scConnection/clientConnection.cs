using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using scMessage;

public class clientConnection
{
    public Socket sSock;
    //This variable is important for identifying which scene is connected to Server
    private string instance;
 

    public clientConnection(Socket s,String instance)
    {
        sSock = s;
        this.instance = instance;
        ThreadPool.QueueUserWorkItem(new WaitCallback(HandleConnection));
    }

    public void HandleConnection(object state)
    {
        Debug.Log("Connected to server.");
        instanceSetConnectedToServer();

        try
        {
            while (sSock.Connected)
            {
                byte[] sizeinfo = new byte[4];

                int bytesRead = 0, currentread = 0;

                currentread = bytesRead = sSock.Receive(sizeinfo);

                while (bytesRead < sizeinfo.Length && currentread > 0)
                {
                    currentread = sSock.Receive(sizeinfo, bytesRead, sizeinfo.Length - bytesRead, SocketFlags.None);
                    bytesRead += currentread;
                }

                int messagesize = BitConverter.ToInt32(sizeinfo, 0);
                byte[] message = new byte[messagesize];

                bytesRead = 0;
                currentread = bytesRead = sSock.Receive(message, bytesRead, message.Length - bytesRead, SocketFlags.None);

                while (bytesRead < messagesize && currentread > 0)
                {
                    currentread = sSock.Receive(message, bytesRead, message.Length - bytesRead, SocketFlags.None);
                    bytesRead += currentread;
                }

                try
                {
                    message incObject = (message)conversionTools.convertBytesToObject(message);

                    if (incObject != null)
                    {
                        //  loginScript.Instance.addServerMessageToQue(incObject);
                        //  DropDownMenuHandle.Instance.addServerMessageToQue(incObject);
                        instanceAddMessageToQue(incObject);
                    }
                }
                catch (Exception er)
                {
                    Debug.Log("There was an error trying to get data: " + er.ToString());
                }
            }
        }
        catch { }

        Debug.Log("Disconnected from server.");
        //loginScript.Instance.connectedToServer = false;
        //DropDownMenuHandle.Instance.connectedToServer = false;
        instanceSetConnectedToServerToFalse();
        sSock.Close();
    }

    //Set the bool variable connectedToServer of the right class to true
    private void instanceSetConnectedToServer()
    {
        switch(instance)
        {
            case "opendata":
                DropDownMenuHandle.Instance.onConnect();
                break;
            case "loginscript":
                loginScript.Instance.onConnect();
                break;

        }
    }
    
    //Set the bool variable connectedToServer of the right class to false
    private void instanceSetConnectedToServerToFalse()
    {
        switch (instance)
        {
            case "opendata":
                DropDownMenuHandle.Instance.connectedToServer = false;
                break;
            case "loginscript":
                loginScript.Instance.connectedToServer = false;
                break;

        }
    }

    //Add message to proper Class que
    private void instanceAddMessageToQue(message incObj)
    {
        switch (instance)
        {
            case "opendata":
                DropDownMenuHandle.Instance.addServerMessageToQue(incObj);
                break;
            case "loginscript":
                loginScript.Instance.addServerMessageToQue(incObj); 
                break;

        }
    }




}