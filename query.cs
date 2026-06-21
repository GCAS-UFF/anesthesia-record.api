using System;
using Npgsql;

class Program {
    static void Main() {
        var connStr = "User Id=postgres.yidudaduvasngangrydi;Password=13676616766m&b;Server=aws-0-us-east-2.pooler.supabase.com;Port=5432;Database=postgres;Search Path=siga_db";
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        
        using var cmd = new NpgsqlCommand("SELECT id, patient_id FROM siga_db.anesthesia_records", conn);
        using var reader = cmd.ExecuteReader();
        while(reader.Read()) {
            Console.WriteLine("AnesthesiaRecord ID: " + reader[0] + " Patient: " + reader[1]);
        }
    }
}
