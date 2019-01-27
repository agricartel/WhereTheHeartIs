using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectGameController : MonoBehaviour, IMiniGame
{
    public TMP_Text goodText;
    public TMP_Text badText;


    public GameObject collectablePrefab;
    public GameObject collectableParent;

    int totalGoodObjects = 0;
    int badObjectThreashold = 3;

    int numGoodObjectsGotten = -1;
    int numBadObjectsGotten = -1;

    public Vector2 worldDimensions;

    List<Collectable> collectables = new List<Collectable>();

    public GameObject ControllerObject { get { return gameObject; } }

    public bool DidComplete()
    {
        return numGoodObjectsGotten >= totalGoodObjects;
    }

    public bool IsFinished()
    {
        return numGoodObjectsGotten >= totalGoodObjects || numBadObjectsGotten >= badObjectThreashold;
    }

    void Start ()
    {
        if(GameController.instance != null)
            GameController.instance.SetMinigame(this);

        for (int i = 0; i < Random.Range(7, 12); i++)
        {
            GameObject collectableObj = Instantiate(collectablePrefab, collectableParent.transform);
            collectableObj.transform.position = worldDimensions * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            Collectable collectable = collectableObj.GetComponent<Collectable>();
            collectable.goodObject = Random.Range(0f, 1f) < 0.6f;

            if (collectable.goodObject)
                totalGoodObjects++;

            collectables.Add(collectable);
        }

        numGoodObjectsGotten = 0;
        numBadObjectsGotten = 0;
	}
	
	void Update ()
    {

        goodText.text = numGoodObjectsGotten + "/" + totalGoodObjects;
        badText.text = numBadObjectsGotten + "/" + badObjectThreashold;

        for (int i = collectables.Count - 1; i >= 0; i--)
        {
            if (collectables[i] == null)
            {
                collectables.RemoveAt(i);
            }
            else
            {
                if (collectables[i].PlayerGotMe())
                {
                    if (collectables[i].goodObject)
                    {
                        numGoodObjectsGotten++;
                    }
                    else
                    {
                        numBadObjectsGotten++;
                    }

                    collectables.RemoveAt(i);
                }
            }
        }

	}
}
