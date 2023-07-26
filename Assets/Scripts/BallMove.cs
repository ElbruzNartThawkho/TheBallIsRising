using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    GameManager gm;
    public int vibrate = 1;
    public int sound = 1;
    public AudioClip jumpSound,failSound;
    [SerializeField] float jumpSpeed;
    [SerializeField] GameObject soundOff, vibrateOff,soundOn,vibrateOn;
    
    private void Start()
    {
        GetSoundandVibration();
        gm = GameManager.instance;
    }
    public void SetSoundandVibration()
    {
        PlayerPrefs.SetInt("SOUND", sound); PlayerPrefs.SetInt("VIBRATE", vibrate);
    }
    public void GetSoundandVibration()
    {
        sound=PlayerPrefs.GetInt("SOUND"); vibrate=PlayerPrefs.GetInt("VIBRATE");
        if (sound == 0)
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
        else
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        if(vibrate == 0)
        {
            vibrateOn.SetActive(false);
            vibrateOff.SetActive(true);
        }
        else
        {
            vibrateOn.SetActive(true);
            vibrateOff.SetActive(false);
        }
    }
    void Update()
    {
        /*if (Input.touchCount > 0)
        {
            Touch finger = Input.GetTouch(0);
            if (finger.phase == TouchPhase.Began)
            {
                RiseUp();
                Debug.Log("Dokundu");
            }
            //if (finger.phase == TouchPhase.Stationary)
            //{
            //    Debug.Log("Dokunyor");
            //}
            //if (finger.phase == TouchPhase.Moved)
            //{
            //    Debug.Log("Sürüklüyor");
            //}
            //if (finger.phase == TouchPhase.Ended)
            //{
            //    Debug.Log("Dokunma Bitti");
            //}
        }*/
    }
    public void VoiceOn()
    {
        sound = 0;
    }

    public void VoiceOff()
    {
        sound = 1; //SetSound();
    }

    public void VibrationOn()
    {
        vibrate = 0; //SetVibrate();
    }

    public void VibrationOff()
    {
        vibrate = 1; //SetVibrate();
    }

    public void RiseUp()
    {
        if (sound == 1)
        {
            GetComponent<AudioSource>().clip = jumpSound;
            GetComponent<AudioSource>().Play();
        }
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 40, 0) * jumpSpeed * 100 * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Respawn")
        {
            gm.GameOver();
            if(sound == 1)
            {
                GetComponent<AudioSource>().clip = failSound;
                GetComponent<AudioSource>().Play();
            }
            if (vibrate == 1)
            {
                Handheld.Vibrate();
                Debug.Log("Titredi");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            other.gameObject.SetActive(false);
            gm.money++;
            gm.SetCash();
        }
    }
}
