using UnityEngine;
using TMPro;

public class PlayerShootTANK : MonoBehaviour
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                // Ammo box
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

                // Barricade box
                if (hit.collider.CompareTag("BarricadeBox"))
                {
                    float distance = Vector3.Distance(
                        transform.position,
                        hit.collider.transform.position
                    );

                    if (distance <= ammoRefillDistance)
                    {
                        Debug.Log("Barricades Refilled!");

                        // Put your barricade refill code here
                        // Example:
                        // boardInventory.AddBoards(4);

                        return;
                    }
                }
            }

            // If we didn't click a refill box, shoot
            if (currentAmmo > 0)
            {
                Shoot();

                currentAmmo--;

                UpdateAmmoUI();
            }
        }
    }

    void Shoot()
    {
        Debug.Log("shoot");

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

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
}