using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI deadEnemyCountText;
    public TextMeshProUGUI playerHealthText;
    
    private void Start()
    {
        // 初始化 UI 元素
        if (deadEnemyCountText == null)
        {
            Debug.LogError("TextMeshProUGUI element not assigned!");
        }
        else
        {
            UpdateDeadEnemyCountText();
        }
        if (playerHealthText == null)
        {
            Debug.LogError("TextMeshProUGUI element not assigned!");
        }
        else
        {
            UpdatePlayerHealthText();
        }
    }

    private void Update()
    {
        UpdatePlayerHealthText();
    }

    public void UpdateDeadEnemyCountText()
    {
        // 更新死亡敌人数量文本
        deadEnemyCountText.text = "Dead Enemies: " + EnemyHealth.deadEnemyCount.ToString();
    }
    public void UpdatePlayerHealthText()
    {
        // 更新玩家血量文本
        playerHealthText.text = "Player Health: " + CharacterControler.health.ToString();
    }
}