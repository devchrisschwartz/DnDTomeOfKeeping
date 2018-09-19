
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
        string[] Names = new string[] { "barbarian", "bard", "cleric", "druid", "fighter", "monk", "paladin", "ranger", "rogue", "sorcerer", "warlock", "wizard" };

        [HttpGet]
        public ActionResult Index()
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();
            ViewBag.PubChar = ORM.Characters.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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
                ViewBag.HitDieSize = jsonClasses["hit_die"];
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


            HttpWebRequest spellApiRequest0 = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}/level/0");

            spellApiRequest0.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellApiResponse0 = (HttpWebResponse)spellApiRequest0.GetResponse();

            if (spellApiResponse0.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellApiResponse0.GetResponseStream());

                string cantrips = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(cantrips);

                ViewBag.Cantrips = jsonSpells["results"];
            }

            HttpWebRequest spellAPIRequest1 = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}/level/1");

            spellAPIRequest1.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellAPIresponse1 = (HttpWebResponse)spellAPIRequest1.GetResponse();

            if (spellAPIresponse1.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellAPIresponse1.GetResponseStream());

                string Spells1 = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(Spells1);

                ViewBag.Spells1 = jsonSpells["results"];
            }

            HttpWebRequest spellAPIRequest2 = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}/level/2");

            spellAPIRequest2.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellAPIresponse2 = (HttpWebResponse)spellAPIRequest2.GetResponse();

            if (spellAPIresponse2.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellAPIresponse2.GetResponseStream());

                string Spells2 = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(Spells2);

                ViewBag.Spells2 = jsonSpells["results"];
            }
            HttpWebRequest spellAPIRequest3 = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}/level/3");

            spellAPIRequest3.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellAPIresponse3 = (HttpWebResponse)spellAPIRequest3.GetResponse();

            if (spellAPIresponse3.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellAPIresponse3.GetResponseStream());

                string Spells3 = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(Spells3);

                ViewBag.Spells3 = jsonSpells["results"];
            }
            HttpWebRequest spellAPIRequest4 = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}/level/4");

            spellAPIRequest4.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellAPIresponse4 = (HttpWebResponse)spellAPIRequest4.GetResponse();

            if (spellAPIresponse4.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellAPIresponse4.GetResponseStream());

                string Spells4 = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(Spells4);

                ViewBag.Spells4 = jsonSpells["results"];
            }
            HttpWebRequest spellAPIRequest5 = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}/level/5");

            spellAPIRequest5.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellAPIresponse5 = (HttpWebResponse)spellAPIRequest5.GetResponse();

            if (spellAPIresponse5.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellAPIresponse5.GetResponseStream());

                string Spells5 = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(Spells5);

                ViewBag.Spells5 = jsonSpells["results"];
            }
            HttpWebRequest spellAPIRequest6 = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}/level/6");

            spellAPIRequest6.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellAPIresponse6 = (HttpWebResponse)spellAPIRequest6.GetResponse();

            if (spellAPIresponse6.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellAPIresponse6.GetResponseStream());

                string Spells6 = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(Spells6);

                ViewBag.Spells6 = jsonSpells["results"];
            }
            HttpWebRequest spellAPIRequest7 = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}/level/7");

            spellAPIRequest7.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellAPIresponse7 = (HttpWebResponse)spellAPIRequest7.GetResponse();

            if (spellAPIresponse1.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellAPIresponse7.GetResponseStream());

                string Spells7 = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(Spells7);

                ViewBag.Spells7 = jsonSpells["results"];
            }
            HttpWebRequest spellAPIRequest8 = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}/level/8");

            spellAPIRequest8.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellAPIresponse8 = (HttpWebResponse)spellAPIRequest8.GetResponse();

            if (spellAPIresponse8.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellAPIresponse8.GetResponseStream());

                string Spells8 = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(Spells8);

                ViewBag.Spells8 = jsonSpells["results"];
            }
            HttpWebRequest spellAPIRequest9 = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/spells/{Names[Class - 1]}/level/9");

            spellAPIRequest9.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse spellAPIresponse9 = (HttpWebResponse)spellAPIRequest9.GetResponse();

            if (spellAPIresponse9.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseData = new StreamReader(spellAPIresponse9.GetResponseStream());

                string Spells9 = responseData.ReadToEnd();

                JObject jsonSpells = JObject.Parse(Spells9);

                ViewBag.Spells9 = jsonSpells["results"];
            }

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.User = User.Identity.GetUserId();
            }

            return View();
        }

        public ActionResult CharacterSubmit(Character newCharacter, string[] Proficiencies, string[] SpellsKnown)
        {

            string featuresString = "";

            string characterLevel;

            for (int i = 1; i <= newCharacter.CharLevel; i++)
            {

                characterLevel = i.ToString();
                HttpWebRequest dndFeatureApiRequest = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/classes/{Names[newCharacter.Class - 1]}/level/{characterLevel}");

                dndFeatureApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

                HttpWebResponse dndFeatureApiResponse = (HttpWebResponse)dndFeatureApiRequest.GetResponse();

                if (dndFeatureApiResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader featureResponseData = new StreamReader(dndFeatureApiResponse.GetResponseStream());

                    string featuredata = featureResponseData.ReadToEnd();

                    JObject jsonFeatures = JObject.Parse(featuredata);

                    for (int j = 0; j < jsonFeatures["features"].Count(); j++)
                    {
                        featuresString += jsonFeatures["features"][j]["name"] + ",";
                    }
                }
            }


            newCharacter.Features = featuresString;

            characterLevel = newCharacter.CharLevel.ToString();
            HttpWebRequest dndSpellSlotApiRequest = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/classes/{Names[newCharacter.Class - 1]}/level/{characterLevel}");
            dndSpellSlotApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse dndSpellSlotApiResponse = (HttpWebResponse)dndSpellSlotApiRequest.GetResponse();

            if (dndSpellSlotApiResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader SpellSlotResponseData = new StreamReader(dndSpellSlotApiResponse.GetResponseStream());

                string spellslotdata = SpellSlotResponseData.ReadToEnd();

                JObject jsonSpellSlot = JObject.Parse(spellslotdata);

                if (jsonSpellSlot["spellcasting"] != null)
                {
                    newCharacter.Cantrips = (int)jsonSpellSlot["spellcasting"]["cantrips_known"];
                    newCharacter.SpellSlot1 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_1"];
                    newCharacter.SpellSlot2 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_2"];
                    newCharacter.SpellSlot3 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_3"];
                    newCharacter.SpellSlot4 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_4"];
                    newCharacter.SpellSlot5 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_5"];
                    newCharacter.SpellSlot6 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_6"];
                    newCharacter.SpellSlot7 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_7"];
                    newCharacter.SpellSlot8 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_8"];
                    newCharacter.SpellSlot9 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_9"];

                }

            }

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
                ViewBag.HitDieSize = jsonClasses["hit_die"];
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

            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            Character characterToEdit = ORM.Characters.Find(CharacterID);

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.User = User.Identity.GetUserId();
                string userid = User.Identity.GetUserId();
                if (characterToEdit.UserID != userid)
                {
                    return RedirectToAction("ViewCharacter", new { CharacterID });
                }
            }

            ViewBag.CharacterData = characterToEdit;

            return View();
        }

        public ActionResult SaveChanges(Character UpdatedCharacter, string[] Proficiencies, string[] SpellsKnown)
        {
            string featuresString = "";

            string characterLevel;

            for (int i = 1; i <= UpdatedCharacter.CharLevel; i++)
            {

                characterLevel = i.ToString();
                HttpWebRequest dndFeatureApiRequest = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/classes/{Names[UpdatedCharacter.Class - 1]}/level/{characterLevel}");

                dndFeatureApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

                HttpWebResponse dndFeatureApiResponse = (HttpWebResponse)dndFeatureApiRequest.GetResponse();

                if (dndFeatureApiResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader featureResponseData = new StreamReader(dndFeatureApiResponse.GetResponseStream());

                    string featuredata = featureResponseData.ReadToEnd();

                    JObject jsonFeatures = JObject.Parse(featuredata);

                    for (int j = 0; j < jsonFeatures["features"].Count(); j++)
                    {
                        featuresString += jsonFeatures["features"][j]["name"] + ",";
                    }
                }
            }


            characterLevel = UpdatedCharacter.CharLevel.ToString();
            HttpWebRequest dndSpellSlotApiRequest = WebRequest.CreateHttp($"http://www.dnd5eapi.co/api/classes/{Names[UpdatedCharacter.Class - 1]}/level/{characterLevel}");
            dndSpellSlotApiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse dndSpellSlotApiResponse = (HttpWebResponse)dndSpellSlotApiRequest.GetResponse();

            if (dndSpellSlotApiResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader SpellSlotResponseData = new StreamReader(dndSpellSlotApiResponse.GetResponseStream());

                string spellslotdata = SpellSlotResponseData.ReadToEnd();

                JObject jsonSpellSlot = JObject.Parse(spellslotdata);

                if (jsonSpellSlot["spellcasting"] != null)
                {
                    UpdatedCharacter.Cantrips = (int)jsonSpellSlot["spellcasting"]["cantrips_known"];
                    UpdatedCharacter.SpellSlot1 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_1"];
                    UpdatedCharacter.SpellSlot2 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_2"];
                    UpdatedCharacter.SpellSlot3 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_3"];
                    UpdatedCharacter.SpellSlot4 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_4"];
                    UpdatedCharacter.SpellSlot5 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_5"];
                    UpdatedCharacter.SpellSlot6 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_6"];
                    UpdatedCharacter.SpellSlot7 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_7"];
                    UpdatedCharacter.SpellSlot8 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_8"];
                    UpdatedCharacter.SpellSlot9 = (int)jsonSpellSlot["spellcasting"]["spell_slots_level_9"];

                }

            }

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
            OldRecord.Features = UpdatedCharacter.Features;
            OldRecord.SpellSlot1 = UpdatedCharacter.SpellSlot1;
            OldRecord.SpellSlot2 = UpdatedCharacter.SpellSlot2;
            OldRecord.SpellSlot3 = UpdatedCharacter.SpellSlot3;
            OldRecord.SpellSlot4 = UpdatedCharacter.SpellSlot4;
            OldRecord.SpellSlot5 = UpdatedCharacter.SpellSlot5;
            OldRecord.SpellSlot6 = UpdatedCharacter.SpellSlot6;
            OldRecord.SpellSlot7 = UpdatedCharacter.SpellSlot7;
            OldRecord.SpellSlot8 = UpdatedCharacter.SpellSlot8;
            OldRecord.SpellSlot9 = UpdatedCharacter.SpellSlot9;

            ORM.Entry(OldRecord).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();


            return RedirectToAction("Tracker");
            //OldRecord.Feats = UpdateItem.Feats;
            //OldRecord.Equipment = UpdateItem.Equipment;
            //OldRecord.Features = UpdateItem.Features;
            //OldRecord.Campaign = UpdateItem.Campaign;
        }

        [Authorize]
        public ActionResult CreateCampaign()
        {
            ViewBag.User = User.Identity.GetUserId();

            return View();
        }

        public ActionResult SaveCampaign(Campaign newCampaign)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            ORM.Campaigns.Add(newCampaign);

            ORM.SaveChanges();

            return RedirectToAction("Campaign", new { id = newCampaign.CampaignID });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Tracker()
        {
            string loggedinuser = User.Identity.GetUserId();
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();
            ViewBag.CharacterToView = ORM.Characters.Where(x => x.UserID == loggedinuser).ToList();

            ViewBag.MyCampaigns = ORM.Campaigns.Where(x => x.DMUserID == loggedinuser).ToList();

            return View();
        }

        [HttpGet]
        public ActionResult ViewCharacter(int CharacterID)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            ViewBag.Character = ORM.Characters.Find(CharacterID);

            return View();
        }

        public ActionResult DeleteCharacter(int charID)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            Character characterToDelete = ORM.Characters.Find(charID);

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.User = User.Identity.GetUserId();
                string userid = User.Identity.GetUserId();
                if (characterToDelete.UserID != userid)
                {
                    return RedirectToAction("ViewCharacter", new { CharacterID = charID });
                }
            }

            ORM.Characters.Remove(characterToDelete);

            ORM.SaveChanges();

            return RedirectToAction("Tracker");
        }

        public ActionResult Campaign(int id)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            ViewBag.ListOfCharacters = ORM.Characters.Where(x => x.Campaign == id).ToList(); //list of characters in the campaign

            ViewBag.AvailableCharacters = ORM.Characters.Where(x => x.Campaign == null).ToList();

            ViewBag.Campaign = ORM.Campaigns.Find(id);

            ViewBag.User = User.Identity.GetUserId();

            return View();
        }

        [Authorize]
        public ActionResult AddToCampaign(int charID, int campaignid)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            Campaign CampValidation = ORM.Campaigns.Find(campaignid);

            string userid = User.Identity.GetUserId();

            if(CampValidation.DMUserID != userid)
            {
                return RedirectToAction("Campaign", new { id = campaignid });
            }
            Character charToEdit = ORM.Characters.Find(charID);

            charToEdit.Campaign = campaignid;

            ORM.Entry(charToEdit).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();

            ViewBag.ListOfCharacters = ORM.Characters.Where(x => x.Campaign == campaignid).ToList();

            return RedirectToAction("Campaign", new { id = campaignid });
        }

        [Authorize]
        public ActionResult RemoveCharacterFromCampaign(int charID, int campaignid)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            Campaign CampValidation = ORM.Campaigns.Find(campaignid);

            string userid = User.Identity.GetUserId();

            if (CampValidation.DMUserID != userid)
            {
                return RedirectToAction("Campaign", new { id = campaignid });
            }
            Character characterToRemove = ORM.Characters.Find(charID);

            characterToRemove.Campaign = null;
            ORM.Entry(characterToRemove).State = System.Data.Entity.EntityState.Modified;

            ORM.SaveChanges();

            ViewBag.ListOfCharacters = ORM.Characters.Where(x => x.Campaign == campaignid).ToList();

            return RedirectToAction("Campaign", new { id = campaignid });
        }

        public ActionResult DeleteCampaign(int campaignID)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            Campaign CampValidation = ORM.Campaigns.Find(campaignID);

            string userid = User.Identity.GetUserId();

            if (CampValidation.DMUserID != userid)
            {
                return RedirectToAction("Campaign", new { id = campaignID });
            }

            Campaign campaignToDelete = ORM.Campaigns.Find(campaignID);

            List<Character> tempcharacters = ORM.Characters.Where(x => x.Campaign == campaignID).ToList();

            foreach(Character c in tempcharacters)
            {
                c.Campaign = null;
            }

            ORM.Campaigns.Remove(campaignToDelete);

            ORM.SaveChanges();

            return RedirectToAction("Tracker");
        }

        [HttpGet]
        public ActionResult UserProfile(string userid)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            // redirect from charactersearch > character result > username (that created that character)

            // pull info from aspnetuser.id
            // pull info from characters.userid
            // pull info from campaigns.dm userid != null

            // display username from the userid
            // display characters from that userid
            // display campaigns if dm userid != null

            ViewBag.User = ORM.AspNetUsers.Find(userid);

            ViewBag.UserChar = ORM.Characters.Where(x => x.UserID == userid).ToList();

            ViewBag.UserCamp = ORM.Campaigns.Where(x => x.DMUserID == userid).ToList();

            return View();
        }

        [HttpGet]
        public ActionResult CCSearch()
        {

            return View();
        }

        public ActionResult SearchCharByName(string charName)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            List<Character> temp = ORM.Characters.Where(x => x.CharName.ToLower().Contains
            (charName.ToLower())).ToList();

            List<string> users = temp.Select(x => x.UserID).Distinct().ToList();

            ViewBag.Characters = ORM.Characters.Where(x => x.CharName.ToLower().Contains
            (charName.ToLower())).ToList();

            ViewBag.User = ORM.AspNetUsers.Where(x => users.Contains(x.Id)).ToList();

            return View("CharacterResult");
        }

        public ActionResult SearchCampaignByName(string campaignName, string userid)
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            List<Campaign> temp = ORM.Campaigns.Where(x => x.CampaignName.ToLower().Contains
            (campaignName.ToLower())).ToList();

            ViewBag.Campaigns = ORM.Campaigns.Where(x => x.CampaignName.ToLower().Contains
            (campaignName.ToLower())).ToList();

            List<string> users = temp.Select(x => x.DMUserID).Distinct().ToList();

            ViewBag.User = ORM.AspNetUsers.Where(x => users.Contains(x.UserName)).ToList();

            return View("CampaignResult");
        }

        public ActionResult BrowseCharacters()
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            ViewBag.CharList = ORM.Characters.ToList();
            return View();
        }

        public ActionResult BrowseCampaigns()
        {
            viewbagofholdingEntities ORM = new viewbagofholdingEntities();

            ViewBag.CampList = ORM.Campaigns.ToList();
            return View();
        }
    }
}