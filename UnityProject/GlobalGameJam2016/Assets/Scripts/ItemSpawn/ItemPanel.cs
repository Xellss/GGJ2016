using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour {

    public Spawner Spawner;
    public TakeItem TakeItem;
    public ItemSpawn ItemSpawn;
    public Transform PanelCanvas;
    //[HideInInspector]
    public bool HaveAllItems = false;
    private bool panelActiv= false;

	void Update () 
    {
        if (Spawner.CurrentWave.Number == TakeItem.ItemCount)
        {
            HaveAllItems = true;
        }
        if (panelActiv && Input.GetKey(KeyCode.E))
        {
            PanelCanvas.gameObject.SetActive(false);
            ItemSpawn.PlacedItems = true;
            Spawner.NextWave();
            Spawner.StartSpawning = true;
            HaveAllItems = false;
            panelActiv = false;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (HaveAllItems))
        {
            PanelCanvas.gameObject.SetActive(true);
            panelActiv = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PanelCanvas.gameObject.SetActive(false);
            panelActiv = false;
        }
    }

}
