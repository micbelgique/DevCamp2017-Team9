using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{

    public GUIText myWeatherLabel;
    public GUITexture myWeatherCondition;

    public string currentIP;
    private string currentCountry;
    private string currentCity;
    private string currentLat;
    private string currentLong;

    //retrieved from weather API
    public string retrievedCountry;
    public string retrievedCity;
    public string retrievedLat;
    public string retrievedLong;
    public string conditionName;
    public string conditionImage;
    public int conditionID;

    public int idMeteo;

    private string internalIP;
    private string externalIP;

    private float time;

    public GameObject fenetre;
    public GameObject fenetrePluie;
    public GameObject fenetreHiver;
    public GameObject vetementEte;
    public GameObject vetementPluie;
    public GameObject vetementHiver;

    void Start()
    {
        StartCoroutine(SendRequest());
    }

    IEnumerator SendRequest()
    {
        //get the players IP, City, Country

        // Get IP address from the open source API "ipify.org"
        // in JSON format and extract the address.
        // This is the easiest and most robust method I found.
        // The other alternative method (less precise) was:
        //		StartCoroutine (NetworkSetup ());
        //		currentIP = externalIP;
        WWW ipRequest = new WWW("https://api.ipify.org?format=json");
        yield return ipRequest;

        if (ipRequest.error == null || ipRequest.error == "")
        {
            var N = JSON.Parse(ipRequest.text);
            currentIP = N["ip"].Value;
        }

        else
        {
            Debug.Log("WWW error: " + ipRequest.error);
            // If unable to get real time weather conditions, select random weather conditions
            conditionImage = "0" + Random.Range(1, 13);
            myWeatherLabel.text = "Données météo indisponibles" + "\n--> Météo aléatoire";
        }


        WWW cityRequest = new WWW("http://www.geoplugin.net/json.gp?ip=" + currentIP); //get our location info
        yield return cityRequest;

        if (cityRequest.error == null || cityRequest.error == "")
        {
            var N = JSON.Parse(cityRequest.text);
            currentCity = N["geoplugin_city"].Value;
            currentLat = N["geoplugin_latitude"].Value;
            currentLong = N["geoplugin_longitude"].Value;
            currentCountry = N["geoplugin_countryName"].Value;
        }

        else
        {
            Debug.Log("WWW error: " + cityRequest.error);  
            // If unable to get real time weather conditions, select random weather conditions
            conditionImage = "0" + Random.Range(1, 13);
            myWeatherLabel.text = "Données météo indisponibles" + "\n--> Météo aléatoire";
        }


        //get the current weather

        WWW request = new WWW("http://api.openweathermap.org/data/2.5/weather?lat=" + currentLat + "&lon=" + currentLong + "&APPID=26caff17d05d33df55248bc0bd8bea91"); //get our weather
        yield return request;



        if (request.error == null || request.error == "")
        {
            var N = JSON.Parse(request.text);

            retrievedCountry = N["sys"]["country"].Value; //get the country
            retrievedCity = N["name"].Value; //get the city
            retrievedLat = N["coord"]["lat"].Value; //get the latitude
            retrievedLong = N["coord"]["lon"].Value; //get the longitude

            string temp = N["main"]["temp"].Value; //get the temperature
            float tempTemp; //variable to hold the parsed temperature
            float.TryParse(temp, out tempTemp); //parse the temperature
            float finalTemp = Mathf.Round((tempTemp - 273.0f) * 10) / 10; //holds the actual converted temperature

            int.TryParse(N["weather"][0]["id"].Value, out conditionID); //get the current condition ID
                                                                        //conditionName = N["weather"][0]["main"].Value; //get the current condition Name
            conditionName = N["weather"][0]["description"].Value; //get the current condition Description
            conditionImage = N["weather"][0]["icon"].Value; //get the current condition Image

            //put all the retrieved stuff in the label
            myWeatherLabel.text =
                "Country: " + retrievedCountry
                + "\nCity: " + retrievedCity
               // + "\nLatitude: " + retrievedLat
               // + "\nLongitude: " + retrievedLong
                + "\nTemperature: " + finalTemp + " C"
                + "\nCurrent Condition: " + conditionName
               // + "\nCondition Code: " + conditionID
               ;
        }
        else
        {
            Debug.Log("WWW error: " + request.error);           
            // If unable to get real time weather conditions, select random weather conditions
            conditionImage = "0" + Random.Range(1, 13);
            myWeatherLabel.text = "Données météo indisponibles" + "\n--> Météo aléatoire";
        }

        // Adapt the background (the window) to the actual weather
        // conditionImage contains an identifier of the type of weather
        // with the following categories (detailed description on http://openweathermap.org/weather-conditions):
        // ID 01X-03X: Clear sky or few clouds --> Summer clothes and background
        // ID 04X-11X and 50X: Probable Rain, Rain or thunderstorm --> Rain clothes and background
        // ID 13X: Snow --> Winter clothes and background

        if (conditionImage.Contains("13"))
        {
            // if it snows, use the sprites for the snowy window, the winter and the summer clothes
            fenetre.SetActive(false);
            fenetrePluie.SetActive(false);
            vetementPluie.SetActive(false);
            idMeteo = 3;
        }
        else if (conditionImage.Contains("01") || conditionImage.Contains("02") || conditionImage.Contains("03"))
        {
            // if it does not snow or rain, use the sprites for the sunny window, the rain and the summer clothes
            fenetreHiver.SetActive(false);
            fenetrePluie.SetActive(false);
            vetementHiver.SetActive(false);
            idMeteo = 1;
        }
        else
        {
            // if it rains or if it is too cloudy, use the sprites for the rainy window, the rain and the summer clothes
            fenetreHiver.SetActive(false);
            fenetre.SetActive(false);
            vetementHiver.SetActive(false);
            idMeteo = 2;
        }
    }


    //	// This deprecated method was used to find the local ip address.
    //	// I keep it for archival purposes. It is a robust method, but it
    //  // gives less precise locations.
    //	public IEnumerator NetworkSetup()
    //	{
    //		Network.Connect ("127.0.0.1");
    //
    //		while (Network.player.externalIP == "UNASSIGNED_SYSTEM_ADDRESS") {
    //			time += Time.deltaTime + 0.01f;
    //
    //			if (time > 10) {
    //				Debug.LogError ("Unable to obtain external ip: Are you sure you're connected to internet?");
    //			}
    //
    //			yield return null;
    //		}
    //
    //		externalIP = Network.player.externalIP;
    //		Network.Disconnect ();
    //	}
}