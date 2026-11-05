using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float shootRange = 100f;
    public int gunDamage = 1;

    [SerializeField] private int maxAmmo = 15;
    private int currentAmmo;

    [SerializeField] private float ammoRefillDistance = 3f;
    public TMP_Text ammoText;


    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
        }
    }

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    private void Update()
    {
        // Don't shoot when clicking UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentAmmo <= 0)
                    return;

                Shoot();

                currentAmmo--;

                UpdateAmmoUI();
            }
        }

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

                        Debug.Log("Ammo Refilled!");
                        return;
                    }
                }
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
                RatHealth Rat = hit.collider.GetComponent<RatHealth>();

                if (Rat != null)
                {
                    Debug.Log("hit rat");

                    Rat.RatTakeDamage(gunDamage);
                }
            }
        }
    }
}
