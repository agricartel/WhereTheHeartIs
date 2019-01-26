using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController instance;

    public enum ItemType
    {
        NONE = 0,

        MAP_NODE_BUTTON
    }

    public GameObject mapNodePrefab;
    public GameObject mapNodeContainer;
    public Camera mapCamera;

    List<MapNodeController> nodeControllers;

    MapModel mapData;

    private void OnEnable()
    {
        instance = this;
    }

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

    private void Update()
    {
        foreach(MapNodeController node in nodeControllers)
        {
            node.gameObject.SetActive(node.nodeData.revealed);
        }
    }

    public void CompleteNode(string id)
    {
        mapData.SetCompleteAndRevealNext(id);
    }

    public void OnClick(ItemType type, int data1 = -1, int data2 = -1, string data3 = null)
    {

        switch (type)
        {
            case ItemType.MAP_NODE_BUTTON:
                {
                    NodeType nodeType = (NodeType)data1;
                    MiniGameType gameType = (MiniGameType)data2;

                    switch(nodeType)
                    {
                        case NodeType.START:
                            {
                                GameController.instance.RunCutScene("Data/CutScenes/CutSceneTest", data3);
                            }
                            break;
                        case NodeType.CUT_SCENE:
                            {
                                GameController.instance.RunCutScene(mapData.GetNode(data3).cutScene, data3);
                            }
                            break;
                        case NodeType.GAME:
                            {
                                GameController.instance.GoToMiniGame(gameType, data3);
                            }
                            break;
                    }

                }
                break;
        }
    }
}
