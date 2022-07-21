using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject gm, ball,ballTrail,redMaterial;
    [SerializeField] List<GameObject> ObsList;
    [SerializeField] List<GameObject> TickList;
    public string OwnMaterialInformation="Yellow ";
    public string[] MatArray = new string[6];
    private void Awake()
    {
        OwnMaterialInformation += GetMaterialPrefs();
        SetMaterialPrefs();
        MaterialControl();
        TickControl(0);
    }
    public void TickControl(int Number)
    {
        if (ObsList[Number].activeSelf == false)
        {
            Debug.Log("cc");
            foreach (GameObject tick in TickList)
            {
                tick.SetActive(false);
            }
            TickList[Number].SetActive(true);
            for (int i = 0; i < TickList.Count; i++)
            {
                switch (Number)
                {
                    case 0:
                        ball.GetComponent<Renderer>().material.color = Color.yellow;
                        ballTrail.SetActive(true); redMaterial.SetActive(false);
                        break;
                    case 1:
                        ball.GetComponent<Renderer>().material.color = Color.red;
                        ballTrail.SetActive(false);redMaterial.SetActive(true);
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
            }
            MaterialControl();
        }
        
    }
    public void moneybas()
    {
        gm.GetComponent<GameManager>().money += 1000;
        gm.GetComponent<GameManager>().SetCash();
    }
    public void MaterialControl()
    {
        MatArray= OwnMaterialInformation.Split(' ');
        foreach (string mat in MatArray)
        {
            Debug.Log(mat);
            switch (mat)
            {
                case "Yellow":
                    ObsList[0].SetActive(false);
                    break;
                case "Red":
                    ObsList[1].SetActive(false);
                    break;
                case "Blue":
                    ObsList[2].SetActive(false);
                    break;
                case "Green":
                    ObsList[3].SetActive(false);
                    break;
                case "Orange":
                    ObsList[4].SetActive(false);
                    break;
                case "Pink":
                    ObsList[5].SetActive(false);
                    break;
            }
        }
    }
    public void SetMaterialBuyPrefs(string matPrice)
    {
        int price = int.Parse(matPrice.Split(' ')[1]);
        string buyMat = matPrice.Split(' ')[0];
        if (gm.GetComponent<GameManager>().money >= price)
        {
            bool control = false;
            MatArray = OwnMaterialInformation.Split(' ');
            foreach (string mat in MatArray)
            {
                if (mat == buyMat)
                {
                    control = true;
                }
            }
            if (control == false)
            {
                gm.GetComponent<GameManager>().money -= price;
                gm.GetComponent<GameManager>().SetCash();
                OwnMaterialInformation += buyMat+" ";
            }
            PlayerPrefs.SetString("OwnMaterial", OwnMaterialInformation);
            MaterialControl();
        }
    }
    public void SetMaterialPrefs()
    {
        PlayerPrefs.SetString("OwnMaterial", OwnMaterialInformation);
    }
    public string GetMaterialPrefs()
    {

        return PlayerPrefs.GetString("OwnMaterial");
    }
}
