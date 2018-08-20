using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerAttributes PlayerAttributes { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerAudioManager PlayerAudioManager { get; private set; }
    public PlayerAnimationManager PlayerAnimationManager { get; private set; }

    private void Awake()
    {

  
        PlayerAnimationManager = GetComponentInChildren<PlayerAnimationManager>();
        PlayerAnimationManager.SetManager(this);

        PlayerAttributes = GetComponent<PlayerAttributes>();
        PlayerAttributes.SetManager(this);

        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerMovement.SetManager(this);

        PlayerAudioManager = GetComponentInChildren<PlayerAudioManager>();
        PlayerAudioManager.SetManager(this);
    }
}