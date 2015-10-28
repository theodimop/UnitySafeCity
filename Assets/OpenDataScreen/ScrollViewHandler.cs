using UnityEngine;
using UnityEngine.UI;
using scMessage;

public class ScrollViewHandler : MonoBehaviour {

    public Text contentText;

    public Text test;
    // Use this for initialization
    void Start () {
        //contentTitleText.text =@"            LOCATION           |      TIME     |         TYPE           |                  CAUSE                   |           VEHICLE           |    VICTIM   |  AGE    ";
        //contentText.text +="\n 17ο χλμ Επ.Οδ.Αλμυρού - Κωφών | 13:00 - 17:00 |        Εκτροπή         | Αίτια αναφερόμενα στην οδό και τον καιρό | Φορτηγό κάτω των 3,5 τόνων  |    Οδηγός   | 55 +    ";
        
        //                      45                             15             22                      42                             28     8       7
       fixRow("12ο χλμ. Επ.Οδ.Φιλιατών - Μαυρονεριου", "09:00 - 13:00", "Ανατροπή","Αίτια αναφερόμενα στην οδό και τον καιρό", " Ι.Χ.Ε.","Οδηγός","55 +");
    }

   public void messageToScrollviewData(message mes )
    {
        Debug.Log("in message to Scroll");
        for (int i =0;i<mes.getSCObjectCount();i++)
        {
           string location = mes.getSCObject(i).getString("location");
           string  time = mes.getSCObject(i).getString("time");
           string type = mes.getSCObject(i).getString("type");
           string cause = mes.getSCObject(i).getString("cause");
           string vehicle = mes.getSCObject(i).getString("vehicle");
           string victim = mes.getSCObject(i).getString("victim");
           string age = mes.getSCObject(i).getString("age");

            Debug.Log(location + "|" + time + "|" + type + "|" + cause + "|" + vehicle + "|" + victim + "|" + age);
            // fixRow(location, time, type, cause, vehicle, victim, age);

             contentText.text += fixRow(location,time,type,cause,vehicle,victim,age);
            
        }
    }
    //location
    public static string fixRow(string s0, string s1, string s2, string s3, string s4, string s5, string s6)
    {
        string location, time, type, cause, vehicle, victim, age;

        location = PadLeftAndRight(s0,45);
        time = PadLeftAndRight(s1, 15);
        type = PadLeftAndRight(s2, 22);
        cause = PadLeftAndRight(s3, 42);
        vehicle = PadLeftAndRight(s4, 28);
        victim = PadLeftAndRight(s5, 10);
        age = PadLeftAndRight(s6, 7);

        Debug.Log(location+"|"+ time + "|" + type + "|" + cause + "|" + vehicle + "|" + victim + "|" + age );
        string dataRow = "\n"+location + "|" + time + "|" + type + "|" + cause + "|" + vehicle + "|" + victim + "|" + age;

        return dataRow;
  //      12ο χλμ. Επ.Οδ.Φιλιατών - Μαυρονεριου    09:00 - 13:00   Ανατροπή Αίτια αναφερόμενα στην οδό και τον καιρό    Ι.Χ.Ε.Οδηγός  55 +        

    }
    //Pad left and right of string
    public static string PadLeftAndRight(string source, int length)
    {
        int spaces = length - source.Length;
        int padLeft = spaces / 2 + source.Length;
        return source.PadLeft(padLeft).PadRight(length);

    }

}
