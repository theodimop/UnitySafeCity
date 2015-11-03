using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DropDownListsController : MonoBehaviour {

    public Dropdown ddMonth, ddYear;
    public Text accType;


	public void DropdownAccidentTypeListener () {
        
        if (accType.text.Equals("Lethals"))
        {
            ddMonth.interactable = false;
            ddYear.interactable = false;
            DropDownMenuHandle.isLethalRequest = true;
        }
        else
        {
            ddMonth.interactable = true;
            ddYear.interactable = true;
            DropDownMenuHandle.isLethalRequest = false;
        }
	}
}
