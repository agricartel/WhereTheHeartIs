using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public enum ItemType
    {
        NONE = 0,

        MAP_NODE_BUTTON
    }

    public GameObject mapNodePrefab;
    public GameObject mapNodeContainer;

    List<MapNodeController> nodeControllers;

    MapModel mapData;

	void Start ()
    {
        mapData = new MapModel();

        nodeControllers = new List<MapNodeController>();

        // Load the map data
        TextAsset mapXml = Resources.Load<TextAsset>("Data/Map");
        XmlDocument mapDoc = new XmlDocument();
        mapDoc.LoadXml(mapXml.text);

        mapData.Load(mapDoc);

        List<MapNode> allNodes = mapData.AllNodes;
        for (int i = allNodes.Count-1; i >= 0; i--)
        {
            MapNode node = allNodes[i];

            GameObject mapNodeObject = Instantiate(mapNodePrefab, mapNodeContainer.transform);
            MapNodeController nodeController = mapNodeObject.GetComponent<MapNodeController>();
            nodeController.nodeData = node;

            nodeControllers.Add(nodeController);
        }
    }
	
	void Update ()
    {
		
	}


    public static void OnClick(ItemType type, int data1 = -1, int data2 = -1)
    {

        switch (type)
        {
            case ItemType.MAP_NODE_BUTTON:
                {
                    NodeType nodeType = (NodeType)data1;
                    MiniGameType gameType = (MiniGameType)data2;

                    if (nodeType == NodeType.GAME)
                    {
                        GameController.instance.GoToMiniGame(gameType);
                    }

                }
                break;
        }
    }
}
