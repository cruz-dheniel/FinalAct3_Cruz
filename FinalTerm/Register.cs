using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FinalTerm
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            // STEP 1: Get user inputs
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // STEP 2: Check empty fields
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // STEP 3: Validate input lengths
            if (fullName.Length > 255 || username.Length > 255)
            {
                MessageBox.Show("Full name or username is too long.");
                return;
            }

            // STEP 4: Check password length
            if (password.Length < 8)
            {
                MessageBox.Show(
                    "Password must be at least 8 characters.");
                return;
            }

            // STEP 5: Confirm password
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                using (OleDbConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // STEP 6: Check if username already exists
                    string checkQuery =
                        "SELECT COUNT(*) FROM [users] WHERE [Username] = ?";

                    using (OleDbCommand checkCmd =
                        new OleDbCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.Add(
                            "@Username", OleDbType.VarWChar, 255
                        ).Value = username;

                        int count =
                            Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show(
                                "Username already exists.");
                            return;
                        }
                    }

                    // STEP 7: Hash the password
                    string hashedPassword =
                        PasswordHelper.HashPassword(password);

                    // STEP 8: Insert new user
                    string insertQuery =
                        "INSERT INTO [users] " +
                        "([FullName], [Username], [PasswordHash]) " +
                        "VALUES (?, ?, ?)";

                    using (OleDbCommand cmd =
                        new OleDbCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add(
                            "@FullName", OleDbType.VarWChar, 255
                        ).Value = fullName;

                        cmd.Parameters.Add(
                            "@Username", OleDbType.VarWChar, 255
                        ).Value = username;

                        cmd.Parameters.Add(
                            "@PasswordHash", OleDbType.VarWChar, 255
                        ).Value = hashedPassword;

                        cmd.ExecuteNonQuery();
                    }
                }

                // STEP 9: Show success message
                MessageBox.Show(
                    "Account registered successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // STEP 10: Clear input fields
                txtFullName.Clear();
                txtUsername.Clear();
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.UseSystemPasswordChar = true;
                txtConfirmPassword.UseSystemPasswordChar = true;
                btnShowPassword.Image = Properties.Resources.EyeOff;
                btnShowPassword.AccessibleName = "Show password";
                btnShowConfirmPassword.Image = Properties.Resources.EyeOff;
                btnShowConfirmPassword.AccessibleName = "Show confirm password";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Registration failed: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtFullname_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            if (txtPassword.UseSystemPasswordChar)
            {
                btnShowPassword.Image = Properties.Resources.EyeOff;
                btnShowPassword.AccessibleName = "Show password";
            }
            else
            {
                btnShowPassword.Image = Properties.Resources.EyeOpen;
                btnShowPassword.AccessibleName = "Hide password";
            }
        }

        private void btnShowConfirmPassword_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = !txtConfirmPassword.UseSystemPasswordChar;
            if (txtConfirmPassword.UseSystemPasswordChar)
            {
                btnShowConfirmPassword.Image = Properties.Resources.EyeOff;
                btnShowConfirmPassword.AccessibleName = "Show confirm password";
            }
            else
            {
                btnShowConfirmPassword.Image = Properties.Resources.EyeOpen;
                btnShowConfirmPassword.AccessibleName = "Hide confirm password";
            }
        }

        private void linkSIgnin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

