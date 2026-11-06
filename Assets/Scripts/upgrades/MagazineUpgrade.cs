using UnityEngine;

public class MagazineUpgrade : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private PlayerShoot playerShoot;

    [SerializeField] private int extraAmmo = 3;

    private void OnMouseDown()
    {
        if (!upgradeManager.upgradePending)
            return;

        playerShoot.IncreaseMaxAmmo(extraAmmo);

        upgradeManager.UpgradeChosen();

        upgradeManager.ShowUpgradeMessage("MAGAZINE CAPACITY INCREASED");
    }
}