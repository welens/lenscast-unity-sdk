using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LensCastSDK
{
    public class LensCast : MonoBehaviour
    {

        public delegate void RemoteControlActions(LensAction[] action, int timestamp);
        
        /// <summary>
        /// Event that gets fired when LensCast recieves a remote command
        /// </summary>
        public static RemoteControlActions OnRemoteCommand;

		 void Start(){
            DontDestroyOnLoad(this.gameObject); //will keep across scenes
            LensCastParser.OnParse += Lenscast_Parse;
		}

        void Lenscast_Parse(LensAction[] action, int timestamp) {
            if (OnRemoteCommand != null) {
                OnRemoteCommand( action, timestamp);
            }
        }
    }
}