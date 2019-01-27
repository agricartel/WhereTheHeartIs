using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vector2 = UnityEngine.Vector2;

public enum NodeType
{
    START,
    GAME,
    CUT_SCENE,
    END
}

public class MapNode
{
    public string id;
    public NodeType type;
    public MiniGameType gameType;
    public string cutScene;
    public Vector2 position;
    public bool completed;
    public bool revealed;
    public MapNode next;
}

public class MapModel
{
    Dictionary<string, MapNode> nodes;

    public List<MapNode> AllNodes { get { return nodes.Values.ToList(); } }

    public MapModel()
    {
        nodes = new Dictionary<string, MapNode>();
    }

    public void SetCompleteAndRevealNext(string id)
    {
        if (nodes.ContainsKey(id))
        {
            nodes[id].completed = true;
            if (nodes[id].next != null)
                nodes[id].next.revealed = true;
        }
    }

    public MapNode GetNode(string id)
    {
        if (nodes.ContainsKey(id))
            return nodes[id];

        return null;
    }

    public void Load(XmlDocument doc)
    {
        XmlNode root = doc.DocumentElement;

        // read nodes in backwards so the NextNodeID attribute exists 
        for (int i = root.ChildNodes.Count-1; i >= 0; i--)
        {
            XmlNode node = root.ChildNodes[i];

            MapNode mapNode = new MapNode();

            foreach (XmlAttribute attr in node.Attributes)
            {
                if (!string.IsNullOrEmpty(attr.Value))
                {
                    switch (attr.Name)
                    {
                        case "ID":
                            mapNode.id = attr.Value;
                            nodes[attr.Value] = mapNode;
                            break;
                        case "NodeType":
                            mapNode.type = (NodeType)Enum.Parse(typeof(NodeType), attr.Value);
                            break;
                        case "GameType":
                            mapNode.gameType = (MiniGameType)Enum.Parse(typeof(MiniGameType), attr.Value);
                            break;
                        case "CutSceneType":
                            mapNode.cutScene = attr.Value;
                            break;
                        case "yPos":
                            mapNode.position.y = int.Parse(attr.Value);
                            break;
                        case "xPos":
                            mapNode.position.x = int.Parse(attr.Value);
                            break;
                        case "NextNodeID":
                            mapNode.next = nodes[attr.Value];
                            break;
                    }
                }
            }

            if (mapNode.type == NodeType.START)
            {
                mapNode.revealed = true;
                //mapNode.next.revealed = true;
            }
        }
    }

}
