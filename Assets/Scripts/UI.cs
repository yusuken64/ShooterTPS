using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI UIText;
	public Image DamageIndicator;

    public float FlashDuration = 0.5f;
    public float MaxAlpha = 0.6f;
    private Player player;
    private Tween flashTween;

    private void Start()
	{
		player = FindFirstObjectByType<Player>();
		player.DamageTaken += Player_DamageTaken;

        DamageIndicator.gameObject.SetActive(false);

    }

	private void OnDestroy()
	{
        if (player != null)
        {
            player.DamageTaken -= Player_DamageTaken;
        }
	}

    private void Player_DamageTaken(int damage)
    {
        // kill any existing tween so hits don't stack weirdly
        flashTween?.Kill();

        // instantly jump to visible
        SetAlpha(MaxAlpha);

        DamageIndicator.gameObject.SetActive(true);
        // fade out
        flashTween = DamageIndicator
            .DOFade(0f, FlashDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => DamageIndicator.gameObject.SetActive(false));
    }

    private void SetAlpha(float a)
    {
        Color c = DamageIndicator.color;
        c.a = a;
        DamageIndicator.color = c;
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
