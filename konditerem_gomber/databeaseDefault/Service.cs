using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databeaseDefault
{
    internal class Service
    {
        private string ConnectionString = "host=localhost;database=konditerem;user=root;password=;";

        public List<Customer> GetCustomer()
        {
            List<Customer> result = new List<Customer>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM `tagok`";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Customer
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("nev"),
                                DOBYear = reader.GetInt32("szuletesi_ev"),
                                LeaseType = reader.GetString("berlet_tipus")
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<Entry> GetEntries()
        {
            List<Entry> result = new List<Entry>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT belepesek.id, tagok.nev as 'tagnev', belepesek.idopont, belepesek.szekreny_szam FROM `belepesek` INNER JOIN tagok ON belepesek.tag_id = tagok.id";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Entry
                            {
                                Id = reader.GetInt32("id"),
                                CustomerName = reader.GetString("tagnev"),
                                EntryDate = reader.GetDateTime("idopont"),
                                CabinetNumber = reader.GetInt32("szekreny_szam")
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<Statistics> GetStatistics()
        {
            List<Statistics> result = new List<Statistics>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT tagok.nev as 'nev', SUM(belepesek.tag_id) AS 'alkalmak_szama' FROM `belepesek` INNER JOIN tagok on belepesek.tag_id = tagok.id GROUP BY nev";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Statistics
                            {
                                Name = reader.GetString("nev"),
                                Entries = reader.GetInt32("alkalmak_szama")
                            });
                        }
                    }
                }
            }
            return result;
        }

        public void NewCustomer(Customer New)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = "INSERT INTO `tagok`(nev, szuletesi_ev, berlet_tipus) VALUES (@name, @DOBYear, @LeaseType)";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", New.Name);
                    cmd.Parameters.AddWithValue("@DOBYear", New.DOBYear);
                    cmd.Parameters.AddWithValue("@LeaseType", New.LeaseType);
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}