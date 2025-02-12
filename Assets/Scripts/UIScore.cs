using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    
    private void OnEnable()
    {
        Coin.OnTriggerPlayer += SetScore;
    }

    private void OnDisable()
    {
        Coin.OnTriggerPlayer -= SetScore;
    }

    private void SetScore(int value)
    {
        int scoreValue = int.Parse(gameObject.GetComponent<Text>().text) + value;
         gameObject.GetComponent<Text>().text = scoreValue.ToString();
    }
}
