using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JmpBtn : MonoBehaviour,IPointerDownHandler
{
    GameObject ball;
    private void Awake()
    {
        ball = GameObject.FindWithTag("Player");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ball.GetComponent<BallMove>().RiseUp();
    }
}
