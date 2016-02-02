using UnityEngine;
using System.Collections;

public class CanvasButtons : MonoBehaviour {

    public Transform Credits;
    
    public void OnClick_StartGame()
    {
        this.gameObject.SetActive(false);
    }
    public void OnClick_Credits()
    {
        Credits.gameObject.SetActive(true);
    }
}
