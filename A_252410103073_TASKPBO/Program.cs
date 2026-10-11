using System;
using System.Collections.Generic;

namespace RumahSakitApp
{
    public class Orang
    {
        public string Nama { get; set; }
        public int Umur { get; set; }

        public Orang(string nama, int umur)
        {
            Nama = nama;
            Umur = umur;
        }

        public virtual void Aktivitas()
        {
            Console.WriteLine($"{Nama} sedang melakukan aktivitas umum.");
        }

        public void InfoOrang()
        {
            Console.WriteLine($"Nama: {Nama}, Umur: {Umur} tahun");
        }
    }

    public class Pasien : Orang
    {
        public string Keluhan { get; set; }

        public Pasien(string nama, int umur, string keluhan) : base(nama, umur)
        {
            Keluhan = keluhan;
        }

        public void CekKeluhan()
        {
            Console.WriteLine($"Keluhan {Nama}: {Keluhan}");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"{Nama} sedang beristirahat menunggu antrian.");
        }
    }

    public class TenagaMedis : Orang
    {
        public string Spesialisasi { get; set; }

        public TenagaMedis(string nama, int umur, string spesialisasi) : base(nama, umur)
        {
            Spesialisasi = spesialisasi;
        }

        public void CekSpesialis()
        {
            Console.WriteLine($"Spesialisasi {Nama}: {Spesialisasi}");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"{Nama} bersiap memberikan pelayanan medis.");
        }
    }

    public class Dokter : TenagaMedis
    {
        public Dokter(string nama, int umur, string spesialisasi) : base(nama, umur, spesialisasi) { }

        public void Diagnosa(Pasien pasien)
        {
            Console.WriteLine($"Dokter {Nama} mendiagnosa {pasien.Nama} dengan keluhan {pasien.Keluhan}.");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"Dokter {Nama} sedang memeriksa pasien di ruangannya.");
        }
    }

    public class Perawat : TenagaMedis
    {
        public Perawat(string nama, int umur) : base(nama, umur, "Keperawatan Umum") { }

        public void CekPasien(Pasien pasien)
        {
            Console.WriteLine($"Perawat {Nama} mengecek kondisi vital {pasien.Nama}.");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"Perawat {Nama} sedang berkeliling bangsal.");
        }
    }

    public class PasienAnak : Pasien
    {
        public PasienAnak(string nama, int umur, string keluhan) : base(nama, umur, keluhan) { }

        public void Menangis()
        {
            Console.WriteLine($"{Nama} menangis karena takut disuntik.");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"{Nama} sedang bermain di ruang tunggu anak.");
        }
    }

    public class PasienDewasa : Pasien
    {
        public PasienDewasa(string nama, int umur, string keluhan) : base(nama, umur, keluhan) { }

        public void Konsultasi()
        {
            Console.WriteLine($"{Nama} sedang berkonsultasi mengenai pengobatan.");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"{Nama} sedang membaca majalah sambil menunggu.");
        }
    }

    public class RumahSakit
    {
        private List<Orang> daftarOrang = new List<Orang>();

        public void TambahOrang(Orang orang)
        {
            daftarOrang.Add(orang);
            Console.WriteLine($"{orang.Nama} telah ditambahkan ke sistem Rumah Sakit.");
        }

        public void DaftarOrang()
        {
            Console.WriteLine("\nDaftar Orang di Rumah Sakit :");
            foreach (var orang in daftarOrang)
            {
                orang.InfoOrang();

                // Menampilkan informasi tambahan berdasarkan tipe
                if (orang is TenagaMedis medis)
                {
                    Console.WriteLine($"  Peran: Tenaga Medis ({medis.Spesialisasi})");
                }
                else if (orang is Pasien pasien)
                {
                    Console.WriteLine($"  Peran: Pasien (Keluhan: {pasien.Keluhan})");
                }
                else
                {
                    Console.WriteLine("  Peran: Pengunjung/Staff Umum");
                }
            }
        }

        public List<Orang> GetDaftarOrang()
        {
            return daftarOrang;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RumahSakit rsHarapan = new RumahSakit();

            Console.WriteLine("INISIALISASI DATA");

            Orang umum = new Orang("Budi", 45);
            Dokter dokterAndi = new Dokter("Andi", 35, "Spesialis Penyakit Dalam");
            Perawat perawatSiti = new Perawat("Siti", 28);
            PasienAnak anakToni = new PasienAnak("Toni", 7, "Demam Tinggi");
            PasienDewasa dewasaRudi = new PasienDewasa("Rudi", 50, "Sakit Perut Kronis");

            rsHarapan.TambahOrang(umum);
            rsHarapan.TambahOrang(dokterAndi);
            rsHarapan.TambahOrang(perawatSiti);
            rsHarapan.TambahOrang(anakToni);
            rsHarapan.TambahOrang(dewasaRudi);

            rsHarapan.DaftarOrang();

            Console.WriteLine("DEMONSTRASI POLYMORPHISM (Method Aktivitas)");
            // Demonstrasikan polymorphism
            foreach (var orang in rsHarapan.GetDaftarOrang())
            {
                // override
                Console.Write($"{orang.Nama}: ");
                orang.Aktivitas();
            }

            Console.WriteLine("\nMEMANGGIL METHOD KHUSUS");

            dokterAndi.CekSpesialis();

            dewasaRudi.CekKeluhan();

            dokterAndi.Diagnosa(dewasaRudi);

            perawatSiti.CekPasien(anakToni);

            anakToni.Menangis();

            dewasaRudi.Konsultasi();

            Console.ReadLine(); 
        }
    }
}