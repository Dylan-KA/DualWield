using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class WeaponHotbar : MonoBehaviour
{
    private PlayerCharacter player;
    private PostProcessVolume blur;
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject leftGun;
    [SerializeField] private GameObject rightGun;
    [SerializeField] private GameObject[] circleBackgroundSprites;
    [SerializeField] private GameObject[] circleBackgroundGreySprites;
    [SerializeField] private GameObject[] circleGunSprites;
    [SerializeField] private GameObject[] circleGlowSprites;
    [SerializeField] private GameObject[] leftGunSprites;
    [SerializeField] private GameObject[] rightGunSprites;
    private bool[] gunsUnlocked = { true, false, false, false };
    public bool[] GunsUnlocked { get => gunsUnlocked; }

    /// <summary>
    /// Checkpoint system should call this when the player spawns, so that they got the proper weapons
    /// </summary>
    /// <param name="hasWind"></param>
    /// <param name="hasFlame"></param>
    /// <param name="hasRocket"></param>
    /// <param name="hasFreeze"></param>
    public void SetUnlockedWeapons(bool[] newGunsUnlocked)
    {
        gunsUnlocked[0] = newGunsUnlocked[0];
        gunsUnlocked[1] = newGunsUnlocked[1];
        gunsUnlocked[2] = newGunsUnlocked[2];
        gunsUnlocked[3] = newGunsUnlocked[3];

        for (int i = 0; i < gunsUnlocked.Length; i++)
        {
            circleBackgroundSprites[i].SetActive(gunsUnlocked[i]);
            circleGlowSprites[i].SetActive(gunsUnlocked[i]);
            circleGunSprites[i].SetActive(gunsUnlocked[i]);
            circleBackgroundGreySprites[i].SetActive(!gunsUnlocked[i]);
        }
    }

    public void UnlockWeapon(WeaponType weaponType)
    {
        int gunIndex = (int)weaponType;
        circleBackgroundSprites[gunIndex].SetActive(true);
        circleGlowSprites[gunIndex].SetActive(true);
        circleGunSprites[gunIndex].SetActive(true);
        circleBackgroundGreySprites[gunIndex].SetActive(false);
        gunsUnlocked[gunIndex] = true;
    }

    void SetLeftWeaponUI()
    {
        WeaponType weaponType = GetSelectedGun();
        if (weaponType == WeaponType.None)
            weaponType = player.leftWeapon.GetWeaponType();

        for (int i = 0; i < leftGunSprites.Length; i++)
        {
            leftGunSprites[i].SetActive(i == (int)weaponType);
        }
    }

    void SetRightWeaponUI()
    {
        WeaponType weaponType = GetSelectedGun();
        if (weaponType == WeaponType.None)
            weaponType = player.rightWeapon.GetWeaponType();

        for (int i = 0; i < rightGunSprites.Length; i++)
        {
            rightGunSprites[i].SetActive(i  == (int)weaponType);
        }
    }

    void HighlightSelectedWeapon()
    {
        WeaponType selectedWeapon = GetSelectedGun();
        for (int i = 0; i < circleGunSprites.Length; i++)
        {
            if (i == (int)selectedWeapon)
            {
                circleBackgroundSprites[i].transform.localScale = new Vector3(1.1f, 1.1f, 1f);
                circleBackgroundSprites[i].GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
                circleGunSprites[i].GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
                circleGlowSprites[i].transform.localScale = new Vector3(1.1f, 1.1f, 1f);
                circleGlowSprites[i].GetComponent<RawImage>().color = new Color(1f, 1f, 1f, .5f);
            }
            else
            {
                circleBackgroundSprites[i].transform.localScale = new Vector3(1f, 1f, 1f);
                circleBackgroundSprites[i].GetComponent<RawImage>().color = new Color(1f, 1f, 1f, .6f);
                circleGunSprites[i].GetComponent<RawImage>().color = new Color(1f, 1f, 1f, .6f);
                circleGlowSprites[i].transform.localScale = new Vector3(1f, 1f, 1f);
                circleGlowSprites[i].GetComponent<RawImage>().color = new Color(1f, 1f, 1f, .25f);
            }
        }
    }

    void SetUIEnabled(bool enabled)
    {
        if (circle.activeSelf != enabled) {
            // toggle UI
            if (enabled)
            {
                Time.timeScale = .2f;
                blur.enabled = true;
                circle.SetActive(true);
                player.SetMouseLocked(false);
            }
            else
            {
                Time.timeScale = 1f;
                blur.enabled = false;
                circle.SetActive(false);
                player.SetMouseLocked(true);
            }
        }
    }

    WeaponType GetSelectedGun()
    {
        Vector3 offsetFromCenter = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        if (offsetFromCenter.magnitude < 35f) return WeaponType.None;

        if (offsetFromCenter.x > 0 && offsetFromCenter.y > 0 && gunsUnlocked[3])
            return WeaponType.RocketLauncher;
        else if (offsetFromCenter.x < 0 && offsetFromCenter.y > 0 && gunsUnlocked[0])
            return WeaponType.Flamethrower;
        else if (offsetFromCenter.x > 0 && offsetFromCenter.y < 0 && gunsUnlocked[2])
            return WeaponType.FreezeGun;
        else if (offsetFromCenter.x < 0 && offsetFromCenter.y < 0 && gunsUnlocked[1])
            return WeaponType.WindGun;
        return WeaponType.None;
    }

    void SetWeapon(Hand hand)
    {
        WeaponType weapon = GetSelectedGun();
        if (weapon == WeaponType.None) return;
        player.EquipWeapon(weapon, hand);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        blur = player.GetComponentInChildren<PostProcessVolume>();
        //SetUnlockedWeapons(true, true, false, false); for testing
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            SetUIEnabled(true);
            HighlightSelectedWeapon();
        }
        else
        {
            SetUIEnabled(false);
        }

        // show left & right guns
        if (Input.GetKeyDown(KeyCode.Q))
        {
            leftGun.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            leftGun.SetActive(false);
            SetWeapon(Hand.Left);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            rightGun.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            rightGun.SetActive(false);
            SetWeapon(Hand.Right);
        }

        // update left and right guns
        if (Input.GetKey(KeyCode.Q))
            SetLeftWeaponUI();
        if (Input.GetKey(KeyCode.E))
            SetRightWeaponUI();
    }
}
