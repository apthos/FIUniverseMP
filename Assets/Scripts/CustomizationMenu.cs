using System.Collections;
using System.Collections.Generic;
using _Scripts;
using SharedLibrary;
using static System.Math;
using UnityEngine.UI;
using UnityEngine;

public class CustomizationMenu : MonoBehaviour
{
    public GameObject data;
    public string username;
    
    // GameObject to be customized for the menu
    public GameObject avatar;

    // Active customization GameObjects
    public GameObject head;
    public GameObject face;
    public GameObject hairStyle;

    // GameObjects and Materials for customization
    public GameObject[] heads;
    public Material[] skinTones;
    public GameObject[] facesMale;
    public GameObject[] facesFemale;
    public GameObject[] hairStylesMale;
    public GameObject[] hairStylesFemale;
    public Material[] hairColors;

    // Player Input
    int headSelection;
    int skinToneSelection;
    int faceSelection;
    int hairStyleSelection;
    int hairColorSelection;
    Selections selectedCharacteristic;

    // Buttons
    public GameObject headButton;
    public GameObject skinToneButton;
    public GameObject faceButton;
    public GameObject hairStyleButton;
    public GameObject hairColorButton;

    // Private variables
    private const int NUM_OF_HAIR_COLORS = 5;
    private const int NUM_OF_FACES = 17;
    private const int NUM_OF_HEADS = 2;
    private const int NUM_OF_SKIN_TONES = 2;
    private const int NUM_OF_HAIR_STYLES = 16;
    public enum Selections
    {
        HEAD,
        SKIN,
        FACE,
        HAIR,
        HAIR_COLOR
    }


    // Start is called before the first frame update
    void Start()
    {
        LoadPreferences();
        UpdateAvatar();
        data = GameObject.Find("Data");
        username = data.GetComponent<HttpManager>().username;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    /************************************************************
                   AVATAR GAME OBJECT CUSTOMIZATION
     ************************************************************/

    // Updates head shape. Also, face & hair style change since they are each fitted to a specific head shape
    public void UpdateHead()
    {   
        if (head) Destroy(head);
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
        Destroy(hairStyle);
        if (headSelection == 0)
        {
            hairStyle = Instantiate(hairStylesMale[hairStyleSelection], avatar.transform);
        }
        else
        {
            hairStyle = Instantiate(hairStylesFemale[hairStyleSelection], avatar.transform);
        }
        hairStyle.GetComponent<Renderer>().material = hairColors[hairColorSelection];
    }

    // Updates the avatar displayed in the customization menu
    public void UpdateAvatar()
    {
        UpdateHead();
        UpdateSkinTone();
    }


    /************************************************************
                            PLAYER PREFERENCES
    ************************************************************/

    // Updates the player preference for a specified avatar characteristic 
    public void ChangePreference(string characteristic, int value)
    {
        PlayerPrefs.SetInt(characteristic, value);
    }

    // Loads the player preferences for the avatar
    public void LoadPreferences()
    {
        headSelection = PlayerPrefs.GetInt(AvatarTypes.HEAD, 0);
        skinToneSelection = PlayerPrefs.GetInt(AvatarTypes.SKIN, 0);
        faceSelection = PlayerPrefs.GetInt(AvatarTypes.FACE, 0);
        hairStyleSelection = PlayerPrefs.GetInt(AvatarTypes.HAIR, 0);
        hairColorSelection = PlayerPrefs.GetInt(AvatarTypes.HAIR_COLOR, 0);
    }

    public void SavePreferences()
    {
        PlayerPrefs.SetInt(AvatarTypes.HEAD, headSelection);
        PlayerPrefs.SetInt(AvatarTypes.SKIN, skinToneSelection);
        PlayerPrefs.SetInt(AvatarTypes.FACE, faceSelection);
        PlayerPrefs.SetInt(AvatarTypes.HAIR, hairStyleSelection);
        PlayerPrefs.SetInt(AvatarTypes.HAIR_COLOR, hairColorSelection);
    }

    // Method in case customization will be saved onto a database per account
    public void SavePreferencesNetwork()
    {
        SavePreferences();
        Debug.Log(faceSelection);
        HttpClient.UpdatePreferences<User>(username, headSelection, skinToneSelection, faceSelection, hairStyleSelection, hairColorSelection);
        Debug.Log("In saver preferences");
    }


    /************************************************************
                                USER INPUT
    ************************************************************/
    public void ChangeSelectedCharacteristic(int characteristic)
    {
        switch (selectedCharacteristic)
        {
            case Selections.HEAD:
                headButton.GetComponent<Button>().image.color = Color.white;
                break;
            case Selections.SKIN:
                skinToneButton.GetComponent<Button>().image.color = Color.white;
                break;
            case Selections.FACE:
                faceButton.GetComponent<Button>().image.color = Color.white;
                break;
            case Selections.HAIR:
                hairStyleButton.GetComponent<Button>().image.color = Color.white;
                break;
            case Selections.HAIR_COLOR:
                hairColorButton.GetComponent<Button>().image.color = Color.white;
                break;
            default:
                break;
        }

        selectedCharacteristic = (Selections) characteristic;

        switch (selectedCharacteristic)
        {
            case Selections.HEAD:
                headButton.GetComponent<Button>().image.color = Color.gray;
                break;
            case Selections.SKIN:
                skinToneButton.GetComponent<Button>().image.color = Color.gray;
                break;
            case Selections.FACE:
                faceButton.GetComponent<Button>().image.color = Color.gray;
                break;
            case Selections.HAIR:
                hairStyleButton.GetComponent<Button>().image.color = Color.gray;
                break;
            case Selections.HAIR_COLOR:
                hairColorButton.GetComponent<Button>().image.color = Color.gray;
                break;
            default:
                break;
        }
    }

    public void ShiftSelection(int value)
    {
        switch(selectedCharacteristic)
        {
            case Selections.HEAD:
                headSelection = mod((headSelection + value), NUM_OF_HEADS);
                break;
            case Selections.SKIN:
                skinToneSelection = mod((skinToneSelection + value), NUM_OF_SKIN_TONES);
                break;
            case Selections.FACE:
                faceSelection = mod((faceSelection + value), NUM_OF_FACES);
                break;
            case Selections.HAIR:
                hairStyleSelection = mod((hairStyleSelection + value), NUM_OF_HAIR_STYLES);
                break;
            case Selections.HAIR_COLOR:
                hairColorSelection = mod((hairColorSelection + value), NUM_OF_HAIR_COLORS);
                break;
            default:
                break;
        }
    }

    int mod(int a, int b)
    {
        return (Abs(a * b) + a) % b;
 }
}
