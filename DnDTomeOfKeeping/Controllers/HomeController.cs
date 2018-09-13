using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;
using DnDTomeOfKeeping.Models;
using Microsoft.AspNet.Identity;

namespace DnDTomeOfKeeping.Controllers
{
    public class HomeController : Controller
    {


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult PickClass()
        {
            HttpWebRequest dndApiRequest = WebRequest.CreateHttp("http://www.dnd5eapi.co/api/classes");

            dndApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse dndApiResponse = (HttpWebResponse)dndApiRequest.GetResponse();

            if (dndApiResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(dndApiResponse.GetResponseStream());

                string data = responseData.ReadToEnd();

                JObject jsonClasses = JObject.Parse(data);

                ViewBag.Classes = jsonClasses["results"];
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult CharacterCreator(int Class)
        {

            string classString = Class.ToString();

            HttpWebRequest dndApiRequest = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/classes/{classString}");

            dndApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse dndApiResponse = (HttpWebResponse)dndApiRequest.GetResponse();

            if (dndApiResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(dndApiResponse.GetResponseStream());

                string data = responseData.ReadToEnd();

                JObject jsonClasses = JObject.Parse(data);
                int size = jsonClasses["proficiency_choices"].Count();
                ViewBag.Name = jsonClasses["name"];
                ViewBag.Classes = jsonClasses["proficiency_choices"][size - 1];
                ViewBag.ClassID = jsonClasses["index"];
                ViewBag.Choose = jsonClasses["proficiency_choices"][size - 1]["choose"];
                ViewBag.Pro = jsonClasses["proficiencies"];
                ViewBag.Saves = jsonClasses["saving_throws"];
            }

            HttpWebRequest dndRaceApiRequest = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/races");

            dndRaceApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse dndRaceApiResponse = (HttpWebResponse)dndRaceApiRequest.GetResponse();

            if (dndRaceApiResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(dndRaceApiResponse.GetResponseStream());

                string data = responseData.ReadToEnd();

                JObject jsonRaces = JObject.Parse(data);

                ViewBag.Races = jsonRaces["results"];
            }
            string[] Names = new string[] { "barbarian", "bard", "cleric", "druid", "fighter", "monk", "paladin", "ranger", "rogue", "sorcerer", "warlock", "wizard" };

            HttpWebRequest spellApiRequest = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}");

            spellApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellApiResponse = (HttpWebResponse)spellApiRequest.GetResponse();

            if (spellApiResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellApiResponse.GetResponseStream());

                string data = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(data);

                ViewBag.Spells = jsonSpells["results"];
            }

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.User = User.Identity.GetUserId();
            }

            return View();
        }

        public ActionResult CreateCampaign()
        {

            ViewBag.User = User.Identity.GetUserId();

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Tracker()
        {
            string loggedinuser = User.Identity.GetUserId();
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();
            ViewBag.CharacterToView = ORM.Characters.Where(x => x.UserID == loggedinuser).ToList();
            //ViewBag.CharacterToView = ORM.Characters.ToList();
            ViewBag.CharacterIDs = ORM.Characters.Where(x => x.CharID > 0).ToList();

            ViewBag.MyCampaigns = ORM.Campaigns.Where(x => x.DMUserID == loggedinuser).ToList();

            return View();
        }

        [HttpGet]
        public ActionResult CCSearch()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Campaign()
        {
            ViewBag.User = User.Identity.GetUserId();

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult CharacterEdit(int CharacterID, int Class)
        {
            string classString = Class.ToString();

            HttpWebRequest dndApiRequest = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/classes/{classString}");

            dndApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse dndApiResponse = (HttpWebResponse)dndApiRequest.GetResponse();

            if (dndApiResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(dndApiResponse.GetResponseStream());

                string data = responseData.ReadToEnd();

                JObject jsonClasses = JObject.Parse(data);
                int size = jsonClasses["proficiency_choices"].Count();
                ViewBag.Name = jsonClasses["name"];
                ViewBag.Classes = jsonClasses["proficiency_choices"][size - 1];

                ViewBag.Choose = jsonClasses["proficiency_choices"][size - 1]["choose"];
                ViewBag.Pro = jsonClasses["proficiencies"];
                ViewBag.Saves = jsonClasses["saving_throws"];
            }

            HttpWebRequest dndRaceApiRequest = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/races");

            dndRaceApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse dndRaceApiResponse = (HttpWebResponse)dndRaceApiRequest.GetResponse();

            if (dndRaceApiResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(dndRaceApiResponse.GetResponseStream());

                string data = responseData.ReadToEnd();

                JObject jsonRaces = JObject.Parse(data);

                ViewBag.Races = jsonRaces["results"];
            }
            string[] Names = new string[] { "barbarian", "bard", "cleric", "druid", "fighter", "monk", "paladin", "ranger", "rogue", "sorcerer", "warlock", "wizard" };

            HttpWebRequest spellApiRequest = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}");

            spellApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellApiResponse = (HttpWebResponse)spellApiRequest.GetResponse();

            if (spellApiResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellApiResponse.GetResponseStream());

                string data = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(data);

                ViewBag.Spells = jsonSpells["results"];
            }

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.User = User.Identity.GetUserId();
            }


            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            Character characterToEdit = ORM.Characters.Find(CharacterID);

