using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour, IMiniGame
{
    public static CutSceneController instance;

    public GameObject ControllerObject { get { return gameObject; } }

    public enum Action
    {
        NONE = 0,

        BOUNCE,
        WALK_CENTER,
        WALK_OFF_LEFT,
        WALK_OFF_RIGHT,
        WALK_ON_LEFT,
        WALK_ON_RIGHT,
        WALK_LEFT,
        WALK_RIGHT,
        HIDE,

        NUM_ACTIONS
    }

    public enum BubbleDirection
    {
        LEFT,
        RIGHT
    }

    // Scene References
    public SpriteRenderer backgroundImage;
    public GameObject leftCharacter;
    public SpriteRenderer leftCharacterImage;
    public GameObject rightCharacter;
    public SpriteRenderer rightCharacterImage;
    public SpeechBubbleController speechBubble;


    // Data
    XmlDocument cutSceneDoc;

    int currentSection;
    Action leftCharacterAction;
    Action rightCharacterAction;

    bool doneCutScene = false;
    float sectionTime = 0;
    float time = 0;

    Vector2 leftCharacterLocation = new Vector2(-6, -1);
    Vector2 rightCharacterLocation = new Vector2(6, -1);


    public bool isTesting = false;
    public string testingFile = "";
    public void OnEnable()
    {
        ResetCutScene();
        instance = this;
        if (isTesting && !string.IsNullOrEmpty(testingFile))
        {
            SetCutScene(testingFile);
        }
    }

    public void OnDestroy()
    {
        instance = null;
    }

    public bool IsFinished()
    {
        return doneCutScene;
    }

    public bool DidComplete()
    {
        return doneCutScene;
    }

    public void ResetCutScene()
    {
        doneCutScene = false;
        currentSection = 0;
        time = 0;
    }

    public void SetCutScene(string file)
    {
        TextAsset xmlAsset = Resources.Load<TextAsset>(file);
        cutSceneDoc = new XmlDocument();
        cutSceneDoc.LoadXml(xmlAsset.text);

        ResetCutScene();

        PlayCurrentSection();
    }

    public void PlayCurrentSection()
    {
        if (cutSceneDoc == null)
            return;

        XmlNode root = cutSceneDoc.DocumentElement;

        if (currentSection >= 0 && currentSection < root.ChildNodes.Count)
        {
            foreach (XmlNode node in root.ChildNodes[currentSection])
            {
                switch (node.Name)
                {
                    case "time":
                        sectionTime = time = float.Parse(node.InnerText);
                        break;
                    case "backgroundImage":
                        backgroundImage.sprite = Resources.Load<Sprite>(node.InnerText);
                        break;
                    case "leftCharacter":
                        LoadCharacter(node, ref leftCharacterImage, ref leftCharacterAction);
                        break;
                    case "rightCharacter":
                        LoadCharacter(node, ref rightCharacterImage, ref rightCharacterAction);
                        break;
                    case "speechBubble":
                        LoadSpeechBubble(node, ref speechBubble);
                        break;
                }
            }
        }
        else
        {
            doneCutScene = true;
        }
    }

    public void LoadCharacter(XmlNode node, ref SpriteRenderer characterImage, ref Action characterAction)
    {
        foreach (XmlNode child in node.ChildNodes)
        {
            switch (child.Name)
            {
                case "image":
                    characterImage.sprite = Resources.Load<Sprite>(child.InnerText);
                    break;
                case "action":
                    characterAction = (Action)System.Enum.Parse(typeof(Action), child.InnerText);
                    break;
            }
        }
    }

    public void LoadSpeechBubble(XmlNode node, ref SpeechBubbleController bubble)
    {
        foreach (XmlNode child in node.ChildNodes)
        {
            switch (child.Name)
            {
                case "text":
                    bubble.gameObject.SetActive(!string.IsNullOrEmpty(child.InnerText));
                    bubble.text = child.InnerText;
                    break;
                case "direction":
                    bubble.flipBubble = (BubbleDirection)System.Enum.Parse(typeof(BubbleDirection), child.InnerText) == BubbleDirection.RIGHT;
                    break;
            }
        }
    }

    void Update ()
    {
        if (doneCutScene)
            return;

        if (time <= 0)
        {
            // next section
            currentSection++;
            PlayCurrentSection();
        }
        else
        {
            time -= Time.deltaTime;

            float percentDone = sectionTime <= 0 ? 0 : (sectionTime - time) / sectionTime;

            // execute actions here
            ExecuteCharacterAction(leftCharacter, leftCharacterAction, leftCharacterLocation, percentDone);
            ExecuteCharacterAction(rightCharacter, rightCharacterAction, rightCharacterLocation, percentDone);

        }
	}


    private void ExecuteCharacterAction(GameObject characterObj, Action characterAction, Vector2 characterNormalPosition, float percentDone)
    {
        characterObj.SetActive(true);
        switch (characterAction)
        {
            case Action.NONE:
                return;
            case Action.BOUNCE:
                characterObj.transform.position = characterNormalPosition + Vector2.up * Mathf.Sin(percentDone * Mathf.PI);
                break;
            case Action.WALK_CENTER:
                characterObj.transform.position = Vector2.Lerp(characterNormalPosition, new Vector2(0, characterNormalPosition.y), percentDone);
                break;
            case Action.WALK_OFF_LEFT:
                characterObj.transform.position = Vector2.Lerp(characterNormalPosition, new Vector2(-12, characterNormalPosition.y), percentDone);
                break;
            case Action.WALK_OFF_RIGHT:
                characterObj.transform.position = Vector2.Lerp(characterNormalPosition, new Vector2(12, characterNormalPosition.y), percentDone);
                break;
            case Action.WALK_ON_LEFT:
                characterObj.transform.position = Vector2.Lerp(new Vector2(-12, characterNormalPosition.y), characterNormalPosition, percentDone);
                break;
            case Action.WALK_ON_RIGHT:
                characterObj.transform.position = Vector2.Lerp(new Vector2(12, characterNormalPosition.y), characterNormalPosition, percentDone);
                break;
            case Action.WALK_RIGHT:
                characterObj.transform.position = Vector2.Lerp(characterObj.transform.position, rightCharacterLocation, percentDone);
                break;
            case Action.WALK_LEFT:
                characterObj.transform.position = Vector2.Lerp(characterObj.transform.position, leftCharacterLocation, percentDone);
                break;
            case Action.HIDE:
                characterObj.SetActive(false);
                break;
        }
    }
}
