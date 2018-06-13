using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LensCastSDK {
    public class LensCastParser : MonoBehaviour {
    
        public delegate void ParseAction(LensAction[] action, int timestamp);
        public static ParseAction OnParse; //internal event for parsing

        public static LensCastParser Instance;

        // Use this for initialization
        void Start () {
            if (Instance == null) {
                Instance = this;
            }
        }

        //called when GameObject is destroyed
		void OnDestroy(){
			if (Instance == this) {
                Instance = null;
            }
		}
      
        /// <summary>
        /// Parses the Remote Command JSON from the LenCastNetwork
        /// </summary>
        /// <param name="json">JSON string value</param>
        /// <returns>Sends an event with the timestamp and the LensAction</returns>
        public void Parse(string json) {
            //use JSONObject to parse
            JSONObject j = new JSONObject(json);
            JSONObject action = j["remoteCommand"]["action"];

            List<LensAction> lenses = new List<LensAction>();

            if (action != null) {
                LensAction la = CreateAction(action);
                lenses.Add(la);
            } else {
                //TODO: agree on format of keys for actions
            }
                     
            int time = (int) j["remoteCommand"]["timestamp"].n; //timestamp value

            if (OnParse != null) {
                OnParse(lenses.ToArray(), time);
            }
            
        }

        /// <summary>
        /// Helper Method to Create a Lens Action
        /// </summary>
        LensAction CreateAction(JSONObject actionJson){
            LensAction lensAction = new LensAction();
            
            lensAction.id = actionJson["id"].str;
            lensAction.action_name = actionJson["name"].str;
            
            JSONObject valueJson = actionJson["value"];

            if (valueJson == null) {
                lensAction.value_id = null;
                lensAction.value_name = null;
            } else {
                lensAction.value_id = valueJson["id"].str;
                lensAction.value_name = valueJson["name"].str;
            }
 
            return lensAction;
        }

    }
}



