using MissionApp.Service;
using MongoDB.Driver.GeoJsonObjectModel;

public class MockLocationService : ILocationService
{
    public GeoJson2DCoordinates GetCoordinates(string address)
    {
        var targetLocation = new GeoJson2DCoordinates(0, 0);
        switch (address)
        {
            case "Avenida Vieira Souto 168 Ipanema, Rio de Janeiro":
                targetLocation = new GeoJson2DCoordinates(-43.1990703, -22.9864527);
                break;
            case "Rynek Glowny 12, Krakow":
                targetLocation = new GeoJson2DCoordinates(19.9375145, 50.0618976);
                break;
            case "27 Derb Lferrane, Marrakech":
                targetLocation = new GeoJson2DCoordinates(-7.9865176, 31.6294722);
                break;
            case "Rua Roberto Simonsen 122, Sao Paulo":
                targetLocation = new GeoJson2DCoordinates(-46.6341127, -23.5505169);
                break;
            case "swietego Tomasza 35, Krakow":
                targetLocation = new GeoJson2DCoordinates(19.9371031, 50.0619474);
                break;
            case "Rue Al-Aidi Ali Al-Maaroufi, Casablanca":
                targetLocation = new GeoJson2DCoordinates(-7.6113808, 33.5883107);
                break;
            case "Rua tamoana 418, tefe":
                targetLocation = new GeoJson2DCoordinates(-64.7128585, -3.3680839);
                break;
            case "Zlota 9, Lublin":
                targetLocation = new GeoJson2DCoordinates(22.5686272, 51.2452729);
                break;
            case "Riad Sultan 19, Tangier":
                targetLocation = new GeoJson2DCoordinates(-5.7999872, 35.7672998);
                break;
            case "atlas marina beach, agadir":
                targetLocation = new GeoJson2DCoordinates(-9.5981524, 30.4201801);
                break;
            default:
                throw new ArgumentException("Address not found");
        }
        return targetLocation;
    }
}