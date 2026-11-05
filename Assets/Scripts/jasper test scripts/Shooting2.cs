using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class Shooting2 : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float shootRange = 100f;

    public int gunDamage = 1;

    public int maxAmmo = 15;
    private int currentAmmo;

    public float ammoRefillDistance = 3f;
    public TMP_Text ammoText;
    private bool isReloading = false;

    //Gun Spirtes
    public Image gunImage;
    public Sprite gunNormalSprite;
    public Sprite gunShootSprite;
    public float flashTime = 0.1f;
    public Sprite gunReloadSprite;
    public float reloadFlashTime = 0.2f;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();

        if (gunImage != null)
        {
            gunImage.enabled =
                Input.GetMouseButton(1) || isReloading;
        }
    }

    private void Update()
    {
        if (gunImage != null)
        {
            gunImage.enabled =
                Input.GetMouseButton(1) || isReloading;
        }

        // Left click on ammo box
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.collider.CompareTag("AmmoBox"))
                {
                    float distance = Vector3.Distance(
                        transform.position,
                        hit.collider.transform.position
                    );

                    if (distance <= ammoRefillDistance)
                    {
                        currentAmmo = maxAmmo;
                        UpdateAmmoUI();

                        StartCoroutine(ReloadFlash());

                        Debug.Log("Ammo Refilled!");
                        return;
                    }
                }
            }
        }

        // Normal shooting
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentAmmo <= 0)
                    return;

                Shoot();

                currentAmmo--;
                UpdateAmmoUI();

                StartCoroutine(GunFlash());
            }
        }
    }

    void Shoot()
    {
        Debug.Log("shoot");

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(
            ray.origin,
            ray.direction * shootRange,
            Color.red,
            1f
        );

        RaycastHit hit;

        int layerMask = ~LayerMask.GetMask("Barricade");

        if (Physics.Raycast(ray, out hit, shootRange, layerMask))
        {
            if (hit.collider.CompareTag("Rat"))
            {
                RatHealth rat = hit.collider.GetComponent<RatHealth>();

                if (rat != null)
                {
                    Debug.Log("hit rat");
                    rat.RatTakeDamage(gunDamage);
                }
            }
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
        }
    }

    IEnumerator GunFlash()
    {
        if (gunImage == null)
            yield break;

        gunImage.sprite = gunShootSprite;

        yield return new WaitForSeconds(flashTime);

        gunImage.sprite = gunNormalSprite;
    }

    IEnumerator ReloadFlash()
    {
        if (gunImage == null)
            yield break;

        isReloading = true;

        gunImage.enabled = true;
        gunImage.sprite = gunReloadSprite;

        yield return new WaitForSeconds(reloadFlashTime);

        gunImage.sprite = gunNormalSprite;

        isReloading = false;
    }

}