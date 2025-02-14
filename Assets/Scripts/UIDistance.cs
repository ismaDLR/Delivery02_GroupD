using UnityEngine;
using UnityEngine.UI;

public class UIDistance : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += SetDistance;
    }

    private void OnDisable()
    {
        Coin.OnTriggerPlayer -= SetDistance;
    }

    private void SetDistance(int value)
    {
        int scoreValue = int.Parse(gameObject.GetComponent<Text>().text) + value;
        gameObject.GetComponent<Text>().text = scoreValue.ToString();
    }
}
