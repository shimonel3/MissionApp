db = db.getSiblingDB('admin');

db = db.getSiblingDB('Mission');
db.createCollection('Missions');

db = db.getSiblingDB('Mission_Test');
db.createCollection('Missions');


db.Mission_Test.insertMany([
    {
        "CodeName": "007",
        "Country": "Brazil",
        "Address": "Avenida Vieira Souto 168 Ipanema, Rio de Janeiro",
        "Date": "Dec 17, 1995, 9:45:17 PM",
        "location": [-43.1990703, -22.9864527]
    },
    {
        "CodeName": "005",
        "Country": "Poland",
        "Address": "Rynek Glowny 12, Krakow",
        "Date": "Apr 5, 2011, 5:05:12 PM",
        "location": [19.9375145, 50.0618976]
    },
    {
        "CodeName": "007",
        "Country": "Morocco",
        "Address": "27 Derb Lferrane, Marrakech",
        "Date": "Jan 1, 2001, 12:00:00 AM",
        "location": [-7.9865176, 31.6294722]
    },
    {
        "CodeName": "005",
        "Country": "Brazil",
        "Address": "Rua Roberto Simonsen 122, Sao Paulo",
        "Date": "May 5, 1986, 8:40:23 AM",
        "location": [-46.6341127, -23.5505169]
    },
    {
        "CodeName": "011",
        "Country": "Poland",
        "Address": "swietego Tomasza 35, Krakow",
        "Date": "Sep 7, 1997, 7:12:53 PM",
        "location": [19.9371031, 50.0619474]
    },
    {
        "CodeName": "003",
        "Country": "Morocco",
        "Address": "Rue Al-Aidi Ali Al-Maaroufi, Casablanca",
        "Date": "Aug 29, 2012, 10:17:05 AM",
        "location": [-7.6113808, 33.5883107]
    },
    {
        "CodeName": "008",
        "Country": "Brazil",
        "Address": "Rua tamoana 418, tefe",
        "Date": "Nov 10, 2005, 1:25:13 PM",
        "location": [-64.7128585, -3.3680839]
    },
    {
        "CodeName": "013",
        "Country": "Poland",
        "Address": "Zlota 9, Lublin",
        "Date": "Oct 17, 2002, 10:52:19 AM",
        "location": [22.5686272, 51.2452729]
    },
    {
        "CodeName": "002",
        "Country": "Morocco",
        "Address": "Riad Sultan 19, Tangier",
        "Date": "Jan 1, 2017, 5:00:00 PM",
        "location": [-5.7999872, 35.7672998]
    },
    {
        "CodeName": "009",
        "Country": "Morocco",
        "Address": "atlas marina beach, agadir",
        "Date": "Dec 1, 2016, 9:21:21 PM",
        "location": [-9.5981524, 30.4201801]
    }
]);

print("Database initialization complete.");