using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationMenu : MonoBehaviour
{
    // GameObject to be customized for the menu
    public GameObject avatar;

    // Active customization GameObjects
    public GameObject head;
    public GameObject face;
    public GameObject hairStyle;
    public GameObject hairColor;

    // GameObjects and Materials for customization
    public GameObject[] heads;
    public Material[] skinTones;
    public GameObject[] facesMale;
    public GameObject[] facesFemale;
    public GameObject[] hairStylesMale;
    public GameObject[] hairStylesFemale;
    public Material[] hairColors;

    // Player Input
    public int headSelection;
    public int skinToneSelection;
    public int faceSelection;
    public int hairStyleSelection;
    public int hairColorSelection;

    // Private variables
    private const int NUM_OF_HAIR_COLORS = 5;
    private const int NUM_OF_HAIR_STYLES = 17;

    // Start is called before the first frame update
    void Start()
    {
        LoadPreferences();
        UpdateAvatar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Updates head shape. Also, face & hair style change since they are each fitted to a specific head shape
    public void UpdateHead()
    {
        Destroy(head);
        head = Instantiate(heads[headSelection], avatar.transform);
        UpdateFace();
        UpdateHair();
    }

    // Updates skin tone of head component
    public void UpdateSkinTone()
    {
        head.GetComponent<Renderer>().material = skinTones[skinToneSelection];
        // Hands will change color too
    }

    // Updates face & eyebrow color
    public void UpdateFace()
    {
        Destroy(face);
        if (headSelection == 0)
        {
            face = Instantiate(facesMale[faceSelection * NUM_OF_HAIR_COLORS + hairColorSelection], avatar.transform);
        }
        else
        {
            face = Instantiate(facesFemale[faceSelection * NUM_OF_HAIR_COLORS + hairColorSelection], avatar.transform);
        }
    }

    // Updates hair style & color
    public void UpdateHair()
    {
        if (headSelection == 0)
        {

        }
        else
        {

        }
    }

    public void UpdateAvatar()
    {
        UpdateHead();
        UpdateSkinTone();
    }

    public void ChangePreference(string characteristic, int value)
    {
        PlayerPrefs.SetInt(characteristic, value);
    }

    public void LoadPreferences()
    {
        headSelection = PlayerPrefs.GetInt(AvatarTypes.HEAD, 0);
        skinToneSelection = PlayerPrefs.GetInt(AvatarTypes.SKIN, 0);
        faceSelection = PlayerPrefs.GetInt(AvatarTypes.FACE, 0);
        hairStyleSelection = PlayerPrefs.GetInt(AvatarTypes.HAIR, 0);
        hairColorSelection = PlayerPrefs.GetInt(AvatarTypes.HAIR_COLOR, 0);
    }

    // Method in case customization will be saved onto a database per account
    public void SavePreferences()
    {
        PlayerPrefs.Save();
    }
}
