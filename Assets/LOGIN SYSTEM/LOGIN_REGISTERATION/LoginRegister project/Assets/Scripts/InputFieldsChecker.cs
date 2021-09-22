using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldsChecker : MonoBehaviour
{
    public Button loginButton;
    public Button registerButton;
 
	public InputField WalletAddress;
   

    public void OnInputUpdate() {
	    // forgotPasswordButton.interactable = IsEmailValid;
	    bool isEverythingValid = IsWalletAddressValid;
        loginButton.interactable = isEverythingValid;
        registerButton.interactable = isEverythingValid;
    }

	bool IsWalletAddressValid {
        get {
	        if ( WalletAddress.text.Length == 42  &&  WalletAddress.text.StartsWith("0") )
                return true;
            return false;
        }
    }
}
