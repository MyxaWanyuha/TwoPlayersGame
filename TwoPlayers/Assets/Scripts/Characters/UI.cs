using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Image[] hearts = new Image[3];
    [SerializeField] ConditionComponent conditionComponent;

    private void DrawHearts(int x)
    {
        for (int i = 0; i <= x-1; i++)
            hearts[i].gameObject.SetActive(true);
        for (int j = 2; j > x-1; j--)
            hearts[j].gameObject.SetActive(false);
    }

    void Update()
    {
        DrawHearts(conditionComponent.Health);
    }
}
