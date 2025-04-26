using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES_Battery_Monitoring
{
    public partial class UpdateForm : Form
    {
        private TextBox txtStatus;
        private TextBox txtResistance;
        private TextBox txtTemperature;
        private TextBox txtCurrent;
        private TextBox txtVoltage;
        private Button btnOK;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btnCancel;

        // 사용자가 입력한 새로운 값 저장
        public string NewStatus { get; private set; }
        public double NewVoltage { get; private set; }
        public double NewCurrent { get; private set; }
        public double NewTemperature { get; private set; }
        public double NewResistance { get; private set; }

        public UpdateForm(string status, double voltage, double current, double temperature, double resistance)
        {
            InitializeComponent();

            // 기존 값 설정
            txtStatus.Text = status;
            txtVoltage.Text = voltage.ToString();
            txtCurrent.Text = current.ToString();
            txtTemperature.Text = temperature.ToString();
            txtResistance.Text = resistance.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 새로운 값 가져오기
            NewStatus = txtStatus.Text;

            try
            {
                NewVoltage = double.Parse(txtVoltage.Text);
                NewCurrent = double.Parse(txtCurrent.Text);
                NewTemperature = double.Parse(txtTemperature.Text);
                NewResistance = double.Parse(txtResistance.Text);
            }
            catch
            {
                MessageBox.Show("숫자 값을 정확히 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtResistance = new System.Windows.Forms.TextBox();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.txtCurrent = new System.Windows.Forms.TextBox();
            this.txtVoltage = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtStatus.Location = new System.Drawing.Point(225, 37);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(329, 35);
            this.txtStatus.TabIndex = 0;
            // 
            // txtResistance
            // 
            this.txtResistance.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtResistance.Location = new System.Drawing.Point(225, 275);
            this.txtResistance.Name = "txtResistance";
            this.txtResistance.Size = new System.Drawing.Size(329, 35);
            this.txtResistance.TabIndex = 1;
            // 
            // txtTemperature
            // 
            this.txtTemperature.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtTemperature.Location = new System.Drawing.Point(225, 216);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(329, 35);
            this.txtTemperature.TabIndex = 2;
            // 
            // txtCurrent
            // 
            this.txtCurrent.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtCurrent.Location = new System.Drawing.Point(225, 153);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Size = new System.Drawing.Size(329, 35);
            this.txtCurrent.TabIndex = 3;
            // 
            // txtVoltage
            // 
            this.txtVoltage.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtVoltage.Location = new System.Drawing.Point(225, 94);
            this.txtVoltage.Name = "txtVoltage";
            this.txtVoltage.Size = new System.Drawing.Size(329, 35);
            this.txtVoltage.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(249, 338);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(102, 43);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(428, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 43);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "Voltage";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "Resistance";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "Temperature";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(84, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "Current";
            // 
            // UpdateForm
            // 
            this.ClientSize = new System.Drawing.Size(670, 428);
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
            this.Name = "UpdateForm";
            this.Load += new System.EventHandler(this.UpdateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
