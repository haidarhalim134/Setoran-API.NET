using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Setoran_API.NET.Models
{
    public class Transaksi
    {
        [Key]
        public int IdTransaksi { get; set; }
        public int IdMotor { get; set; }
        public int IdPelanggan { get; set; }
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalSelesai { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalHarga { get; set; }
        public StatusTransaksi Status { get; set; }

        [ForeignKey("IdMotor")]
        public Motor Motor { get; set; }

        [ForeignKey("IdPelanggan")]
        public Pelanggan Pelanggan { get; set; }

        public Pembayaran Pembayaran { get; set; }

        public static async Task<Transaksi> InitiateTransaction(DbContext _context, PostTransaksiDTO transaksi)
        {
            var motor = await _context.Set<Motor>().FindAsync(transaksi.IdMotor);
            if (motor == null)
            {
                throw new Exception("Motor tidak ditemukan");
            }

            if (motor.StatusMotor != StatusMotor.Tersedia)
            {
                throw new Exception("Motor tidak tersedia");
            }
            
            var totalHarga = motor.HargaHarian * (transaksi.TanggalSelesai - transaksi.TanggalMulai).Days;

            if (transaksi.idDiscount != null)
            {
                var diskon = await _context.Set<Diskon>().FindAsync(transaksi.idDiscount);
                if (diskon == null)
                {
                    throw new Exception("Diskon tidak ditemukan");
                }
                totalHarga = totalHarga - diskon.JumlahDiskon;
            }

            if (transaksi.idVoucher != null)
            {
                var voucher = await _context.Set<Voucher>().FindAsync(transaksi.idVoucher);
                if (voucher == null)
                {
                    throw new Exception("Voucher tidak ditemukan");
                }
                totalHarga = totalHarga - voucher.PersenVoucher * totalHarga / 100;
                var pelanggan = await _context.Set<Pelanggan>().FindAsync(transaksi.IdPelanggan);
                if (pelanggan == null)
                {
                    throw new Exception("Pelanggan tidak ditemukan");
                }
                Voucher.UseVoucher(_context, voucher, pelanggan);
            }


            var newTransaksi = new Transaksi
            {
                IdMotor = transaksi.IdMotor,
                IdPelanggan = transaksi.IdPelanggan,
                TanggalMulai = transaksi.TanggalMulai,
                TanggalSelesai = transaksi.TanggalSelesai,
                TotalHarga = totalHarga,
                Status = StatusTransaksi.Dibuat // Set default status
            };

            await _context.AddAsync(newTransaksi);

            await _context.SaveChangesAsync();

            var pembayaran = new Pembayaran
            {
                IdTransaksi = newTransaksi.IdTransaksi,
                MetodePembayaran = transaksi.MetodePembayaran,
                StatusPembayaran = StatusPembayaran.BelumLunas,
                TanggalPembayaran = null
            };

            await _context.AddAsync(pembayaran);
            await _context.SaveChangesAsync();

            motor.StatusMotor = StatusMotor.Disewa;
            _context.Set<Motor>().Update(motor);

            // gak perlu di safe karena relasi nya udah ada
            newTransaksi.Pembayaran = pembayaran;

            return newTransaksi;
        }

        public async Task CompleteTransaction(DbContext _context)
        {
            Status = StatusTransaksi.Selesai;

            var motor = await _context.Set<Motor>().FindAsync(IdMotor);
            motor!.StatusMotor = StatusMotor.Tersedia;

            _context.Update(this);
            _context.Update(Motor);

            await _context.SaveChangesAsync();
        }

        public static void Seed(DbContext dbContext)
        {
            var faker = new Faker("id_ID");
            var motors = dbContext.Set<Motor>().ToList();
            var pelanggans = dbContext.Set<Pelanggan>().ToList();
            var statusOptions = new[] { "dibuat", "berlangsung", "batal", "selesai" };

            // buat populasi graph di dashboard website
            var dateRanges = new List<(string label, DateTime start, DateTime end)>
            {
                ("3_months", DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow.AddMonths(-1)),
                ("30_days", DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(-7)),
                ("7_days", DateTime.UtcNow.AddDays(-7), DateTime.UtcNow),
            };



            foreach (var (label, startDate, endDate) in dateRanges)
            {
                for (int i = 0; i < 50; i++)
                {
                    var tanggalSelesai = faker.Date.Between(startDate, endDate);
                    var durasi = faker.Random.Int(1, 5);
                    var tanggalMulai = tanggalSelesai.AddDays(durasi * -1);

                    var transaksiDto = new PostTransaksiDTO
                    {
                        IdMotor = faker.PickRandom(motors).IdMotor,
                        IdPelanggan = faker.PickRandom(pelanggans).IdPelanggan,
                        TanggalMulai = tanggalMulai,
                        TanggalSelesai = tanggalSelesai,
                        MetodePembayaran = faker.PickRandom<MetodePembayaran>()
                    };

                    var transaksi = InitiateTransaction(dbContext, transaksiDto).Result;
                    transaksi.CompleteTransaction(dbContext).Wait();

                    transaksi.Pembayaran.StatusPembayaran = StatusPembayaran.Lunas;
                    transaksi.Pembayaran.TanggalPembayaran = DateTime.Now.ToUniversalTime();
                    dbContext.Update(transaksi.Pembayaran);
                }   
            }

            dbContext.SaveChanges();
        }
    }

    public enum StatusTransaksi
    {
        Dibuat,
        Berlangsung,
        Batal,
        Selesai
    }
}