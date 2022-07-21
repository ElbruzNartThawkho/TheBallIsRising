using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    GameObject Gm;
    private void Awake()
    {
        Gm = GameObject.FindWithTag("GameController");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Gm.GetComponent<GameManager>().FinishLevel();
        }
    }

}
