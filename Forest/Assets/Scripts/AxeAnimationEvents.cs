using UnityEngine;

public class AxeAnimationEvents : MonoBehaviour
{
    public static AxeAnimationEvents Instance { set; get; }

    AxeThrow _axeThrow;

    public GameObject axeHips;      // Axe In Hips Rig
    public GameObject axeRightHand; // Axe In Right Hand Rig

    public bool axeTaken;           // Axe In Right Hand Rig is Activate Control
    public bool axeAnimPlaying;     // Axe animation playing control

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _axeThrow = AxeThrow.Instance;

        axeTaken = false;
        axeAnimPlaying= false;

        axeRightHand.SetActive(false);
        axeHips.SetActive(true);
    }

    // Changing Active Axe
    public void EquipAxe()
    {
        axeHips.SetActive(false);
        axeRightHand.SetActive(true);
        axeTaken = true;
    }

    public void UnEquipAxe()
    {
        axeRightHand.SetActive(false);
        axeHips.SetActive(true);
        axeTaken = false;
    }

    // Character will not move while the EquipAxe animation is playing.
    public void StopMove()
    {
        axeAnimPlaying = true;
    }

    // Character can move again.
    public void StartMove()
    {
        axeAnimPlaying = false;
    }

    public void AxeThrowOnAnim()
    {
        _axeThrow.ThrowAxe();
    }










}
