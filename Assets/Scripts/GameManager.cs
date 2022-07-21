using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using EasyMobile;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject start, finish, currentLvlGO, nextLvlGO, levelProgressBar, levelCompleteScreen, ball, panelBlock, gameOverScreen, moneyText, shopScreen;
    
    [SerializeField] List<GameObject> chunks;
    Vector3 placeTransform;
    public int currentLevel = 0, money = 0, deathCount = 0;
    
    private void Awake()
    {
        shopScreen.GetComponent<Shop>().Awake();
        deathCount = GetDeathCount();
        deathCount++;
        SetDeathCount();
        money = GetCash();
        moneyText.GetComponent<TextMeshProUGUI>().text = money.ToString();
        currentLevel = GetCurrentLevel();
        currentLvlGO.GetComponent<TextMeshProUGUI>().text = currentLevel.ToString();
        currentLevel++;
        nextLvlGO.GetComponent<TextMeshProUGUI>().text = currentLevel.ToString();
        currentLevel--;
        placeTransform = start.transform.position;
    }
    private void Start()
    {
        RuntimeManager.Init();
        LevelCreate();
        levelProgressBar.GetComponent<Slider>().maxValue = GameObject.FindWithTag("Finish").transform.position.y;
        levelProgressBar.GetComponent<Slider>().minValue = start.transform.position.y;
        levelProgressBar.GetComponent<Slider>().value = ball.transform.position.y;
    }
    private void Update()
    {

        levelProgressBar.GetComponent<Slider>().value=ball.transform.position.y;
    }
    public void Close()=>Application.Quit();
    public void FinishLevel()
    {
        panelBlock.SetActive(true);
        currentLevel++;
        SetCurrentLevel();
        ball.SetActive(false);
        levelCompleteScreen.SetActive(true);
    }
    public void GameOver()
    {
        
        if (deathCount % 4 == 0&& deathCount!=0)
        {
            Advertising.ShowInterstitialAd();
        }
        panelBlock.SetActive(true);
        gameOverScreen.SetActive(true);
        ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        ball.GetComponent<SphereCollider>().enabled = false;
    }
    public void SetCash()
    {
        moneyText.GetComponent<TextMeshProUGUI>().text = money.ToString();
        PlayerPrefs.SetInt("Cash", money);
    }
    public int GetCash()
    {
        return PlayerPrefs.GetInt("Cash");
    }
    public void SetCurrentLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
    }
    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel");
    }
    public void SetDeathCount()
    {
        PlayerPrefs.SetInt("Death", deathCount);
    }
    public int GetDeathCount()
    {
        return PlayerPrefs.GetInt("Death");
    }
    void LevelCreate()
    {
        placeTransform.y += 17;
        for (int i = 0; i < Random.Range(4, 10); i++)
        {
            Instantiate(chunks[Random.Range(0, chunks.Count)], placeTransform, Quaternion.identity);
            placeTransform.y += Random.Range(7, 13);
        }
        Instantiate(finish, placeTransform, Quaternion.identity);
    }
    public void ReloadScene()
    {
        ball.GetComponent<BallMove>().SetSoundandVibration();
        SceneManager.LoadScene("Main");
    }

    public void Rewarded()
    {
        
        Advertising.ShowRewardedAd();
        
    }
    private void OnEnable()
    {
        Advertising.RewardedAdCompleted += Reward;
    }
    private void OnDisable()
    {
        Advertising.RewardedAdCompleted -= Reward;
    }
    private void Reward(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        money += 50;
        SetCash();
    }
}
