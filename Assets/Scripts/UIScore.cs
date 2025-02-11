using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        Coin.OnTriggerPlayer += SetScore;
    }

    private void OnDisable()
    {
        Coin.OnTriggerPlayer -= SetScore;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetScore(int value)
    {
        int scoreValue = int.Parse(gameObject.GetComponent<Text>().text) + value;
         gameObject.GetComponent<Text>().text = scoreValue.ToString();
    }
}
