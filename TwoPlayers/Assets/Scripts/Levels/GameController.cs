using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int points = 0;
    int maxPoints = 0;
    [SerializeField] Text score;
    public float timer;
    public bool isGameFinished = false;
    public int GetPoints() => points;
    public void AddPoints(int p) 
    {
        points += p;
        score.text = string.Format("{0}/{1}", points,maxPoints);
    } 
    public int GetMaxPoints() => maxPoints;

    static GameController instance;
    public static GameController GetInstance() => instance;
    private void Update()
    {
        //if (!isGameFinished)
            timer += Time.deltaTime;
    }
    void Start()
    {
        instance = this;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        maxPoints += enemies * 10;

        var coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach(var e in coins)
        {
            maxPoints += e.GetComponent<PickUpCoin>().GetPoints();
        }

        var Chests = GameObject.FindGameObjectsWithTag("MainChest");
        foreach (var e in Chests)
        {
            var items = e.GetComponent<Chest>().PickUpItems;
            foreach (var i in items)
            {
                var coin = i.GetComponent<PickUpCoin>();
                if (coin)
                {
                    maxPoints += coin.GetPoints();
                }
            }
        }
        score.text = string.Format("0/{0}", maxPoints);
    }
}
