using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{
    public UnityEvent<int> MoneyValueChanged;
    public UnityEvent<int> GemsValueChanged;

    private int _money;
    private int _gems;

    public void Initialize(int money, int gems)
    {
        SetMoney(money);
        SetGems(gems);
    }

    private void SetMoney(int value)
    {
        _money = value;
        MoneyValueChanged.Invoke(_money);
    }

    private void SetGems(int value)
    {
        _gems = value;
        GemsValueChanged.Invoke(_gems);
    }
}