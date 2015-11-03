using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using scMessage;
using UnityEngine;

namespace OpenDataScreen
{
    //This class collect the preferences of the opedDataquery and creates the components of message 
    public class HandlePlayerQuery
    {
        //if player havent set values to all options
        public bool emptyDropdown;      

        //empty constructor
        public HandlePlayerQuery()
        {
          
        }

        //this method gets a raw message with the prefs of user
        //and returns a message ready to send to server or null
        public message messageOpenDataReady(message m)
        {
            //Initialize
            emptyDropdown = false;
            //INPUT
            string county, month, year, accidentType;
            //OUTPUT
            string elCounty,dateMY,table;

            //GET INPUT
            county = m.getSCObject(0).getString("county");
            month = m.getSCObject(0).getString("month");
            year = m.getSCObject(0).getString("year");
            accidentType = m.getSCObject(0).getString("accidentType");

            //PREPARE OUTPUT
            elCounty = getCountyInGreek(county);
            dateMY = getMonth(month) + "-" + year;
            table = getAccidentType(accidentType);


            //Build a ReadyTOSend message
            message newMes = new message("opendata");
            scObject obj = new scObject("OpenDataObject");
            obj.addString("county", elCounty);
            obj.addString("dateMY", dateMY);
            obj.addString("table", table);

            newMes.addSCObject(obj);

            //return it
            if (!emptyDropdown)
            {
                if(!accidentType.Equals("Lethals"))
                {
                    int num;
                    if(month.Equals("Select month") || year.Equals("Select year") || !(int.TryParse(year, out num)))
                    {
                        return null;
                    }
                    else
                    {
                        return newMes;
                    }
                }
                return newMes;
            }
            else
                return null;
        }

