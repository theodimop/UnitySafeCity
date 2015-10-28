using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using scMessage;

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
            string county, day, month, year, accidentType,toDay,toMonth,toYear;
            //OUTPUT
            string elCounty,fromDate, toDate,table;

            //GET INPUT
            county = m.getSCObject(0).getString("county");
            day= m.getSCObject(0).getString("day");
            month = m.getSCObject(0).getString("month");
            year = m.getSCObject(0).getString("year");
            accidentType = m.getSCObject(0).getString("accidentType");
            toDay = m.getSCObject(0).getString("toDay");
            toMonth = m.getSCObject(0).getString("toMonth");
            toYear = m.getSCObject(0).getString("toYear");
            
            //check empty lists
            checkEmptyDropDownList(day,toDay,year,toYear);

            //PREPARE OUTPUT
            elCounty = getCountyInGreek(county);
            fromDate = day + "-" + getMonth(month) + "-" + year;
            toDate = toDay + "-" + getMonth(toMonth) + "-" + toYear;
            table = getAccidentType(accidentType);

            //Build a ReadyTOSend message
            message newMes = new message("opendata");
            scObject obj = new scObject("OpenDataObject");
            obj.addString("county", elCounty);
            obj.addString("fromDate", fromDate);
            obj.addString("toDate", toDate);
            obj.addString("table", table);

            newMes.addSCObject(obj);

            //return it
            if (!emptyDropdown)
                return newMes;
            else
                return null;
        }

        private void checkEmptyDropDownList(string day, string toDay, string year, string toYear)
        {
            int num=0;
            if (!(int.TryParse(day, out num) || int.TryParse(toDay, out num) || int.TryParse(year, out num) || int.TryParse(toYear, out num)))
            {
                emptyDropdown = true;
            }
        }

        private string getCountyInGreek(String c)
        {
            /*Δ. ΤΡΟΧ. ΑΤΤΙΚΗΣ
Δ. ΤΡΟΧ.ΘΕΣ/ΝΙΚΗΣ
Α.Δ. ΑΙΤΩΛΙΑΣ
Α.Δ. ΑΚΑΡΝΑΝΙΑΣ
Α.Δ. ΑΛΕΞ/ΠΟΛΗΣ
Α.Δ. ΑΡΓΟΛΙΔΑΣ
ΑΔ ΑΡΚΑΔΙΑΣ
Α.Δ. ΑΡΤΑΣ
Α.Δ. ΑΧΑΪΑΣ
Α.Δ. ΒΟΙΩΤΙΑΣ
Α.Δ. ΓΡΕΒΕΝΩΝ
Α.Δ. ΔΡΑΜΑΣ
Α.Δ. ΔΩΔΕΚΑΝΗΣΟΥ
Α.Δ. ΕΥΒΟΙΑΣ
Α.Δ. ΕΥΡΥΤΑΝΙΑΣ 
Α.Δ. ΖΑΚΥΝΘΟΥ
Α.Δ. ΗΛΕΙΑΣ 
Α.Δ. ΗΜΑΘΙΑΣ
Α.Δ. ΗΡΑΚΛΕΙΟΥ
Α.Δ. ΘΕΣΠΡΩΤΙΑΣ
Α.Δ. ΙΩΑΝΝΙΝΩΝ
Α.Δ. ΚΑΒΑΛΑΣ 
Α.Δ. ΚΑΡΔΙΤΣΑΣ
Α.Δ. ΚΑΣΤΟΡΙΑΣ
Α.Δ. ΚΕΡΚΥΡΑΣ
Α.Δ. ΚΕΦΑΛΛΗΝΙΑΣ
Α.Δ. ΚΙΛΚΙΣ
Α.Δ. ΚΟΖΑΝΗΣ
Α.Δ. ΚΟΡΙΝΘΙΑΣ
Α.Δ. ΚΥΚΛΑΔΩΝ
Α.Δ. ΛΑΚΩΝΙΑΣ
Α.Δ. ΛΑΡΙΣΗΣ
Α.Δ. ΛΑΣΙΘΙΟΥ
Α.Δ. ΛΕΣΒΟΥ
Α.Δ. ΛΕΥΚΑΔΟΣ
Α.Δ. ΜΑΓΝΗΣΙΑΣ
Α.Δ. ΜΕΣΣΗΝΙΑΣ
Α.Δ. ΞΑΝΘΗΣ
Α.Δ. ΟΡΕΣΤΙΑΔΑΣ
Α.Δ. ΠΕΛΛΑΣ
Α.Δ. ΠΙΕΡΙΑΣ
Α.Δ. ΠΡΕΒΕΖΗΣ
Α.Δ. ΡΕΘΥΜΝΗΣ
Α.Δ. ΡΟΔΟΠΗΣ
Α.Δ. ΣΑΜΟΥ
Α.Δ. ΣΕΡΡΩΝ
Α.Δ. ΤΡΙΚΑΛΩΝ
Α.Δ. ΦΘΙΩΤΙΔΑΣ
Α.Δ. ΦΛΩΡΙΝΑΣ
Α.Δ. ΦΩΚΙΔΑΣ
Α.Δ. ΧΑΛΚΙΔΙΚΗΣ
Α.Δ. ΧΑΝΙΩΝ
Α.Δ. ΧΙΟΥ
Δ. ΑΣΤ.ΠΕΙΡΑΙΑ
Δ.Α.Α. ΑΘΗΝΩΝ
Δ.Α.Κ.Α. ΘΕΣ/ΝΙΚΗΣ
*/
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
                default:
                    emptyDropdown = true;
                    break;
            }
            return month;
        }

    }
}
