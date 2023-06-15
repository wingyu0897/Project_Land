using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;

	private Camera cam;
	List<ObjectFader> faded = new List<ObjectFader>();

	private void Awake()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		transform.position = new Vector3(target.position.x, transform.position.y, target.transform.position.z);

		Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit[] hits;

		hits = Physics.RaycastAll(ray, 100f);
		if (hits.Length != 0)
		{
			foreach (ObjectFader fader in faded)
			{
				fader.doFade = false;
			}

			faded.Clear();

			foreach (RaycastHit hit in hits)
			{
				if (hit.collider.TryGetComponent(out ObjectFader fader))
				{
					fader.doFade = true;
					faded.Add(fader);
				}
			}
		}
	}
}
