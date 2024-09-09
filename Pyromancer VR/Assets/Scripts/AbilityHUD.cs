using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class AbilityHUD : MonoBehaviour
{
    public PlayerAbilities leftHandController;
    public Image leftAbilityHUD;
    public PlayerAbilities rightHandController;
    public Image rightAbilityHUD;

    public Sprite leftFireBall;
    public Sprite leftFireWall;
    public Sprite leftFireStream;
    public Sprite rightFireBall;
    public Sprite rightFireWall;
    public Sprite rightFireStream;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHandController.abilityType == PlayerAbilities.AbilityType.FireBall) {
            leftAbilityHUD.sprite = leftFireBall;
        }
        else if (leftHandController.abilityType == PlayerAbilities.AbilityType.FireWall) {
            leftAbilityHUD.sprite = leftFireWall;
        }
        else if (leftHandController.abilityType == PlayerAbilities.AbilityType.FireStream) {
            leftAbilityHUD.sprite = leftFireStream;
        }
        if (rightHandController.abilityType == PlayerAbilities.AbilityType.FireBall) {
            rightAbilityHUD.sprite = rightFireBall;
        }
        else if (rightHandController.abilityType == PlayerAbilities.AbilityType.FireWall) {
            rightAbilityHUD.sprite = rightFireWall;
        }
        else if (rightHandController.abilityType == PlayerAbilities.AbilityType.FireStream) {
            rightAbilityHUD.sprite = rightFireStream;
        }
    }
}
