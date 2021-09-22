using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetCustID : MonoBehaviour
{
	public static InputField CustID;
	public InputField ID;
	public GameObject INPUTS;
    // Start is called before the first frame update
    void Start()
    {
	    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void SubmitButton()
	{
		CustID=ID;
		Debug.Log("This is new ID : " + CustID);
		Debug.Log("This is new ID text : " + CustID.text);
		INPUTS.SetActive(false);
		SceneManager.LoadScene("MainMenu");
	}
}
