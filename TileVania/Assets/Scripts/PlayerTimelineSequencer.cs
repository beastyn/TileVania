using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerTimelineSequencer : MonoBehaviour {

    PlayableDirector myPlayableDirector;
   
   
	void Start () {
        myPlayableDirector = GetComponent<PlayableDirector>();
 
	}
	
    public bool IsIntroPlayerEnded()
    {
        if (myPlayableDirector)
        {
            return myPlayableDirector.state == PlayState.Paused;
        }
        else
        {
            return true;
        }
    }
}
