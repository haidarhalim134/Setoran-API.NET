using System.ComponentModel.DataAnnotations;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Setoran_API.NET.Models
{
    public class Voucher
    {
        [Key]
        public int IdVoucher { get; set; }
        public string NamaVoucher { get; set; }
        public StatusVoucher StatusVoucher { get; set; }
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalAkhir { get; set; }
        public int PersenVoucher { get; set; }
        public string KodeVoucher { get; set; }

        /// <summary>
        /// list pelanggan yang pernah menggunakan voucher ini
        /// </summary>
        public List<Pelanggan> Pelanggans { get; set; }

        public static IQueryable<Voucher> GetActive(Database db, Pelanggan pelanggan)
        {
            return db.Voucher
                .Where(v => v.StatusVoucher == StatusVoucher.Aktif)
                .Where(v => v.TanggalMulai <= DateTime.Now.ToUniversalTime() && DateTime.Now.ToUniversalTime() <= v.TanggalAkhir)
                .Where(v => !db.VoucherUsed.Any(vu => vu.Voucher.IdVoucher == v.IdVoucher && vu.Pelanggan.IdPelanggan == pelanggan.IdPelanggan));
        }
        
        public static void UseVoucher(Database db, Voucher voucher, Pelanggan pelanggan)
        {
            var voucherUsed = new VoucherUsed {
                Voucher=voucher,
                Pelanggan=pelanggan
            };

            db.Add(voucherUsed);
            db.SaveChanges();
        }

        public static void Seed(DbContext dbContext)
        {
            for (int i = 0; i < 10; i++)
            {
                var faker = new Faker("id_ID");

                var tanggalMulai = faker.Date.PastOffset(1).UtcDateTime.Date;
                var daysToAdd = faker.Random.Int(1, 10);
                var tanggalAkhir = tanggalMulai.AddDays(daysToAdd);

                var voucher = new Voucher
                {
                    NamaVoucher = string.Join(" ", faker.Lorem.Words(2)),
                    StatusVoucher = faker.PickRandom(new[] { StatusVoucher.Aktif, StatusVoucher.NonAktif }),
                    TanggalMulai = tanggalMulai,
                    TanggalAkhir = tanggalAkhir,
                    PersenVoucher = faker.Random.Int(5, 50),
                    KodeVoucher = faker.Random.Replace("PROMO##??").ToUpper()
                };

                dbContext.Set<Voucher>().Add(voucher);
                dbContext.SaveChanges();

                var users = dbContext.Set<Pelanggan>().ToList();
                foreach (var user in users)
                {
                    if (faker.Random.Bool(0.5f))
                    {
                        var createdAt = faker.Date.Between(voucher.TanggalMulai, voucher.TanggalAkhir);

                        var voucherUsed = new VoucherUsed
                        {
                            Voucher = voucher,
                            Pelanggan = user,
                        };

                        dbContext.Set<VoucherUsed>().Add(voucherUsed);
                    }
                }

                dbContext.SaveChanges();
            }
        }
    }
    public enum StatusVoucher
    {
        Aktif,
        NonAktif
    }
}