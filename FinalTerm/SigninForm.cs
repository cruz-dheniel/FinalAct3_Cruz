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
    public partial class SigninForm : Form
    {
        public SigninForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Please enter your username and password.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                
                using (OleDbConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    
                    string query =
                        "SELECT [PasswordHash] FROM [users] " +
                        "WHERE [Username] = ?";

                    using (OleDbCommand cmd =
                        new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.Add(
                            "@Username", OleDbType.VarWChar, 255
                        ).Value = username;

                        object result = cmd.ExecuteScalar();

                        
                        if (result == null || result == DBNull.Value)
                        {
                            MessageBox.Show(
                                "Invalid username or password.",
                                "Sign In Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }

                        
                        string storedHash = Convert.ToString(result);

                        bool isValid =
                            PasswordHelper.VerifyPassword(
                                password, storedHash);

                        if (!isValid)
                        {
                            MessageBox.Show(
                                "Invalid username or password.",
                                "Sign In Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Login failed: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Authentication succeeded. The database connection is already disposed.
            this.Hide();
            try
            {
                using (DashboardForm dashboard = new DashboardForm())
                {
                    dashboard.ShowDialog(this);
                }
            }
            finally
            {
                // Log Out and the X button both return to this same Sign In form.
                txtUsername.Clear();
                txtPassword.Clear();
                txtPassword.UseSystemPasswordChar = true;
                btnShowPassword.Image = Properties.Resources.EyeOff;
                btnShowPassword.AccessibleName = "Show password";
                this.Show();
                txtUsername.Focus();
            }
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            // Toggle the entered password between visible text and masked characters.
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


        private void btnTestConnection_Click(object sender, EventArgs e)
        {

            try
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    MessageBox.Show(
                        "Database connected successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database connection failed:\n" + ex.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            try
            {
                using (Register registerForm = new Register())
                {
                    registerForm.ShowDialog(this);
                }
            }
            finally
            {
                txtPassword.Clear();
                txtPassword.UseSystemPasswordChar = true;
                btnShowPassword.Image = Properties.Resources.EyeOff;
                btnShowPassword.AccessibleName = "Show password";
                this.Show();
            }
        }
    }
}
