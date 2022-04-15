using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void startDeathTransition() {
        UIManager ui = GetComponent<UIManager>();
        if (ui != null) {
            ui.startDeathTransition();
        }
    }
    
    public void endDeathTransition() {
        UIManager ui = GetComponent<UIManager>();
        if (ui != null) {
            ui.endDeathTransition();
        }
    }
}
