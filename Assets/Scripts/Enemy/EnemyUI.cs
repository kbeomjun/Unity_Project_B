using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    public void Init(float currentHealth, float maxHealth)
    {
        UpdateHealth(currentHealth, maxHealth);
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }

}
