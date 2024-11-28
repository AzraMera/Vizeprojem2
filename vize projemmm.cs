using System;
using System.Collections.Generic;

// Temel sınıf (BaseClass)
public class BaseClass
{
    public int Id { get; set; }
    public string Ad { get; set; }

    public BaseClass(int id, string ad)
    {
        Id = id;
        Ad = ad;
    }

    public virtual void BilgiGoster()
    {
        Console.WriteLine($"ID: {Id}, Ad: {Ad}");
    }
}

// IPerson Interface
public interface IPerson
{
    void Login();
}

// Öğrenci Sınıfı (Ogrenci)
public class Ogrenci : BaseClass, IPerson
{
    public string Bolum { get; set; }

    public Ogrenci(int id, string ad, string bolum)
        : base(id, ad)
    {
        Bolum = bolum;
    }

    public void Login()
    {
        Console.WriteLine($"{Ad} başarıyla giriş yaptı.");
    }

    public override void BilgiGoster()
    {
        base.BilgiGoster();
        Console.WriteLine($"Bölüm: {Bolum}");
    }
}

// Öğretim Görevlisi Sınıfı (OgretimGorevlisi)
public class OgretimGorevlisi : BaseClass, IPerson
{
    public string Unvan { get; set; }

    public OgretimGorevlisi(int id, string ad, string unvan)
        : base(id, ad)
    {
        Unvan = unvan;
    }

    public void Login()
    {
        Console.WriteLine($"{Ad} öğretim görevlisi olarak giriş yaptı.");
    }

    public override void BilgiGoster()
    {
        base.BilgiGoster();
        Console.WriteLine($"Unvan: {Unvan}");
    }
}

// Ders Sınıfı (Ders)
public class Ders
{
    public string DersAdi { get; set; }
    public int Kredi { get; set; }
    public OgretimGorevlisi OgretimGorevlisi { get; set; }
    public List<Ogrenci> KayitliOgrenciler { get; set; }

    public Ders(string dersAdi, int kredi, OgretimGorevlisi ogretimGorevlisi)
    {
        DersAdi = dersAdi;
        Kredi = kredi;
        OgretimGorevlisi = ogretimGorevlisi;
        KayitliOgrenciler = new List<Ogrenci>();
    }

    public void OgrenciKaydet(Ogrenci ogrenci)
    {
        KayitliOgrenciler.Add(ogrenci);
    }

    // Dersin bilgilerini ve kayıtlı öğrencileri listeleyen fonksiyon
    public void DersBilgisiGoster()
    {
        Console.WriteLine($"\nDers Adı: {DersAdi}");
        Console.WriteLine($"Kredi: {Kredi}");
        Console.WriteLine($"Öğretim Görevlisi: {OgretimGorevlisi.Ad}");

        // Kayıtlı öğrenciler listesi
        Console.WriteLine("Kayıtlı Öğrenciler:");

        if (KayitliOgrenciler.Count == 0)
        {
            // Eğer öğrenci kaydı yoksa
            Console.WriteLine("Henüz kaydolmuş öğrenci yok.");
        }
        else
        {
            foreach (var ogrenci in KayitliOgrenciler)
            {
                ogrenci.BilgiGoster();
            }
        }
    }
}

// Ana Program
public class Program
{
    private static List<Ogrenci> ogrenciler = new List<Ogrenci>();
    private static List<OgretimGorevlisi> ogretmenler = new List<OgretimGorevlisi>();
    private static List<Ders> dersler = new List<Ders>();

    public static void Main()
    {
        while (true)
        {
            // Menü ekranı
            Console.Clear();
            Console.WriteLine("Ana Menü:");
            Console.WriteLine("1. Öğrenci Ekle");
            Console.WriteLine("2. Öğretim Görevlisi Ekle");
            Console.WriteLine("3. Ders Ekle");
            Console.WriteLine("4. Ders Bilgisi Göster");
            Console.WriteLine("5. Çıkış");
            Console.Write("Bir seçenek girin (1-5): ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    OgrenciEkle();
                    break;

                case "2":
                    OgretimGorevlisiEkle();
                    break;

                case "3":
                    DersEkle();
                    break;

                case "4":
                    DersBilgisiGoster();
                    break;

                case "5":
                    Console.WriteLine("Çıkılıyor...");
                    return;

                default:
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                    break;
            }

            Console.WriteLine("\nDevam etmek için bir tuşa basın...");
            Console.ReadKey();
        }
    }

