using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private List<GameObject> healthObjects = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            healthObjects.Add(transform.GetChild(i).gameObject);
        }
    }

    public void LoseHealth()
    {
        for (var i = healthObjects.Count -1; i > -1; i--)
        {
            if (!healthObjects[i].GetComponent<HealthObject>().isFull)
            {
                continue;
            }


            healthObjects[i].GetComponent<HealthObject>().UiLoseHealth();
            if (i == 0)
            {
                //Last Health, do something epic
            }
            break;
        }
    }
}
