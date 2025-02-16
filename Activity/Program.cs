using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Activity
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RegistrationForm());
        }
    }

    public class RegistrationForm : Form
    {
        // تعريف الحقول والعناصر
        private TextBox nameBox, emailBox, passwordBox;
        private RadioButton maleRadio, femaleRadio, otherRadio;
        private Button registerButton, colorButton;
        private DateTimePicker birthDatePicker;
        private ComboBox countryBox;
        private Label resultLabel, colorLabel;
        private ColorDialog colorDialog;
        private Color favoriteColor;

        public RegistrationForm()
        {
            // ضبط خصائص النافذة
            this.Text = "Student Registration Form";
            this.Size = new Size(400, 450);
            this.BackColor = Color.LightBlue;

            // إنشاء الحقول
            Label nameLabel = new Label { Text = "Name:", Location = new Point(20, 20) };
            nameBox = new TextBox { Location = new Point(120, 20), Width = 200 };

            Label emailLabel = new Label { Text = "Email:", Location = new Point(20, 60) };
            emailBox = new TextBox { Location = new Point(120, 60), Width = 200 };

            Label passwordLabel = new Label { Text = "Password:", Location = new Point(20, 100) };
            passwordBox = new TextBox { Location = new Point(120, 100), Width = 200, PasswordChar = '*' };

            Label genderLabel = new Label { Text = "Gender:", Location = new Point(20, 140) };
            maleRadio = new RadioButton { Text = "Male", Location = new Point(120, 140) };
            femaleRadio = new RadioButton { Text = "Female", Location = new Point(180, 140) };
            otherRadio = new RadioButton { Text = "Other", Location = new Point(250, 140) };

            Label birthDateLabel = new Label { Text = "Birthdate:", Location = new Point(20, 180) };
            birthDatePicker = new DateTimePicker { Location = new Point(120, 180), Format = DateTimePickerFormat.Short };

            Label countryLabel = new Label { Text = "Country:", Location = new Point(20, 220) };
            countryBox = new ComboBox { Location = new Point(120, 220), Width = 200 };
            countryBox.Items.AddRange(new string[] { "Yemen", "USA", "UK", "Canada", "Germany", "France", "Japan" });

            Label colorTextLabel = new Label { Text = "Favorite Color:", Location = new Point(20, 260) };
            colorButton = new Button { Text = "Pick Color", Location = new Point(120, 260) };
            colorLabel = new Label { Text = "No color selected", Location = new Point(220, 260), Width = 100 };
            colorDialog = new ColorDialog();
            colorButton.Click += (sender, e) =>
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    favoriteColor = colorDialog.Color;
                    colorLabel.Text = favoriteColor.Name;
                }
            };

            registerButton = new Button { Text = "Register", Location = new Point(120, 300) };
            registerButton.Click += RegisterUser;

            resultLabel = new Label { Text = "", Location = new Point(20, 340), Width = 350, Height = 60, ForeColor = Color.DarkBlue };

            // إضافة العناصر إلى النافذة
            this.Controls.AddRange(new Control[] { nameLabel, nameBox, emailLabel, emailBox, passwordLabel, passwordBox,
                genderLabel, maleRadio, femaleRadio, otherRadio, birthDateLabel, birthDatePicker,
                countryLabel, countryBox, colorTextLabel, colorButton, colorLabel,
                registerButton, resultLabel });
        }

        private void RegisterUser(object sender, EventArgs e)
        {
            string name = nameBox.Text;
            string email = emailBox.Text;
            string password = passwordBox.Text;
            string gender = maleRadio.Checked ? "Male" : femaleRadio.Checked ? "Female" : "Other";
            string birthDate = birthDatePicker.Value.ToShortDateString();
            string country = countryBox.SelectedItem?.ToString() ?? "Not Selected";
            string color = favoriteColor == Color.Empty ? "Not Selected" : favoriteColor.Name;

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            resultLabel.Text = $"Name: {name}\nEmail: {email}\nGender: {gender}\nBirthdate: {birthDate}\nColor: {color}\nCountry: {country}";
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}