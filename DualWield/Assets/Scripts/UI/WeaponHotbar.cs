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
    [SerializeField] private GameObject[] circleGunSprites;
    [SerializeField] private GameObject[] circleGlowSprites;
    [SerializeField] private GameObject[] leftGunSprites;
    [SerializeField] private GameObject[] rightGunSprites;

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

        if (offsetFromCenter.x > 0 && offsetFromCenter.y > 0)
            return WeaponType.RocketLauncher;
        else if (offsetFromCenter.x < 0 && offsetFromCenter.y > 0)
            return WeaponType.Flamethrower;
        else if (offsetFromCenter.x > 0 && offsetFromCenter.y < 0)
            return WeaponType.FreezeGun;
        else
            return WeaponType.WindGun;
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
    }

    // Update is called once per frame
    void Update()
    {
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
