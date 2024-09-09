// Code by Anne-Lii Hansen anha2324@student.miun.se

using System;// inkluderar funktioner som Console och DateTime
using System.Globalization;//inkludera hantering av datum, tid och nummerformat

class Program //definierar huvudklassen med namnet Program
{

    static void Main(string[] args) //startpunkt main, startar med denna kod
    {
        Console.WriteLine("Skriv in ditt födelsedatum (YYYY-MM-DD):");//uppmaning att skriva ett datum
        string input = Console.ReadLine(); //inmatade datumet

        //omvandla input till ett giltigt datum
        if (!DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))//kontrollerar formatet på inmatningen
        {
            Console.WriteLine("Felaktigt datum eller format YYYY-MM-DD");//felmeddelande vid fel format eller felaktigt datum
            return;
        }
        
        //kontroll att datumet har passerat
        if (date > DateTime.Now)
        {
            Console.WriteLine("Detta är ett framtida datum. Ange ett datum som har passerat.");
            return;
        }

        int year = date.Year; // inmatat år
        int month = date.Month; // inmatad månad
        int day = date.Day; // inmatad dag

        //räkna ut århundrade och året inom århundradet
        int century = year / 100 ; //räknar ut århundrade
        int yearOfCentury = year % 100; //modulusoperatorn för att få fram året inom seklet

        //justera enligt Zellers algoritm. Om januari(1) och februari(2) lägg till 12 på månadsnumret och ta bort ett år
        if (month < 3)
        {
            month += 12;
            year -= 1;
        }

        //uträkning av veckodag med Zellers algoritm
        //weekday = (d + ((13*(m+1))/5) + y + (y/4) + (c/4) + 5*c ) % 7;
        int weekday = (day + ((13 * (month + 1)) / 5) + yearOfCentury + (yearOfCentury / 4) + (century / 4) + (5 * century)) % 7; // ger resultat i tal 0 till 6 där 0 är lördag.

        //konvertera veckodag till ISO standard alltså måndag = 1 och söndag = 7 enligt beskrivning
        //dayOfWeek = ( ( dayOfWeek + 5 ) mod 7 ) + 1
        weekday = ((weekday + 5) % 7) + 1;

        //string med veckans dagar med index 1 till 7
        string[] weekdays = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag", "Lördag", "Söndag" };

        Console.WriteLine($"Din födelsedag {input} inträffade på en {weekdays[weekday - 1]}.");  // -1 då index vanligtvis startar på 0  
    }
}

