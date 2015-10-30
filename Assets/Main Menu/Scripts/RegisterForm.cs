using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RegisterForm : MonoBehaviour {

    public void clearRegisterForm()
    {
       
        //get firstname
        InputField inputField = GameObject.Find("InputFieldFirstName").GetComponent<InputField>();
        inputField.text = "";

        inputField = GameObject.Find("InputFieldLastName").GetComponent<InputField>();
        inputField.text = "";

        inputField = GameObject.Find("InputFieldDateOfBirth").GetComponent<InputField>();
        inputField.text = "";

        inputField = GameObject.Find("InputFieldEmail").GetComponent<InputField>();
        inputField.text = "";

        inputField = GameObject.Find("InputFieldUsername").GetComponent<InputField>();
        inputField.text = "";

        inputField = GameObject.Find("InputFieldPassword").GetComponent<InputField>();
        inputField.text = "";

        inputField = GameObject.Find("InputFieldRpassword").GetComponent<InputField>();
        inputField.text = "";


        GameObject errorTextOb = GameObject.Find("TextRegisterError");
        Text errorText = errorTextOb.GetComponent<Text>();
        errorText.text = "";

    }
}
