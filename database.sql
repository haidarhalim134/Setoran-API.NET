BEGIN TRANSACTION;

-- hasil export dari database laravel bisa dipake sebagai referensi untuk buat model, update as needed

CREATE TABLE IF NOT EXISTS "users" (
	"id"	integer NOT NULL,
	"name"	varchar NOT NULL,
	"email"	varchar NOT NULL,
	"email_verified_at"	datetime,
	"password"	varchar NOT NULL,
	"remember_token"	varchar,
	PRIMARY KEY("id" AUTOINCREMENT)
);

CREATE TABLE IF NOT EXISTS "pelanggans" (
	"id_pelanggan"	integer NOT NULL,
	"id_pengguna"	integer NOT NULL,
	"nomor_SIM"	varchar,
	PRIMARY KEY("id_pelanggan" AUTOINCREMENT),
	FOREIGN KEY("id_pengguna") REFERENCES "penggunas"("id_pengguna")
);
CREATE TABLE IF NOT EXISTS "mitras" (
	"id_mitra"	integer NOT NULL,
	"id_pengguna"	integer NOT NULL,
	"status"	varchar NOT NULL DEFAULT 'inactive' CHECK("status" IN ('active', 'inactive')),
	PRIMARY KEY("id_mitra" AUTOINCREMENT),
	FOREIGN KEY("id_pengguna") REFERENCES "penggunas"("id_pengguna")
);
CREATE TABLE IF NOT EXISTS "motors" (
	"id_motor"	integer NOT NULL,
	"plat_nomor"	varchar NOT NULL,
	"id_mitra"	integer NOT NULL,
	"nomor_STNK"	varchar NOT NULL,
	"nomor_BPKB"	varchar NOT NULL,
	"model"	varchar NOT NULL,
	"brand"	varchar NOT NULL,
	"tipe"	varchar NOT NULL,
	"tahun"	integer NOT NULL,
	"transmisi"	varchar NOT NULL,
	"status_motor"	varchar NOT NULL,
	"harga_harian"	numeric NOT NULL,
	"diskon_percentage"	integer,
	"diskon_amount"	integer,
	FOREIGN KEY("id_mitra") REFERENCES "mitras"("id_mitra"),
	PRIMARY KEY("id_motor" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "ulasans" (
	"id_ulasan"	integer NOT NULL,
	"id_motor"	integer NOT NULL,
	"id_pelanggan"	integer NOT NULL,
	"rating"	integer NOT NULL,
	"komentar"	text,
	"tanggal_ulasan"	datetime NOT NULL,
	PRIMARY KEY("id_ulasan" AUTOINCREMENT),
	FOREIGN KEY("id_motor") REFERENCES "motors"("id_motor"),
	FOREIGN KEY("id_pelanggan") REFERENCES "pelanggans"("id_pelanggan")
);
CREATE TABLE IF NOT EXISTS "pembayarans" (
	"id_pembayaran"	integer NOT NULL,
	"id_transaksi"	integer NOT NULL,
	"metode"	varchar NOT NULL,
	"nominal"	numeric NOT NULL,
	"status_pembayaran"	varchar NOT NULL,
	"tanggal_bayar"	datetime,
	PRIMARY KEY("id_pembayaran" AUTOINCREMENT),
	FOREIGN KEY("id_transaksi") REFERENCES "transaksis"("id_transaksi")
);
CREATE TABLE IF NOT EXISTS "transaksis" (
	"id_transaksi"	integer NOT NULL,
	"id_motor"	integer NOT NULL,
	"id_pelanggan"	integer NOT NULL,
	"tanggal_mulai"	date NOT NULL,
	"tanggal_selesai"	date NOT NULL,
	"status_transaksi"	varchar NOT NULL,
	"durasi"	integer NOT NULL,
	"nominal"	numeric NOT NULL,
	"id_pembayaran"	integer,
	FOREIGN KEY("id_pelanggan") REFERENCES "pelanggans"("id_pelanggan") on delete no action on update no action,
	PRIMARY KEY("id_transaksi" AUTOINCREMENT),
	FOREIGN KEY("id_motor") REFERENCES "motors"("id_motor") on delete no action on update no action,
	FOREIGN KEY("id_pembayaran") REFERENCES "pembayarans"("id_pembayaran")
);
CREATE TABLE IF NOT EXISTS "vouchers" (
	"id_voucher"	integer NOT NULL,
	"nama_voucher"	varchar NOT NULL,
	"status_voucher"	varchar NOT NULL CHECK("status_voucher" IN ('aktif', 'nonAktif')),
	"tanggal_mulai"	date NOT NULL,
	"tanggal_akhir"	date NOT NULL,
	"persen_voucher"	integer,
	"kode_voucher"	varchar,
	PRIMARY KEY("id_voucher" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "admins" (
	"id_admin"	integer NOT NULL,
	"nama"	varchar NOT NULL,
	"email"	varchar NOT NULL,
	"password"	varchar NOT NULL,
	PRIMARY KEY("id_admin" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "image_data" (
	"id_gambar"	integer NOT NULL,
	"data"	blob NOT NULL,
	PRIMARY KEY("id_gambar" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "diskons" (
	"id"	integer NOT NULL,
	"nama"	varchar NOT NULL,
	"status_promo"	varchar NOT NULL,
	"tanggal_mulai"	date NOT NULL,
	"tanggal_akhir"	date NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "notifikasis" (
	"id_notifikasi"	integer NOT NULL,
	"id_pengguna"	integer NOT NULL,
	"judul"	varchar NOT NULL,
	"deskripsi"	text NOT NULL,
	"navigasi"	varchar DEFAULT '' CHECK("navigasi" IN ('', 'transaksi', 'editProfile')),
	"data_navigasi"	text,
	"is_sent"	tinyint(1) NOT NULL DEFAULT '0',
	"is_read"	tinyint(1) NOT NULL DEFAULT '0',
	PRIMARY KEY("id_notifikasi" AUTOINCREMENT),
	FOREIGN KEY("id_pengguna") REFERENCES "penggunas"("id_pengguna")
);
CREATE TABLE IF NOT EXISTS "device_tokens" (
	"id"	integer NOT NULL,
	"id_pengguna"	integer NOT NULL,
	"device_token"	varchar NOT NULL,
	FOREIGN KEY("id_pengguna") REFERENCES "penggunas"("id_pengguna") on delete cascade,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "voucher_useds" (
	"id"	integer NOT NULL,
	"id_voucher"	integer NOT NULL,
	"id_pengguna"	integer NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT),
	FOREIGN KEY("id_voucher") REFERENCES "vouchers"("id_voucher") on delete cascade,
	FOREIGN KEY("id_pengguna") REFERENCES "penggunas"("id_pengguna") on delete cascade
);
CREATE TABLE IF NOT EXISTS "motor_images" (
	"id"	integer NOT NULL,
	"id_gambar"	integer NOT NULL,
	"id_motor"	integer NOT NULL,
	"label"	varchar,
	PRIMARY KEY("id" AUTOINCREMENT),
	FOREIGN KEY("id_gambar") REFERENCES "image_data"("id_gambar"),
	FOREIGN KEY("id_motor") REFERENCES "motors"("id_motor")
);
CREATE TABLE IF NOT EXISTS "penggunas" (
	"id_pengguna"	integer NOT NULL,
	"nama"	varchar NOT NULL,
	"email"	varchar NOT NULL,
	"password"	varchar NOT NULL,
	"tanggal_lahir"	date,
	"nomor_telepon"	varchar,
	"umur"	integer,
	"nomor_KTP"	varchar,
	"alamat"	text,
	"id_gambar"	integer,
	PRIMARY KEY("id_pengguna" AUTOINCREMENT),
	FOREIGN KEY("id_gambar") REFERENCES "image_data"("id_gambar")
);

CREATE UNIQUE INDEX IF NOT EXISTS "users_email_unique" ON "users" (
	"email"
);
CREATE INDEX IF NOT EXISTS "sessions_user_id_index" ON "sessions" (
	"user_id"
);
CREATE INDEX IF NOT EXISTS "sessions_last_activity_index" ON "sessions" (
	"last_activity"
);
CREATE INDEX IF NOT EXISTS "jobs_queue_index" ON "jobs" (
	"queue"
);
CREATE UNIQUE INDEX IF NOT EXISTS "failed_jobs_uuid_unique" ON "failed_jobs" (
	"uuid"
);
CREATE INDEX IF NOT EXISTS "personal_access_tokens_tokenable_type_tokenable_id_index" ON "personal_access_tokens" (
	"tokenable_type",
	"tokenable_id"
);
CREATE UNIQUE INDEX IF NOT EXISTS "personal_access_tokens_token_unique" ON "personal_access_tokens" (
	"token"
);
CREATE UNIQUE INDEX IF NOT EXISTS "motors_plat_nomor_unique" ON "motors" (
	"plat_nomor"
);
CREATE UNIQUE INDEX IF NOT EXISTS "admins_email_unique" ON "admins" (
	"email"
);
CREATE UNIQUE INDEX IF NOT EXISTS "vouchers_kode_voucher_unique" ON "vouchers" (
	"kode_voucher"
);
CREATE UNIQUE INDEX IF NOT EXISTS "device_tokens_device_token_unique" ON "device_tokens" (
	"device_token"
);
CREATE UNIQUE INDEX IF NOT EXISTS "penggunas_email_unique" ON "penggunas" (
	"email"
);
CREATE VIEW pengguna_pelanggan_view AS
            SELECT
                pelanggans.id_pelanggan,
                pelanggans.nomor_SIM,
                penggunas.nama,
                penggunas.email,
                penggunas.password,
                penggunas.tanggal_lahir,
                penggunas.umur,
                penggunas.nomor_KTP,
                penggunas.nomor_telepon,
                penggunas.alamat
            FROM pelanggans
            JOIN penggunas ON pelanggans.id_pengguna = penggunas.id_pengguna;
CREATE VIEW transaksi_motor_pelanggan_view AS
                SELECT
                    transaksis.id_transaksi,
                    transaksis.id_motor,
                    transaksis.id_pelanggan,
                    transaksis.id_pembayaran,
                    transaksis.tanggal_mulai,
                    transaksis.tanggal_selesai,
                    transaksis.status_transaksi,
                    transaksis.durasi,
                    transaksis.nominal,
                    motors.plat_nomor,
                    motors.nomor_STNK,
                    motors.nomor_BPKB,
                    motors.model,
                    motors.brand,
                    motors.tipe,
                    motors.tahun,
                    motors.transmisi,
                    motors.status_motor,
                    motors.harga_harian,
                    pelanggans.nomor_SIM,
                    penggunas.nama,
                    penggunas.email,
                    penggunas.password,
                    penggunas.tanggal_lahir,
                    penggunas.umur,
                    penggunas.nomor_KTP,
                    penggunas.nomor_telepon,
                    penggunas.alamat
                FROM transaksis
                LEFT JOIN motors ON transaksis.id_motor = motors.id_motor
                LEFT JOIN pelanggans ON transaksis.id_pelanggan = pelanggans.id_pelanggan
                LEFT JOIN penggunas ON pelanggans.id_pengguna = penggunas.id_pengguna;
COMMIT;
