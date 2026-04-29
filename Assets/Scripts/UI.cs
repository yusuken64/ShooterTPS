using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI UIText;
    public TextMeshProUGUI ObjectiveText;
    public TextMeshProUGUI InteractableText;
    public Image DamageIndicator;
    public Image DamageDirectionIndicator;

    public float FlashDuration = 0.5f;
    public float MaxAlpha = 0.6f;
    private Player player;
    private Tween flashTween;

    public ResultsScreen ResultsScreen;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        player.DamageTaken += Player_DamageTaken;

        DamageIndicator.gameObject.SetActive(false);
        DamageDirectionIndicator.gameObject.SetActive(false);

        InteractableText.text = "";
        ResultsScreen.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.DamageTaken -= Player_DamageTaken;
        }
    }

    private void Player_DamageTaken(int damage, Vector3 hitPosition)
	{
		Vector3 dir = (hitPosition - player.transform.position).normalized;
		Vector3 localDir = Camera.main.transform.InverseTransformDirection(dir);
		ShowDamageDirectionIndicator(localDir);

		Flash();
	}

	private void Flash()
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

        DamageDirectionIndicator.gameObject.SetActive(true);
        // fade out
        flashTween = DamageDirectionIndicator
            .DOFade(0f, FlashDuration + 1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => DamageDirectionIndicator.gameObject.SetActive(false));
    }

	private void ShowDamageDirectionIndicator(Vector3 localDir)
    {
        // ignore vertical for now (top-down projection)
        Vector2 dir2D = new Vector2(localDir.x, localDir.z).normalized;

        // place indicator on a circle around center
        float radius = 200f; // distance from center in UI space
        Vector2 uiPos = dir2D * radius;

        DamageDirectionIndicator.rectTransform.anchoredPosition = uiPos;

        // rotate it so it points inward
        float angle = Mathf.Atan2(dir2D.y, dir2D.x) * Mathf.Rad2Deg;
        DamageDirectionIndicator.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void SetAlpha(float a)
    {
        Color c = DamageIndicator.color;
        c.a = a;
        DamageIndicator.color = c;
        DamageDirectionIndicator.color = c;
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
