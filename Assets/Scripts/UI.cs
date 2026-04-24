using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI UIText;

	private Player player;

	private void Start()
	{
		player = FindFirstObjectByType<Player>();
	}

    void Update()
    {
        if (player == null) return;

        int health = player.Health;

        int ammo = 0;
        int maxAmmo = 0;

        if (player.CurrentGun != null)
        {
            ammo = player.CurrentGun.AmmoInClip;
            maxAmmo = player.CurrentGun.ClipSize;
        }

        UIText.text =
@$"HP: {health}
Ammo: {ammo}/{maxAmmo}";
    }
}
