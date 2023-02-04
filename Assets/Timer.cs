using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public class OnTickEvent : Eventargs{
        public int ticker;
    }
    public static event EventHandler<OnTickEvent> _onTick;
    private const float MAX_TICK = .5f;

    private int ticker;
    private float timerfortick;

    private void Awake(){
        ticker = 0;
    }
    // Update is called once per frame
    private void Update(){
        timerfortick += Timer.deltaTime;
        if (timerfortick>=MAX_TICK){
            timerfortick -= MAX_TICK;
            ticker++;
            if (OnTickEvent != null){ _onTick(this,OnTickEvent);{ticker = ticker;}
        }
    }
}}
