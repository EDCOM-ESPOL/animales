using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.SoundManagerNamespace;

public class DDOL : UnitySingletonPersistent<DDOL> {

    // Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GameStateManager.Instance.LoadScene("MainScreen");
    }

}