    // Öğrenci ekleme fonksiyonu
    private static void OgrenciEkle()
    {
        Console.WriteLine("\nÖğrenci Ekle");

        Console.Write("Öğrenci Adı: ");
        string ad = Console.ReadLine();

        Console.Write("Öğrenci Bölümü: ");
        string bolum = Console.ReadLine();

        int id = ogrenciler.Count + 1; // ID otomatik olarak artacak
        Ogrenci ogrenci = new Ogrenci(id, ad, bolum);

        ogrenciler.Add(ogrenci);
        Console.WriteLine($"Öğrenci {ad} başarıyla eklendi.");
    }

    // Öğretim Görevlisi ekleme fonksiyonu
    private static void OgretimGorevlisiEkle()
    {
        Console.WriteLine("\nÖğretim Görevlisi Ekle");

        Console.Write("Öğretim Görevlisi Adı: ");
        string ad = Console.ReadLine();

        Console.Write("Öğretim Görevlisi Unvanı: ");
        string unvan = Console.ReadLine();

        int id = ogretmenler.Count + 1; // ID otomatik olarak artacak
        OgretimGorevlisi ogretimGorevlisi = new OgretimGorevlisi(id, ad, unvan);

        ogretmenler.Add(ogretimGorevlisi);
        Console.WriteLine($"Öğretim Görevlisi {ad} başarıyla eklendi.");
    }

    // Ders ekleme fonksiyonu
    private static void DersEkle()
    {
        Console.WriteLine("\nDers Ekle");

        Console.Write("Ders Adı: ");
        string dersAdi = Console.ReadLine();

        Console.Write("Ders Kredisi: ");
        int kredi = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Öğretim Görevlisi Seçin:");

        for (int i = 0; i < ogretmenler.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ogretmenler[i].Ad} ({ogretmenler[i].Unvan})");
        }

        Console.Write("Bir öğretim görevlisi seçin: ");
        int ogretimGorevlisiSecim = Convert.ToInt32(Console.ReadLine()) - 1;

        if (ogretimGorevlisiSecim >= 0 && ogretimGorevlisiSecim < ogretmenler.Count)
        {
            OgretimGorevlisi ogretimGorevlisi = ogretmenler[ogretimGorevlisiSecim];
            Ders ders = new Ders(dersAdi, kredi, ogretimGorevlisi);

            // Dersleri listelemeden önce öğrenci kaydetme
            Console.WriteLine("Öğrenci kaydetmek ister misiniz? (Evet/Hayır)");
            string secim = Console.ReadLine().ToLower();

            if (secim == "evet")
            {
                Console.WriteLine("Öğrenciler:");
                for (int i = 0; i < ogrenciler.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ogrenciler[i].Ad}");
                }

                Console.Write("Öğrenci seçin: ");
                int ogrenciSecim = Convert.ToInt32(Console.ReadLine()) - 1;

                if (ogrenciSecim >= 0 && ogrenciSecim < ogrenciler.Count)
                {
                    ders.OgrenciKaydet(ogrenciler[ogrenciSecim]);
                    Console.WriteLine("Öğrenci derse kaydedildi.");
                }
            }

            dersler.Add(ders);
            Console.WriteLine("Ders başarıyla eklendi.");
        }
        else
        {
            Console.WriteLine("Geçersiz öğretim görevlisi seçimi.");
        }
    }

    // Ders bilgilerini gösterme fonksiyonu
    private static void DersBilgisiGoster()
    {
        Console.WriteLine("\nTüm Dersler:");

        if (dersler.Count == 0)
        {
            Console.WriteLine("Henüz ders eklenmemiş.");
        }
        else
        {
            foreach (var ders in dersler)
            {
                ders.DersBilgisiGoster();
            }
        }
    }
}
