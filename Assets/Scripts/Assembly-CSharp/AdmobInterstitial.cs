using UnityEngine;

public class AdmobInterstitial : MonoBehaviour
{
	private void Start()
	{
		string[] testDeviceIDs = new string[1] { "E92E9A6745B85439C2EA180AB0010A87" };
		EasyGoogleMobileAds.GetInterstitialManager().SetTestDevices(true, testDeviceIDs);
		EasyGoogleMobileAds.GetInterstitialManager().PrepareInterstitial("ca-app-pub-1165471332330497/6804448566");
		EasyGoogleMobileAds.GetInterstitialManager().ShowInterstitial();
	}
}