        private string getCountyInGreek(String c)
        {

            string countyInGreek = null;
            switch (c)
            {
                case "ATTIKHS":
                    countyInGreek = "ΑΤΤΙΚΗΣ";
                    break;
                case "AITOLIAS":
                    countyInGreek = "ΑΙΤΩΛΙΑΣ";
                    break;
                case "AKARNANIAS":
                    countyInGreek = "ΑΚΑΡΝΑΝΙΑΣ";
                    break;
                case "ALEX/POLHS":
                    countyInGreek = "ΑΛΕΞ/ΠΟΛΗΣ";
                    break;
                case "ARGOLIDAS":
                    countyInGreek = "ΑΡΓΟΛΙΔΑΣ";
                    break;
                case "ARKADIAS":
                    countyInGreek = "ΑΡΚΑΔΙΑΣ";
                    break;
                case "ARTAS":
                    countyInGreek = "ΑΡΤΑΣ";
                    break;
                case "ACHAIAS":
                    countyInGreek = "ΑΧΑΪΑΣ";
                    break;
                case "VOIOTIAS":
                    countyInGreek = "ΒΟΙΩΤΙΑΣ";
                    break;
                case "GREVENON":
                    countyInGreek = "ΓΡΕΒΕΝΩΝ";
                    break;
                case "DRAMAS":
                    countyInGreek = "ΔΡΑΜΑΣ";
                    break;
                case "DODEKANHSOY":
                    countyInGreek = "ΔΩΔΕΚΑΝΗΣΟΥ";
                    break;
                case "EYVOIAS":
                    countyInGreek = "ΕΥΒΟΙΑΣ";
                    break;
                case "EYRYTANIAS":
                    countyInGreek = "ΕΥΡΥΤΑΝΙΑΣ";
                    break;
                case "ZAKYNTHOY":
                    countyInGreek = "ΖΑΚΥΝΘΟΥ";
                    break;
                case "HLEIAS":
                    countyInGreek = "ΗΛΕΙΑΣ";
                    break;
                case "HMATHIAS":
                    countyInGreek = "ΗΜΑΘΙΑΣ";
                    break;
                case "HRAKLEIOY":
                    countyInGreek = "ΗΡΑΚΛΕΙΟΥ";
                    break;
                case "THES/NIKHS":
                    countyInGreek = "θΕΣ/ΝΙΚΗΣ";
                    break;
                case "THESPROTIAS":
                    countyInGreek = "ΘΕΣΠΡΩΤΙΑΣ";
                    break;
                case "IOANNINON":
                    countyInGreek = "ΙΩΑΝΝΙΝΩΝ";
                    break;
                case "KAVALAS":
                    countyInGreek = "ΚΑΒΑΛΑΣ";
                    break;
                case "KARDITSAS":
                    countyInGreek = "ΚΑΡΔΙΤΣΑΣ";
                    break;
                case "KASTORIAS":
                    countyInGreek = "ΚΑΣΤΟΡΙΑΣ";
                    break;
                case "KERKYRAS":
                    countyInGreek = "ΚΕΡΚΥΡΑΣ";
                    break;
                case "KEFALLHNIAS":
                    countyInGreek = "ΚΕΦΑΛΛΗΝΙΑΣ";
                    break;
                case "KILKIS":
                    countyInGreek = "ΚΙΛΚΙΣ";
                    break;
                case "KOZANHS":
                    countyInGreek = "ΚΟΖΑΝΗΣ";
                    break;
                case "KORINTHIAS":
                    countyInGreek = "ΚΟΡΙΝΘΙΑΣ";
                    break;
                case "KYKLADON":
                    countyInGreek = "ΚΥΚΛΑΔΩΝ";
                    break;
                case "LAKONIAS":
                    countyInGreek = "ΛΑΚΩΝΙΑΣ";
                    break;
                case "LARISHS":
                    countyInGreek = "ΛΑΡΙΣΗΣ";
                    break;
                case "LASITHIOY":
                    countyInGreek = "ΛΑΣΙΘΙΟΥ";
                    break;
                case "LESVOY":
                    countyInGreek = "ΛΕΣΒΟΥ";
                    break;
                case "LEYKADOS":
                    countyInGreek = "ΛΕΥΚΑΔΟΣ";
                    break;
                case "MAGNHSIAS":
                    countyInGreek = "ΜΑΓΝΗΣΙΑΣ";
                    break;
                case "MESSHNIAS":
                    countyInGreek = "ΜΕΣΣΗΝΙΑΣ";
                    break;
                case "XANTHIS":
                    countyInGreek = "ΞΑΝΘΗΣ";
                    break;
                case "ORESTIADAS":
                    countyInGreek = "ΟΡΕΣΤΙΑΔΑΣ";
                    break;
                case "PELLAS":
                    countyInGreek = "ΠΕΛΛΑΣ";
                    break;
                case "PIERIAS":
                    countyInGreek = "ΠΙΕΡΙΑΣ";
                    break;
                case "PREVEZHS":
                    countyInGreek = "ΠΡΕΒΕΖΗΣ";
                    break;
                case "RETHYMNHS":
                    countyInGreek = "ΡΕΘΥΜΝΗΣ";
                    break;
                case "RODOPHS":
                    countyInGreek = "ΡΟΔΟΠΗΣ";
                    break;
                case "SAMOY":
                    countyInGreek = "ΣΑΜΟΥ";
                    break;
                case "SERRON":
                    countyInGreek = "ΣΕΡΡΩΝ";
                    break;
                case "TRIKALON":
                    countyInGreek = "ΤΡΙΚΑΛΩΝ";
                    break;
                case "FTHIOTIDAS":
                    countyInGreek = "ΦΘΙΩΤΙΔΑΣ";
                    break;
                case "FLORINAS":
                    countyInGreek = "ΦΛΩΡΙΝΑΣ";
                    break;
                case "FOKIDOS":
                    countyInGreek = "ΦΩΚΙΔΑΣ";
                    break;
                case "CHALKIDIKHS":
                    countyInGreek = "ΧΑΛΚΙΔΙΚΗΣ";
                    break;
                case "CHANION":
                    countyInGreek = "ΧΑΝΙΩΝ";
                    break;
                case "CHIOY":
                    countyInGreek = "ΧΙΟΥ";
                    break;
                default:
                    emptyDropdown = true;
                    break;
            }
            return countyInGreek;
        }

        private string getAccidentType(String t)
        {
            string tableName = null;
            switch(t)
            {
                case "Lethals":
                    tableName = "Lethal_Accidents";
                    break;
                case "Slights":
                    tableName = "Slight_Accidents";
                    break;
                case "Serious":
                    tableName = "Serious_Accidents";
                    break;
                default:
                    emptyDropdown = true;
                    break;

            }
            return tableName;
        }

        private string getMonth(string m)
        {
            string month=null;
            switch (m)
            {
                case "January":
                    month = "Ιαν";
                    break;
                case "February":
                    month = "Φεβ";
                    break;
                case "March":
                    month = "Μαρ";
                    break;
                case "April":
                    month = "Απρ";
                    break;
                case "May":
                    month = "Μαϊ";
                    break;
                case "June":
                    month = "Ιουν";
                    break;
                case "July":
                    month = "Ιουλ";
                    break;
                case "August":
                    month = "Αυγ";
                    break;
                case "September":
                    month = "Σεπ";
                    break;
                case "October":
                    month = "Οκτ";
                    break;
                case "November":
                    month = "Νοε";
                    break;
                case "December":
                    month = "Δεκ";
                    break;
               
            }
            return month;
        }

    }
}
