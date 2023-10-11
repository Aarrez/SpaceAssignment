using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static UnityAction<int> AddScore;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AddScore += UpdateScore;
    }

    private void UpdateScore(int score) { }
}