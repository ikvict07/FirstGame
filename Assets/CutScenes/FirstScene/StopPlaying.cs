using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

using static Scene;
public class StopPlaying : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public bool isPlayerStarted = false;

    void Update() {
        if (isPlayerStarted == false && VideoPlayer.isPlaying == true) {
            // When the player is started, set this information
            isPlayerStarted = true;
        }
        if (isPlayerStarted == true && VideoPlayer.isPlaying == false ) {
            // Wehen the player stopped playing, hide it
            VideoPlayer.gameObject.SetActive(false);
            
            LoadScene(1);
        }
    }   
}