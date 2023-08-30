using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial", menuName = "ScriptableObjects/Tutorial", order = 1)]
public class Tutorial : ScriptableObject
{
    public string PTName;
    public string ENName;
    public Sprite[] PTtutorialImage;
    public Sprite[] ENtutorialImage;
    public string[] PTtutorialText;
    public string[] ENtutorialText;
    
}
