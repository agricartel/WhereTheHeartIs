using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MapNodeController : MonoBehaviour
{

    public Sprite complete;
    public Sprite uncomplete;

    public Sprite knownConnection;
    public Sprite unknownConnection;

    public GameObject connector;
    public Button buttonObj;

    Image myImage;
    Image connectorImage;

    RectTransform rectTransform;

    public MapNode nodeData;

	void Start ()
    {
        myImage = buttonObj.image;
        rectTransform = GetComponent<RectTransform>();

        connectorImage = connector.GetComponent<Image>();

        buttonObj.onClick.AddListener(() => MapController.OnClick(MapController.ItemType.MAP_NODE_BUTTON, (int)nodeData.type, (int)nodeData.gameType, nodeData.id));
	}

    void Update ()
    {
        if (nodeData == null)
            return;

        rectTransform.localPosition = nodeData.position;

        myImage.sprite = nodeData.completed ? complete : uncomplete;

        connector.SetActive(nodeData.next != null);

        if (nodeData.next != null)
        {
            connectorImage.sprite = nodeData.next.revealed ? knownConnection : unknownConnection;

            Vector2 midpoint = (nodeData.position + nodeData.next.position)/2;
            float dist = Vector2.Distance(nodeData.position, nodeData.next.position);
            float angle = Mathf.Atan2(nodeData.next.position.y - nodeData.position.y, nodeData.next.position.x - nodeData.position.x) * Mathf.Rad2Deg;

            //connector.transform.position = midpoint;
            RectTransform connectorTransform = connector.GetComponent<RectTransform>();
            connectorTransform.sizeDelta = new Vector2(dist, 20);
            connectorTransform.rotation = Quaternion.Euler(0, 0, angle);
            connectorTransform.localPosition = nodeData.next.position - midpoint;
        }


	}
}
