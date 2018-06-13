using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LensCastSDK{
    public class LensCastNetwork : MonoBehaviour
    {
        //just a file to TEST JSON before networking is implemented
        public TextAsset text;

        // Just a Test to replace the Networking for now
        IEnumerator Start() {
            while (true) {
                yield return new WaitForSeconds(1); //simulate action every second
                LensCastParser.Instance.Parse(text.text);
            }
        }
    }
}