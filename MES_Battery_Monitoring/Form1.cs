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

            //INSPECTIONDATE 컬럼이 존재하는지 확인 후 날짜 포맷 변경
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("수정할 데이터를 선택하세요.");
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];

            int batteryID = Convert.ToInt32(row.Cells["BATTERYID"].Value);
            double voltage = Convert.ToDouble(row.Cells["VOLTAGE"].Value);
            double current = Convert.ToDouble(row.Cells["CURRENTVALUE"].Value);
            double temperature = Convert.ToDouble(row.Cells["TEMPERATURE"].Value);
            double resistance = Convert.ToDouble(row.Cells["RESISTANCE"].Value);
            string status = row.Cells["STATUS"].Value.ToString();

            // 🔹 새로운 수정 폼 띄우기
            UpdateForm updateForm = new UpdateForm(status, voltage, current, temperature, resistance);
            if (updateForm.ShowDialog() == DialogResult.OK)
            {
                // ✅ 입력값이 제대로 전달되었는지 확인
                MessageBox.Show($"수정할 데이터 확인\nStatus: {updateForm.NewStatus}\nVoltage: {updateForm.NewVoltage}\nCurrent: {updateForm.NewCurrent}\nTemperature: {updateForm.NewTemperature}\nResistance: {updateForm.NewResistance}");

                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = "UPDATE BatteryInfo SET Status = :newStatus, Voltage = :newVoltage, CurrentValue = :newCurrent, " +
                                       "Temperature = :newTemperature, Resistance = :newResistance WHERE BatteryID = :batteryID";

                        using (OracleCommand cmd = new OracleCommand(query, conn))
                        {
                            cmd.Parameters.Add(":newStatus", OracleDbType.Varchar2).Value = updateForm.NewStatus;
                            cmd.Parameters.Add(":newVoltage", OracleDbType.Double).Value = updateForm.NewVoltage;
                            cmd.Parameters.Add(":newCurrent", OracleDbType.Double).Value = updateForm.NewCurrent;
                            cmd.Parameters.Add(":newTemperature", OracleDbType.Double).Value = updateForm.NewTemperature;
                            cmd.Parameters.Add(":newResistance", OracleDbType.Double).Value = updateForm.NewResistance;
                            cmd.Parameters.Add(":batteryID", OracleDbType.Int32).Value = batteryID;

                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("데이터가 성공적으로 업데이트되었습니다.");
                                LoadBatteryData(); // 데이터 다시 불러오기
                            }
                            else
                            {
                                MessageBox.Show("데이터 업데이트에 실패했습니다.");
                            }
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
}