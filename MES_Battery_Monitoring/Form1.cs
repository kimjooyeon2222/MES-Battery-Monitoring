using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES_Battery_Monitoring
{
    public partial class Form1 : Form
    {

        // Oracle DB 연결 문자열 (비밀번호는 본인 환경에 맞게 변경!)
        string connectionString = "User Id=system;Password=0000;Data Source=localhost:1521/XE";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadBatteryData();

        }
        //Oracle에서 배터리 데이터를 가져오는 메서드
        private void LoadBatteryData()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM BatteryInfo";

                    using (OracleDataAdapter adapter = new OracleDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt; // UI의 DataGridView에 데이터 표시
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("오류 발생: " + ex.Message);
                }
            }
        }
    }
}