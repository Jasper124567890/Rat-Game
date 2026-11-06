using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float shootRange = 100f;

    public int gunDamage = 1;

    [Header("Ammo")]
    [SerializeField] private int maxAmmo = 9;
    private int currentAmmo;

    [SerializeField] private float ammoRefillDistance = 3f;

    [Header("UI")]
    public TMP_Text ammoText;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Shoot
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

        // Ammo refill
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

    private void Shoot()
    {
        Debug.Log("Shoot");

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(
            ray.origin,
            ray.direction * shootRange,
            Color.red,
            1f
        );

        RaycastHit hit;

        int layerMask = ~LayerMask.GetMask("Barricade");

        if (Physics.Raycast(
            ray,
            out hit,
            shootRange,
            layerMask))
        {
            if (hit.collider.CompareTag("Rat"))
            {
                // Normal Rat
                RatHealth rat =
                    hit.collider.GetComponent<RatHealth>();

                if (rat != null)
                {
                    rat.RatTakeDamage(gunDamage);

                    Debug.Log("Hit Rat");

                    return;
                }

                // Rat King
                RatKingHealth ratKing =
                    hit.collider.GetComponent<RatKingHealth>();

                if (ratKing != null)
                {
                    ratKing.RatTakeDamage(gunDamage);

                    Debug.Log("Hit Rat King");

                    return;
                }
            }
        }
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text =
                "Ammo: " +
                currentAmmo +
                "/" +
                maxAmmo;
        }
    }

    public void IncreaseMaxAmmo(int amount)
    {
        maxAmmo += amount;

        currentAmmo += amount;

        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        UpdateAmmoUI();

        Debug.Log(
            "Magazine Upgraded! Max Ammo: " +
            maxAmmo
        );
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}