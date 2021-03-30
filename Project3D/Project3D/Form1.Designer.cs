namespace Project3D
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBoxShadingType = new System.Windows.Forms.GroupBox();
            this.radioButtonPhong = new System.Windows.Forms.RadioButton();
            this.radioButtonGouraud = new System.Windows.Forms.RadioButton();
            this.radioButtonFlat = new System.Windows.Forms.RadioButton();
            this.groupBoxCamera = new System.Windows.Forms.GroupBox();
            this.radioButtonDynamic = new System.Windows.Forms.RadioButton();
            this.radioButtonFollowing = new System.Windows.Forms.RadioButton();
            this.radioButtonCameraStatic = new System.Windows.Forms.RadioButton();
            this.groupBoxFog = new System.Windows.Forms.GroupBox();
            this.radioButtonFogEnabled = new System.Windows.Forms.RadioButton();
            this.radioButtonFogDisabled = new System.Windows.Forms.RadioButton();
            this.groupBoxFloor = new System.Windows.Forms.GroupBox();
            this.buttonAddFloor = new System.Windows.Forms.Button();
            this.buttonRemoveFloor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxShadingType.SuspendLayout();
            this.groupBoxCamera.SuspendLayout();
            this.groupBoxFog.SuspendLayout();
            this.groupBoxFloor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1200, 800);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBoxShadingType
            // 
            this.groupBoxShadingType.Controls.Add(this.radioButtonPhong);
            this.groupBoxShadingType.Controls.Add(this.radioButtonGouraud);
            this.groupBoxShadingType.Controls.Add(this.radioButtonFlat);
            this.groupBoxShadingType.Location = new System.Drawing.Point(1225, 13);
            this.groupBoxShadingType.Name = "groupBoxShadingType";
            this.groupBoxShadingType.Size = new System.Drawing.Size(145, 117);
            this.groupBoxShadingType.TabIndex = 1;
            this.groupBoxShadingType.TabStop = false;
            this.groupBoxShadingType.Text = "Shading Type";
            // 
            // radioButtonPhong
            // 
            this.radioButtonPhong.AutoSize = true;
            this.radioButtonPhong.Location = new System.Drawing.Point(6, 85);
            this.radioButtonPhong.Name = "radioButtonPhong";
            this.radioButtonPhong.Size = new System.Drawing.Size(70, 21);
            this.radioButtonPhong.TabIndex = 2;
            this.radioButtonPhong.TabStop = true;
            this.radioButtonPhong.Text = "Phong";
            this.radioButtonPhong.UseVisualStyleBackColor = true;
            this.radioButtonPhong.CheckedChanged += new System.EventHandler(this.radioButtonPhong_CheckedChanged);
            // 
            // radioButtonGouraud
            // 
            this.radioButtonGouraud.AutoSize = true;
            this.radioButtonGouraud.Location = new System.Drawing.Point(6, 58);
            this.radioButtonGouraud.Name = "radioButtonGouraud";
            this.radioButtonGouraud.Size = new System.Drawing.Size(85, 21);
            this.radioButtonGouraud.TabIndex = 1;
            this.radioButtonGouraud.TabStop = true;
            this.radioButtonGouraud.Text = "Gouraud";
            this.radioButtonGouraud.UseVisualStyleBackColor = true;
            this.radioButtonGouraud.CheckedChanged += new System.EventHandler(this.radioButtonGouraud_CheckedChanged);
            // 
            // radioButtonFlat
            // 
            this.radioButtonFlat.AutoSize = true;
            this.radioButtonFlat.Location = new System.Drawing.Point(6, 31);
            this.radioButtonFlat.Name = "radioButtonFlat";
            this.radioButtonFlat.Size = new System.Drawing.Size(52, 21);
            this.radioButtonFlat.TabIndex = 0;
            this.radioButtonFlat.TabStop = true;
            this.radioButtonFlat.Text = "Flat";
            this.radioButtonFlat.UseVisualStyleBackColor = true;
            this.radioButtonFlat.CheckedChanged += new System.EventHandler(this.radioButtonFlat_CheckedChanged);
            // 
            // groupBoxCamera
            // 
            this.groupBoxCamera.Controls.Add(this.radioButtonDynamic);
            this.groupBoxCamera.Controls.Add(this.radioButtonFollowing);
            this.groupBoxCamera.Controls.Add(this.radioButtonCameraStatic);
            this.groupBoxCamera.Location = new System.Drawing.Point(1225, 136);
            this.groupBoxCamera.Name = "groupBoxCamera";
            this.groupBoxCamera.Size = new System.Drawing.Size(145, 113);
            this.groupBoxCamera.TabIndex = 2;
            this.groupBoxCamera.TabStop = false;
            this.groupBoxCamera.Text = "Camera";
            // 
            // radioButtonDynamic
            // 
            this.radioButtonDynamic.AutoSize = true;
            this.radioButtonDynamic.Location = new System.Drawing.Point(6, 84);
            this.radioButtonDynamic.Name = "radioButtonDynamic";
            this.radioButtonDynamic.Size = new System.Drawing.Size(83, 21);
            this.radioButtonDynamic.TabIndex = 2;
            this.radioButtonDynamic.TabStop = true;
            this.radioButtonDynamic.Text = "Dynamic";
            this.radioButtonDynamic.UseVisualStyleBackColor = true;
            this.radioButtonDynamic.CheckedChanged += new System.EventHandler(this.radioButtonDynamic_CheckedChanged);
            // 
            // radioButtonFollowing
            // 
            this.radioButtonFollowing.AutoSize = true;
            this.radioButtonFollowing.Location = new System.Drawing.Point(6, 57);
            this.radioButtonFollowing.Name = "radioButtonFollowing";
            this.radioButtonFollowing.Size = new System.Drawing.Size(87, 21);
            this.radioButtonFollowing.TabIndex = 1;
            this.radioButtonFollowing.TabStop = true;
            this.radioButtonFollowing.Text = "Following";
            this.radioButtonFollowing.UseVisualStyleBackColor = true;
            this.radioButtonFollowing.CheckedChanged += new System.EventHandler(this.radioButtonFollowing_CheckedChanged);
            // 
            // radioButtonCameraStatic
            // 
            this.radioButtonCameraStatic.AutoSize = true;
            this.radioButtonCameraStatic.Location = new System.Drawing.Point(6, 30);
            this.radioButtonCameraStatic.Name = "radioButtonCameraStatic";
            this.radioButtonCameraStatic.Size = new System.Drawing.Size(64, 21);
            this.radioButtonCameraStatic.TabIndex = 0;
            this.radioButtonCameraStatic.TabStop = true;
            this.radioButtonCameraStatic.Text = "Static";
            this.radioButtonCameraStatic.UseVisualStyleBackColor = true;
            this.radioButtonCameraStatic.CheckedChanged += new System.EventHandler(this.radioButtonCameraStatic_CheckedChanged);
            // 
            // groupBoxFog
            // 
            this.groupBoxFog.Controls.Add(this.radioButtonFogEnabled);
            this.groupBoxFog.Controls.Add(this.radioButtonFogDisabled);
            this.groupBoxFog.Location = new System.Drawing.Point(1225, 255);
            this.groupBoxFog.Name = "groupBoxFog";
            this.groupBoxFog.Size = new System.Drawing.Size(145, 85);
            this.groupBoxFog.TabIndex = 3;
            this.groupBoxFog.TabStop = false;
            this.groupBoxFog.Text = "Fog";
            // 
            // radioButtonFogEnabled
            // 
            this.radioButtonFogEnabled.AutoSize = true;
            this.radioButtonFogEnabled.Location = new System.Drawing.Point(6, 57);
            this.radioButtonFogEnabled.Name = "radioButtonFogEnabled";
            this.radioButtonFogEnabled.Size = new System.Drawing.Size(81, 21);
            this.radioButtonFogEnabled.TabIndex = 0;
            this.radioButtonFogEnabled.TabStop = true;
            this.radioButtonFogEnabled.Text = "Enabled";
            this.radioButtonFogEnabled.UseVisualStyleBackColor = true;
            this.radioButtonFogEnabled.CheckedChanged += new System.EventHandler(this.radioButtonFogEnabled_CheckedChanged);
            // 
            // radioButtonFogDisabled
            // 
            this.radioButtonFogDisabled.AutoSize = true;
            this.radioButtonFogDisabled.Location = new System.Drawing.Point(6, 30);
            this.radioButtonFogDisabled.Name = "radioButtonFogDisabled";
            this.radioButtonFogDisabled.Size = new System.Drawing.Size(84, 21);
            this.radioButtonFogDisabled.TabIndex = 1;
            this.radioButtonFogDisabled.TabStop = true;
            this.radioButtonFogDisabled.Text = "Disabled";
            this.radioButtonFogDisabled.UseVisualStyleBackColor = true;
            this.radioButtonFogDisabled.CheckedChanged += new System.EventHandler(this.radioButtonFogDisabled_CheckedChanged);
            // 
            // groupBoxFloor
            // 
            this.groupBoxFloor.Controls.Add(this.buttonRemoveFloor);
            this.groupBoxFloor.Controls.Add(this.buttonAddFloor);
            this.groupBoxFloor.Location = new System.Drawing.Point(1225, 346);
            this.groupBoxFloor.Name = "groupBoxFloor";
            this.groupBoxFloor.Size = new System.Drawing.Size(145, 86);
            this.groupBoxFloor.TabIndex = 4;
            this.groupBoxFloor.TabStop = false;
            this.groupBoxFloor.Text = "Floor";
            // 
            // buttonAddFloor
            // 
            this.buttonAddFloor.Location = new System.Drawing.Point(6, 23);
            this.buttonAddFloor.Name = "buttonAddFloor";
            this.buttonAddFloor.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFloor.TabIndex = 0;
            this.buttonAddFloor.Text = "Add";
            this.buttonAddFloor.UseVisualStyleBackColor = true;
            this.buttonAddFloor.Click += new System.EventHandler(this.buttonAddFloor_Click);
            // 
            // buttonRemoveFloor
            // 
            this.buttonRemoveFloor.Location = new System.Drawing.Point(6, 52);
            this.buttonRemoveFloor.Name = "buttonRemoveFloor";
            this.buttonRemoveFloor.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveFloor.TabIndex = 1;
            this.buttonRemoveFloor.Text = "Remove";
            this.buttonRemoveFloor.UseVisualStyleBackColor = true;
            this.buttonRemoveFloor.Click += new System.EventHandler(this.buttonRemoveFloor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 953);
            this.Controls.Add(this.groupBoxFloor);
            this.Controls.Add(this.groupBoxFog);
            this.Controls.Add(this.groupBoxCamera);
            this.Controls.Add(this.groupBoxShadingType);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "3D Poject";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxShadingType.ResumeLayout(false);
            this.groupBoxShadingType.PerformLayout();
            this.groupBoxCamera.ResumeLayout(false);
            this.groupBoxCamera.PerformLayout();
            this.groupBoxFog.ResumeLayout(false);
            this.groupBoxFog.PerformLayout();
            this.groupBoxFloor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBoxShadingType;
        private System.Windows.Forms.RadioButton radioButtonPhong;
        private System.Windows.Forms.RadioButton radioButtonGouraud;
        private System.Windows.Forms.RadioButton radioButtonFlat;
        private System.Windows.Forms.GroupBox groupBoxCamera;
        private System.Windows.Forms.RadioButton radioButtonDynamic;
        private System.Windows.Forms.RadioButton radioButtonFollowing;
        private System.Windows.Forms.RadioButton radioButtonCameraStatic;
        private System.Windows.Forms.GroupBox groupBoxFog;
        private System.Windows.Forms.RadioButton radioButtonFogDisabled;
        private System.Windows.Forms.RadioButton radioButtonFogEnabled;
        private System.Windows.Forms.GroupBox groupBoxFloor;
        private System.Windows.Forms.Button buttonRemoveFloor;
        private System.Windows.Forms.Button buttonAddFloor;
    }
}

