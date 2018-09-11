using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;
using DnDTomeOfKeeping.Models;

namespace DnDTomeOfKeeping.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
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

                ViewBag.Classes = jsonClasses;
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

            return View();
        }

        [HttpGet]
        public ActionResult Tracker()
        {
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
            return View();
        }

        public ActionResult CharacterEdit()
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            Character characterToEdit = ORM.Characters.Find(1);

            ViewBag.CharacterData = characterToEdit;

            return View();
        }

        public ActionResult CharacterSubmit(Character newCharacter)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            ORM.Characters.Add(newCharacter);

            ORM.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditCharacter()
        {
            //Character OldRecord = ORM.Characters.Find(UpdateItem.CharID);

            //OldRecord.CharName = UpdateItem.CharName;
            //OldRecord.Class = UpdateItem.Class;
            //OldRecord.HitPoints = UpdateItem.HitPoints;
            //OldRecord.Alignment = UpdateItem.Alignment;
            //OldRecord.CharLevel = UpdateItem.CharLevel;
            //OldRecord.Feats = UpdateItem.Feats;
            //OldRecord.Equipment = UpdateItem.Equipment;
            //OldRecord.Features = UpdateItem.Features;
            //OldRecord.Campaign = UpdateItem.Campaign;
            //OldRecord.UserID = UpdateItem.UserID;
            //OldRecord.Race = UpdateItem.Race;
            //OldRecord.SpellsKnown = UpdateItem.SpellsKnown;
            //OldRecord.Proficiencies = UpdateItem.Proficiencies;
            //OldRecord.Strength = UpdateItem.Strength;
            //OldRecord.Dexterity = UpdateItem.Dexterity;
            //OldRecord.Constitution = UpdateItem.Constitution;
            //OldRecord.Intellegence = UpdateItem.Intellegence;
            //OldRecord.Wisdom = UpdateItem.Wisdom;
            //OldRecord.Charisma = UpdateItem.Charisma;

            return View();
        }
    }
}