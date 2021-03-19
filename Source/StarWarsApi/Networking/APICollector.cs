using System;
using System.Net.Http;
using System.Net.Http.Headers;
using StarWarsApi.Models;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace StarWarsApi.Repository
{
    public static class APICollector
    {
        public static User ParseUser(Uri URL)
        {
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = URL;
                httpclient.DefaultRequestHeaders.Add("User-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(URL).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }
            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var parsedUser = JsonConvert.DeserializeObject<User>(jsonResult);
                var resultobject = JsonConvert.DeserializeObject<JObject>(jsonResult);
                foreach (var property in resultobject.Properties())
                {
                    if (property.Name == "homeworld")
                    {
                        parsedUser.Homeplanet = returnHomeworld(property.Value.ToString());
                    }
                }
                parsedUser.StarWarsID = int.Parse(Regex.Replace(URL.ToString(),@"[^\d]", string.Empty));
                return parsedUser;
            }
            else
            {
                throw new Exception("Parse was empty; Is the URL in correct format?");
            }
        }
        public static User ParseUser(string name)
        {
            var foundUser = false;
            var user = new User();
            for (var i = 1; i <= 9; i++)
            {
                if (!foundUser)
                {
                    var tempUsers =
                        returnUsersFromList("https://swapi.dev/api/people/?page=" + i.ToString());
                    foreach (var s in tempUsers)
                    {
                        if (s.Name != name) continue;
                        foundUser = true;
                        user = s;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (foundUser)
            {
                return user;
            }
            else
            {
                throw new Exception("Could not find a user based on " + name + ". Did you enter the name correctly?");
            }
        }
        public static SpaceShip ParseShip(Uri URL)
        {
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = URL;
                httpclient.DefaultRequestHeaders.Add("User-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(URL).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }
            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var parsedShip = JsonConvert.DeserializeObject<SpaceShip>(jsonResult);
                var resultobject = JsonConvert.DeserializeObject<JObject>(jsonResult);
                foreach (var property in resultobject.Properties())
                {
                    if (property.Name == "length")
                    {
                        parsedShip.Size = new SpaceShip.ShipSize(0,
                            int.Parse(property.Value.ToString().Replace(",", "").Replace(".", "")), 0);
                    }
                }
                return parsedShip;
            }
            else
            {
                throw new Exception("Parse was empty; Is the URL in correct format?");
            }
        }
        public static SpaceShip ParseShip(string name)
        {
            var foundShip = false; //A check to verify a ship has been found by name
            SpaceShip ship = new SpaceShip(); //Had to initialize because code didn't realize it was bound to the foundShip bool and i got build errors.
            for (var i = 1; i <= 4; i++)
            {
                var tempShips =
                    returnSpaceShipsFromList("https://swapi.dev/api/starships/?page=" + i.ToString());
                foreach (var s in tempShips)
                {
                    if (s.Name != name) continue;
                    foundShip = true;
                    ship = s;
                }
            }
            if (!foundShip)
            {
                throw new Exception("Could not find a ship based on " + name + ". Did you enter the name correctly?");
            }
            else
            {
                return ship;
            }
        }
        public static SpaceShip[] ReturnShips()
        {
            List<SpaceShip> spaceShips = new List<SpaceShip>();
            for (int i = 1; i <= 4; i++)
            {
                IEnumerable<SpaceShip> tempShips =
                    returnSpaceShipsFromList("https://swapi.dev/api/starships/?page=" + i.ToString());
                spaceShips.AddRange(tempShips);
            }

            return spaceShips.ToArray();
        }
        private static User.Homeworld returnHomeworld(string URL)
        {
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(URL); 
                httpclient.DefaultRequestHeaders.Add("User-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(URL).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }
            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var homeworld = JsonConvert.DeserializeObject<User.Homeworld>(jsonResult);
                return homeworld;
            }
            else
            {
                throw ParseFailedIncorrectURL(URL);
            }
        }
        private static IEnumerable<SpaceShip> returnSpaceShipsFromList(string URL)
        {
            List<SpaceShip> spaceShips = new List<SpaceShip>();
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(URL);
                httpclient.DefaultRequestHeaders.Add("User-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(URL).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }
            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var resultObjects = AllChildren(JObject.Parse(jsonResult))
                  .First(c => c.Type == JTokenType.Array && c.Path.Contains("results"))
                  .Children<JObject>();
              foreach (var result in resultObjects) {
                  
                  SpaceShip spaceShip = result.ToObject<SpaceShip>();
                  foreach (var property in result.Properties())
                  {
                      if (property.Name == "length")
                      {
                          try
                          {
                              spaceShip.Size = new SpaceShip.ShipSize(0, long.Parse(property.Value.ToString().Replace(",", "").Replace(".","")), 0);
                          }
                          catch
                          {
                              throw new Exception($"Failed to parse length.Input value: {property.Value}");
                          }
                      }
                  }
                  spaceShips.Add(spaceShip);
              }
            }
            else
            {
                throw ParseFailedIncorrectURL(URL);
            }
            return spaceShips.ToArray();
        }
        private static IEnumerable<User> returnUsersFromList(string URL)
        {
            var users = new List<User>();
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(URL);
                httpclient.DefaultRequestHeaders.Add("User-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(URL).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }
            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var resultObjects = AllChildren(JObject.Parse(jsonResult))
                    .First(c => c.Type == JTokenType.Array && c.Path.Contains("results"))
                    .Children<JObject>();
                foreach (var result in resultObjects)
                {
                    var user = result.ToObject<User>();
                    foreach (var property in result.Properties()) {
                        if (property.Name == "homeworld")
                        {
                            user.Homeplanet = returnHomeworld(property.Value.ToString());
                        }
                    }
                    users.Add(user);
                }
            }
            else
            {
                throw ParseFailedIncorrectURL(URL);
            }
            return users.ToArray();
        }

        private static Exception ParseFailedIncorrectURL(string URL)
        {
            throw new Exception($"Parse was empty; Is the URL in correct format? Input: {URL}");
        }

        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }
    }
}