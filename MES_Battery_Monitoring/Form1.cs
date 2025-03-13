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
            LoadBatteryData(); // 데이터 불러오기

            // 🔹 INSPECTIONDATE 컬럼이 존재하는지 확인 후 날짜 포맷 변경
            if (dataGridView1.Columns.Contains("INSPECTIONDATE"))
            {
                dataGridView1.Columns["INSPECTIONDATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }

            Dock = DockStyle.Fill; // DataGridView 크기 자동 확장
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query;

                    // 입력값이 숫자인지 판별
                    bool isNumeric = int.TryParse(searchValue, out int numericValue);

                    if (isNumeric)
                    {
                        query = "SELECT * FROM BatteryInfo WHERE BatteryID = :search";
                    }
                    else
                    {
                        query = "SELECT * FROM BatteryInfo WHERE Status LIKE :search";
                        searchValue = $"%{searchValue}%";  // LIKE 검색을 위한 % 추가
                    }

                    using (OracleDataAdapter adapter = new OracleDataAdapter(query, conn))
                    {
                        // 숫자일 때는 숫자로 바인딩, 문자일 때는 문자열로 바인딩
                        if (isNumeric)
                        {
                            adapter.SelectCommand.Parameters.Add(":search", OracleDbType.Int32).Value = numericValue;
                        }
                        else
                        {
                            adapter.SelectCommand.Parameters.Add(":search", OracleDbType.Varchar2).Value = searchValue;
                        }

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("오류 발생: " + ex.Message);
                }
            }
        }


        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            LoadBatteryData();

        }

        private void btnFilterDefective_Click(object sender, EventArgs e)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM BatteryInfo WHERE Status = 'Defective'";

                    using (OracleDataAdapter adapter = new OracleDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("오류 발생: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}