using Mapbox.Geocoding;
using Mapbox.Unity;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleSceneUI : MonoBehaviour 
{
    public GameObject nameCompanyPanel;
    public TextMeshProUGUI[] companyNameTMP;
	ForwardGeocodeResource _resource;
	Vector2d _coordinate;
	public TextMeshProUGUI city;
	bool _hasResponse;

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
	public void BtnConnect()
    {
        nameCompanyPanel.SetActive(true);
		HandleUserInput(city.text);

    }

    public void BtnStartGame()
    {
        if(companyNameTMP[0].text.Length + companyNameTMP[0].text.Length > 2)
        {
            DoNotDeleteData.Instance.companyName[0] = companyNameTMP[0].text.ToUpper();
            DoNotDeleteData.Instance.companyName[1] = companyNameTMP[1].text.ToUpper();
        }
        else
        {
            DoNotDeleteData.Instance.companyName[0] = "TELE";
            DoNotDeleteData.Instance.companyName[1] = "CITY";
        }

        SceneManager.LoadScene("MainScene");
    }

	void HandleUserInput(string searchString)
	{
        if (searchString.Length < 2)
            searchString = "Helsinki";

		_hasResponse = false;
		_resource.Query = searchString;
		Debug.Log ("Get data now!!");
		MapboxAccess.Instance.Geocoder.Geocode(_resource, HandleGeocoderResponse);
	}

	void HandleGeocoderResponse(ForwardGeocodeResponse res)
	{
		_hasResponse = true;
		if (null != res.Features && res.Features.Count > 0)
		{
			var center = res.Features[0].Center;
			_coordinate = res.Features[0].Center;
			DoNotDeleteData.Instance.city = _coordinate;

			Debug.Log (_coordinate);
		}

		Response = res;
		OnGeocoderResponse(res);
	}
}
