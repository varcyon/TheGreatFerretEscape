using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace com.VarcyonSariouGames.TheGreatFerretEscape {
    public class MPData : MonoBehaviour {
        public static MPData MPD;
        public string displayName;
         public void Awake () {
            if (MPData.MPD == null) {
                MPData.MPD = this;
            } else {
                if (MPData.MPD != this) {
                    Destroy (this.gameObject);
                }
            }
         }
    }
}
