using UnityEngine;

public class Pistol : MonoBehaviour
{
	public GameObject bulletPrefab;
	private Transform firePoint;
	public float bulletForce = 20f;
	public int maxShotsBeforeReload = 3;
	public float reloadTime = 1f;

	private int shotsRemaining;
	private bool isReloading = false;

	private void Awake()
	{
		firePoint = transform.GetChild(1).gameObject.GetComponent<Transform>();
		shotsRemaining = maxShotsBeforeReload;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1") && !isReloading && shotsRemaining > 0)
		{
			Debug.Log("pressing lmb");
			Shoot();
		}

		if (Input.GetKeyDown(KeyCode.R) && !isReloading)
		{
			Debug.Log("reloading");
			Reload();
		}
	}

	private void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
		shotsRemaining--;

		if (shotsRemaining <= 0)
		{
			Reload();
		}
	}

	private void Reload()
	{
		isReloading = true;
		shotsRemaining = maxShotsBeforeReload;
		Invoke("FinishReload", reloadTime);
	}

	private void FinishReload()
	{
		isReloading = false;
	}
}