            ViewBag.CharacterData = characterToEdit;

            return View();
        }

        public ActionResult CharacterSubmit(Character newCharacter, string[] Proficiencies, string[] SpellsKnown)
        {
            if (SpellsKnown != null)
            {
                string spells = "";

                foreach (string spell in SpellsKnown)
                {
                    spells += spell + ",";
                }
                newCharacter.SpellsKnown = spells;
            }

            string s = "";

            foreach (string p in Proficiencies)
            {
                s += p + ",";
            }
            newCharacter.Proficiencies = s;
            //Session["t"] = newCharacter.Proficiencies;
            //Session["t2"] = newCharacter.Proficiencies[1];
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            ORM.Characters.Add(newCharacter);
            ORM.SaveChanges();
            return RedirectToAction("Tracker");
        }

        public ActionResult SaveChanges(Character UpdatedCharacter, string[] Proficiencies, string[] SpellsKnown)
        {
            if (SpellsKnown != null)
            {
                string spells = "";

                foreach (string spell in SpellsKnown)
                {
                    spells += spell + ",";
                }
                UpdatedCharacter.SpellsKnown = spells;
            }

            string s = "";

            foreach (string p in Proficiencies)
            {
                s += p + ",";
            }
            UpdatedCharacter.Proficiencies = s;

            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            Character OldRecord = ORM.Characters.Find(UpdatedCharacter.CharID);

            OldRecord.HitPoints = UpdatedCharacter.HitPoints;
            OldRecord.Alignment = UpdatedCharacter.Alignment;
            OldRecord.CharLevel = UpdatedCharacter.CharLevel;
            OldRecord.SpellsKnown = UpdatedCharacter.SpellsKnown;
            OldRecord.Proficiencies = UpdatedCharacter.Proficiencies;
            OldRecord.Strength = UpdatedCharacter.Strength;
            OldRecord.Dexterity = UpdatedCharacter.Dexterity;
            OldRecord.Constitution = UpdatedCharacter.Constitution;
            OldRecord.Intelligence = UpdatedCharacter.Intelligence;
            OldRecord.Wisdom = UpdatedCharacter.Wisdom;
            OldRecord.Charisma = UpdatedCharacter.Charisma;

            ORM.Entry(OldRecord).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();


            return RedirectToAction("Tracker");
            //OldRecord.Feats = UpdateItem.Feats;
            //OldRecord.Equipment = UpdateItem.Equipment;
            //OldRecord.Features = UpdateItem.Features;
            //OldRecord.Campaign = UpdateItem.Campaign;
        }


        public ActionResult SearchCharByName(string charName)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            ViewBag.Characters = ORM.Characters.Where(x => x.CharName.ToLower().Contains
            (charName.ToLower())).ToList();

            return View("CharacterResult");
        }

        public ActionResult SearchCampaignByName(string campaignName)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            ViewBag.Campaigns = ORM.Campaigns.Where(x => x.CampaignName.ToLower().Contains
            (campaignName.ToLower())).ToList();

            return View("CampaignResult");
        }

        public ActionResult SaveCampaign(Campaign newCampaign)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();
            // ViewBag.campaign = ORM.Campaigns;

            ORM.Campaigns.Add(newCampaign);

            ORM.SaveChanges();

            return RedirectToAction("Campaign");

        }

        [HttpGet]
        public ActionResult ViewCharacter(int CharacterID)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            //ViewBag.CharacterToView = ORM.Characters.Where(x => x.CharID.ToString().Contains(CharID.ToString())).ToList();

            ViewBag.Character = ORM.Characters.Find(CharacterID);

            return View();
        }
    }
}