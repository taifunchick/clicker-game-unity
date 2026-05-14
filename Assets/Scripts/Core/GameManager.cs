using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI crystalsText;
    public Slider levelSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levCountText;

    private int coins = 0;
    private int crystals = 0;
    private int level = 1;
    private int clicksForNextLevel = 100;

    private int clickPower = 1;
    private int autoClickLevel = 0;
    private bool x2BonusActive = false;
    private float x2BonusTimer = 0f;

    private int baseClickPowerPrice = 10;
    private int baseAutoClickerPrice = 200;
    private int baseX2BonusPrice = 10;

    void Start()
    {
        LoadData();
        UpdateUI();

        if (autoClickLevel > 0)
        {
            InvokeRepeating("AutoClick", 1f, 1f);
        }
    }

    void Update()
    {
        if (x2BonusActive)
        {
            x2BonusTimer -= Time.deltaTime;
            if (x2BonusTimer <= 0)
            {
                x2BonusActive = false;
            }
        }
    }

    public void OnCoinClicked()
    {
        int reward = clickPower;
        if (x2BonusActive) reward *= 2;

        AddCoins(reward);
    }

    public void BuyClickPower()
    {
        int price = GetClickPowerPrice();
        if (coins >= price)
        {
            coins -= price;
            clickPower++;
            Save();
            UpdateUI();
        }
    }

    public void BuyAutoClicker()
    {
        int price = GetAutoClickerPrice();
        if (coins >= price)
        {
            coins -= price;
            autoClickLevel++;

            if (autoClickLevel == 1)
            {
                InvokeRepeating("AutoClick", 1f, 1f);
            }

            Save();
            UpdateUI();
        }
    }

    public void BuyX2Bonus()
    {
        int price = GetX2BonusPrice();
        if (crystals >= price)
        {
            crystals -= price;
            x2BonusActive = true;
            x2BonusTimer = 30f;
            PlayerPrefs.SetInt("X2Uses", PlayerPrefs.GetInt("X2Uses", 0) + 1);
            Save();
            UpdateUI();
        }
    }

    void AutoClick()
    {
        int reward = 2 * autoClickLevel;
        if (x2BonusActive) reward *= 2;
        AddCoins(reward);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        CheckLevelUp();
        UpdateUI();
        Save();
    }

    void CheckLevelUp()
    {
        while (coins >= clicksForNextLevel)
        {
            coins -= clicksForNextLevel;

            level++;
            crystals += 20;
            clicksForNextLevel *= 2;
            PlayerPrefs.SetInt("Level", level);
        }
    }

    public int GetClickPowerPrice()
    {
        return baseClickPowerPrice + ((clickPower - 1) * 15);
    }

    public int GetAutoClickerPrice()
    {
        return baseAutoClickerPrice + (autoClickLevel * 100);
    }

    public int GetX2BonusPrice()
    {
        return baseX2BonusPrice + (PlayerPrefs.GetInt("X2Uses", 0) * 2);
    }

    void UpdateUI()
    {
        coinsText.text = coins.ToString("N0");
        crystalsText.text = crystals.ToString();

        float progress = (float)coins / clicksForNextLevel;
        levelSlider.value = Mathf.Clamp01(progress);

        levelText.text = $"Before level {level + 1}:";
        levCountText.text = $"{coins:N0} / {clicksForNextLevel:N0} coins";
    }

    void LoadData()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        crystals = PlayerPrefs.GetInt("Crystals", 25);
        level = PlayerPrefs.GetInt("Level", 1);
        clickPower = PlayerPrefs.GetInt("ClickPower", 1);
        autoClickLevel = PlayerPrefs.GetInt("AutoClicker", 0);
        clicksForNextLevel = PlayerPrefs.GetInt("ClicksForNextLevel", 100);
    }

    void Save()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("Crystals", crystals);
        PlayerPrefs.SetInt("ClickPower", clickPower);
        PlayerPrefs.SetInt("AutoClicker", autoClickLevel);
        PlayerPrefs.SetInt("ClicksForNextLevel", clicksForNextLevel);
        PlayerPrefs.Save();
    }
}