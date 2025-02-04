using System.Text;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour{
    public delegate void SetHealthBarValue(int current, int max);
    public static event SetHealthBarValue OnHealthBarValueChanged;
    
    public delegate void SetHungerBarValue(int current, int max);
    static event SetHungerBarValue OnHungerBarValueChanged;
    
    public delegate void SetThirstBarValue(int current, int max);
    static event SetThirstBarValue OnThirstBarValueChanged;

    [Header("HUD Settings")]
    public Image HealthBar;
    public Image HungerBar, ThirstBar;
    [Space] 
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hungerText, thirstText;
    
    private StringBuilder _sbHealthText = new("100"), _sbHungerText = new("100"), _sbThirstText = new("100");
    
    private void OnEnable(){
        OnHealthBarValueChanged += setHealthBarValue;
        OnHungerBarValueChanged += setHungerBarValue;
        OnThirstBarValueChanged += setThirstBarValue;
    }

    private void OnDisable(){
        OnHealthBarValueChanged -= setHealthBarValue;
        OnHungerBarValueChanged -= setHungerBarValue;
        OnThirstBarValueChanged -= setThirstBarValue;
    }
    
    void setHealthBarValue(int current, int max){
        _sbHealthText.Clear();
        
        _sbHealthText.Append(current.ToString());
        _sbHealthText.Append('/');
        _sbHealthText.Append(max.ToString());
        
        HealthBar.fillAmount = (float)current / max;
        healthText.text = _sbHealthText.ToString();
    }
    
    void setHungerBarValue(int current, int max){
        _sbHungerText.Clear();
        
        _sbHungerText.Append(current.ToString());
        _sbHungerText.Append('/');
        _sbHungerText.Append(max.ToString());
        
        HungerBar.fillAmount = (float)current / max;
        hungerText.text = _sbHungerText.ToString();
    }
    
    void setThirstBarValue(int current, int max){
        _sbThirstText.Clear();
        
        _sbThirstText.Append(current.ToString());
        _sbThirstText.Append('/');
        _sbThirstText.Append(max.ToString());
        
        ThirstBar.fillAmount = (float)current / max;
        thirstText.text = _sbThirstText.ToString();
    }

    public static void UpdateHealthBar(int current, int max){
        OnHealthBarValueChanged(current, max);
    }
    
    public static void UpdateHungerBar(int current, int max){
        OnHungerBarValueChanged(current, max);
    }
    
    public static void UpdateThirstBar(int current, int max){
        OnThirstBarValueChanged(current, max);
    }
}