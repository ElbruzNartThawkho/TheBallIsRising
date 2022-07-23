using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShopInventory
{
    public bool yellow = false;
    public bool red = false;
    public bool blue = false;
    public bool green = false;
    public bool gray = false;
    public bool pink = false;
    public int lastSelect = 0;
}
public class JsonSave : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
    }
    public void Save(ShopInventory ýnventory)
    {
        string shopInventoryJsonFormat = JsonUtility.ToJson(ýnventory);//json convert
        File.WriteAllText(Application.persistentDataPath + "/InvertoryData.json", shopInventoryJsonFormat);
    }
    public ShopInventory Load()
    {
        string jsonShopInventoryData = File.ReadAllText(Application.persistentDataPath + "/InvertoryData.json");
        ShopInventory readShopInventory = JsonUtility.FromJson<ShopInventory>(jsonShopInventoryData);   
        return readShopInventory;
    }
}
