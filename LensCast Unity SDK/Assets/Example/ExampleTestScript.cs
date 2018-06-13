using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LensCastSDK;

public class ExampleTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LensCast.OnRemoteCommand += OnActionRecieved;
	}

    //Action Recieved from Lens Cast and can  act based on attributes
    void OnActionRecieved(LensAction[] action, int timestamp) {
        Debug.LogError("Action Name is " + action[0].action_name);
	Debug.LogError("Value Name is " + action[0].value_name);
	Debug.LogError("Value ID is " + action[0].value_id);
    }

}
