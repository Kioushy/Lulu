using UnityEngine;
[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public Sprite avatar;
    [TextArea(2, 5)]
    public string text;
}
