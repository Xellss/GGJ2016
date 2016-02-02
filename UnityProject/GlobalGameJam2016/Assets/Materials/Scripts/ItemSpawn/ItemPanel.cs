using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour {

    public Spawner Spawner;
    public TakeItem TakeItem;
    public ItemSpawn ItemSpawn;
    public Transform PanelCanvas;
    public Transform BuyMenu;
    //[HideInInspector]
    public bool HaveAllItems = false;
    private bool panelActiv= false;
    private bool stageClear = false;

	void Update () 
    {
        if (Spawner.CurrentWave.Number == TakeItem.ItemCount)
        {
            HaveAllItems = true;
        }
        if (panelActiv && Input.GetKey(KeyCode.E))
        {
            PanelCanvas.gameObject.SetActive(false);
            Spawner.Spawning = false;
            panelActiv = false;
            HaveAllItems = false;
        }
        if (stageClear && Spawner.AliveEnemys.Count == 0)
        {
            BuyMenu.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.B))
        {
            BuyMenu.gameObject.SetActive(true);
        }
	}
    public void NextWave()
    {
        ItemSpawn.PlacedItems = true;
        Spawner.NextWave();
        Spawner.StartSpawning = true;
        
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
