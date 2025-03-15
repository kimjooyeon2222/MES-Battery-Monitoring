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
        // 환경변수에서 DB 비밀번호 가져오기
        private string dbPassword = Environment.GetEnvironmentVariable("ORACLE_DB_PASSWORD");

        // Oracle DB 연결 문자열 (비밀번호를 하드코딩하지 않음)
        private string connectionString;
       /* // Oracle DB 연결 문자열 (비밀번호는 본인 환경에 맞게 변경!)
        string connectionString = "User Id=system;Password=0000;Data Source=localhost:1521/XE";*/

        public Form1()
        {
            InitializeComponent();
            // 환경변수 확인 후 DB 연결 문자열 구성
            if (string.IsNullOrEmpty(dbPassword))
            {
                MessageBox.Show("환경변수 'ORACLE_DB_PASSWORD'가 설정되지 않았습니다. 환경변수를 확인하세요.");
                Environment.Exit(0); // 프로그램 종료
            }

            connectionString = $"User Id=system;Password={dbPassword};Data Source=localhost:1521/XE";
        
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. DataGridView에서 선택한 행이 있는지 확인
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 데이터를 선택하세요.");
                return;
            }

            // 2. 선택된 행 정보 가져오기
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            int batteryID = Convert.ToInt32(row.Cells["BATTERYID"].Value);

            // 3. 삭제 확인 메시지
            DialogResult confirm = MessageBox.Show("정말 삭제하시겠습니까?", "삭제 확인",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                // 4. Oracle DB 연결 후 DELETE 실행
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM BatteryInfo WHERE BatteryID = :batteryID";

                        using (OracleCommand cmd = new OracleCommand(query, conn))
                        {
                            cmd.Parameters.Add(":batteryID", OracleDbType.Int32).Value = batteryID;

                            int result = cmd.ExecuteNonQuery(); // 실행 후 삭제된 행 수 반환
                            if (result > 0)
                            {
                                MessageBox.Show("데이터가 성공적으로 삭제되었습니다.");
                                LoadBatteryData(); // 새로고침
                            }
                            else
                            {
                                MessageBox.Show("데이터 삭제에 실패했습니다.");
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // InsertForm 폼 열기
            InsertForm insertForm = new InsertForm();
            if (insertForm.ShowDialog() == DialogResult.OK)
            {
                // INSERT 실행 전에 값이 정상적으로 전달되는지 확인
                MessageBox.Show($"입력한 값 확인\nVoltage: {insertForm.NewVoltage}\nCurrent: {insertForm.NewCurrent}\nTemperature: {insertForm.NewTemperature}\nResistance: {insertForm.NewResistance}\nStatus: {insertForm.NewStatus}");

                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = @"INSERT INTO BatteryInfo (Voltage, CurrentValue, Temperature, Resistance, Status)
                                 VALUES (:newVoltage, :newCurrent, :newTemperature, :newResistance, :newStatus)";

                        using (OracleCommand cmd = new OracleCommand(query, conn))
                        {
                            // ✅ 바인드 변수명을 SQL과 일치하도록 수정
                            cmd.Parameters.Add(":voltage", OracleDbType.Double).Value = insertForm.NewVoltage;
                            cmd.Parameters.Add(":current", OracleDbType.Double).Value = insertForm.NewCurrent;
                            cmd.Parameters.Add(":temperature", OracleDbType.Double).Value = insertForm.NewTemperature;
                            cmd.Parameters.Add(":resistance", OracleDbType.Double).Value = insertForm.NewResistance;
                            cmd.Parameters.Add(":status", OracleDbType.Varchar2).Value = insertForm.NewStatus;

                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("데이터가 성공적으로 추가되었습니다.");
                                LoadBatteryData(); // 새로고침
                            }
                            else
                            {
                                MessageBox.Show("데이터 추가에 실패했습니다.");
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