using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LoginFormExmaple
{
    public partial class Form1 : Form
    {
        private List<User> users;

        public Form1()
        {
            InitializeComponent();

            try
            {
                users = new List<User>
                {
                    new User("Razil", "razilcavadov_2005", "Student"),
                    new User("Aslan", "aslanmemmedov_2005", "Student"),
                    new User("Fuad", "fuadaskerov_1997", "Teacher")
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while initializing the users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            button1.Click += button1_Click;
            textPassword.UseSystemPasswordChar = true; // sifre formasinda kod yazilsin deye istifade olunur
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = textUsername.Text;
                string password = textPassword.Text;

                
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Username and Password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool isValidUser = false;
                foreach (var user in users)
                {
                    try
                    {
                        if (user.Username == username && user.Password == password)
                        {
                            isValidUser = true;
                            break; 
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while checking credentials: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                
                if (isValidUser)
                {
                    MessageBox.Show("Login successful! Welcome!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields(); 
                    NavigateBasedOnRole(username); 
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Input error: {ex.Message}", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Format error: {ex.Message}", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            textUsername.Clear();
            textPassword.Clear();
        }

        private void NavigateBasedOnRole(string username)
        {
            try
            {
                var user = users.Find(u => u.Username == username);

                if (user != null)
                {
                    if (user.Role == "Student")
                    {
                        MessageBox.Show("Redirecting to student portal...", "Redirect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (user.Role == "Teacher")
                    {
                        MessageBox.Show("Redirecting to teacher portal...", "Redirect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Role is not recognized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("User not found in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while navigating based on role: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class User
    {
        private string username;
        private string password;
        private string role;

        public string Username
        {
            get => username;
            private set => username = value ?? throw new ArgumentNullException(nameof(value), "Username cannot be null");
        }

        public string Password
        {
            get => password;
            private set => password = value ?? throw new ArgumentNullException(nameof(value), "Password cannot be null");
        }

        public string Role
        {
            get => role;
            private set => role = value ?? throw new ArgumentNullException(nameof(value), "Role cannot be null");
        }

        public User(string username, string password, string role)
        {
            this.username = username;
            this.password = password;
            this.role = role;
        }
    }
}
