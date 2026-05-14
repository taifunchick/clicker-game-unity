using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameManager gameManager;

    public Button buyClickPowerBtn;
    public Button buyAutoClickerBtn;
    public Button buyX2BonusBtn;

    public TextMeshProUGUI clickPowerPriceText;
    public TextMeshProUGUI autoClickerPriceText;
    public TextMeshProUGUI x2BonusPriceText;

    void Start()
    {
        buyClickPowerBtn.onClick.AddListener(gameManager.BuyClickPower);
        buyAutoClickerBtn.onClick.AddListener(gameManager.BuyAutoClicker);
        buyX2BonusBtn.onClick.AddListener(gameManager.BuyX2Bonus);

        UpdatePrices();
    }

    void Update()
    {
        UpdatePrices();
    }

    void UpdatePrices()
    {
        clickPowerPriceText.text = $"{gameManager.GetClickPowerPrice()}";
        autoClickerPriceText.text = $"{gameManager.GetAutoClickerPrice()}";
        x2BonusPriceText.text = $"{gameManager.GetX2BonusPrice()}";
    }
}