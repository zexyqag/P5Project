using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Public fields

	public List<Transform> playerVariants;		// A list for storing the two different player variants (HeadHand && RayCast)
	public List<Vector2> keyboards;				// A list for storing the two different keyboard size perameters (x = Size, y = Spacing)

	public bool StartWithHeadHand;				// Boolean used for determaning if the test starts with HeadHand

	#endregion

	#region Private fields
	private Transform currentPlayerVariant;																							// The player veriant curently existing in the scene

	private List<KeyValuePair<Transform, Vector2>> PlayerAndKeyboardVariantPairs = new List<KeyValuePair<Transform, Vector2>>();	// A list of keyvalue pairs, storing the correct keyboard for a specific player variant

	private bool isBothPlayerVeriantsTestet = false;                                                                                // bool is true if both player imput types has bin testet
    #endregion

    #region Unity Functions
    private void Start() 
	{		
		EventSystem.onSwtichInputMethod += SwitchPlayerVariants;																// Subscripes to the switch input Event

		for (int i = 0; i < playerVariants.Count; i++)																	// Iterates for each player variant (2)
		{
			PlayerAndKeyboardVariantPairs.Add(new KeyValuePair<Transform, Vector2>(playerVariants[i], keyboards[i]));	// Adds a key value pair with matching variant and keyboard to the key value list
		}

		SetupPlayerAndKeyboard(PlayerPrefs.GetInt("StartWithHeadHand"));												// Player prefs is used for saving data (a boolean in this case) outside of runtime
		ReversePlayerPrefBoolean();																						// Switches the state of the player pref int, thier by changeching the nex player variant
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))	// If R is pressed on the keyboard
        {
			ReversePlayerPrefBoolean();		// Switch the next player variant
			SceneManager.LoadScene(0);		// Load the start scene
        }
    }
	#endregion

	void ReversePlayerPrefBoolean()
    {
		PlayerPrefs.SetInt("StartWithHeadHand", PlayerPrefs.GetInt("StartWithHeadHand") == 1 ? 0 : 1); // switches the state of StartWithHeadHand between 1 and 0
	}

	public void SwitchPlayerVariants()
    {
        if (isBothPlayerVeriantsTestet)										// If both player variants are testet, retun and do nothing else in this function
			return;

		Destroy(currentPlayerVariant.gameObject);							// Destroys the current, and at this point, completet player variant
		SetupPlayerAndKeyboard(PlayerPrefs.GetInt("StartWithHeadHand"));	// Call the next setup based on witch playervariant is next
		isBothPlayerVeriantsTestet = true;									
	}

	private void SetupPlayerAndKeyboard(int element)
	{ // The discreat parameter integer of this function represents the Player Variant type (HeadHand or Raycast)                                                                                  

		EventSystem.onTestType(PlayerAndKeyboardVariantPairs[element].Key.name);		// Invokes the event onTestType, witch takes a string, in this case the name of the Player Variant type

		currentPlayerVariant = Instantiate(PlayerAndKeyboardVariantPairs[element].Key); // Instansiate the correct player variant prefap and sets currentPlayerVariant to this variant

		KeyboardPlacer KP = this.GetComponent<KeyboardPlacer>();						// Gets the Keyboard placer componant (Script) and defines it as KP for ease of reference
		KP.Size = PlayerAndKeyboardVariantPairs[element].Value.x;						// Sets the Size of KP to the corect float, defined in the Editor
		KP.Spacing = PlayerAndKeyboardVariantPairs[element].Value.y;                    // Sets the Spacinng of KP to the corect float, defined in the Editor
		KP.Place();																		// Calls the place unction in KP
	}
}
