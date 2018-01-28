using Mapbox.Geocoding;
using Mapbox.Unity;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class bootItUp : MonoBehaviour
{
	ForwardGeocodeResource _resource;
	Vector2d _coordinate;
	public TextMeshProUGUI city;
	bool _hasResponse;
	public GameObject datastorage;

	public bool HasResponse
	{
		get
		{
			return _hasResponse;
		}
	}

	public ForwardGeocodeResponse Response { get; private set; }
	public event Action<ForwardGeocodeResponse> OnGeocoderResponse = delegate { };

	void Awake()
	{
		_resource = new ForwardGeocodeResource("");

	}
	void Start()
	{
		
	}
	public void sceneChange()
	{
		HandleUserInput(city.text);
	}

	void HandleUserInput(string searchString)
	{
		_hasResponse = false;
		if (!string.IsNullOrEmpty(searchString))
		{
			_resource.Query = searchString;
			Debug.Log ("Get data now!!");
			MapboxAccess.Instance.Geocoder.Geocode(_resource, HandleGeocoderResponse);
		}
	}


	void HandleGeocoderResponse(ForwardGeocodeResponse res)
	{
		_hasResponse = true;
		if (null != res.Features && res.Features.Count > 0)
		{
			var center = res.Features[0].Center;
			_coordinate = res.Features[0].Center;
			Debug.Log (_coordinate);
		}

		Response = res;
		OnGeocoderResponse(res);
	}
}
	