using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject gm, ball,ballTrail,redMaterial;
    [SerializeField] List<GameObject> ObsList;
    [SerializeField] List<GameObject> TickList;
    JsonSave jsonSave;
    ShopInventory inventory = new ShopInventory();
    int joinCount = 0;
    public void Awake()
    {
        jsonSave = GetComponent<JsonSave>();
        joinCount = PlayerPrefs.GetInt("Join");
        if(joinCount == 0)
        {
            inventory.yellow = true;
            inventory.red = false;
            inventory.blue = false;
            inventory.green = false;
            inventory.gray = false;
            inventory.pink = false;
            inventory.lastSelect = 0;
            jsonSave.Save(inventory);
            joinCount++;
            PlayerPrefs.SetInt("Join", joinCount);
        }
        inventory = jsonSave.Load();
        MaterialControl();
        TickControl(inventory.lastSelect);
    }
    public void TickControl(int Number)
    {
        if (ObsList[Number].activeSelf == false)
        {
            foreach (GameObject tick in TickList)
            {
                tick.SetActive(false);
            }
            TickList[Number].SetActive(true);
            switch (Number)
            {
                case 0:
                    ball.GetComponent<Renderer>().material.color = Color.yellow;
                    ballTrail.SetActive(true); redMaterial.SetActive(false);
                    break;
                case 1:
                    ball.GetComponent<Renderer>().material.color = Color.red;
                    ballTrail.SetActive(false); redMaterial.SetActive(true);
                    break;
                case 2:
                    ball.GetComponent<Renderer>().material.color = Color.blue;
                    ballTrail.GetComponent<TrailRenderer>().startColor = Color.blue;
                    ballTrail.SetActive(true); redMaterial.SetActive(false);
                    break;
                case 3:
                    ball.GetComponent<Renderer>().material.color = Color.green;
                    ballTrail.GetComponent<TrailRenderer>().startColor = Color.green;
                    ballTrail.SetActive(true); redMaterial.SetActive(false);
                    break;
                case 4:
                    ball.GetComponent<Renderer>().material.color = Color.gray;
                    ballTrail.GetComponent<TrailRenderer>().startColor = Color.gray;
                    ballTrail.SetActive(true); redMaterial.SetActive(false);
                    break;
                case 5:
                    ball.GetComponent<Renderer>().material.color = Color.magenta;
                    ballTrail.GetComponent<TrailRenderer>().startColor = Color.magenta;
                    ballTrail.SetActive(true); redMaterial.SetActive(false);
                    break;
            }
            inventory.lastSelect = Number;
            jsonSave.Save(inventory);
            MaterialControl();
        }
        
    }
    public void MoneyBas()
    {
        gm.GetComponent<GameManager>().money += 1000;
        gm.GetComponent<GameManager>().SetCash();
    }
    public void MaterialControl()
    {
        if (inventory.yellow)
            ObsList[0].SetActive(false);
        if (inventory.red)
            ObsList[1].SetActive(false);
        if (inventory.blue)
            ObsList[2].SetActive(false);
        if (inventory.green)
            ObsList[3].SetActive(false);
        if (inventory.gray)
            ObsList[4].SetActive(false);
        if (inventory.pink)
            ObsList[5].SetActive(false);
    }
    public void SetMaterialBuyPrefs(int number)
    {
        switch (number)
        {
            case 0:
                inventory.yellow=PriceAndBuyControl(inventory.yellow, 0);
                break;
            case 1:
                inventory.red = PriceAndBuyControl(inventory.red, 50);
                break;
            case 2:
                inventory.blue = PriceAndBuyControl(inventory.blue, 100);
                break;
            case 3:
                inventory.green = PriceAndBuyControl(inventory.green, 150);
                break;
            case 4:
                inventory.gray = PriceAndBuyControl(inventory.gray, 200);
                break;
            case 5:
                inventory.pink = PriceAndBuyControl(inventory.pink, 250);
                break;
        }
        jsonSave.Save(inventory);
        MaterialControl();
    }
    bool PriceAndBuyControl(bool status ,int price)
    {
        if (gm.GetComponent<GameManager>().money >= price)
        {
            if (status == false)
            {
                gm.GetComponent<GameManager>().money -= 0;
                gm.GetComponent<GameManager>().SetCash();
                status = true;
            }
        }
        return status;
    }
}
