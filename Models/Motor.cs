using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Setoran_API.NET.Models
{
    public class Motor
    {
        [Key]
        public int IdMotor { get; set; }
        public string PlatNomor { get; set; }
        public int IdMitra { get; set; }
        public string NomorSTNK { get; set; }
        public string NomorBPKB { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Tipe { get; set; }
        public int Tahun { get; set; }
        public TransmisiMotor Transmisi { get; set; }
        public StatusMotor StatusMotor { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal HargaHarian { get; set; }
        public List<Diskon> Diskon { get; set; }
        public List<Ulasan> Ulasan { get; set; }
        public int? IdMotorImage { get; set; }

        [ForeignKey("IdMitra")]
        public Mitra Mitra { get; set; }

        [ForeignKey("IdMotorImage")]
        public MotorImage? MotorImage { get; set; }

        public decimal GetBestDiscount()
        {
            Diskon? diskonTerbaik = null;

            if (Diskon != null)
            {
                foreach (var discount in Diskon)
                {
                    if (discount.TanggalMulai != null && discount.TanggalAkhir != null &&
                        discount.TanggalMulai <= DateTime.Now &&
                        discount.TanggalAkhir >= DateTime.Now)
                    {
                        if (diskonTerbaik != null)
                        {
                            if (discount.JumlahDiskon > diskonTerbaik.JumlahDiskon)
                            {
                                diskonTerbaik = discount;
                            }
                        }
                        else
                        {
                            diskonTerbaik = discount;
                        }
                    }
                }
            }

            return diskonTerbaik != null ? diskonTerbaik.JumlahDiskon : 0;
        }

        public static void Seed(DbContext dbContext)
        {
            var faker = new Faker("id_ID");
            var mitra = dbContext.Set<Mitra>().FirstOrDefault();
            if (mitra == null) return;

            var transmisiOptions = new[] { "Matic", "Manual" };
            var regionCodes = new[] { "B", "D", "F", "E", "Z", "T" };
            var random = new Random();

            string RandomPlatNomor()
            {
                var region = regionCodes[random.Next(regionCodes.Length)].Substring(0, 1); // Ensure only 1 char
                var numbers = random.Next(0, 10000).ToString("D4"); // Always 4 digits, padded with zeroes
                var letters = new string(Enumerable.Range(0, 4)
                    .Select(_ => (char)random.Next('A', 'Z' + 1)).ToArray()); // 4 letters
                return $"{region}{numbers}{letters}";
            }

            var motorList = new[]
            {
                ("Honda", "Beat", "scooter", (60000m, 75000m), new MotorImage { Front = "07c7461b-bdcc-4128-a400-66edfb0b276b.png", Rear = "e8b6aa4c-ace6-4f92-8037-c6694e4b9e46.png", Left = "bf1d1a38-5ce1-4014-a5b3-cb3d4db4b248.png", Right = "ed407e62-2daf-4419-946f-1faca5e52569.webp" }),
                ("Yamaha", "NMAX", "scooter", (100000m, 130000m), new MotorImage { Front = "8853c147-c86d-4861-b16f-eac91ea22fa8.png", Rear = "19ebe0c0-509d-4c14-b6b3-9114e1a42998.png", Left = "1ec45ed0-bb93-462d-ac2a-5ea3b5ac33ff.png", Right = "48579eb2-b70a-4d30-b4c6-44b97a9fc98b.png" }),
                ("Honda", "PCX 160", "scooter", (110000m, 140000m), new MotorImage { Front = "7ad93606-eb66-4d1a-8f4f-6d2363107056.png", Rear = "1ac0a25f-547e-48c7-8593-1b735a1ff64b.png", Left = "6020b294-84b3-4d16-9ade-12f1c378f883.png", Right = "03a8b78f-a175-4b0c-be7f-e674752d3b48.png" }),
                ("Honda", "Vario 125", "scooter", (70000m, 90000m), new MotorImage { Front = "60a00f6a-c2f7-44bc-878d-c6d23fcb5f66.png", Rear = "fec45a1e-94bc-4cf0-9d53-5318e1079128.png", Left = "528ee71f-c9f0-4dc2-aba3-edf50a115896.png", Right = "3184910d-299f-49ef-a7ba-bc474b39d0a0.png" }),
                ("Honda", "Vario 160", "scooter", (90000m, 115000m), new MotorImage { Front = "f87baa44-4ac4-4d2b-be92-56f87ff09f0c.png", Rear = "ae5fe5d1-b610-48ce-a4eb-915cdb98628d.png", Left = "942e9d2a-2aad-4462-877d-f9f191753655.png", Right = "80aad6a5-a991-4e45-85a4-f7104ab5a9ff.png" }),
                ("Yamaha", "NMAX 155", "scooter", (105000m, 135000m), new MotorImage { Front = "748e6f16-3515-4b3f-bbb3-83b664f4205d.png", Rear = "d54d040a-d571-4747-8f6a-f13ede66a4e0.png", Left = "71c1655f-efc8-4b64-bdaa-352a9ef2b808.png", Right = "593a13c5-a268-431f-a121-7dd367e6af62.png" }),
                ("Yamaha", "Aerox Alpha", "scooter", (95000m, 120000m), new MotorImage { Front = "329cdc4f-8377-4621-8747-89587b5b50e9.png", Rear = "6a53a9a6-4def-4c8c-80e4-a76e8838d6f1.png", Left = "2f303d88-9643-4e95-ac13-de3580565c0d.png", Right = "3dec0e24-991b-4dfe-901f-ecc1b5327223.png" }),
            };

            foreach (var (brand, model, type, priceRange, motorImageTemplate) in motorList)
            {
                for (int i = 0; i < random.Next(3, 5); i++)
                {
                    var rawHarga = (decimal)(random.NextDouble() * (double)(priceRange.Item2 - priceRange.Item1)) + priceRange.Item1;
                    var harga = Math.Round(rawHarga / 1000, 0) * 1000;
                    var tahun = random.Next(2020, 2025);

                    var motor = new Motor
                    {
                        PlatNomor = RandomPlatNomor(),
                        IdMitra = mitra.IdMitra,
                        NomorSTNK = string.Concat(Enumerable.Range(0, 10).Select(_ => random.Next(10).ToString())),
                        NomorBPKB = string.Concat(Enumerable.Range(0, 10).Select(_ => random.Next(10).ToString())),
                        Model = model,
                        Brand = brand,
                        Tipe = type,
                        Tahun = tahun,
                        Transmisi = faker.PickRandom<TransmisiMotor>(),
                        StatusMotor = StatusMotor.Tersedia,
                        HargaHarian = harga
                    };

                    dbContext.Set<Motor>().Add(motor);
                    dbContext.SaveChanges();

                    var motorImage = new MotorImage
                    {
                        Front = motorImageTemplate.Front,
                        Rear = motorImageTemplate.Rear,
                        Left = motorImageTemplate.Left,
                        Right = motorImageTemplate.Right,
                        IdMotor = motor.IdMotor
                    };

                    motorImage.IdMotor = motor.IdMotor;
                    dbContext.Set<MotorImage>().Add(motorImage);
                    dbContext.SaveChanges();

                    motor.IdMotorImage = motorImage.Id;
                    dbContext.Update(motor);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    public enum StatusMotor
    {
        Diajukan,
        Tersedia,
        Disewa,
        DalamPerbaikan,
        TidakTersedia
    }

    public enum TransmisiMotor
    {
        Matic,
        Manual
    }
}