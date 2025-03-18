using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES_Battery_Monitoring
{
    public partial class InsertForm : Form
    {
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label1;
        private Button btnCancel;
        private Button btnOK;
        private TextBox txtVoltage;
        private TextBox txtCurrent;
        private TextBox txtTemperature;
        private TextBox txtResistance;
        private TextBox txtStatus;

        // 사용자가 입력한 값들을 외부에서 읽을 수 있도록 프로퍼티로 선언
        public double NewVoltage { get; private set; }
        public double NewCurrent { get; private set; }
        public double NewTemperature { get; private set; }
        public double NewResistance { get; private set; }
        public string NewStatus { get; private set; }

        public InsertForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtVoltage = new System.Windows.Forms.TextBox();
            this.txtCurrent = new System.Windows.Forms.TextBox();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.txtResistance = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(127, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 24);
            this.label6.TabIndex = 24;
            this.label6.Text = "Current";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(99, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 24);
            this.label5.TabIndex = 23;
            this.label5.Text = "Temperature";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 24);
            this.label4.TabIndex = 22;
            this.label4.Text = "Resistance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 24);
            this.label3.TabIndex = 21;
            this.label3.Text = "Voltage";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 24);
            this.label1.TabIndex = 20;
            this.label1.Text = "Status";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(471, 356);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 43);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(292, 356);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(102, 43);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            // 
            // txtVoltage
            // 
            this.txtVoltage.Location = new System.Drawing.Point(268, 112);
            this.txtVoltage.Name = "txtVoltage";
            this.txtVoltage.Size = new System.Drawing.Size(329, 35);
            this.txtVoltage.TabIndex = 17;
            // 
            // txtCurrent
            // 
            this.txtCurrent.Location = new System.Drawing.Point(268, 171);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Size = new System.Drawing.Size(329, 35);
            this.txtCurrent.TabIndex = 16;
            // 
            // txtTemperature
            // 
            this.txtTemperature.Location = new System.Drawing.Point(268, 234);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(329, 35);
            this.txtTemperature.TabIndex = 15;
            // 
            // txtResistance
            // 
            this.txtResistance.Location = new System.Drawing.Point(268, 293);
            this.txtResistance.Name = "txtResistance";
            this.txtResistance.Size = new System.Drawing.Size(329, 35);
            this.txtResistance.TabIndex = 14;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(268, 55);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(329, 35);
            this.txtStatus.TabIndex = 13;
            // 
            // InsertForm
            // 
            this.ClientSize = new System.Drawing.Size(724, 447);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtVoltage);
            this.Controls.Add(this.txtCurrent);
            this.Controls.Add(this.txtTemperature);
            this.Controls.Add(this.txtResistance);
            this.Controls.Add(this.txtStatus);
            this.Name = "InsertForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            {
                try
                {
                    // 🔹 입력값을 읽어오기 전에 빈 값인지 확인
                    if (string.IsNullOrWhiteSpace(txtVoltage.Text) ||
                        string.IsNullOrWhiteSpace(txtCurrent.Text) ||
                        string.IsNullOrWhiteSpace(txtTemperature.Text) ||
                        string.IsNullOrWhiteSpace(txtResistance.Text) ||
                        string.IsNullOrWhiteSpace(txtStatus.Text))
                    {
                        MessageBox.Show("모든 값을 입력하세요!", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 🔹 입력값을 숫자로 변환
                    if (!double.TryParse(txtVoltage.Text.Trim(), out double voltage) ||
                        !double.TryParse(txtCurrent.Text.Trim(), out double current) ||
                        !double.TryParse(txtTemperature.Text.Trim(), out double temperature) ||
                        !double.TryParse(txtResistance.Text.Trim(), out double resistance))
                    {
                        MessageBox.Show("숫자 값을 올바르게 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 🔹 정상적인 값 저장
                    NewVoltage = voltage;
                    NewCurrent = current;
                    NewTemperature = temperature;
                    NewResistance = resistance;
                    NewStatus = txtStatus.Text.Trim();

                    // 🔹 입력값 확인
                    MessageBox.Show($"입력한 값 확인\nVoltage: {NewVoltage}\nCurrent: {NewCurrent}\nTemperature: {NewTemperature}\nResistance: {NewResistance}\nStatus: {NewStatus}");

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("입력값 변환 중 오류 발생: " + ex.Message);
                }
            }
        }
    }
}