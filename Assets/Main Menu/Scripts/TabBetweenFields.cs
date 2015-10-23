using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/*This is a simple class for helping the user
 to fill all the fields in a menu. Simple
 when he press tab the next field is focused.*/
public class TabBetweenFields : MonoBehaviour {


    public InputField[] fields;
    private static int i;
  
	
	// Update is called once per frame
	void Update () {

        checkTab();
	}

    private void checkTab()
    {
        //when player enters tab the next field is focused
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            switch (getSelectedInputField())
            {

                case 0:
                    i = 1;
                    break;
                case 1:
                    i = 2;
                    break;
                case 2:
                    i = 3;
                    break;
                case 3:
                    i = 4;
                    break;
                case 4:
                    i = 5;
                    break;
                case 5:
                    i = 6;
                    break;
                case 6:
                    i = 0;
                    break;
            }
            fields[i].ActivateInputField();
        }
 

    }
    //Find which field is the selected
    private int getSelectedInputField()
    {
        if (fields[0].isFocused)
            return 0;
        else if (fields[1].isFocused)
            return 1;
        else if (fields[2].isFocused)
            return 2;
        else if (fields[3].isFocused)
            return 3;
        else if (fields[4].isFocused)
            return 4;
        else if (fields[5].isFocused)
            return 5;
        else if (fields[6].isFocused)
            return 6;

        //Something went wrong
        return -1;
    }

 
}
