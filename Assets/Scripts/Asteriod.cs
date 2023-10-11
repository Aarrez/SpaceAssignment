using UnityEngine;

public class Asteriod : MonoBehaviour
{
    public void AsteriodTakeDamage()
    {
        ScoreManager.AddScore?.Invoke(1);
        Destroy(gameObject);
    }
}