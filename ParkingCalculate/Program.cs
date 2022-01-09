using ParkingCalculate.Model;
using System;
using System.Collections.Generic;

namespace ParkingCalculate
{
    class Program
    {
        public static List<Cars> cars;
        static void Main(string[] args)
        {
            // Cars clasımdan cars adında liste oluşturdum
            cars = new List<Cars>();

            while (true)
            {
                //seçim için ekrana yazdım
                Console.WriteLine("");
                Console.WriteLine("1. Yeni araç girişi \n" + "2- Araç çıkışı");
                Console.WriteLine("");

                //kullanıcıdan alınan değeri integera çevirdim
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    //kullanıcı 1 seçimi yapınca çalışacak komut
                    case 1:
                        Console.WriteLine("otoparka giriş yapıcak plakayı giriniz");
                        string Plate = Console.ReadLine();

                        //CheckPlate fonksiyonuna istek gönderdim ve plateResult1 degişkenine atadım
                        var plateResult1 = CheckPlate(Plate);

                        //giriş yapıcak plaka mevcut mu? mevcut ise çalışacak komut
                        if (plateResult1 != null)
                        {
                            Console.WriteLine("Böyle bir plaka mevcut");
                            break;
                        }
                        //giriş yapıcak plaka mevcut değil ise çalışacak komut
                        Console.WriteLine("giriş saati giriniz");
                        //Kullanıcıdan giriş saati alan komut
                        DateTime starttime;
                        if (!DateTime.TryParse(Console.ReadLine(), out starttime))
                        {
                            Console.WriteLine("Hatalı tarih/saat formatı");
                            break;
                        }

                        //AddPlate fonksiyonuna istek gönderdim
                        AddPlate(Plate, starttime);

                        break;
                    //kullanıcı 2 seçimini yapınca çalışacak komut
                    case 2:
                        Console.WriteLine("otoparktan çıkış yapıcak plakayı giriniz");
                        string plate = Console.ReadLine();

                        //CheckPlate fonksiyonuna istek gönderdim ve plateResult2  değişkenine atadım
                        var plateResult2 = CheckPlate(plate);

                        //çıkış yapıcak plaka mevcut mu? mevcut değil ise çalışacak komut
                        if (CheckPlate(plate) == null)
                        {
                            Console.WriteLine("Aradığınız plaka bulunamadı.");
                            break;
                        }
                        // çıkış yapıcak plaka mevcut değil ise çalışacak komut
                        Console.WriteLine("cıkıs saati giriniz");
                        //Kullanıcıdan çıkış saati alan komut
                        DateTime endTime;
                        if (!DateTime.TryParse(Console.ReadLine(), out endTime))
                        {
                            Console.WriteLine("Hatalı tarih/saat formatı");
                            break;
                        }

                        // CalculatePrice fonksiyonuna istek gönderdim ve tutar değişkenine atadım

                        double tutar = CalculatePrice(plateResult2, endTime);
                        Console.WriteLine($"Ödenecek tutar: {tutar} TL");


                        break;
                    default:
                        // menüde 1 ve 2 dışında seçim olursa çalışacak komut
                        Console.WriteLine("Tanımlanamayan seçim");
                        break;
                }
            }

        }
        //AddPlate adında fonksiyon oluşturdum
        static void AddPlate(string plate, DateTime startTime)
        {
            cars.Add(new Cars
            {
                startTime = startTime, //kullanıcıdan aldıgım giriş tarihini Cars clasımdaki startTime değişkenine ekledim
                plate = plate ////kullanıcıdan aldıgım plakayı Cars clasımdaki plate değişkenine ekledim
            });
        }

        static Cars CheckPlate(string plate)
        {
            var result = cars.Find(x => x.plate == plate);
            //kullanıcıdan aldığım çıkış yapılan plaka cars listemdeki herhangi bir plate(plakaya)'e eşit ise çalışacak şartım
            if (result != null)
            {
                return result;
            }
            //kullanıcıdan aldığım çıkış yapılan plate(plaka) cars listemdeki herhangi bir plate(plakaya)'e eşit değil ise
            return null;
        }
        //ücret hesaplaması için oluşturduğum fonksiyon
       
        static double CalculatePrice(Cars plateResult, DateTime endTime)
        {
          

            double ucret = 1;

            double time = (endTime.Hour - plateResult.startTime.Hour);

            if (endTime.Hour < plateResult.startTime.Hour)
            {
                time =+ 24;
            }


            if (time==0) { time = 1; }

            if (time <= 3)
            {
                ucret = time * 3;
            }
            else if (time > 3 && time <= 6)
            {
                ucret = (time-3) * 2 + 9; 
            }
            else if (time > 6 && time <= 9)
            {
                ucret = (time - 6)* 5 + 15;
            }
            else if (time > 9 && time <= 12)
            {
                ucret = (time - 9) * 5 + 20;
            }
            else if (time > 12 && time <= 15)
            {
                ucret = (time - 12) * 5 + 25;
            }
            else if (time > 15 && time <= 18)
            {
                ucret = (time - 15) * 5 + 30;
            }
            else if (time > 18 && time <= 21)
            {
                ucret = (time - 18)*5 + 35;
            }
            else if (time > 21 && time <= 24)
            {
                ucret = (time - 21)*5 + 40;
            }
            cars.Remove(plateResult);

            return ucret;
        }
    }
}
