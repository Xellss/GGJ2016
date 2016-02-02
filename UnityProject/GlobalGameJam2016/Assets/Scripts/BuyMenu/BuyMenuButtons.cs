using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyMenuButtons : MonoBehaviour
{
    public Health Health;
    public Text GoldText;
    public Weapon Weapon;
    public Movement Movement;
    public ItemPanel ItemPanel;

    public Text costPointer;
    public Text costHP;
    public Text costMoveSpeed;
    public Text costShootDamage;
    public Text costShootSpeed;

    private int myGold = 0;
    private int currentGold;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
    }

    private bool parseGold(string text)
    {
        myGold = int.Parse(GoldText.text);
        currentGold = int.Parse(text);

        if (currentGold > myGold)
        {
            return false;
        }
        else
        {
            myGold -= currentGold;
            GoldText.text = myGold.ToString();
            return true;
        }
    }
    public void OnClick_NextWave()
    {
        this.gameObject.SetActive(false);
        ItemPanel.NextWave();
    }
    public void OnBuy_Pointer()
    {
        if (parseGold(costPointer.text))
        {
            Weapon.ShowLaser = true;
        }

    }
    public void OnBuy_HP()
    {
        if (parseGold(costHP.text))
        {
            float i = Health.MaxHealth / 100 * 10;
            Health.GiveHealth(i);
        }

    }
    public void OnBuy_MoveSpeed()
    {
        if (parseGold(costMoveSpeed.text))
        {
            Movement.Speed += 1;
        }

    }
    public void OnBuy_ShootSpeed()
    {
        if (parseGold(costShootSpeed.text))
        {
            float i = Weapon.Delay / 100 * 5;
            Weapon.Delay -= i;
        }

    }
    public void OnBuy_ShootDMG()
    {
        if (parseGold(costShootDamage.text))
        {
            float i = Weapon.Damage / 100 * 5;
            Weapon.Damage += i;
        }

    }
}
