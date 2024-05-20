using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
	public GameObject bulletPrefab;
	private Transform firePoint;
	public float bulletForce = 20f;
	public int bulletCurrentDamage = 1;
	public int maxShotsBeforeReload = 1;
	public float reloadTime = 2f;

	private int shotsRemaining;
	private bool isReloading = false;
	private float reloadProgress;

	[SerializeField] private Slider reloadSlider;

	private void Awake()
	{
		firePoint = transform.GetChild(1).gameObject.GetComponent<Transform>();
		shotsRemaining = maxShotsBeforeReload;

		reloadSlider = GameObject.Find("Reload Slider").GetComponent<Slider>();
		reloadSlider.value = 0;
		reloadSlider.gameObject.SetActive(false); // Initially hide the slider
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1") && !isReloading && shotsRemaining > 0)
		{
			Shoot();
		}

		if (Input.GetKeyDown(KeyCode.R) && !isReloading)
		{
			Debug.Log("reloading");
			StartReload();
		}

		if (isReloading)
		{
			reloadProgress -= Time.deltaTime;
			reloadSlider.value = reloadProgress / reloadTime; // Update slider value based on reload progress
		}
	}

	private void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bulletDamage = bullet.GetComponent<Bullet>();
		bulletDamage.damage = bulletCurrentDamage;
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
		shotsRemaining--;

		if (shotsRemaining <= 0)
		{
			StartReload();
		}
	}

	private void StartReload()
	{
		isReloading = true;
		reloadProgress = reloadTime;
		reloadSlider.value = 1;
		reloadSlider.gameObject.SetActive(true); // Show the slider when reloading
		Invoke("FinishReload", reloadTime);
	}

	private void FinishReload()
	{
		isReloading = false;
		shotsRemaining = maxShotsBeforeReload;
		reloadSlider.value = 0;
		reloadSlider.gameObject.SetActive(false); // Hide the slider when reloading is finished
	}
}